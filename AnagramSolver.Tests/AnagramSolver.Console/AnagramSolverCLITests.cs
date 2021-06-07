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
using System.IO;
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
        private Mock<IApiWordService> _apiWordService;

        [SetUp]
        public void Setup()
        {
            _words = GetWords();

            _apiWordService = new();
            _anagramSolverService = new();

            var output = new StringWriter();
            System.Console.SetOut(output);

            var input = new StringReader("sula");
            System.Console.SetIn(input);

            _anagramSolverCLI = new(_anagramSolverService.Object, _apiWordService.Object);
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
        public void ReadAndExecute_GivenEmptyIEnumerable_ThrowsNoException()
        {
            var myWord = "sula";
            _anagramSolverService.Setup(m => m.GetUniqueAnagrams(myWord)).Returns(Enumerable.Empty<Word>);

            Assert.DoesNotThrow(() => _anagramSolverCLI.ReadAndExecute());
        }

        [Test]
        public void ReadAndExecute_GivenValidIEnumerable_ThrowsNoException()
        {
            var myWord = "veidas";

            var output = new StringWriter();
            System.Console.SetOut(output);

            var input = new StringReader(myWord);
            System.Console.SetIn(input);

            _anagramSolverService.Setup(m => m.GetUniqueAnagrams(myWord)).Returns(_words);

            Assert.DoesNotThrow(() => _anagramSolverCLI.ReadAndExecute());
        }
    }
}
