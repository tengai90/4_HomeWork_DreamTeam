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
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Service()
        {
            return View();
        }

        //Paradigms
        public ActionResult DeleteParadigm(int? id)
        {
            int idt = id.GetValueOrDefault();

            var oldParadigms = db.LanguageParadigms.Where(p => p.ParadigmID == idt);
            foreach(var item in oldParadigms)
            {
                db.DeleteObject(item);
                db.SaveChanges();
            }

            Paradigm todelete = db.Paradigms.SingleOrDefault(p => p.ParadigmID == idt);
            db.Paradigms.DeleteObject(todelete);
            db.SaveChanges();

            return RedirectToAction("Paradigms", "Home");
        }

        [HttpPost]
        public ActionResult ParadigmEdit (Paradigm obj)
        {
            Paradigm check = db.Paradigms.SingleOrDefault(p => p.Name == obj.Name);
            if (check == null)
            {
                Paradigm toedit = db.Paradigms.SingleOrDefault(p => p.ParadigmID == obj.ParadigmID);
                toedit.Name = obj.Name;
                db.SaveChanges();

            }

            return View();
        }
        public ActionResult EditParadigm(int? id)
        {
            int idt = id.GetValueOrDefault();
            Paradigm toedit = db.Paradigms.SingleOrDefault(p => p.ParadigmID == idt);

            return View(toedit);
        }

        [HttpPost]
        public ActionResult ParadigmCreate (Paradigm obj)
        {
            Paradigm check = db.Paradigms.SingleOrDefault(p => p.Name == obj.Name);
            if (check == null)
            {
                Paradigm toadd = new Paradigm();
                toadd.Name = obj.Name;
                db.Paradigms.AddObject(toadd);
                db.SaveChanges();

            }
            return View();
        }

        public ActionResult CreateParadigm()
        {

            return View();
        }

        public ActionResult Paradigms()
        {

            return View(db.Paradigms.ToList());
        }

            //Paradigms
        
        //Language Control
        [HttpPost]
        public ActionResult EditLanguage(newLanguagePack obj)
        {
            if (obj.Name != null)
            {
                Language eLan = db.Languages.SingleOrDefault(p => p.LanguageID == obj.id);
                eLan.Name = obj.Name;
                eLan.Description = obj.Description;
                eLan.Date = new DateTime(int.Parse(obj.Date), 01, 01);
                db.SaveChanges();

                var todeleteIDE = db.LanguageIDEs.Where(p => p.LanguageID == obj.id).ToList();
                foreach (var item in todeleteIDE)
                {
                    db.LanguageIDEs.DeleteObject(item);
                    db.SaveChanges();
                }
                var todeleteParadigms = db.LanguageParadigms.Where(p => p.LanguageID == obj.id).ToList();
                foreach (var item in todeleteParadigms)
                {
                    db.LanguageParadigms.DeleteObject(item);
                    db.SaveChanges();
                }
                var todeleteRelations = db.LanguageRelationships.Where(p => p.LanguageID == obj.id).ToList();
                foreach (var item in todeleteRelations)
                {
                    db.LanguageRelationships.DeleteObject(item);
                    db.SaveChanges();
                }

                if (obj.IDEId != null)
                {

                    foreach (int i in obj.IDEId)
                    {
                        LanguageIDE addLIDE = new LanguageIDE();
                        addLIDE.LanguageID = obj.id;
                        addLIDE.IDEID = i;

                        db.LanguageIDEs.AddObject(addLIDE);
                        db.SaveChanges();
                    }
                }
                if (obj.ParadigmId != null)
                {

                    foreach (int i in obj.ParadigmId)
                    {
                        LanguageParadigm addPIDE = new LanguageParadigm();
                        addPIDE.LanguageID = obj.id;
                        addPIDE.ParadigmID = i;

                        db.LanguageParadigms.AddObject(addPIDE);
                        db.SaveChanges();
                    }
                }
                if (obj.RelationshipId != null)
                {

                    foreach (int i in obj.RelationshipId)
                    {
                        LanguageRelationship addRIDE = new LanguageRelationship();
                        addRIDE.LanguageID = obj.id;
                        addRIDE.ParentID = i;

                        db.LanguageRelationships.AddObject(addRIDE);
                        db.SaveChanges();

                    }

                }
            }

            return View();
        }

        public ActionResult LanguageEdit(int? id, string name, string desc, string date)
        {
            LanguageForWork LanView = new LanguageForWork();
            int idt = id.GetValueOrDefault();
            LanView.Lan = db.Languages.SingleOrDefault(p => p.LanguageID == idt);
            LanView.AllIDE = db.IDEs.ToList();
            LanView.AllLIDE = db.LanguageIDEs.ToList();
            LanView.AllParadigm = db.Paradigms.ToList();
            LanView.LanList = db.Languages.ToList();
            LanView.AllLParadigm = db.LanguageParadigms.ToList();
            LanView.Rellist = db.LanguageRelationships.ToList();

            ViewBag.name = name;
            ViewBag.desc = desc;
            ViewBag.date = date;
            ViewBag.Id = idt;

            return View(LanView);
            
        }

        public ActionResult LanguageDelete(int? id)
        {
            int idt = id.GetValueOrDefault();


            var todeleteIDE = db.LanguageIDEs.Where(p => p.LanguageID == idt).ToList();
            foreach (var item in todeleteIDE)
            {
                db.LanguageIDEs.DeleteObject(item);
                db.SaveChanges();
            }
            var todeleteParadigms = db.LanguageParadigms.Where(p => p.LanguageID == idt).ToList();
            foreach (var item in todeleteParadigms)
            {
                db.LanguageParadigms.DeleteObject(item);
                db.SaveChanges();
            }
            var todeleteRelations = db.LanguageRelationships.Where(p => p.LanguageID == idt).ToList();
            foreach (var item in todeleteRelations)
            {
                db.LanguageRelationships.DeleteObject(item);
                db.SaveChanges();
            }

            Language dLan = db.Languages.SingleOrDefault(p => p.LanguageID == idt);
            db.Languages.DeleteObject(dLan);
            db.SaveChanges();
            return RedirectToAction("Languages", "Home");

        }

        [HttpPost]
        public ActionResult LanguageCreate(newLanguagePack obj)
        {
            
                Language check = db.Languages.SingleOrDefault(l => l.Name == obj.Name);
                if (check == null)
                {
                    Language toadd = new Language();
                    toadd.Name = obj.Name;
                    toadd.Date = new DateTime(int.Parse(obj.Date), 01, 01);
                    toadd.Description = obj.Description;

                    db.Languages.AddObject(toadd);

                    db.SaveChanges();
                    if (obj.IDEId != null)
                    {
                        Language newLan = db.Languages.SingleOrDefault(l => l.Name == obj.Name);
                        foreach (int i in obj.IDEId)
                        {
                            LanguageIDE addLIDE = new LanguageIDE();
                            addLIDE.LanguageID = newLan.LanguageID;
                            addLIDE.IDEID = i;

                            db.LanguageIDEs.AddObject(addLIDE);
                            db.SaveChanges();
                        }
                    }
                    if (obj.ParadigmId != null)
                    {
                        Language newLan = db.Languages.SingleOrDefault(l => l.Name == obj.Name);
                        foreach (int i in obj.ParadigmId)
                        {
                            LanguageParadigm addPIDE = new LanguageParadigm();
                            addPIDE.LanguageID = newLan.LanguageID;
                            addPIDE.ParadigmID = i;

                            db.LanguageParadigms.AddObject(addPIDE);
                            db.SaveChanges();
                        }
                    }
                    if (obj.RelationshipId != null)
                    {
                        Language newLan = db.Languages.SingleOrDefault(l => l.Name == obj.Name);
                        foreach (int i in obj.RelationshipId)
                        {
                            LanguageRelationship addRIDE = new LanguageRelationship();
                            addRIDE.LanguageID = newLan.LanguageID;
                            addRIDE.ParentID = i;

                            db.LanguageRelationships.AddObject(addRIDE);
                            db.SaveChanges();

                        }

                    }
                }
            
                return View();

            }

        public ActionResult CreateLanguage(int? id)
        {
            LanguageForWork LanView = new LanguageForWork();
            int idt = id.GetValueOrDefault();
            LanView.Lan = db.Languages.SingleOrDefault(p => p.LanguageID == idt);
            LanView.AllIDE = db.IDEs.ToList();
            LanView.AllParadigm = db.Paradigms.ToList();
            LanView.LanList = db.Languages.ToList();
            LanView.Rellist = db.LanguageRelationships.ToList();
           
            
            return View(LanView);
        }

        public ActionResult Languages()
        {
            return View(db.Languages.ToList());
           
        }

        //Language Control end

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
