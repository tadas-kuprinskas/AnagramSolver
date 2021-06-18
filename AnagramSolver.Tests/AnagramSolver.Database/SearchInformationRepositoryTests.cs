using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Contracts.Models;
using AnagramSolver.Repository;
using AutoFixture;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Tests.AnagramSolver.Database
{
    [TestFixture]
    public class SearchInformationRepositoryTests
    {
        private SearchInformationRepository _searchInformationRepository;
        private Settings _options;
        private SearchInformation _searchInformation;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _searchInformation = new()
            {
                UserIp = "1",
                Anagram = "parkakti",
                SearchedWord = "praktika",
                SearchTime = DateTime.Now
            };

            _fixture = new();

            _options = new() { ConnectionString = "Server=.;Database=Anagram_Solver;Trusted_Connection=True;", NumberOfAnagrams = 10 };

            SqlConnection _sqlConnection = new(_options.ConnectionString);

            var mockOptions = new Mock<IOptions<Settings>>();
            mockOptions.Setup(ap => ap.Value).Returns(_options);

            _searchInformationRepository = new(mockOptions.Object);
        }

        [Test]
        public void AddSearchInformation_GivenObject_ThrownNoExceptions()
        {
            Assert.DoesNotThrow(() => _searchInformationRepository.AddSearchInformation(_searchInformation));
        }

        [Test]
        public void AddSearchInformation_GivenObject_NumberOfItemsIncreases()
        {
            var numberBefore = _searchInformationRepository.ReturnSearchInformation().Count;

            var uniqueSearchInformation = _fixture.Create<SearchInformation>();

            _searchInformationRepository.AddSearchInformation(uniqueSearchInformation);

            var numberAfter = _searchInformationRepository.ReturnSearchInformation().Count;

            numberBefore.ShouldBeLessThan(numberAfter);
        }

        [Test]
        public void ReturnSearchInformation_GivenCorrectValues_ReturnsNotNullCorrectTypeResult()
        {
            var informationList = _searchInformationRepository.ReturnSearchInformation();

            informationList.ShouldNotBeNull();
            informationList.ShouldBeOfType<List<SearchInformation>>();
        }
    }
}
