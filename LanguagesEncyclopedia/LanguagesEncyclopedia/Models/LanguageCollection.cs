using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguagesEncyclopedia.Models
{
    public class LanguageForWork
    {
        public LanguagesEncyclopedia.Views.Home.Language Lan { get; set; }
        public IEnumerable<LanguagesEncyclopedia.Views.Home.IDE> AllIDE { get; set; }
        public IEnumerable<LanguagesEncyclopedia.Views.Home.LanguageIDE> AllLIDE { get; set; }
        public IEnumerable<LanguagesEncyclopedia.Views.Home.Paradigm> AllParadigm { get; set; }
        public IEnumerable<LanguagesEncyclopedia.Views.Home.Language> LanList { get; set; }
        public IEnumerable<LanguagesEncyclopedia.Views.Home.LanguageRelationship> Rellist { get; set; }
        public IEnumerable<LanguagesEncyclopedia.Views.Home.LanguageParadigm> AllLParadigm { get; set; }

        public string GetTickIDE(int id, LanguageForWork model)
        {

            var check = model.AllIDE.SingleOrDefault(p => p.IDEID == id);
            if (check != null) return "true";
            else return "false";

        }
        public string GetTickParadigm(int id, LanguageForWork model)
        {
            if (model.AllParadigm!=null){
                var check = model.AllParadigm.SingleOrDefault(p => p.ParadigmID == id);
                if (check != null) return "true";
                else return "false";
            }
             else return "false";

        }
        public string GetTickLan(int id,int id2, LanguageForWork model)
        {

            if (model.Rellist != null)
            {
                var check = model.Rellist.SingleOrDefault(p => p.ParentID == id && p.LanguageID == id2);
                if (check != null) return "true";
                else return "false";
            }
            else return "false";

        }
    }

    public class newLanguagePack
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public int[] IDEId { get; set; }
        public int[] ParadigmId { get; set; }
        public int[] RelationshipId { get; set; }
        public int id { get; set; }
    }
    


}