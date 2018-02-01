using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using WHYB.BLL.DTO;
using WHYB.BLL.Infrastructure;
using WHYB.BLL.Interfaces;
using WHYB.DAL.Entities;
using WHYB.DAL.Identity;
using WHYB.DAL.Interfaces;

namespace WHYB.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _database;
        private readonly IRepository<ClientProfile> _clientProfileRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public UserService(IUnitOfWork uow, IRepository<ClientProfile> clientProfileRepository, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _database = uow;
            _clientProfileRepository = clientProfileRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(userDto.Email);

            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };

                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded)
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }

                await _userManager.AddToRoleAsync(user.Id, userDto.Role);
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };

                _clientProfileRepository.Create(clientProfile);
                await _database.SaveAsync();

                return new OperationDetails(true, "Регистрация прошла успешно", "");
            }

            return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
        }
        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;

            ApplicationUser user = await _userManager.FindAsync(userDto.Email, userDto.Password);

            if (user != null)
            {
                claim =
                    await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }


        public static Type get()
        {
            return typeof(ApplicationUserManager);
        }
        public static Type get1()
        {
            return typeof(ApplicationUser);
        }

        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await _roleManager.CreateAsync(role);
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
