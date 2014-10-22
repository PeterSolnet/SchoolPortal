using PortalEngine.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalEngine.DAL.ViewModels
{
    public class ViewModelRoles
    {
        public ICollection<UserRole> Roles { get; set; }

    }
}