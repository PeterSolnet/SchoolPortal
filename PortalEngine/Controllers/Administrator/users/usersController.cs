using PortalEngine.DAL.Domain;
using PortalEngine.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data;

namespace PortalEngine.Controllers.Administrator.users
{
    public class usersController : Controller
    {
        private readonly SchoolPortalEntities _db;


        public usersController(SchoolPortalEntities db)
        {
            _db = db;
        }

        //Admin Index Page
        public ActionResult Index()
        {
            return View();
        }


        //List Module
        [HttpGet]
        public ActionResult modules(int page = 1, int pageCount = 5) 
        {
            var model = new UserModuleViewModel();
            ViewData["createmodule"] = model;
            var result = _db.Modules.GroupBy(x => x.Name).Select(m => m.FirstOrDefault()).OrderBy(c => c.Name).ToPagedList(page, pageCount);
            return View(result);
        }

                
        //Post New Module
        [HttpPost]
        public ActionResult createmodule(UserModuleViewModel model, int page = 1, int pageCount = 5)
        {
            Session["Message"] = "";
            
            try
            {
                if (ModelState.IsValid)
                {
                    var Module = new Module();
                    Module.Name = model.Name;
                    Module.Description = model.Description;
                    Module.DateCreated = DateTime.Now;
                    _db.Modules.Add(Module);
                    _db.SaveChanges();
                    Session["Message"] = Infrastructure.ApplicationSettings.Message("Success", model.Name + " Module  created successfully!", "Success");
                    return RedirectToAction("modules");
                }
                Session["Message"] = Infrastructure.ApplicationSettings.Message("Critical", model.Name + " Module  creation failed!", "Critical");
                 return RedirectToAction("modules");
            }
            catch (Exception ex) {
                Session["Message"] = Infrastructure.ApplicationSettings.Message("Critical", model.Name + " Module  creation failed!", "Critical");
                return RedirectToAction("modules");
            }
        }



        public ActionResult statusChange(int ID)
        {
            var model = _db.Modules.Where(x => x.ID == ID);

            string Name ="";
            if (model != null)
            {
                string st = "";
                foreach(var t in model){
                    Name = t.Name;
                if (t.Status == true)
                {
                    t.Status = false;
                    st = "disabled";
                }
                else
                {
                    t.Status = true;
                    st = "enabled";
                }
                //db.Entry(model).State = EntityState.Modified;
                }
                _db.SaveChanges();

                Session["Message"] = Infrastructure.ApplicationSettings.Message("Success", Name + " Module  " + st + " successfully!", "Success");
                return RedirectToAction("modules");
                
            }

            Session["Message"] = Infrastructure.ApplicationSettings.Message("Critical", Name + " Module   Failed!", "Critical");
            return RedirectToAction("modules");
        }


        [HttpGet]
        public JsonResult editmodules(int ID)
        {
            var result = _db.Modules.Single(j => j.ID == ID);//.GroupBy(x => x.Name).Select(m => m.FirstOrDefault()).OrderBy(c => c.Name);
            //var mm = new Module();
            return Json(new {
                ModuleName = result.Name,
                ModuleID = result.ID,
                ModuleDesc = result.Description,
                ModuleStatus = result.Status,
            }, JsonRequestBehavior.AllowGet);
        }

        //Edit Module
        
        [HttpPost]
        public ActionResult ModifyModules(UserModuleViewModel UserModule,int page = 1, int pageCount = 5)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    Module updateModule = (from m in _db.Modules
                                           where m.ID == UserModule.ID
                                           select m).SingleOrDefault();
                    updateModule.Name = UserModule.Name;
                    updateModule.Description = UserModule.Description;
                    updateModule.Status = UserModule.Status;
                    
                    _db.SaveChanges();
                    Session["Message"] = Infrastructure.ApplicationSettings.Message("Success", UserModule.Name + " Module  modified successfully!", "Success");
                }

                else
                {
                    Session["Message"] = Infrastructure.ApplicationSettings.Message("Critical", UserModule.Name + " Module Update  Failed!", "Critical");
                }

                var model = new UserModuleViewModel();
                ViewData["createmodule"] = model;
                var result = _db.Modules.GroupBy(x => x.Name).Select(m => m.FirstOrDefault()).OrderBy(c => c.Name).ToPagedList(page, pageCount);

