using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
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
    public class WordServiceDbTests
    {
        private WordServiceDb _wordServiceDb;
        private Mock<IWordRepository> _mockWordRepository;
        private string _word;
        private IList<Word> _results;

        [SetUp]
        public void Setup()
        {
            _word = "visma";

            Word firstWord = new()
            {
                Id = 1,
                Value = "Vismantas",
                OrderedValue = "aaimnsstv",
                PartOfSpeech = "tikr. dkt"
            };

            Word secondWord = new()
            {
                Id = 2,
                Value = "Vismante",
                OrderedValue = "aeimnstv",
                PartOfSpeech = "tikr. dkt"
            };

            _results = new List<Word>() { firstWord, secondWord };

            _mockWordRepository = new();
            _mockWordRepository.Setup(m => m.SearchForWords(_word)).Returns(_results);
            _mockWordRepository.Setup(m => m.GetPaginatedWords(1, 2, _word)).Returns(_results);

            _wordServiceDb = new(_mockWordRepository.Object);
        }

        [Test]
        public void SearchForWords_GivenCorrectValues_ReturnsCorrectTypeResult()
        {
            var words = _wordServiceDb.SearchForWords(_word);

            words.ShouldNotBeNull();
            words.ShouldNotBeOfType<IEnumerable<string>>();
        }

        [Test]
        public void GetPaginatedWords_GivenPageSize_ReturnsCorrectAmountOfItemsOnPage()
        {
           
            var paginatedWords = _wordServiceDb.GetPaginatedWords(1, 2, _word);

            paginatedWords.ShouldNotBeNull();
            paginatedWords.Count().ShouldBe(2);
        }
    }
}
