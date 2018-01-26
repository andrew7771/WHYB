using WHYB.BLL.Interfaces;

namespace WHYB.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        private readonly IUserService _userService;

        public ServiceCreator() { }

        public ServiceCreator(IUserService userService)
        {
            _userService = userService;
        }

        public IUserService CreateUserService(string connection)
        {
            return _userService;
        }
    }
}
