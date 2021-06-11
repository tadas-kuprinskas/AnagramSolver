using AnagramSolver.BusinessLogic.StaticHelpers;
using AnagramSolver.BusinessLogic.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileDownloadController : Controller
    {
        private readonly BusinessLogic.Utilities.Settings _options;

        public FileDownloadController(IOptions<Settings> options)
        {
            _options = options.Value;
        }

        [HttpGet("Dictionary")]
        public FileContentResult DownloadDictionary()
        {
            var filePath = PathGetting.GetFilePath("AnagramSolver\\" + _options.FilePath);

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filePath);
        }
    }
}
