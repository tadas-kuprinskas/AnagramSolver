using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Models
{
    public class Word_CachedWord_Additionals
    {
        public Word Word { get; set; }
        public CachedWord CachedWord { get; set; }
        public int Id { get; set; }
    }
}
