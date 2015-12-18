using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using LanguagesEncyclopedia.Models;
using System.Data;
using System.Configuration;

namespace LanguagesEncyclopedia.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/


        EncyclopediaEntities db = new EncyclopediaEntities();
       

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Licenses()
        {
            List<License> licenses = new List<License>();

            licenses = db.Licenses.ToList();
            
            return View(licenses);
        }

        //Edit License
        public ActionResult Edit(int? id, string name)
        {
            int idt = id.GetValueOrDefault();


            ViewBag.Name = name;
            ViewBag.Id = idt;
            return View();
        }

        [HttpPost]
        public ActionResult EditLicense(LicenseClass obj)
        {
            License check = db.Licenses.SingleOrDefault(License => License.Name == obj.Name);
            if (check == null)
            {
                License toedit = db.Licenses.SingleOrDefault(License => License.LicenseID == obj.id);
                toedit.Name = obj.Name;
                db.SaveChanges();
                
            }

            return View();
        }



        //End Edit License


       //Delete License
        public ActionResult DeleteLicense(int? id)
        {
            int idt = id.GetValueOrDefault();
            LicenseIDE check = db.LicenseIDEs.SingleOrDefault(LicenseIDE => LicenseIDE.LicenseID == idt);
            if (check == null)
            {
                License todelete= db.Licenses.SingleOrDefault(License =>License.LicenseID ==idt);
                db.Licenses.DeleteObject(todelete);
                db.SaveChanges();
            }


           return RedirectToAction("Licenses", "Home");
        }

        //End Delete License

        //Add License

        
 public ActionResult Create(int? id)    
    {    
     int idt = id.GetValueOrDefault();
     ViewBag.AID = idt;

        return View();    
    } 
        
        [HttpPost]
        public ActionResult AddLicense(License obj)
        {
            License check = db.Licenses.SingleOrDefault(License => License.Name == obj.Name);
            if (check == null)
            {
                License toadd=new License();
                toadd.Name=obj.Name;
                db.Licenses.AddObject(toadd);
                db.SaveChanges();

            }

            return View();
        }
   

  
        //End Add License

    }
}
