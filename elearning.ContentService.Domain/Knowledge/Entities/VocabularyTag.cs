using elearning.ContentService.Domain.MasterData.Entities;
using System;

namespace elearning.ContentService.Domain.Knowledge.Entities
{
    /// <summary>
    /// Bảng liên kết Nhiều - Nhiều giữa Từ vựng và Thẻ phân loại/Chủ đề
    /// </summary>
    public class VocabularyTag
    {
        /// <summary>
        /// ID của Từ vựng
        /// </summary>
        public Guid VocabularyId { get; set; }

        /// <summary>
        /// ID của Thẻ phân loại
        /// </summary>
        public Guid TagId { get; set; }

        /// <summary>
        /// Từ vựng liên kết
        /// </summary>
        public Vocabulary Vocabulary { get; set; } = null!;

        /// <summary>
        /// Thẻ phân loại liên kết
        /// </summary>
        public Tag Tag { get; set; } = null!;
    }
}
