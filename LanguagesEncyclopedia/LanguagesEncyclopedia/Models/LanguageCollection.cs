using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguagesEncyclopedia.Models
{
    public class LanguageForWork
    {
        public LanguagesEncyclopedia.Language Lan { get; set; }
        public IEnumerable<LanguagesEncyclopedia.IDE> AllIDE { get; set; }
        public IEnumerable<LanguagesEncyclopedia.LanguageIDE> AllLIDE { get; set; }
        public IEnumerable<LanguagesEncyclopedia.Paradigm> AllParadigm { get; set; }
        public IEnumerable<LanguagesEncyclopedia.Language> LanList { get; set; }
        public IEnumerable<LanguagesEncyclopedia.LanguageRelationship> Rellist { get; set; }
        public IEnumerable<LanguagesEncyclopedia.LanguageParadigm> AllLParadigm { get; set; }

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