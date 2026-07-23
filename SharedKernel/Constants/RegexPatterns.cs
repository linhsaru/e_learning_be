using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Constants
{
    public static class RegexPatterns
    {
        public const string Email = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        public const string PhoneNumber = @"^\d{10,11}$";
        public const string UserName = @"^[a-zA-Z0-9_]+$";
        public const string Url = @"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$";
        public const string Guid =
            @"^[0-9a-fA-F]{8}\-
            [0-9a-fA-F]{4}\-
            [0-9a-fA-F]{4}\-
            [0-9a-fA-F]{4}\-
            [0-9a-fA-F]{12}$";
    }
}
