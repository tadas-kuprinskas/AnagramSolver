﻿using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Console;
using AnagramSolver.Console.Writers;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AutoFixture;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Tests.AnagramSolver.Console
{
    [TestFixture]
    public class AnagramSolverCLITests
    {
        private static HashSet<Word> GetWords()
        {
            var orderedWord = "adeisv";
            var orderedSecondWord = "aegrs";

            var fixture = new Fixture();

            fixture.Customize<Word>(w => w.With(p => p.Value, "dievas").With(p => p.OrderedValue, orderedWord));
            var firstWord = fixture.Create<Word>();

            fixture.Customize<Word>(w => w.With(p => p.Value, "vedasi").With(p => p.OrderedValue, orderedWord));
            var secondWord = fixture.Create<Word>();

            fixture.Customize<Word>(w => w.With(p => p.Value, "vedasi").With(p => p.OrderedValue, orderedWord));
            var thirdWord = fixture.Create<Word>();

            fixture.Customize<Word>(w => w.With(p => p.Value, "serga").With(p => p.OrderedValue, orderedSecondWord));
            var fourthWord = fixture.Create<Word>();

            HashSet<Word> words = new() { firstWord, secondWord, thirdWord, fourthWord };

            return words;
        }

        [Test]
        public void ReadAndExecute_GivenValidWord_ThrowsNoException()
        {
            var words = GetWords();
            Mock<IWriter> _consoleWriter = new();
            _consoleWriter.Setup(m => m.ReadLine("\nPlease enter your word:")).Returns("sula");

            Mock<IAnagramSolverService> _anagramSolverService = new();
            _anagramSolverService.Setup(m => m.GetUniqueAnagrams("sula")).Returns(words);

            AnagramSolverCLI _anagramSolverCLI = new(_anagramSolverService.Object, _consoleWriter.Object);

            Assert.DoesNotThrow(() => _anagramSolverCLI.ReadAndExecute());
        }

        [Test]
        public void ReadAndExecute_GivenEmptyIEnumerable_VerifiesThatMethodWasPerformed()
        {
            var myWord = "sula";
            Mock<IWriter> _consoleWriter = new();
            _consoleWriter.Setup(m => m.ReadLine("\nPlease enter your word:")).Returns(myWord);

            Mock<IAnagramSolverService> _anagramSolverService = new();
            _anagramSolverService.Setup(m => m.GetUniqueAnagrams(myWord)).Returns(Enumerable.Empty<Word>);

            AnagramSolverCLI _anagramSolverCLI = new(_anagramSolverService.Object, _consoleWriter.Object);

            _anagramSolverCLI.ReadAndExecute();

            _consoleWriter.Verify(m => m.PrintLine($"Your word \"{myWord}\" has no anagrams"));
        }

        [Test]
        public void ReadAndExecute_GivenValidIEnumerable_VerifiesThatCorrectMethodWasPerformed()
        {
            var myWord = "veidas";
            var words = GetWords();

            Mock<IWriter> _consoleWriter = new();
            _consoleWriter.Setup(m => m.ReadLine("\nPlease enter your word:")).Returns(myWord);

            Mock<IAnagramSolverService> _anagramSolverService = new();
            _anagramSolverService.Setup(m => m.GetUniqueAnagrams(myWord)).Returns(words);

            AnagramSolverCLI _anagramSolverCLI = new(_anagramSolverService.Object, _consoleWriter.Object);

            _anagramSolverCLI.ReadAndExecute();

            _consoleWriter.Verify(m => m.PrintAnagrams(words, myWord));
        }
    }
}
