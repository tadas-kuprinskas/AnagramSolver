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
            CachedWord firstCachedWord = new()
            {
                Id = 1,
                Value = "sula"
            };

            CachedWord secondCachedWord = new()
            {
                Id = 2,
                Value = "veidas"
            };

            Word firstWord = new()
            {
                Id = 1,
                Value = "visma",
                OrderedValue = "aimsv",
                PartOfSpeech = "dkt"
            };

            Word secondWord = new()
            {
                Id = 2,
                Value = "rytas",
                OrderedValue = "ayrst",
                PartOfSpeech = "dkt"
            };

            List<CachedWord> cachedWords = new() { firstCachedWord, secondCachedWord };
            List<Word> words = new() { firstWord, secondWord };


            var word = "sula";
            Mock<IAnagramSolverService> _mockAnagramSolverService = new();
            Mock<ISearchInformationService> _mockSearchInformationService = new();
 
            Mock<IWordServiceDb> _mockWordServiceDb = new();

            Mock<ICachedWordService> _mockCachedWordService = new();
            _mockCachedWordService.Setup(m => m.SearchCachedWord(word)).Returns(cachedWords);
            _mockCachedWordService.Setup(m => m.GetCachedAnagrams(word)).Returns(words);

            _wordController = new(_mockAnagramSolverService.Object, _mockCachedWordService.Object, _mockWordServiceDb.Object,
                _mockSearchInformationService.Object);
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
