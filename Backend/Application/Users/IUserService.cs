using Application.Users.Requests;
using Application.Users.Responses;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public interface IUserService
    {
        Task<IList<UserResponse>> GetAllUsersAsync();
        Task<Result<UserResponse>> GetUserByIdAsync(Guid userId);
        Task<Result> CreateUserAsync(CreateUserRequest request);
        Task<Result> UpdateUserAsync(Guid userId, UpdateUserRequest request);
        Task<Result> DeleteUserAsync(Guid userId);
    }
}
