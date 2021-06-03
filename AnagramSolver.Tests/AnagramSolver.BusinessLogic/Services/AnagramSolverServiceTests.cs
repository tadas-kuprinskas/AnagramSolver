using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
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

namespace AnagramSolver.Tests.AnagramSolver.BusinessLogic.Services
{
    [TestFixture]
    public class AnagramSolverServiceTests
    {
        private HashSet<Word> _words;
        private Dictionary<string, HashSet<Word>> _dictionary;
        private AnagramSolverService _anagramSolverService;
        private Mock<IWordRepository> _mockWordRepository;

        [SetUp]
        public void Setup()
        {
            var orderedWord = "adeisv";
            _words = GetWords();

            _dictionary = new Dictionary<string, HashSet<Word>>
            {
                { orderedWord, _words }
            };

            _mockWordRepository = new Mock<IWordRepository>();
            _mockWordRepository.Setup(w => w.ReadAndGetDictionary()).Returns(_dictionary);

            var mockValidationService = new Mock<IValidationService>();

            WordHandlingOptions wordHandlingOptions = new() { NumberOfAnagrams = 10 };
            var mockOptions = new Mock<IOptions<WordHandlingOptions>>();

            mockOptions.Setup(ap => ap.Value).Returns(wordHandlingOptions);

            _anagramSolverService = new(_mockWordRepository.Object, mockValidationService.Object, mockOptions.Object);
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

            fixture.Customize<Word>(w => w.With(p => p.Value, "dievas").With(p => p.OrderedValue, orderedWord));
            var fourthWord = fixture.Create<Word>();

            fixture.Customize<Word>(w => w.With(p => p.Value, "garse").With(p => p.OrderedValue, orderedSecondWord));
            var fifthWord = fixture.Create<Word>();

            fixture.Customize<Word>(w => w.With(p => p.Value, "serga").With(p => p.OrderedValue, orderedSecondWord));
            var sixthWord = fixture.Create<Word>();

            fixture.Customize<Word>(w => w.With(p => p.Value, "serga").With(p => p.OrderedValue, orderedSecondWord));
            var seventhWord = fixture.Create<Word>();

            HashSet<Word> words = new() { firstWord, secondWord, thirdWord, fourthWord, fifthWord, sixthWord, seventhWord };

            return words;
        }

        [TestCase("adeisv")]
        public void FindSingleWordAnagrams_GivenSameValueWords_ReturnsFourUniqueAnagrams(string orderedWord)
        {
            var anagrams = _anagramSolverService.FindSingleWordAnagrams(orderedWord);

            anagrams.Count().ShouldBe(4);
        }

        [TestCase("veidas")]
        public void GetUniqueAnagrams_GivenSingleInput_ReturnsFourUniqueAnagramsOfWord(string singleWord)
        {
            var uniqueAnagrams = _anagramSolverService.GetUniqueAnagrams(singleWord);

            uniqueAnagrams.ShouldBeUnique();
            uniqueAnagrams.Count().ShouldBe(4);
        }

        [TestCase("geras veidas visma")]
        public void GetUniqueAnagrams_GivenMultipleWordInput_ReturnsSixUniqueAnagrams(string multipleWord)
        {
            var orderedSecondWord = "aimsv";

            Fixture fixture = new();

            fixture.Customize<Word>(w => w.With(p => p.Value, "savim").With(p => p.OrderedValue, orderedSecondWord));
            var firstWord = fixture.Create<Word>();

            fixture.Customize<Word>(w => w.With(p => p.Value, "visam").With(p => p.OrderedValue, orderedSecondWord));
            var secondWord = fixture.Create<Word>();

            fixture.Customize<Word>(w => w.With(p => p.Value, "visam").With(p => p.OrderedValue, orderedSecondWord));
            var thirdWord = fixture.Create<Word>();

            HashSet<Word> _words = new() { firstWord, secondWord, thirdWord };

            _dictionary.Add(orderedSecondWord, _words);

            _mockWordRepository = new Mock<IWordRepository>();
            _mockWordRepository.Setup(w => w.ReadAndGetDictionary()).Returns(_dictionary);

            var uniqueAnagrams = _anagramSolverService.GetUniqueAnagrams(multipleWord);

            uniqueAnagrams.ShouldBeUnique();
            uniqueAnagrams.Count().ShouldBe(6);
        }
    }
}
