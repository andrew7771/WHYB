using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WHYB.BLL.DTO;
using WHYB.BLL.Infrastructure;
using WHYB.DAL.Entities;

namespace WHYB.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        SignInManager<ApplicationUser, string> SignInManager { get; }
        IAuthenticationManager AuthenticationManager { get; }
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);

    }
}
