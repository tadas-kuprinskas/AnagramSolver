﻿using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Contracts.Models;
using AnagramSolver.Database;
using AutoFixture;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Tests.AnagramSolver.Database
{
    [TestFixture]
    public class WordRepositoryTests
    {
        private Settings _options;
        private WordRepository _wordRepository;
        private IEnumerable<string> _words;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            _words = new List<string>() { "sportas", "vedasi", "sula", "veidas", "alus", "praktika", "veidas"};

            _options = new() { FilePath = "AnagramSolver.Contracts/Data/zodynas.txt" };

            var mockOptions = new Mock<IOptions<Settings>>();
            mockOptions.Setup(ap => ap.Value).Returns(_options);

            _wordRepository = new(mockOptions.Object);
        }

        [Test]
        public void ReadAndGetDictionary_GivenCorrectValuesInMethod_ReturnsCorrectTypeNotEmptyDictionary()
        {
            var dictionary = _wordRepository.ReadAndGetDictionary();

            dictionary.ShouldBeOfType<Dictionary<string, HashSet<Word>>>();
            dictionary.ShouldNotBeEmpty();
        }

        [TestCase(1, 20)]
        public void GetPaginatedWords_GivenPageSize_ReturnsCorrectAmountOfItemsOnPage(int currentPage, int pageSize)
        {
            var wordList = _wordRepository.GetPaginatedWords(currentPage, pageSize, _words, "sula");

            wordList.Count().ShouldBe(1);
        }

        [TestCase(3, 50)]
        public void GetPaginatedWords_GivenDifferentPageSizeAndPageNumber_ReturnsCorrectAmountOfItemsOnPage(int currentPage, int pageSize)
        {
            var wordList = _wordRepository.GetPaginatedWords(currentPage, pageSize, _words, "veidas");

            wordList.Count().ShouldBe(2);
        }
    }
}
