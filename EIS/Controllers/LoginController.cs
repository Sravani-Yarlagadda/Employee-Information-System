using EIS.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EIS.Controllers
{
    public class LoginController : Controller
    {

        EmployeeEntities db = new EmployeeEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(EmpDetail objchk)
        {
            if(ModelState.IsValid)
            {
                using(EmployeeEntities db = new EmployeeEntities())
                {
                  
                    var obj = db.EmpDetails.Where(a => a.Emp_id.Equals(objchk.Emp_id) && a.Password.Equals(objchk.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["Emp_id"] = obj.Emp_id.ToString();
                        Session["Fname"] = obj.Fname.ToString();
                        return RedirectToAction("Index", "EmpDetails");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The Emp_id or password is incorrect");
                    }


                }

            }
           
            return View(objchk);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}