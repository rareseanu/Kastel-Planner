using Application.Roles.Requests;
using Application.Roles.Responses;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Roles
{
    public interface IRoleService
    {
        Task<IList<RoleResponse>> GetAllRolesAsync();
        Task<Result<RoleResponse>> GetRoleByAsync(Guid id);
        Task<Result<RoleResponse>> CreateRoleAsync(CreateRoleRequest request);
        Task<Result<RoleResponse>> UpdateRoleAsync(Guid roleId, UpdateRoleRequest request);
        Task<Result> DeleteRoleAsync(Guid roleId);
    }
}
