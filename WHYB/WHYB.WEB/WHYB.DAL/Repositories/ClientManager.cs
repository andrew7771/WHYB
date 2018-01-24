using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHYB.DAL.Context;
using WHYB.DAL.Entities;
using WHYB.DAL.Interfaces;

namespace WHYB.DAL.Repositories
{
    public class ClientManager: IClientManager
    {
        public WhybDbContext Context { get; set; }

        public ClientManager(WhybDbContext context)
        {
            Context = context;
        }
        public void Create(ClientProfile clientProfile)
        {
            Context.ClientProfiles.Add(clientProfile);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
