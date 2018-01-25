using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
        private IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);

            if (user == null)
            {
                user = new ApplicationUser {Email = userDto.Email, UserName = userDto.Email};

                var result = await Database.UserManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded)
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }

                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                ClientProfile clientProfile = new ClientProfile {Id = user.Id, Address = userDto.Address, Name = userDto.Name};

                Database.ClientProfileRepositoryManager.Create(clientProfile);
                await Database.SaveAsync();

                return new OperationDetails(true, "Регистрация прошла успешно", "");
            }
         
             return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;

            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);

            if (user != null)
            {
                claim =
                    await Database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }

        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole {Name = roleName};
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
