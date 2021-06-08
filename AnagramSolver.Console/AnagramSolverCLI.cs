using AnagramSolver.BusinessLogic.Exceptions;
using AnagramSolver.Console.Writers;
using AnagramSolver.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Console
{
    public class AnagramSolverCLI
    {
        private readonly IAnagramSolverService _anagramSolverService;
        private readonly IWriter _consoleWriter;
        private readonly IClientService _clientService;

        public AnagramSolverCLI(IAnagramSolverService anagramSolverService, IClientService clientService)
        {
            _anagramSolverService = anagramSolverService;
            _clientService = clientService;
            _consoleWriter = new ConsoleWriter();
        }

        public void ReadAndExecute()
        {
            try
            {
                var myWord = _consoleWriter.ReadLine("\nPlease enter your word:");
                var anagrams = _anagramSolverService.GetUniqueAnagrams(myWord);

                if(!anagrams.Any())
                {
                    _consoleWriter.PrintLine($"Your word \"{myWord}\" has no anagrams");
                }
                else
                {
                    _consoleWriter.PrintAnagrams(anagrams, myWord);
                }    
            }
            catch (AggregateException ae)
            {
                ae.Handle(ex => {
                    if (ex is CustomException)
                        _consoleWriter.PrintLine(ex.Message);
                    return ex is CustomException;
                });
            }
        }

        public void SendRequestAndExecute()
        {
            try
            {
                var myWord = _consoleWriter.ReadLine("\nPlease enter your word:");
                var anagrams = _clientService.SendGetAnagramsRequestAsync(myWord).Result;

                if (!anagrams.Any())
                {
                    _consoleWriter.PrintLine($"Your word \"{myWord}\" has no anagrams");
                }
                else
                {
                    _consoleWriter.PrintAnagrams(anagrams, myWord);
                }
            }
            catch (AggregateException ae)
            {
                ae.Handle(ex => {
                    if (ex is CustomException)
                        _consoleWriter.PrintLine(ex.Message);
                    return ex is CustomException;
                });
            }
        }
    }
}
