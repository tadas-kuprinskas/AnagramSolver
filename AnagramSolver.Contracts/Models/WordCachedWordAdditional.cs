using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Models
{
    public class WordCachedWordAdditional
    {
        public int WordId { get; set; }
        public int CachedWordId { get; set; }
        public int Id { get; set; }

        public virtual CachedWord CachedWord { get; set; }
        public virtual Word Word { get; set; }
    }
}
