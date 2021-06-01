using AnagramSolver.Console;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AnagramSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = DependencyInjection.ConfigureServices();

            var anagramSolverCli = serviceProvider.GetService<AnagramSolverCLI>();

            while (true)
            {
                anagramSolverCli.ReadAndExecute();
            }
        }
    }
}
