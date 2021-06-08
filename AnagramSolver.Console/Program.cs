using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Console;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AnagramSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            AnagramSolver.DependencyInjection.DependencyInjection.ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            var anagramSolverService = serviceProvider.GetService<IAnagramSolverService>();
            var apiWordService = serviceProvider.GetService<IClientService>();

            var anagramSolverCli = new AnagramSolverCLI(anagramSolverService, apiWordService);

            while (true)
            {
                anagramSolverCli.SendRequestAndExecute();
            }
        }
    }
}
