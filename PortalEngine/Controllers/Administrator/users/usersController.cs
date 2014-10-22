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
                    Session["Message"] = Infrastructure.ApplicationSettings.Message("Success","Module [" + model.Name + "]  created successfully!", "Success");
                    return RedirectToAction("modules");
                }
                Session["Message"] = Infrastructure.ApplicationSettings.Message("Critical", "Module [" + model.Name + "]  creation failed!", "Critical");
                 return RedirectToAction("modules");
            }
            catch (Exception ex) {
                Session["Message"] = Infrastructure.ApplicationSettings.Message("Critical", "Module [" + model.Name + "]  creation failed!", "Critical");
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
            
                Session["Message"] = Infrastructure.ApplicationSettings.Message("Success", "Module [" + Name + "]  " + st + " successfully!", "Success");
                return RedirectToAction("modules");
                
            }

            Session["Message"] = Infrastructure.ApplicationSettings.Message("Critical", "Module [" + Name + "]   Failed!", "Critical");
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
                    Session["Message"] = Infrastructure.ApplicationSettings.Message("Success", "Module [" + UserModule.Name + "]  modified successfully!", "Success");
                }

                else
                {
                    Session["Message"] = Infrastructure.ApplicationSettings.Message("Critical", "Module [" + UserModule.Name + "] Update  Failed!", "Critical");
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
                Session["Message"] = Infrastructure.ApplicationSettings.Message("Critical", "Module [" + UserModule.Name + "] Update  Failed! " + ex.Message, "Critical");
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


        public ActionResult Roles(int page = 1, int pageCount = 5)
        {
            var model = new UserModuleViewModel();
            ViewData["createmodule"] = model;
            var result = _db.Modules.GroupBy(x => x.Name).Select(m => m.FirstOrDefault()).OrderBy(c => c.Name).ToPagedList(page, pageCount);
            return View(result);
        
        }
    }
}
