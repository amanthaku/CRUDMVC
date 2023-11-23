using CRUDMVC.DB;
using CRUDMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CRUDMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Read ()
        {
           DataEntities db = new DataEntities();
            var res = db.Employes.ToList();
            List<EmployeModel> list = new List<EmployeModel>();
            foreach (var i in res)
            {
                list.Add(new EmployeModel()
                {
                    id = i.id,First_Name=i.First_Name,Last_Name=i.Last_Name,Department=i.Department,salary=i.salary,Genders=i.Genders
                    ,Date=i.Date
                    

                });
            }
            return View(list);

        }
       
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeModel model)
        {
            DataEntities db = new DataEntities();
            Employe tb = new Employe();
            tb.id = model.id;
            tb.First_Name= model.First_Name;
            tb.Last_Name= model.Last_Name;
            tb.Department = model.Department;
            tb.salary = model.salary;
            tb.Genders = model.Genders;
            tb.Date = model.Date;
            if (model.id == 0)
            {
                db.Employes.Add(tb);
                db.SaveChanges();
            }
            else
            {
                db.Entry(tb).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();


            }

            return RedirectToAction("Read");
        }
        public ActionResult Delete(int id)
        {
            DataEntities db = new DataEntities();
            var deltedata = db.Employes.Where(m => m.id == id).First();
            db.Employes.Remove(deltedata);
            db.SaveChanges();
            return RedirectToAction("Read");
        }
        public ActionResult Edit(int id )
        {
            DataEntities db = new DataEntities();
            var editdata = db.Employes.Where(m=>m.id == id).First();
            EmployeModel model = new EmployeModel();
            model.id = editdata.id;
            model.First_Name = editdata.First_Name;
            model.Last_Name = editdata.Last_Name;
            model.salary = editdata.salary;
            model.Department = editdata.Department;
            model.Genders = editdata.Genders;   
            model.Date = editdata.Date;

            return View("Create", model);
                    

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("login");
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel obj)
        {
            DataEntities db = new DataEntities();
            var res = db.verifications.Where(m=>m.Email.ToLower() == obj.Email.ToLower()).FirstOrDefault();
            if (res == null)
            {
                TempData["Emailid"] = "Invaild Email id ";
            }
            else
            {
                if (res.Email.ToLower() == obj.Email.ToLower() && res.Password == obj.Password) {
                    FormsAuthentication.SetAuthCookie(obj.Email, false);
                    Session["Email"]=res.Email;
                    return RedirectToAction("Read", "Home");
                }
                else
                {
                    TempData["Pass"] = "Invaild Password";
                }
            }

            return View();
        }

    }
}