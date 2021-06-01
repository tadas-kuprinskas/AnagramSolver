﻿using AnagramSolver.BusinessLogic.Services;
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

        public AnagramSolverCLI(IAnagramSolverService anagramSolverService, IWriter consoleWriter)
        {
            _anagramSolverService = anagramSolverService;
            _consoleWriter = consoleWriter;
        }

        public void ReadAndExecute()
        {
            var myWord = _consoleWriter.ReadLine("Please enter your word");
            var anagrams = _anagramSolverService.GetUniqueAnagrams(myWord);

            _consoleWriter.PrintAnagrams(anagrams, myWord);
        }
    }
}
