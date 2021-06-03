using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.BusinessLogic.Utilities;
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
    public class ValidationServiceTests
    {
        [TestCase("sula")]
        [TestCase("praktika")]
        public void ValidateInputLength_GivenValidLengthInputs_ThrowsNoException(string myWord)
        {
            WordHandlingOptions wordHandlingOptions = new() { MinInputLength = 3 };
            var mockOptions = new Mock<IOptions<WordHandlingOptions>>();
            mockOptions.Setup(ap => ap.Value).Returns(wordHandlingOptions);

            ValidationService validationService = new(mockOptions.Object);

            Assert.DoesNotThrow(() => validationService.ValidateInputLength(myWord));
        }

        [TestCase("as")]
        [TestCase("ir")]
        public void ValidateInputLength_GivenInvalidLengthInputs_ThrowsArgumentExceptionWithCorrectMessage(string myWord)
        {
            WordHandlingOptions wordHandlingOptions = new() { MinInputLength = 3 };
            var mockOptions = new Mock<IOptions<WordHandlingOptions>>();
            mockOptions.Setup(ap => ap.Value).Returns(wordHandlingOptions);

            ValidationService validationService = new(mockOptions.Object);

            Assert.Throws<ArgumentException>(() => validationService.ValidateInputLength(myWord)).Message.ShouldBe($"Input cannot be shorter than {wordHandlingOptions.MinInputLength} letters");
        }

        //[TestCase("rope")]
        //public void ValidateSingleWordAnagrams_GivenIEnumerableOfNull_ThrowsArgumentExceptionWithCorrectMessage(string myWord)
        //{
        //    IEnumerable<Word> words = null;

        //    WordHandlingOptions wordHandlingOptions = new() { MinInputLength = 3 };
        //    var mockOptions = new Mock<IOptions<WordHandlingOptions>>();

        //    mockOptions.Setup(ap => ap.Value).Returns(wordHandlingOptions);

        //    ValidationService validationService = new(mockOptions.Object);

        //    Assert.Throws<ArgumentException>(() => validationService.ValidateSingleWordAnagrams(words, myWord)).Message.ShouldBe($"There are no anagrams for your word \"{myWord}\"");
        //}

        //[Test]
        //public void ValidateSingleWordAnagrams_GivenValidIEnumerable_ThrowsNoExceptions()
        //{
        //    string myWord = "veidas";
        //    var fixture = new Fixture();

        //    fixture.Customize<Word>(w => w.With(p => p.Value, "dievas"));
        //    var firstWord = fixture.Create<Word>();

        //    fixture.Customize<Word>(w => w.With(p => p.Value, "vedasi"));
        //    var secondWord = fixture.Create<Word>();

        //    HashSet<Word> words = new() { firstWord, secondWord };

        //    WordHandlingOptions wordHandlingOptions = new() { MinInputLength = 3 };
        //    var mockOptions = new Mock<IOptions<WordHandlingOptions>>();

        //    mockOptions.Setup(ap => ap.Value).Returns(wordHandlingOptions);

        //    ValidationService validationService = new(mockOptions.Object);

        //    Assert.DoesNotThrow(() => validationService.ValidateSingleWordAnagrams(words, myWord));
        //}
    }
}
