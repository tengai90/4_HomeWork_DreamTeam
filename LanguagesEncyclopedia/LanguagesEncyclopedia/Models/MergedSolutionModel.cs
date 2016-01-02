using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;

namespace LanguagesEncyclopedia.Models
{
    public class MergedSolutionModel
    {
        public Solution solution {get; set;}
        public List<SelectListItem> LanguageTasks { get; set; }

        public MergedSolutionModel()
        { }


        public MergedSolutionModel(List<SelectListItem> LanguageTasks)
        {
            SetLanguagesTasks(LanguageTasks);
        }

        public void SetLanguagesTasks(List<SelectListItem> LanguageTasks)
        {
            this.LanguageTasks = LanguageTasks;
        }
    }
}