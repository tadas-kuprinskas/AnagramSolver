using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Utilities
{
    public class Settings
    {
        public const string HandlingOptions = "HandlingOptions";
        public int MinInputLength { get; set; }
        public int NumberOfAnagrams { get; set; }
        public string FilePath { get; set; }
        public string WordUri { get; set; }
    }
}
