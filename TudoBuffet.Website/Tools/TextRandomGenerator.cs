using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TudoBuffet.Website.Tools
{
    public class TextRandomGenerator
    {
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxy";

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
