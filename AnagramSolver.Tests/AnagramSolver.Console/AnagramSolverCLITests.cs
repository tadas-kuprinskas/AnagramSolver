using AnagramSolver.BusinessLogic.Services;
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
        private HashSet<Word> _words;
        private AnagramSolverCLI _anagramSolverCLI;
        private Mock<IAnagramSolverService> _anagramSolverService;
        private Mock<IWriter> _consoleWriter;

        [SetUp]
        public void Setup()
        {
            _words = GetWords();

            _anagramSolverService = new();
            _consoleWriter = new();
            _consoleWriter.Setup(m => m.ReadLine("\nPlease enter your word:")).Returns("sula");

            _anagramSolverCLI = new(_anagramSolverService.Object, _consoleWriter.Object);
        }

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
            _anagramSolverService.Setup(m => m.GetUniqueAnagrams("sula")).Returns(_words);

            Assert.DoesNotThrow(() => _anagramSolverCLI.ReadAndExecute());
        }

        [Test]
        public void ReadAndExecute_GivenEmptyIEnumerable_VerifiesThatMethodWasPerformed()
        {
            _anagramSolverService.Setup(m => m.GetUniqueAnagrams("sula")).Returns(Enumerable.Empty<Word>);

            _anagramSolverCLI.ReadAndExecute();

            _consoleWriter.Verify(m => m.PrintLine($"Your word \"sula\" has no anagrams"), Times.Once());
        }

        [Test]
        public void ReadAndExecute_GivenValidIEnumerable_VerifiesThatCorrectMethodWasPerformed()
        {
            var myWord = "veidas";

            _consoleWriter.Setup(m => m.ReadLine("\nPlease enter your word:")).Returns(myWord);

            _anagramSolverService.Setup(m => m.GetUniqueAnagrams(myWord)).Returns(_words);

            _anagramSolverCLI.ReadAndExecute();

            _consoleWriter.Verify(m => m.PrintAnagrams(_words, myWord), Times.Once());
        }
    }
}
