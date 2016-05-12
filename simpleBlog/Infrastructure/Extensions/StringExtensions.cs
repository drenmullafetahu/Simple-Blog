using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace authcontroller.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string Slugify(this string that)
        {
            that = Regex.Replace(that, @"[^a-zA-Z-0-9\s]", "");
            that = that.ToLower();
            that = Regex.Replace(that, @"\s", "-");
            return that;
        }
    }
}