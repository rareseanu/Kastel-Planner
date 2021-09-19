using Application.Users.Requests;
using Application.Users.Responses;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Users
{
    public interface IUserService
    {
        Task<IList<UserResponse>> GetAllUsersAsync();
        Task<Result<UserResponse>> GetUserByIdAsync(Guid userId);
        Task<Result<AuthenticateResponse>> Authenticate(AuthenticateRequest request);
        Task<Result<AuthenticateResponse>> RefreshToken(string refreshToken);
        Task<Result<RevokeTokenResponse>> RevokeToken(string refreshToken);
        Task<Result<UserResponse>> CreateUserAsync(CreateUserRequest request);
        Task<Result<UserResponse>> ResetPassword(ResetPasswordRequest request);
        Task<Result<UserResponse>> ForgotPassword(CreatePasswordResetToken request);
        Task<Result<UserResponse>> UpdateUserAsync(Guid userId, UpdateUserRequest request);
        Task<Result> DeleteUserAsync(Guid userId);
    }
}
