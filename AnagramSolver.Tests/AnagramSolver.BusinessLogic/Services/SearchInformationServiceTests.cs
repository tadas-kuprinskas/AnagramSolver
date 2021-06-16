using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AutoFixture;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Tests.AnagramSolver.BusinessLogic.Services
{
    [TestFixture]
    public class SearchInformationServiceTests
    {
        private SearchInformationService _searchInformationService;
        private Mock<ISearchInformationRepository> _mockSearchInformationRepository;
        private List<Word> _uniqueAnagrams;
        private string _myWord;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            _myWord = "praktika";

            _uniqueAnagrams = new() { new Word() { Id = 1, OrderedValue = "aaikkprt", PartOfSpeech = "vksm", Value = "parkakti" } };

            _mockSearchInformationRepository = new();
            var allSearches = fixture.CreateMany<SearchInformation>(4);
            _mockSearchInformationRepository.Setup(m => m.ReturnSearchInformation()).Returns(allSearches);

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var ipAddress = fixture.Create<IPAddress>();

            mockHttpContextAccessor.Setup(p => p.HttpContext.Connection.RemoteIpAddress).Returns(ipAddress);

            _searchInformationService = new(_mockSearchInformationRepository.Object, mockHttpContextAccessor.Object);
        }

        [Test]
        public void RecordSearchInformation_GivenCorrectValues_VerifyThatMethodIsExecuted()
        {
            _searchInformationService.RecordSearchInformation(_uniqueAnagrams, _myWord);
            _mockSearchInformationRepository.Verify(m => m.AddSearchInformation(It.IsAny<SearchInformation>()), Times.Once);
        }

        [Test]
        public void ReturnAllSearches_GivenAmountOfObjects_ReturnsCorrectAmountOfObjects()
        {
            var searchList = _searchInformationService.ReturnAllSearches();

            searchList.ShouldNotBeEmpty();
            searchList.ShouldNotBeNull();
            searchList.Count().ShouldBe(4);
        }

        [Test]
        public void ReturnAllSearches_GivenEmptyListIfNoSearchesWereMade_ReturnsEmptyCollection()
        {
            _mockSearchInformationRepository.Setup(m => m.ReturnSearchInformation()).Returns(Enumerable.Empty<SearchInformation>());

            var searchList = _searchInformationService.ReturnAllSearches();

            searchList.ShouldBeEmpty();
            searchList.Count().ShouldBe(0);
        }

        [Test]
        public void GetUserIp_GivenCorrectValues_ReturnsCorrectTypeResult()
        {
            var ip = _searchInformationService.GetUserIP();
            ip.ShouldNotBeNullOrEmpty();
            ip.ShouldBeOfType<string>();
        }
    }
}
