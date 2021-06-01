using AnagramSolver.BusinessLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.StaticHelpers
{
    public static class ConfigurationValues
    {
        public static int GetNumberOfAnagrams()
        {          
            return int.Parse(Settings.ConfigurationBuilder.Build().GetSection("numberOfAnagrams").Value);
        }

        public static int GetMinInputLength()
        {
            return int.Parse(Settings.ConfigurationBuilder.Build().GetSection("minInputLength").Value);
        }
    }
}
