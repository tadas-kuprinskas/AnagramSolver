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
        private readonly IApiWordService _apiWordService;

        public AnagramSolverCLI(IAnagramSolverService anagramSolverService, IWriter consoleWriter, IApiWordService apiWordService)
        {
            _anagramSolverService = anagramSolverService;
            _consoleWriter = consoleWriter;
            _apiWordService = apiWordService;
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
            catch (ArgumentException ex)
            {
                _consoleWriter.PrintLine(ex.Message);
            }
        }

        public void SendRequestAndExecute()
        {
            try
            {
                var myWord = _consoleWriter.ReadLine("\nPlease enter your word:");
                var anagrams = _apiWordService.SendGetAnagramsRequest(myWord).Result;

                if (!anagrams.Any())
                {
                    _consoleWriter.PrintLine($"Your word \"{myWord}\" has no anagrams");
                }
                else
                {
                    _consoleWriter.PrintAnagrams(anagrams, myWord);
                }
            }
            catch (ArgumentException ex)
            {
                _consoleWriter.PrintLine(ex.Message);
            }
        }
    }
}
