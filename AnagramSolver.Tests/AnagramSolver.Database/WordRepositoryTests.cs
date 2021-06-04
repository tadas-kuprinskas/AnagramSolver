using AnagramSolver.Contracts.Models;
using AnagramSolver.Database;
using AutoFixture;
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
            WordRepository wordRepository = new();

            var dictionary = wordRepository.ReadAndGetDictionary();

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

            WordRepository wordRepository = new();
            wordRepository.AddWordToDictionary(dictionary, orderedWord, word, newWord.PartOfSpeech);

            dictionary.ShouldNotBeEmpty();
            dictionary.Count.ShouldBe(3);
        }
    }
}
