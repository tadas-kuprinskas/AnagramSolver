using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApi.Controllers;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Tests.AnagramSolver.WebApi.Controllers
{
    [TestFixture]
    public class WordControllerTests
    {
        private WordController _wordController;

        [SetUp]
        public void Setup()
        {
            Mock<IAnagramSolverService> _mockAnagramSolverService = new();
            Mock<IWordRepository> _mockWordRepository = new();

            _wordController = new(_mockWordRepository.Object, _mockAnagramSolverService.Object);
        }

        [TestCase(1, 30)]
        public void GetPaginatedWords_GivenPageSize_ReturnsCorrectTypeOfResult(int currentPage, int membersNumber)
        {
            var result = _wordController.GetPaginatedWords(currentPage, membersNumber, "sula");

            Assert.IsInstanceOf<IEnumerable<string>>(result);
        }

        [TestCase("sula")]
        public void GetUniqueAnagrams_GivenInput_ReturnsCorrectTypeOfResult(string myWord)
        {
            var result = _wordController.GetUniqueAnagrams(myWord);

            Assert.IsInstanceOf<IEnumerable<string>>(result);
        }

        [TestCase("sula")]
        public void GetUniqueAnagrams_GivenInput_ReturnsSuccessfulStatusCode(string myWord)
        {
            var result = _wordController.GetUniqueAnagrams(myWord);

            result.ShouldNotBeNull();
        }
    }
}
