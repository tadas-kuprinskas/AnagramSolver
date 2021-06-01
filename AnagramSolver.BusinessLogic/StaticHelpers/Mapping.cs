using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.StaticHelpers
{
    public static class Mapping
    {
        public static Word MapToWord(string orderedWord, string firstWord, string partOfSpeech)
        {
            return new Word()
            {
                Value = firstWord,
                PartOfSpeech = partOfSpeech,
                OrderedValue = orderedWord
            };
        }
    }
}
