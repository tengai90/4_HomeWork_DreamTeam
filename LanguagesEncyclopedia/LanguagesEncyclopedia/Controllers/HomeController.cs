using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguagesEncyclopedia.Views.Home;
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
        private SqlConnection con;

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
        public ActionResult Edit(int? id)
        {
            int idt = id.GetValueOrDefault();

            connection();
            con.Open();
            SqlCommand com = con.CreateCommand();

            com.CommandText = "Select Name from License where LicenseID=@Id";
            com.Parameters.AddWithValue("@Id", idt);
            SqlDataReader reader = com.ExecuteReader();
            string name="";

            while(reader.Read())
             name=reader["Name"].ToString();

            reader.Close();
            con.Close();

            ViewBag.Name = name;
            ViewBag.Id = idt;
            return View();
        }

        [HttpPost]
        public ActionResult EditLicense(LicenseClass obj)
        {
            EditDetails(obj);
            return View();
        }

        public void EditDetails(LicenseClass obj)
        {
            connection();
            con.Open();

            SqlCommand com = con.CreateCommand();
            com.CommandText = "SELECT Name from License";
            com.CommandType = CommandType.Text;

            SqlDataReader reader = com.ExecuteReader();

            bool edit = true;

            while (reader.Read())
                if (reader["Name"].ToString() == obj.Name) edit = false;

            reader.Close();
            if (edit)
            {
                com = con.CreateCommand();

                com.CommandText = "Update License set Name=@Name where LicenseID=@Id";
                com.Parameters.AddWithValue("@Id", obj.id);
                com.Parameters.AddWithValue("@Name", obj.Name);
                com.ExecuteNonQuery();


                con.Close();                         
            }
        }

        //End Edit License


       //Delete License
        public ActionResult DeleteLicense(int? id)
        {
            int idt = id.GetValueOrDefault();

            connection();
            con.Open();
            SqlCommand com = con.CreateCommand();
            
                com.CommandText = "delete from License where LicenseID=@Id";
                com.Parameters.AddWithValue("@Id", idt);
                com.ExecuteNonQuery();

    
                con.Close();
               
               return RedirectToAction("Licenses", "Home");
        }

        //End Delete License

        //Add License

        
 public ActionResult Create()    
    {    
           
        return View();    
    } 
        
        [HttpPost]
        public ActionResult AddLicense(LicenseClass obj)
        {
           AddDetails(obj);
            
            return View();
        }

        //To Handle connection related activities    
        private void connection()
        {
            con = new SqlConnection("Data Source=SQL5018.Smarterasp.net;Initial Catalog=DB_9E5584_encyclopediadb;user id=DB_9E5584_encyclopediadb_admin;password=P@ssw0rd12345;");

        }
        //To add Records into database     
        private void AddDetails(LicenseClass obj)
        {
            connection();
            con.Open();
            SqlCommand com = con.CreateCommand();
            com.CommandText = "SELECT Name from License";
            com.CommandType = CommandType.Text;
           
            SqlDataReader reader = com.ExecuteReader();

            bool add = true;

            while (reader.Read())
                if (reader["Name"].ToString() == obj.Name) add = false;

            reader.Close();
            if (add)
            {
                 com = new SqlCommand("AddLicense", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Name", obj.Name);
                
                com.ExecuteNonQuery();
                
                
            }

            con.Close();
        }
  
        //End Add License

    }
}
