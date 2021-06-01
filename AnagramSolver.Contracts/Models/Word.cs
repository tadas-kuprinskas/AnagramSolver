using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Models
{
    public class Word
    {
        public string Value { get; set; }
        public string PartOfSpeech { get; set; }
        public string OrderedValue { get; set; }
    }
}
