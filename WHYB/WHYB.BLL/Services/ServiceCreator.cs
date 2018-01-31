using WHYB.BLL.Interfaces;
using WHYB.DAL.Repositories;

namespace WHYB.BLL.Services
{
    public class ServicesCreator /*: IServicesCreator*/
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new IdentityUnitOfWork(connection));
        }

        //private readonly IUserService _userService;

        //public ServicesCreator() { }

        //public ServicesCreator(IUserService userService)
        //{
        //    _userService = userService;
        //}

        //public IUserService CreateUserService(string connection)
        //{
        //    return _userService;
        //}
    }
}
