using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Models
{
    public class SearchInformation
    {
        public string UserIp { get; set; }
        public DateTime SearchTime { get; set; }
        public string SearchedWord { get; set; }
        public string Anagram { get; set; }
    }
}
