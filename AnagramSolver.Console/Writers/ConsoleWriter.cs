using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Console.Writers
{
    public class ConsoleWriter : IWriter
    {
        public virtual void PrintLine(string input)
        {
            System.Console.WriteLine(input);
        }

        public void PrintLine()
        {
            System.Console.WriteLine();
        }

        public virtual string ReadLine(string message)
        {
            PrintLine(message);
            return System.Console.ReadLine();
        }

        public virtual void PrintAnagrams(IEnumerable<Word> anagrams, string myWord)
        {
            foreach (var anagram in anagrams)
            {
                System.Console.Write($"{anagram.Value} ");             
            }
        }

        public void PrintAnagrams(IEnumerable<string> anagrams, string myWord)
        {
            foreach (var anagram in anagrams)
            {
                System.Console.Write($"{anagram} ");
            }
        }
    }
}
