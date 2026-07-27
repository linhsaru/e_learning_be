using elearning.ContentService.Domain.Knowledge.Entities;
using SharedKernel.Common;
using System;
using System.Collections.Generic;

namespace elearning.ContentService.Domain.MasterData.Entities
{
    /// <summary>
    /// Thực thể Ngôn ngữ (Master Data)
    /// </summary>
    public class Language : BaseEntity<Guid>
    {
        /// <summary>
        /// Mã ngôn ngữ chuẩn ISO (VD: zh, en, vi, ko, ja)
        /// </summary>
        public required string Code { get; set; }

        /// <summary>
        /// Tên ngôn ngữ hiển thị (VD: Tiếng Trung, English, Tiếng Việt)
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Danh sách các cấp độ học thuộc ngôn ngữ này
        /// </summary>
        public ICollection<Level> Levels { get; set; } = new List<Level>();

        /// <summary>
        /// Danh sách các từ vựng thuộc ngôn ngữ này
        /// </summary>
        public ICollection<Vocabulary> Vocabularies { get; set; } = new List<Vocabulary>();

        /// <summary>
        /// Danh sách các cấu trúc ngữ pháp thuộc ngôn ngữ này
        /// </summary>
        public ICollection<Grammar> Grammars { get; set; } = new List<Grammar>();
    }
}
