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
        private AnagramSolverCLI _anagramSolverCLI;
        private Mock<IAnagramSolverService> _anagramSolverService;
        private Mock<IClientService> _apiWordService;

        [SetUp]
        public void Setup()
        {
            _apiWordService = new();
            _anagramSolverService = new();

            var output = new StringWriter();
            System.Console.SetOut(output);

            var input = new StringReader("sula");
            System.Console.SetIn(input);

            _anagramSolverCLI = new(_anagramSolverService.Object, _apiWordService.Object);
        }

        [Test]
        public void ReadAndExecute_GivenValidWord_ThrowsNoException()
        {
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

            Assert.DoesNotThrow(() => _anagramSolverCLI.ReadAndExecute());
        }
    }
}
