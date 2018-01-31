using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHYB.BLL.Interfaces
{
    public interface IServicesCreator
    {
        IUserService CreateUserService(string connection);
    }
}
