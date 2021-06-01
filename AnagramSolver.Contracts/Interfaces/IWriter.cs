using AnagramSolver.Contracts.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWriter
    {
        void PrintLine();
        void PrintLine(string input);
        string ReadLine(string message);
        void PrintAnagrams(IEnumerable<Word> anagrams, string myWord);
    }
}