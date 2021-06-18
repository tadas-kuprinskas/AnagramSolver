using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Models
{
    public class Word
    {
        public Word()
        {
            WordCachedWordAdditionals = new HashSet<WordCachedWordAdditional>();
        }

        public int Id { get; set; }
        public string Value { get; set; }
        public string PartOfSpeech { get; set; }
        public string OrderedValue { get; set; }

        public virtual ICollection<WordCachedWordAdditional> WordCachedWordAdditionals { get; set; }

        public override bool Equals(object obj)
        {
            return this.Value.Equals(((Word)obj).Value);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }
}
