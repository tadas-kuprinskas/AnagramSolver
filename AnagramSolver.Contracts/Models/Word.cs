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
        public string Value { get; set; }
        public string PartOfSpeech { get; set; }
        public string OrderedValue { get; set; }

        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            return this.Value.Equals(((Word)obj).Value);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }

    public class WordComparer : IEqualityComparer<Word>
    {
        public bool Equals(Word x, Word y)
        {
            return x.Value.Trim().ToLower().Equals(y.Value.Trim().ToLower());
        }

        public int GetHashCode(Word obj)
        {
            return obj.Value.GetHashCode();
        }
    }
}
