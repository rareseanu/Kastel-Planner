using Application.RepositoryInterfaces;
using Application.Roles.Requests;
using Application.Roles.Responses;
using Domain;
using Domain.Roles;
using Domain.Roles.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Roles
{
    public sealed class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Result> CreateRoleAsync(CreateRoleRequest request)
        {
            Result<RoleName> roleNameOrError = RoleName.Create(request?.RoleName);
            if (roleNameOrError.IsFailure)
            {
                return Result.Failure(roleNameOrError.Error);
            }

            var role = new Role(roleNameOrError.Value);

            await _roleRepository.AddAsync(role);

            return Result.Success();
        }

        public async Task<Result> DeleteRoleAsync(Guid roleId)
        {
            var role = await _roleRepository.GetByIdAsync(roleId);

            if (role == null)
            {
                return Result.Failure($"Role with Id {roleId} was not found");
            }

            await _roleRepository.Delete(role);

            return Result.Success();
        }

        public async Task<IList<RoleResponse>> GetAllRolesAsync()
        {
            var response = new List<RoleResponse>();

            var roles = await _roleRepository.GetAllAsync();

            foreach (var role in roles)
            {
                var roleResponse = new RoleResponse
                {
                    Id = role.Id,
                    RoleName = role.RoleName.Value
                };

                response.Add(roleResponse);
            }

            return response;
        }

        public async Task<Result<RoleResponse>> GetRoleByAsync(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);

            if (role == null)
            {
                return Result.Failure<RoleResponse>($"Role with Id {id} was not found");
            }

            var response = new RoleResponse()
            {
                Id = role.Id,
                RoleName = role.RoleName.Value
            };

            return Result.Success(response);
        }

        public async Task<Result> UpdateRoleAsync(Guid roleId, UpdateRoleRequest request)
        {
            Result<RoleName> roleNameOrError = RoleName.Create(request?.RoleName);
            if (roleNameOrError.IsFailure)
            {
                return Result.Failure(roleNameOrError.Error);
            }

            var role = await _roleRepository.GetByIdAsync(roleId);

            if (role == null)
            {
                return Result.Failure($"Role with Id {roleId} was not found");
            }

            role.UpdateRole(roleNameOrError.Value);

            await _roleRepository.Update(role);

            return Result.Success();
        }
    }
}
