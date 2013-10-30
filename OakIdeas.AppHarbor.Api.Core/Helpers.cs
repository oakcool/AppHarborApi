using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLWebUtility;

namespace OakIdeas.AppHarbor.Api.Core
{
    public class Helpers
    {
        /// <summary>
        /// Parses a query string into a dictionary collection
        /// </summary>
        /// <param name="queryString">the query string to parse</param>
        /// <returns>a dictionary collection of querystring items</returns>
        public static Dictionary<string, string> ParseQueryString(string queryString)
        {
            Dictionary<string, string> nameValueCollection = new Dictionary<string, string>();
            string[] items = queryString.Split('&');

            foreach (string item in items)
            {
                if (item.Contains("="))
                {
                    string[] nameValue = item.Split('=');
                    if (nameValue[0].Contains("?"))
                        nameValue[0] = nameValue[0].Replace("?", "");
                    nameValueCollection.Add(nameValue[0], WebUtility.UrlDecode(nameValue[1]));
                }
            }

            return nameValueCollection;
        }

        private static char[] HexUpperChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        private static char[] HexLowerChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

        /// <summary>
        /// Converts a specified character into its hexadecimal equivalent.
        /// </summary>
        /// <param name="character">Converts a specified character into its hexadecimal equivalent.</param>
        /// <returns>The hexadecimal representation of the specified character.</returns>
        public static string HexEscape(char character)
        {
            if (character > '\x00ff')
            {
                throw new ArgumentOutOfRangeException("character");
            }
            char[] to = new char[3];
            int pos = 0;
            EscapeAsciiChar(character, to, ref pos);
            return new string(to);
        }

        private static void EscapeAsciiChar(char ch, char[] to, ref int pos)
        {
            to[pos++] = '%';
            to[pos++] = HexUpperChars[(ch & 240) >> 4];
            to[pos++] = HexUpperChars[ch & '\x000f'];
        }
    }
}
