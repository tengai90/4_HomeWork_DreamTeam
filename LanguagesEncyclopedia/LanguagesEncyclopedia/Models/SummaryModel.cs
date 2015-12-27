using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguagesEncyclopedia.Models
{
    public class SummaryModel
    {
        public int LanguageCount { get; set; }
        public int IDECount { get; set; }
        public int LicenseCount { get; set; }
        public int ParadigmCount { get; set; }
        public int TaskCount { get; set; }
        public int SolutionCount { get; set; }
    }
}