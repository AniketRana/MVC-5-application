using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;
using MyApp.Db.DbOperation;

namespace MVC_AppWithDB.Controllers
{
    public class HomeController : Controller
    {
        EmpRepository repository = null;

        public HomeController()
        {
            repository = new EmpRepository();

        }
        // GET: Home
        public ActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmpModel model)
        {
            if (ModelState.IsValid)
            {
                //below line add in edmx model cs file in top namespace
                //using SqlProviderServices = System.Data.Entity.SqlServer.SqlProviderServices;
                
                int id= repository.AddEmp(model);
                if (id > 0)
                {
                    ModelState.Clear();
                    ViewBag.Issuccess = "Data Added";
                }
            }
            return View();
        }
    }
}