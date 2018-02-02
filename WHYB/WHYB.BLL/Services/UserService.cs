using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
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
        //private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IRepository<ClientProfile> _clientProfileRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser, string> _signInManager;
        private readonly IAuthenticationManager _authenticationManager;

        public UserService(IUnitOfWork uow,
            IRepository<ClientProfile> clientProfileRepository,
            UserManager<ApplicationUser> userManager,
            //RoleManager<ApplicationRole> roleManager, 
            SignInManager<ApplicationUser, string> signInManager,
            IAuthenticationManager authenticationManager 
            )
        {
            _database = uow;
            _clientProfileRepository = clientProfileRepository;
            _userManager = userManager;
             //_roleManager = roleManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
        }

        //public UserService(
        //    IRepository<ClientProfile> clientProfileRepository,
        //    UserManager<ApplicationUser> userManager,
        //    SignInManager<ApplicationUser, string> signInManager,
        //    IAuthenticationManager authenticationManager, IdentityDbContext<ApplicationUser> db)
        //{
        //    _db = db;
        //    _clientProfileRepository = clientProfileRepository;
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //    _authenticationManager = authenticationManager;
        //}

        public SignInManager<ApplicationUser, string> SignInManager => _signInManager;
        public IAuthenticationManager AuthenticationManager => _authenticationManager;
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

        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            //foreach (string roleName in roles)
            //{
            //    var role =  await _roleManager.FindByNameAsync(roleName);
            //    if (role == null)
            //    {
            //        role = new ApplicationRole { Name = roleName };
            //        await _roleManager.CreateAsync(role);
            //    }
            //}
            await Create(adminDto);
        }
        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
