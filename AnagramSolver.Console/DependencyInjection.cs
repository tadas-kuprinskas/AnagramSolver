using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Console.Writers;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Database;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Console
{
    public static class DependencyInjection
    {
        public static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<AnagramSolverCLI>()
                .AddSingleton<IAnagramSolverService, AnagramSolverService>()
                .AddSingleton<IWordRepository, WordRepository>()
                .AddSingleton<IValidationService, ValidationService>()
                .AddSingleton<IWriter, ConsoleWriter>()
                .BuildServiceProvider();
        }
    }
}
