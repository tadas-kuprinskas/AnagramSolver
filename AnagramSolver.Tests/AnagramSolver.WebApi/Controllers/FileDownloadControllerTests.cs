using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
    public class FileDownloadControllerTests
    {
        private Settings _handlingOptions;
        private FileDownloadController _fileDownloadController;

        [SetUp]
        public void Setup()
        {
            _handlingOptions = new() { FilePath = "AnagramSolver.Contracts/Data/zodynas.txt" };
            var mockOptions = new Mock<IOptions<Settings>>();
            mockOptions.Setup(ap => ap.Value).Returns(_handlingOptions);

            _fileDownloadController = new(mockOptions.Object);
        }

        [Test]
        public void DownloadDictionary_GivenCorrectValues_ReturnsCorrectTypeResult()
        {
            var file = _fileDownloadController.DownloadDictionary();

            file.ShouldNotBeNull();
            file.ShouldBeOfType<FileContentResult>();
        }

        [Test]
        public void DownloadDictionary_GivenCorrectValues_ReturnsCorrectName()
        {
            var file = _fileDownloadController.DownloadDictionary();
            var fileName = file.FileDownloadName;

            file.ContentType.ShouldBe("application/octet-stream");
        }
    }
}
