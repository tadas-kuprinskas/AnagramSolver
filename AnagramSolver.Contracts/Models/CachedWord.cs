using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Models
{
    public class CachedWord
    {
        public CachedWord()
        {
            WordCachedWordAdditionals = new HashSet<WordCachedWordAdditional>();
        }

        public int Id { get; set; }
        public string Value { get; set; }

        public virtual ICollection<WordCachedWordAdditional> WordCachedWordAdditionals { get; set; }
    }
}
