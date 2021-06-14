using AnagramSolver.BusinessLogic.Services;
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

namespace AnagramSolver.Tests.AnagramSolver.BusinessLogic.Services
{
    [TestFixture]
    public class CachedWordServiceTests
    {
        private CachedWordService _cachedWordService;
        private Mock<ICachedWordRepository> _mockCachedWordsRepository;
        private string _myWord;
        private List<Word> _anagrams;

        [SetUp]
        public void Setup()
        {
            _myWord = "sula";
            var fixture = new Fixture();

            _anagrams = fixture.CreateMany<Word>(8).ToList();
            var cachedWord = fixture.Create<CachedWord>();
            var words = fixture.CreateMany<Word>(8).ToList();

            _mockCachedWordsRepository = new();
            _mockCachedWordsRepository.Setup(m => m.AddCachedWord(_myWord)).Returns(2);
            _mockCachedWordsRepository.Setup(m => m.SearchCachedWord(_myWord)).Returns(cachedWord);
            _mockCachedWordsRepository.Setup(m => m.GetCachedAnagrams(_myWord)).Returns(words);

            _cachedWordService = new(_mockCachedWordsRepository.Object);
        }

        [Test]
        public void InsertCachedWordIntoTables_GivenCorrectValues_VerifyThatMethodWasCalled()
        {
            _cachedWordService.InsertCachedWordIntoTables(_myWord, _anagrams);

            var id = _anagrams.FirstOrDefault().Id;

            _mockCachedWordsRepository.Verify(m => m.AddToAdditionalTable(id, 2), Times.AtLeastOnce);
        }

        [Test]
        public void SearchCachedWord_GivenCorrectValues_ReturnsCorrectTypeResult()
        {
            var cachedWord = _cachedWordService.SearchCachedWord(_myWord);

            cachedWord.ShouldNotBeNull();
            cachedWord.ShouldBeOfType<CachedWord>();
        }

        [Test]
        public void GetCachedAnagrams_GivenCorrectValues_ReturnsCorrectTypeResult()
        {
            var words = _cachedWordService.GetCachedAnagrams(_myWord);

            words.ShouldNotBeNull();
            words.ShouldBeOfType<List<Word>>();
        }
    }
}
