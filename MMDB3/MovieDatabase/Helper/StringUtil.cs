﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMDB.MovieDatabase.Helper
{
    public static class StringUtil
    {
        public static bool Contains(this string str, string str2, bool ignoreCase)
        {
            str = (ignoreCase) ? str.ToLower() : str;
            str2 = (ignoreCase) ? str2.ToLower() : str2;
            return str.Contains(str2);
        }
    }
}
