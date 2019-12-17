using System;
using System.Collections.Generic;
using System.Text;

namespace Argentex.Core.Service.Identity
{
    public class SanitizeForwardSlash
    {
        public const string Old = "/";
        public const string New = "@$@";
    }

    public class SanitizeDoubleEqual
    {
        public const string Old = "==";
        public const string New = "@-@";
    }

    public class SanitizePlus
    {
        public const string Old = "+";
        public const string New = "@*@";
    }
}
