using PortalEngine.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalEngine.DAL.ViewModels
{
    public class UserRolesViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> ModuleID { get; set; }
        public Nullable<int> RolesID { get; set; }
        public string Priviledge { get; set; }
        public Nullable<bool> PriviledgeValue { get; set; }
        public Nullable<bool> Status { get; set; }
        public ICollection<Module> Modules { get; set; }

        SchoolPortalEntities _db = new SchoolPortalEntities();
        public SelectList Listmodules
        {
            get { return new SelectList(_db.Modules.ToList(), "ID", "Name", ModuleID); }
        }
    }
}