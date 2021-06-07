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
            Mock<IApiWordService> _mockApiWordService = new();
            Mock<IAnagramSolverService> _mockAnagramSolverService = new();

            _wordController = new(_mockApiWordService.Object, _mockAnagramSolverService.Object);
        }

        [TestCase(10, 20)]
        public void GetPaginatedWords_GivenPageSize_ReturnsSuccessfulStatusCode(int currentPage, int membersNumber)
        {
            var result = _wordController.GetPaginatedWords(currentPage, membersNumber);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestCase(1, 30)]
        public void GetPaginatedWords_GivenPageSize_ReturnsCorrectTypeOfResult(int currentPage, int membersNumber)
        {
            var result = _wordController.GetPaginatedWords(currentPage, membersNumber);

            Assert.IsInstanceOf<IActionResult>(result);
        }

        [TestCase("sula")]
        public void GetUniqueAnagrams_GivenInput_ReturnsCorrectTypeOfResult(string myWord)
        {
            var result = _wordController.GetUniqueAnagrams(myWord);

            Assert.IsInstanceOf<IActionResult>(result);
        }

        [TestCase("sula")]
        public void GetUniqueAnagrams_GivenInput_ReturnsSuccessfulStatusCode(string myWord)
        {
            var result = _wordController.GetUniqueAnagrams(myWord);

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}
