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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Tests.AnagramSolver.BusinessLogic.Services
{
    [TestFixture]
    public class AnagramClientServiceTests
    {
        private Settings _handlingOptions;
        private AnagramClientService _anagramClientService;

        [SetUp]
        public void Setup()
        {
            _handlingOptions = new() { WordUri = "https://localhost:44379/Word/Anagrams?myWord=" };
            var mockOptions = new Mock<IOptions<Settings>>();
            mockOptions.Setup(ap => ap.Value).Returns(_handlingOptions);

            var mockHttpClient = new Mock<IHttpClientFactory>();

            var mockValidationService = new Mock<IValidationService>();

            _anagramClientService = new(mockOptions.Object, mockValidationService.Object, mockHttpClient.Object);
        }

        [TestCase("veidas")]
        public void SendGetAnagramsRequest_GivenWord_ReturnsCorrectAmountOfAnagrams(string myWord)
        {
            var wordList = _anagramClientService.SendGetAnagramsRequestAsync(myWord).Result;

            wordList.Count().ShouldBe(3);
        }

        [TestCase("sula")]
        public async Task SendGetAnagramsRequest_GivenWord_ReturnsCorrectTypeResult(string myWord)
        {
            var wordList = await _anagramClientService.SendGetAnagramsRequestAsync(myWord);

            wordList.ShouldNotBeNull();
            wordList.ShouldBeOfType<List<string>>();
        }
    }
}
