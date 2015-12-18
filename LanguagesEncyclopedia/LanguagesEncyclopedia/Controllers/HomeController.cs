using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using LanguagesEncyclopedia.Models;
using System.Data;
using System.Configuration;
using System.Data.EntityClient;
using LanguagesEncyclopedia.Views.Home;


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
        //IDE Control
        //IDE DELETE
        public ActionResult DeleteIDE(int? id)
        {
            int idt = id.GetValueOrDefault();
            

            var todelete = db.LicenseIDEs.Where(p => p.IDEID == idt).ToList();
            foreach (var item in todelete)
            {
                db.LicenseIDEs.DeleteObject(item);
                db.SaveChanges();
            }

            IDE dIDE = db.IDEs.SingleOrDefault(p => p.IDEID == idt);
            db.IDEs.DeleteObject(dIDE);
            db.SaveChanges();
            return RedirectToAction("IDE", "Home");
        }



       //IDE DELETE


        //IDE Edit
        public ActionResult IDEEdit(int? id, string name, string url)
        {
            List<License> licenses = new List<License>();
            ViewModel DoubleModel = new ViewModel();
            licenses = db.Licenses.ToList();

            int idt = id.GetValueOrDefault();


            DoubleModel.LicensesIDE = db.LicenseIDEs.Where(r => r.IDEID == idt);


            DoubleModel.Licenses = licenses;
            ViewBag.eURL = url;
            ViewBag.eName = name;
            ViewBag.IDID = idt;

            return View(DoubleModel);
        }
    

        [HttpPost]
        public ActionResult EditIDE(IDEClass obj)
        {
            
                IDE eIDE = db.IDEs.SingleOrDefault(IDE => IDE.IDEID == obj.id);
                eIDE.Name = obj.Name;
                eIDE.URL = obj.url;
                db.SaveChanges();

                var todelete = db.LicenseIDEs.Where(p => p.IDEID == obj.id).ToList();
                foreach(var item in todelete)
                {
                    db.LicenseIDEs.DeleteObject(item);
                    db.SaveChanges();
                }

                if (obj.licensesIDs != null)
                {
                    
                    foreach (int i in obj.licensesIDs)
                    {
                        LicenseIDE addIDE = new LicenseIDE();
                        addIDE.IDEID = obj.id;
                        addIDE.LicenseID = i;

                        db.LicenseIDEs.AddObject(addIDE);
                        db.SaveChanges();
                    }
                }
                
     
            return View();
        }
        //IDE Edit END
        //IDE ADD
        public ActionResult IDE()
        {
            List<IDE> IDEss = new List<IDE>();

            IDEss = db.IDEs.ToList();

            return View(IDEss);
        }

        public ActionResult IDECreate()
        {

            List<License> licenses = new List<License>();

            licenses = db.Licenses.ToList();

            return View(licenses);
        }

        [HttpPost]
        public ActionResult CreateIDE(IDEClass obj)
        {
            IDE check = db.IDEs.SingleOrDefault(IDE =>IDE.Name == obj.Name);
            if (check == null)
            {
                IDE toadd = new IDE();
                toadd.Name = obj.Name;
                toadd.URL = obj.url;

                db.IDEs.AddObject(toadd);

                db.SaveChanges();
                if (obj.licensesIDs!=null)
                {
                    IDE newIDE = db.IDEs.SingleOrDefault(IDE => IDE.Name == obj.Name);
                   foreach(int i in obj.licensesIDs)
                    {
                        LicenseIDE addIDE = new LicenseIDE();
                        addIDE.IDEID = newIDE.IDEID;
                        addIDE.LicenseID = i;

                        db.LicenseIDEs.AddObject(addIDE);
                        db.SaveChanges();
                    }
                }

               

            }
            return View();
        }

        //IDE ADD END
        //IDE Control END
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
        public ActionResult AddLicense(LicenseClass obj)
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
