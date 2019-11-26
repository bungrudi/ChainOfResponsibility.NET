using System;
using System.Collections.Generic;
using System.Text;

namespace BaseClasses
{
    public static class StringExt
    {
        public static String AddRandom(this String str, int len)
        {
            Random rnd = new Random();
            for (var i = 0; i < len; i++)
            {
                str += ((char)(rnd.Next(1, 26) + 64)).ToString();
            }

            return str;
        }
    }
}
