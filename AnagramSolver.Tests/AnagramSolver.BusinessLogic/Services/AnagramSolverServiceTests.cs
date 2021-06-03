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
        [TestCase("adeisv")]
        public void FindSingleWordAnagrams_GivenSameValueWords_ReturnsUniqueAnagrams(string orderedWord)
        {
            Word firstWord = new()
            {
                Value = "dievas",
                OrderedValue = orderedWord,
                PartOfSpeech = "dkt"
            };

            Word secondWord = new()
            {
                Value = "dievas",
                OrderedValue = orderedWord,
                PartOfSpeech = "dkt"
            };

            Word thirdWord = new()
            {
                Value = "vedasi",
                OrderedValue = orderedWord,
                PartOfSpeech = "vksm"
            };

            Word fourthWord = new()
            {
                Value = "vedasi",
                OrderedValue = orderedWord,
                PartOfSpeech = "vksm"
            };

            HashSet<Word> words = new() { firstWord, secondWord, thirdWord, fourthWord };

            Dictionary<string, HashSet<Word>> dictionary = new();
            dictionary.Add(orderedWord, words);

            var mockWordRepository = new Mock<IWordRepository>();
            mockWordRepository.Setup(w => w.ReadAndGetDictionary()).Returns(dictionary);

            var mockValidationService = new Mock<IValidationService>();

            WordHandlingOptions wordHandlingOptions = new() { NumberOfAnagrams = 10 };
            var mockOptions = new Mock<IOptions<WordHandlingOptions>>();

            mockOptions.Setup(ap => ap.Value).Returns(wordHandlingOptions);

            var anagramSolverService = new AnagramSolverService(mockWordRepository.Object, mockValidationService.Object, mockOptions.Object);

            var anagrams = anagramSolverService.FindSingleWordAnagrams(orderedWord);

            anagrams.Count().ShouldBe(2);
        }

        [TestCase("veidas")]
        public void GetUniqueAnagrams_GivenSingleInput_ReturnsUniqueAnagrams(string singleWord)
        {
            var fixture = new Fixture();
            var orderedWord = "adeisv";

            fixture.Customize<Word>(w => w.With(p => p.Value, "dievas").With(p => p.OrderedValue, orderedWord));            
            var firstWord = fixture.Create<Word>();

            fixture.Customize<Word>(w => w.With(p => p.Value, "vedasi").With(p => p.OrderedValue, orderedWord));
            var secondWord = fixture.Create<Word>();

            fixture.Customize<Word>(w => w.With(p => p.Value, "vedasi").With(p => p.OrderedValue, orderedWord));
            var thirdWord = fixture.Create<Word>();

            fixture.Customize<Word>(w => w.With(p => p.Value, "dievas").With(p => p.OrderedValue, orderedWord));
            var fourthWord = fixture.Create<Word>();

            HashSet<Word> words = new() { firstWord, secondWord, thirdWord, fourthWord };

            Dictionary<string, HashSet<Word>> dictionary = new();
            dictionary.Add(orderedWord, words);

            var mockWordRepository = new Mock<IWordRepository>();
            mockWordRepository.Setup(w => w.ReadAndGetDictionary()).Returns(dictionary);

            var mockValidationService = new Mock<IValidationService>();

            WordHandlingOptions wordHandlingOptions = new() { NumberOfAnagrams = 10 };
            var mockOptions = new Mock<IOptions<WordHandlingOptions>>();

            mockOptions.Setup(ap => ap.Value).Returns(wordHandlingOptions);

            var anagramSolverService = new AnagramSolverService(mockWordRepository.Object, mockValidationService.Object, mockOptions.Object);

            var uniqueAnagrams = anagramSolverService.GetUniqueAnagrams(singleWord);

            uniqueAnagrams.Count().ShouldBe(2);
        }

        [TestCase("geras veidas")]
        public void GetUniqueAnagrams_GivenMultipleWordInput_ReturnsUniqueAnagrams(string multipleWord)
        {
            var fixture = new Fixture();
            var orderedWord = "adeisv";
            var orderedSecondWord = "aegrs";

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

            Dictionary<string, HashSet<Word>> dictionary = new();
            dictionary.Add(orderedWord, words);
            dictionary.Add(orderedSecondWord, words);

            var mockWordRepository = new Mock<IWordRepository>();
            mockWordRepository.Setup(w => w.ReadAndGetDictionary()).Returns(dictionary);

            var mockValidationService = new Mock<IValidationService>();

            WordHandlingOptions wordHandlingOptions = new() { NumberOfAnagrams = 10 };
            var mockOptions = new Mock<IOptions<WordHandlingOptions>>();

            mockOptions.Setup(ap => ap.Value).Returns(wordHandlingOptions);

            var anagramSolverService = new AnagramSolverService(mockWordRepository.Object, mockValidationService.Object, mockOptions.Object);

            var uniqueAnagrams = anagramSolverService.GetUniqueAnagrams(multipleWord);

            uniqueAnagrams.Count().ShouldBe(4);
        }
    }
}
