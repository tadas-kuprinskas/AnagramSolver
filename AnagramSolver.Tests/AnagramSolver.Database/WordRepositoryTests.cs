using AnagramSolver.BusinessLogic.Utilities;
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

        [SetUp]
        public void Setup()
        {
            _options = new() { FilePath = "AnagramSolver.Contracts/Data/zodynas.txt" };

            var mockOptions = new Mock<IOptions<Settings>>();
            mockOptions.Setup(ap => ap.Value).Returns(_options);

            _wordRepository = new(mockOptions.Object);
        }

        private static Dictionary<string, HashSet<Word>> GetDictionary()
        {
            var orderedWord = "adeisv";
            var orderedSecondWord = "aegrs";

            var fixture = new Fixture();

            fixture.Customize<Word>(w => w.With(p => p.Value, "dievas").With(p => p.OrderedValue, orderedWord));
            var firstWord = fixture.Create<Word>();

            fixture.Customize<Word>(w => w.With(p => p.Value, "vedasi").With(p => p.OrderedValue, orderedWord));
            var secondWord = fixture.Create<Word>();

            fixture.Customize<Word>(w => w.With(p => p.Value, "garse").With(p => p.OrderedValue, orderedSecondWord));
            var thirdWord = fixture.Create<Word>();

            fixture.Customize<Word>(w => w.With(p => p.Value, "serga").With(p => p.OrderedValue, orderedSecondWord));
            var fourthWord = fixture.Create<Word>();

            HashSet<Word> firstHashSet = new() { firstWord, secondWord };
            HashSet<Word> secondHashSet = new() { thirdWord, fourthWord };

            Dictionary<string, HashSet<Word>> dictionary = new() 
            {
                { orderedWord, firstHashSet },
                { orderedSecondWord, secondHashSet }
            };

            return dictionary;
        }

        [Test]
        public void ReadAndGetDictionary_GivenCorrectValuesInMethod_ReturnsCorrectTypeNotEmptyDictionary()
        {
            var dictionary = _wordRepository.ReadAndGetDictionary();

            dictionary.ShouldBeOfType<Dictionary<string, HashSet<Word>>>();
            dictionary.ShouldNotBeEmpty();
        }

        [Test]
        public void AddWordToDictionary_AddingNewValue_DictionaryCountIncreases()
        {
            var dictionary = GetDictionary();
            var orderedWord = "aimsv";
            var word = "visam";

            Word newWord = new()
            {
                Value = "savim",
                OrderedValue = orderedWord,
                PartOfSpeech = "įv"
            };

            WordRepository.AddWordToDictionary(dictionary, orderedWord, word, newWord.PartOfSpeech);

            dictionary.ShouldNotBeEmpty();
            dictionary.Count.ShouldBe(3);
        }
    }
}
