using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Review20180708.Function
{
    public static class StringExtension
    {
        public static bool IsNotNullOrTrimEmpty(this string v)
        {
            return v != null && v != "";
        }

        public static bool IsNullOrTrimEmpty(this string v)
        {
            return !IsNotNullOrTrimEmpty(v);
        }
    }

}