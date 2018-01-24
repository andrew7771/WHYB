using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WHYB.BLL.DTO;
using WHYB.BLL.Infrastructure;

namespace WHYB.BLL.Interfaces
{
    public interface IUserService
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
    }
}
