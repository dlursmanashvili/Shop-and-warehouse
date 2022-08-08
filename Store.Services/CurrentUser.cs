using System;
using System.Collections.Generic;

namespace Store.Services
{
    public static class CurrentUser
    {
        public static int UserId { get; internal set; }
        public static IEnumerable<short> Permissions { get; internal set; }
    }
}
