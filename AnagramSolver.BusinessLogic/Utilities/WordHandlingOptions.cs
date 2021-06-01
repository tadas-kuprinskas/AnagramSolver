using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Utilities
{
    public class WordHandlingOptions
    {
        public const string WordHandling = "WordHandling";
        public int MinInputLength { get; set; }
        public int NumberOfAnagrams { get; set; }   
    }
}
