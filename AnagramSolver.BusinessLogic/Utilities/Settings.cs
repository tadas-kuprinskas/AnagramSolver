using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Utilities
{
    public static class Settings
    {
        public static IConfigurationBuilder ConfigurationBuilder { get; set; }

        static Settings()
        {
            GetConfigurations();
        }

        private static void GetConfigurations()
        {
            ConfigurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), @"../../../AnagramSolver.Console"))
            .AddJsonFile("appsettings.json");
        }
    }
}
