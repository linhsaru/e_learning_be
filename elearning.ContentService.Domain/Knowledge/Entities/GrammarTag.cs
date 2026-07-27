using elearning.ContentService.Domain.MasterData.Entities;
using System;

namespace elearning.ContentService.Domain.Knowledge.Entities
{
    /// <summary>
    /// Bảng liên kết Nhiều - Nhiều giữa Cấu trúc ngữ pháp và Thẻ phân loại
    /// </summary>
    public class GrammarTag
    {
        /// <summary>
        /// ID của Cấu trúc ngữ pháp
        /// </summary>
        public Guid GrammarId { get; set; }

        /// <summary>
        /// ID của Thẻ phân loại
        /// </summary>
        public Guid TagId { get; set; }

        /// <summary>
        /// Ngữ pháp liên kết
        /// </summary>
        public Grammar Grammar { get; set; } = null!;

        /// <summary>
        /// Thẻ phân loại liên kết
        /// </summary>
        public Tag Tag { get; set; } = null!;
    }
}