                return RedirectToAction("modules");
                //return View(result);
            }
            catch (Exception ex) {

                var model = new UserModuleViewModel();
                ViewData["createmodule"] = model;
                var result = _db.Modules.GroupBy(x => x.Name).Select(m => m.FirstOrDefault()).OrderBy(c => c.Name).ToPagedList(page, pageCount);
                Session["Message"] = Infrastructure.ApplicationSettings.Message("Critical", UserModule.Name + " Module Update  Failed! ", "Critical");
                return RedirectToAction("modules");
            }
            
           
        }


        public ActionResult Search(string search, int page = 1, int pageCount = 5)
        { 
            var model = new UserModuleViewModel();
            ViewData["createmodule"] = model;
            var result = _db.Modules.Where(x=>x.Name==search).OrderBy(c => c.Name).ToPagedList(page, pageCount);
            return View(result);
        }


        [HttpGet]
        public ActionResult Roles(int page = 1, int pageCount = 5)
        {

            var createRole = new UserRolesViewModel();
            ViewData["createRoles"] = createRole;
            Session["rCount"] = _db.userRolesListing("", 9).Count();
            var result = _db.userRolesListing("", 9).ToList().ToPagedList(page, pageCount);
            return View(result);
        }


        [HttpPost]
        public ActionResult CreateRoles(string Name, string Priviledges)
        {
            if (Priviledges == "")
            {
                Session["Message"] = Infrastructure.ApplicationSettings.Message("Critical", Name + " Role Creation  Failed! ", "Critical");
                return RedirectToAction("Roles");
            }

            Priviledges = Priviledges.Remove(Priviledges.Length - 1);

            string[] RolePrivi = Priviledges.Split('|');

            var UserRoles = new UserRole();


            for (int l = 0; (l <= RolePrivi.Length - 1); l++)
            {
                UserRoles.RolesID = Convert.ToInt32(Session["rCount"]);
                UserRoles.Name = Name;
                UserRoles.ModuleID = Convert.ToInt32(Getstring(RolePrivi[l].ToString(), '_', 1));
                UserRoles.Priviledge = GetPriviledges(RolePrivi[l].ToString(), ':');
                UserRoles.Status = true;
                _db.UserRoles.Add(UserRoles);
                _db.SaveChanges();

            }

            Session["Message"] = Infrastructure.ApplicationSettings.Message("Success", Name + " Role Creation  Successfull! ", "Success");
            return RedirectToAction("Roles");
        }

        private string Getstring(string text2search, char delimeter, int pos)
        {
            if (text2search.Length > 0)
            {
                string[] collect = text2search.Split(delimeter);
                return collect[pos];
            }
            return "";
        }

        private string GetPriviledges(string text2search, char delimeter)
        {
            if (text2search.Length > 0)
            {
                string pr = "";

                string[] collect = text2search.Split(delimeter);
                collect = collect.Where(c => c != collect[0]).ToArray();

                for (int i = 0; (i <= collect.Length - 1); i++)
                {
                    int index = collect[i].IndexOf(';');
                    if (index > 0)
                        collect[i] = collect[i].Substring(0, index);
                    pr += collect[i] + ",";
                }
                pr = pr.Remove(pr.Length - 1);
                return pr;
            }
            return "";
        }

        
        public ActionResult EndableRole(string Name, bool Enable)
        {
            var UserRoles = new UserRole();

            try
            {

                if (Enable == true)
                {
                    var some = _db.UserRoles.Where(x => x.Name == Name).ToList();
                    some.ForEach(a =>
                    {
                        a.Status = false;
                    });
                }

                else if (Enable == false)
                {
                    var some = _db.UserRoles.Where(x => x.Name == Name).ToList();
                    some.ForEach(a =>
                    {
                        a.Status = true;
                    });
                }

                _db.SaveChanges();

                if (Enable == true)
                {
                    Session["Message"] = Infrastructure.ApplicationSettings.Message("Success", Name + " Role Disabled  Successfuly! ", "Success");
                }
                else
                {
                    Session["Message"] = Infrastructure.ApplicationSettings.Message("Success", Name + " Role Enabled  Successfuly! ", "Success");
                }
            }
            catch (Exception e)
            {
                Session["Message"] = Infrastructure.ApplicationSettings.Message("Critical", Name + " Role Operation  Failed! ", "Critical");
            }
            return RedirectToAction("Roles");
        }
    }
}
