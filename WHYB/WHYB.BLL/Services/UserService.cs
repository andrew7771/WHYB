using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using WHYB.BLL.DTO;
using WHYB.BLL.Infrastructure;
using WHYB.BLL.Interfaces;
using WHYB.DAL.Entities;
using WHYB.DAL.Interfaces;

namespace WHYB.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _database;

        public UserService(IUnitOfWork uow)
        {
            _database = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await _database.UserManager.FindByEmailAsync(userDto.Email);

            if (user == null)
            {
                user = new ApplicationUser {Email = userDto.Email, UserName = userDto.Email};

                var result = await _database.UserManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded)
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }

                await _database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                ClientProfile clientProfile = new ClientProfile {Id = user.Id, Address = userDto.Address, Name = userDto.Name};

                _database.ClientProfileRepository.Create(clientProfile);
                await _database.SaveAsync();

                return new OperationDetails(true, "Регистрация прошла успешно", "");
            }
         
             return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;

            ApplicationUser user = await _database.UserManager.FindAsync(userDto.Email, userDto.Password);

            if (user != null)
            {
                claim =
                    await _database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }

        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await _database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole {Name = roleName};
                    await _database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }
        
        
        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
