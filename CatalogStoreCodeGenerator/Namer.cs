using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanizer;

namespace CatalogStoreCodeGenerator
{
    /// <summary>
    /// Wraps the underlying engine used for deriving type and property names from the table and column names.
    /// </summary>
    /// <remarks>Currently using the Humanizer package.</remarks>
    public static class Namer
    {
        private static TextInfo _textInfo = new CultureInfo("en-US", useUserOverride: false).TextInfo;

        public static string GetPascalCaseSingular(string str)
        {
            string[] typeNameParts = str.Split('.', '_');
            for (int i = 0; i < typeNameParts.Length; i++)
            {
                // Whitelist any string we know the Humanizer sigularization engine can't deal with.
                //
                if (typeNameParts[i] == "sys")
                {
                    continue;
                }

                typeNameParts[i] = typeNameParts[i].Singularize(inputIsKnownToBePlural: false);
            }

            string newStr = string.Join("_", typeNameParts);
            return newStr.Pascalize();
        }

        public static string GetCamelCaseSingular(string str)
        {
            string[] typeNameParts = str.Split('.', '_');
            for (int i = 0; i < typeNameParts.Length; i++)
            {
                // Whitelist any string we know the Humanizer sigularization engine can't deal with.
                //
                if (typeNameParts[i] == "sys")
                {
                    continue;
                }

                typeNameParts[i] = typeNameParts[i].Singularize(inputIsKnownToBePlural: false);
            }

            string newStr = string.Join("_", typeNameParts);
            return newStr.Camelize();
        }

        public static string GetPascalCasePlural(string str)
        {
            string[] typeNameParts = str.Split('.', '_');
            string newStr = string.Join("_", typeNameParts);
            return newStr.Pascalize();
        }

        public static string GetCamelCaseSingularPlural(string str)
        {
            string[] typeNameParts = str.Split('.', '_');
            string newStr = string.Join("_", typeNameParts);
            return newStr.Camelize();
        }
    }
}
