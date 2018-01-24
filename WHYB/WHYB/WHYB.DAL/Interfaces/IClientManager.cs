using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHYB.DAL.Entities;

namespace WHYB.DAL.Interfaces
{
    public interface IClientManager
    {
        void Create(ClientProfile clientProfile);
    }
}
