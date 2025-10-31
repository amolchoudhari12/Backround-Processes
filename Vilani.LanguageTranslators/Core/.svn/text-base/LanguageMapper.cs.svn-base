using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vilani.LanguageTranslators
{
    public static class LanguageMapper
    {

        public static string ToLanguageStringCode(int languageID)
        {

            switch (languageID)
            {
                case 1:
                    return "en";
                case 2:
                    return "hi";

                default:
                    break;
            }
            return string.Empty;

        }

        public static int ToLanguageID(string language)
        {
            switch (language)
            {
                case "en":
                    return 1;

                case "hi":
                    return 2;

                default:
                    break;
            }
            return 0;
        }


    }
}
