using Application.RepositoryInterfaces;
using Application.Users.Requests;
using Application.Users.Responses;
using Domain;
using Domain.Users;
using Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserResponse>> Authenticate(AuthenticateRequest request)
        {
            Result<Email> userEmailOrError = Email.Create(request.Email);
            if(userEmailOrError.IsFailure)
            {
                return Result.Failure<UserResponse>(userEmailOrError.Error);
            }

            User user = await _userRepository.GetFirstByPredicateAsync(u =>
                    u.Email.Value == userEmailOrError.Value.Value);
            if(user == null)
            {
                return Result.Failure<UserResponse>($"User {request.Email} was not found.");
            }

            Result<Password> userPasswordOrError = Password.Create(request.Password, user.Password.PasswordSalt);
            if (userPasswordOrError.IsFailure)
            {
                return Result.Failure<UserResponse>(userPasswordOrError.Error);
            }

            if (!user.Password.Equals(userPasswordOrError.Value))
            {
                return Result.Failure<UserResponse>("Incorrect password.");
            }

            UserResponse response = new UserResponse()
            {
                Id = user.Id,
                PersonId = user.PersonId,
                Email = user.Email.Value
            };

            return Result.Success(response);
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

            Result<Password> userPasswordOrError = Password.Create(request.Password);
            var result = Result.Combine(userEmailOrError, userPasswordOrError);
            if (result.IsFailure)
            {
                return Result.Failure<UserResponse>(result.Error);
            }

            User user = new User(request.PersonId, userEmailOrError.Value, userPasswordOrError.Value);
            await _userRepository.AddAsync(user);
            UserResponse userResponse = new UserResponse()
            {
                Id = user.Id,
                PersonId = user.PersonId,
                Email = user.Email.Value
            };

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

            foreach(User user in users)
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
    }
}
