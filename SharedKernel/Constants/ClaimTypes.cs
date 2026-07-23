using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Constants
{
    /// <summary>
    /// Mục đích: Khi dùng JWT thì sẽ có các claim, claim là các thông tin về người dùng, ví dụ: tên, email, role, ...
    /// </summary>
    public static class ClaimTypes
    {
        public const string UserId = "user_id";
        public const string UserName = "user_name";
        public const string Email = "email";
        public const string Role = "role";
        public const string Permission = "permission";
        public const string TenantId = "tenant_id";
    }
}
