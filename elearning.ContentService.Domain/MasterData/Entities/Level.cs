using SharedKernel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace elearning.ContentService.Domain.MasterData.Entities
{
    /// <summary>
    /// Cấp độ học
    /// </summary>
    public class Level : BaseEntity<Guid>
    {
        public required string Code { get; set; } //Mã định danh cấp độ: HSK, Topik,...
        public string? Name { get; set; } //Tên hiển thị
        public int OrderIndex { get; set; } //Thứ tự sắp xếp độ khó từ thấp đến cao

        public Guid LanguageId { get; set; }

        public Language Language { get; private set; } = null!;
    }
}
