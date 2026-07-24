using SharedKernel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace elearning.ContentService.Domain.MasterData.Entities
{
    /// <summary>
    /// Thẻ phân loại, chủ đề
    /// </summary>
    public class Tag : BaseEntity<Guid>
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public int Type { get; set; }
    }
}
