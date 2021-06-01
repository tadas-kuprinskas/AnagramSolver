using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.Console.Writers
{
    public class ConsoleWriter : IWriter
    {
        public void PrintLine(string input)
        {
            System.Console.WriteLine(input);
        }

        public void PrintLine()
        {
            System.Console.WriteLine();
        }

        public string ReadLine(string message)
        {
            PrintLine(message);
            return System.Console.ReadLine();
        }
    }
}
