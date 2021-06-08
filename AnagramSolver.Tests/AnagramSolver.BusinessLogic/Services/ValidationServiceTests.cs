using AnagramSolver.BusinessLogic.Exceptions;
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
        private Settings _handlingOptions;
        private ValidationService _validationService;

        [SetUp]
        public void Setup()
        {
            _handlingOptions = new() { MinInputLength = 3 };
            var mockOptions = new Mock<IOptions<Settings>>();
            mockOptions.Setup(ap => ap.Value).Returns(_handlingOptions);

            _validationService = new(mockOptions.Object);
        }

        [TestCase("sula")]
        [TestCase("praktika")]
        public void ValidateInputLength_GivenValidLengthInputs_ThrowsNoException(string myWord)
        {
            Assert.DoesNotThrow(() => _validationService.ValidateInputLength(myWord));
        }

        [TestCase("as")]
        [TestCase("ir")]
        public void ValidateInputLength_GivenInvalidLengthInputs_ThrowsArgumentExceptionWithCorrectMessage(string myWord)
        {
            Assert.Throws<CustomException>(() => _validationService.ValidateInputLength(myWord)).Message.ShouldBe($"Input cannot be shorter than {_handlingOptions.MinInputLength} letters");
        }
    }
}
