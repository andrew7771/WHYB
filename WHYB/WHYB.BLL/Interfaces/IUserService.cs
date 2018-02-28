using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WHYB.BLL.DTO;
using WHYB.BLL.Infrastructure;
using WHYB.DAL.Entities;

namespace WHYB.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        #region properties
        SignInManager<ApplicationUser, string> SignInManager { get; }
        IAuthenticationManager AuthenticationManager { get; }
        UserManager<ApplicationUser> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        #endregion

        #region methods
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task<OperationDetails> Create(UserDTO userDto);
        Task<string> GenerateEmailConfirmationTokenAsync(string userId);
        Task<string> FindUserIdByEmailAsync(string email);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        Task SendEmailasync(string userId, string subject, string body);
        Task<OperationDetails> ConfirmEmailAsync(string userId, string code);
        Task<bool> EmailConfirmed(string email);

        #endregion


    }
}
