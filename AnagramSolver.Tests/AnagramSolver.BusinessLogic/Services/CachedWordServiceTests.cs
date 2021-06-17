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
        private Mock<IWordRepository> _mockWordRepository;
        private string _myWord;
        private List<Word> _anagrams;
        private Word _word;
        private CachedWord _cachedWord;

        [SetUp]
        public void Setup()
        {
            _myWord = "sula";

            _cachedWord = new()
            {
                Id = 1,
                Value = "sula"
            };

            _word = new()
            {
                Id = 1,
                OrderedValue = "alsu",
                PartOfSpeech = "dkt",
                Value = "sula"
            };

            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _anagrams = fixture.CreateMany<Word>(8).ToList();
            _anagrams.Add(_word);
            var cachedWord = fixture.Create<CachedWord>();
            var words = fixture.CreateMany<Word>(8).ToList();

            _mockWordRepository = new();

            _mockCachedWordsRepository = new();
            _mockCachedWordsRepository.Setup(m => m.AddCachedWord(_cachedWord)).Returns(_cachedWord);
            _mockCachedWordsRepository.Setup(m => m.SearchCachedWord(_myWord)).Returns(cachedWord);
            _mockCachedWordsRepository.Setup(m => m.GetCachedAnagrams(_myWord)).Returns(words);
            _mockWordRepository.Setup(m => m.GetWord(_myWord)).Returns(_word);

            _cachedWordService = new(_mockCachedWordsRepository.Object, _mockWordRepository.Object);
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
