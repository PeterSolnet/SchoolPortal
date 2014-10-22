using PortalEngine.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PortalEngine.Controllers.Administrator
{
    public class AdministratorController : Controller
    {
        private readonly SchoolPortalEntities db;

        public AdministratorController(SchoolPortalEntities _db)
        {
            db = _db;
        }

        public ActionResult Index(int page = 1, int pageCount = 5)
        {

            return View();
        }

    }
}
