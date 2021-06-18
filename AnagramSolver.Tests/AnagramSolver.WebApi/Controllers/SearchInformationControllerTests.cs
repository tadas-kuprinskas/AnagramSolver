using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApi.Controllers;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Tests.AnagramSolver.WebApi.Controllers
{
    [TestFixture]
    public class SearchInformationControllerTests
    {
        private SearchInformationController _searchInformationController;

        [SetUp]
        public void Setup()
        {
            SearchInformation firstSearch = new()
            {
                UserIp = "1",
                Anagram = "parkakti",
                SearchedWord = "praktika",
                SearchTime = DateTime.Now
            };

            SearchInformation secondSearch = new()
            {
                UserIp = "1",
                Anagram = "veidas, vedasi",
                SearchedWord = "dievas",
                SearchTime = DateTime.Now
            };

            List<SearchInformation> infos = new() { firstSearch, secondSearch };

            Mock<ISearchInformationService> mockSearchInformationService = new();
            mockSearchInformationService.Setup(m => m.ReturnAllSearches()).Returns(infos);

            _searchInformationController = new(mockSearchInformationService.Object);
        }

        [Test]
        public void GetAllSearchHistory_GivenCorrectValues_ReturnsCorrectTypeResult()
        {
            var information = _searchInformationController.GetAllSearchHistory();

            information.FirstOrDefault().ShouldBeOfType<SearchInformation>();
        }

        [Test]
        public void GetAllSearchHistory_GivenCorrectValues_ReturnsCorrectCount()
        {
            var information = _searchInformationController.GetAllSearchHistory();

            information.Count().ShouldBe(2);
        }
    }
}
