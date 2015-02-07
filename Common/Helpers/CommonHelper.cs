using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Helpers
{
    public class CommonHelper
    {
        public static String RegExReplace(String inputString, String replaceValue)
        {
            String pattern = @"[\r|\n|\t]";

            // Specify your replace string value here.

            inputString = Regex.Replace(inputString, pattern, replaceValue);

            return inputString;
        }
    }
}
