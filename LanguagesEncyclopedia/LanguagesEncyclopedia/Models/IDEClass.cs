
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguagesEncyclopedia.Models
{
    public class IDEClass
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string url { get; set; }
        public List<int> licensesIDs { get; set; }
    }

    public class ViewModel
    {
        public IEnumerable<LanguagesEncyclopedia.Views.Home.License>  Licenses { get; set; }
        public IEnumerable<LanguagesEncyclopedia.Views.Home.LicenseIDE> LicensesIDE { get; set; }

        public string GetTickLiceneses( int lic, ViewModel model)
        {

            var check = model.LicensesIDE.SingleOrDefault(LicenesesIDE => LicenesesIDE.LicenseID == lic);
            if (check != null) return "true";
            else return "false";

        }
    }

   
}