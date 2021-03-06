using Application.RepositoryInterfaces;
using Application.Users.Requests;
using Application.Users.Responses;
using Domain;
using Domain.Configurations;
using Domain.RefreshTokens;
using Domain.RefreshTokens.ValueObjects;
using Domain.Users;
using Domain.Users.ValueObjects;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Domain.ResetPasswordTokens;
using Application.Utils;
namespace Application.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IPersonRoleRepository _personRoleRepository;
        private readonly IResetPasswordTokenRepository _resetPasswordTokenRepository;
        private readonly IEmailService _emailService;

        private readonly JwtConfig _jwtConfig;

        public UserService(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, 
                IPersonRoleRepository personRoleRepository, IResetPasswordTokenRepository resetPasswordTokenRepository,
                    IEmailService emailService, IOptions<JwtConfig> jwtConfig)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _personRoleRepository = personRoleRepository;
            _resetPasswordTokenRepository = resetPasswordTokenRepository;
            _emailService = emailService;
            _jwtConfig = jwtConfig.Value;
        }

        public async Task<Result<AuthenticateResponse>> Authenticate(AuthenticateRequest request)
        {
            Result<Email> userEmailOrError = Email.Create(request.Email);
            if(userEmailOrError.IsFailure)
            {
                return Result.Failure<AuthenticateResponse>(userEmailOrError.Error);
            }

            User user = await _userRepository.GetFirstByPredicateAsync(u =>
                    u.Email.Value == userEmailOrError.Value.Value);
            if(user == null)
            {
                return Result.Failure<AuthenticateResponse>($"User {request.Email} was not found.");
            }

            if(user.Password == null)
            {
                return Result.Failure<AuthenticateResponse>("First time log in detected. Please choose a password.");
            }

            Result<Password> userPasswordOrError = Password.Create(request.Password, user.Password.PasswordSalt);
            if (userPasswordOrError.IsFailure)
            {
                return Result.Failure<AuthenticateResponse>(userPasswordOrError.Error);
            }

            if (!user.Password.Equals(userPasswordOrError.Value))
            {
                return Result.Failure<AuthenticateResponse>("Incorrect password.");
            }

            Result<Token> token = Token.Create(GenerateToken());
            if(token.IsFailure)
            {
                return Result.Failure<AuthenticateResponse>(token.Error);
            }
            RefreshToken refreshToken = new RefreshToken(token.Value,
                    DateTime.Now.AddDays(7), DateTime.Now, user.Id);
            await _refreshTokenRepository.AddAsync(refreshToken);

            var personRoles = await _personRoleRepository.GetAllByPredicateAsync(pr => pr.PersonId.Equals(user.PersonId),
                    pr => pr.Role);
            string[] roles = personRoles.Select(p => p.Role.RoleName.Value).ToArray();

            AuthenticateResponse response = new AuthenticateResponse()
            {
                Id = user.Id,
                Token = GenerateJwtToken(user),
                RefreshToken = token.Value.Value,
                Expires = refreshToken.ExpiresAt,
                PersonId = user.PersonId,
                Email = user.Email.Value,
                Roles = roles
            };

            return Result.Success(response);
        }

        public async Task<Result<AuthenticateResponse>> RefreshToken(string refreshToken)
        {
            if(string.IsNullOrEmpty(refreshToken))
            {
                return Result.Failure<AuthenticateResponse>("Invalid refresh token request.");
            }
            RefreshToken token = await _refreshTokenRepository.GetFirstByPredicateAsync(r => 
                    r.Token.Value.Equals(refreshToken));
            if(token == null)
            {
                return Result.Failure<AuthenticateResponse>($"Refresh token {refreshToken} was not found.");
            }

            User userWithRefreshToken = await _userRepository.GetByIdAsync(token.UserId);
            if(userWithRefreshToken == null)
            {
                return Result.Failure<AuthenticateResponse>($"Refresh token with user ID {token.UserId} was not found");
            }

            await _refreshTokenRepository.Delete(token);
            if (DateTime.Now >= token.ExpiresAt || token.RevokedAt != null)
            {
                return Result.Failure<AuthenticateResponse>("Expired refresh token.");
            }

            Result<Token> newToken = Token.Create(GenerateToken());
            if (newToken.IsFailure)
            {
                return Result.Failure<AuthenticateResponse>(newToken.Error);
            }
            RefreshToken newRefreshToken = new RefreshToken(newToken.Value,
                    DateTime.Now.AddDays(7), DateTime.Now, userWithRefreshToken.Id);

            await _refreshTokenRepository.AddAsync(newRefreshToken);

            var personRoles = await _personRoleRepository.GetAllByPredicateAsync(pr => pr.PersonId.Equals(userWithRefreshToken.PersonId),
                pr => pr.Role);
            string[] roles = personRoles.Select(p => p.Role.RoleName.Value).ToArray();

            AuthenticateResponse response = new AuthenticateResponse()
            {
                Id = userWithRefreshToken.Id,
                Token = GenerateJwtToken(userWithRefreshToken),
                RefreshToken = newToken.Value.Value,
                Expires = newRefreshToken.ExpiresAt,
                PersonId = userWithRefreshToken.PersonId,
                Email = userWithRefreshToken.Email.Value,
                Roles = roles
            };

            return Result.Success(response);
        }

        public async Task<Result<RevokeTokenResponse>> RevokeToken(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Result.Failure<RevokeTokenResponse>("Invalid refresh token request.");
            }
            RefreshToken token = await _refreshTokenRepository.GetFirstByPredicateAsync(r => 
                    r.Token.Value.Equals(refreshToken));
            if(token == null)
            {
                return Result.Failure<RevokeTokenResponse>($"Refresh token {refreshToken} was not found.");
            }
            token.RevokedAt = DateTime.Now;
            await _refreshTokenRepository.Update(token);

            RevokeTokenResponse revokeTokenResponse = new RevokeTokenResponse()
            {
                RefreshToken = refreshToken,
                RevokedAt = token.RevokedAt.Value
            };

            return Result.Success(revokeTokenResponse);
        }

        public async Task<Result<UserResponse>> CreateUserAsync(CreateUserRequest request)
        {
            Result<Email> userEmailOrError = Email.Create(request.Email);

            User foundUser = await _userRepository.GetFirstByPredicateAsync(u =>
                    u.Email.Value == userEmailOrError.Value.Value);
            if(foundUser != null)
            {
                return Result.Failure<UserResponse>($"Email {request.Email} is already taken.");
            }

            if (userEmailOrError.IsFailure)
            {
                return Result.Failure<UserResponse>(userEmailOrError.Error);
            }

            User user = new User(request.PersonId, userEmailOrError.Value, null);

            Result<Token> token = Token.Create(GenerateToken());
            if (token.IsFailure)
            {
                return Result.Failure<UserResponse>(token.Error);
            }
            ResetPasswordToken resetToken = new ResetPasswordToken(token.Value, DateTime.Now.AddMinutes(30), user.Id);

            await _userRepository.AddAsync(user);
            await _resetPasswordTokenRepository.AddAsync(resetToken);
            
            
            UserResponse userResponse = new UserResponse()
            {
                Id = user.Id,
                PersonId = user.PersonId,
                Email = user.Email.Value,
                ResetPasswordToken = resetToken.Token.Value
            };
            _emailService.Send(user.Email.Value, "Kastel Planner - New Account",
                $"<h2>Hello,</h2> <h3>Choose your password using the link below. Make sure to use the security token provided in this email. </h3> https://localhost:4200/password-reset <br> <h3>Security token:</h3> <span>{resetToken.Token.Value}</span>");

            return Result.Success(userResponse);
        }

        public async Task<Result<UserResponse>> ForgotPassword(CreatePasswordResetToken request)
        {
            Result<Email> userEmailOrError = Email.Create(request.Email);
            if (userEmailOrError.IsFailure)
            {
                return Result.Failure<UserResponse>(userEmailOrError.Error);
            }

            User foundUser = await _userRepository.GetFirstByPredicateAsync(u =>
                    u.Email.Value == userEmailOrError.Value.Value);
            if (foundUser == null)
            {
                return Result.Failure<UserResponse>($"Email {request.Email} not found.");
            }

            Result<Token> token = Token.Create(GenerateToken());
            if (token.IsFailure)
            {
                return Result.Failure<UserResponse>(token.Error);
            }
            ResetPasswordToken resetToken = new ResetPasswordToken(token.Value, DateTime.Now.AddMinutes(30), foundUser.Id);
            await _resetPasswordTokenRepository.AddAsync(resetToken);

            UserResponse userResponse = new UserResponse()
            {
                Id = foundUser.Id,
                PersonId = foundUser.PersonId,
                Email = foundUser.Email.Value,
                ResetPasswordToken = resetToken.Token.Value
            };
            _emailService.Send(foundUser.Email.Value, "Kastel Planner - Forgotten Password", $"<h2>Hello,</h2> <h3>Reset your password using the link below. Make sure to use the security token provided in this email. </h3> https://localhost:4200/password-reset <br> <h3>Security token:</h3> <span>{resetToken.Token.Value}</span>");

            return Result.Success(userResponse);

        }

        public async Task<Result<UserResponse>> ResetPassword(ResetPasswordRequest request)
        {
            Result<Email> userEmailOrError = Email.Create(request.Email);

            User foundUser = await _userRepository.GetFirstByPredicateAsync(u =>
                    u.Email.Value == userEmailOrError.Value.Value);
            if (foundUser == null)
            {
                return Result.Failure<UserResponse>($"Email {request.Email} not found.");
            }

            ResetPasswordToken token = await _resetPasswordTokenRepository.GetFirstByPredicateAsync(t =>
                    t.Token.Value.Equals(request.Token));
            if (token == null || DateTime.Now > token.ExpiresAt || !token.UserId.Equals(foundUser.Id))
            {
                return Result.Failure<UserResponse>("Invalid token.");
            }

            Result<Password> userPasswordOrError = Password.Create(request.Password);
            if (userPasswordOrError.IsFailure)
            {
                return Result.Failure<UserResponse>(userPasswordOrError.Error);
            }

            foundUser.UpdateUser(foundUser.PersonId, foundUser.Email, userPasswordOrError.Value);
            await _userRepository.Update(foundUser);

            UserResponse userResponse = new UserResponse()
            {
                Id = foundUser.Id,
                PersonId = foundUser.PersonId,
                Email = foundUser.Email.Value,
                ResetPasswordToken = token.Token.Value
            };
            await _resetPasswordTokenRepository.Delete(token);

            return Result.Success(userResponse);
        }

        public async Task<Result> DeleteUserAsync(Guid userId)
        {
            User user = await _userRepository.GetByIdAsync(userId);

            if(user == null)
            {
                return Result.Failure<UserResponse>($"User with id {userId} was not found.");
            }

            await _userRepository.Delete(user);
            return Result.Success();
        }

        public async Task<IList<UserResponse>> GetAllUsersAsync()
        {
            var response = new List<UserResponse>();
            var users = await _userRepository.GetAllAsync();

            foreach (User user in users)
            {
                UserResponse userResponse = new UserResponse()
                {
                    Id = user.Id,
                    PersonId = user.PersonId,
                    Email = user.Email.Value
                };
                response.Add(userResponse);
            }
            return response;
        }

        public async Task<Result<UserResponse>> GetUserByIdAsync(Guid userId)
        {
            User user = await _userRepository.GetByIdAsync(userId);

            if(user == null)
            {
                return Result.Failure<UserResponse>($"User with id {userId} was not found.");
            }

            UserResponse response = new UserResponse()
            {
                Id = user.Id,
                PersonId = user.PersonId,
                Email = user.Email.Value
            };

            return Result.Success(response);
        }

        public async Task<Result<UserResponse>> UpdateUserAsync(Guid userId, UpdateUserRequest request)
        {
            Result<Email> userEmailOrError = Email.Create(request?.Email);
            Result<Password> userPasswordOrError = Password.Create(request?.Password);

            var result = Result.Combine(userEmailOrError, userPasswordOrError);
            if (result.IsFailure)
            {
                return Result.Failure<UserResponse>(result.Error);
            }

            User user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                return Result.Failure<UserResponse>($"User with id {userId} was not found.");
            }
            user.UpdateUser(user.PersonId, userEmailOrError.Value, userPasswordOrError.Value);
            await _userRepository.Update(user);

            UserResponse response = new UserResponse()
            {
                Id = user.Id,
                PersonId = user.PersonId,
                Email = user.Email.Value
            };

            return Result.Success(response);
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSecret = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Email", user.Email.Value)
                }),
                Expires = DateTime.UtcNow.AddSeconds(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtSecret),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(jwtTokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }

        private string GenerateToken()
        {
            var randomBytes = new byte[32];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }
    }
}
