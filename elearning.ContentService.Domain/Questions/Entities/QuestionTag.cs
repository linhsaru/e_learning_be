using elearning.ContentService.Domain.MasterData.Entities;
using System;

namespace elearning.ContentService.Domain.Questions.Entities
{
    /// <summary>
    /// Bảng liên kết Nhiều - Nhiều giữa Câu hỏi và Thẻ phân loại/Chủ đề
    /// </summary>
    public class QuestionTag
    {
        /// <summary>
        /// ID của Câu hỏi
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// ID của Thẻ phân loại
        /// </summary>
        public Guid TagId { get; set; }

        /// <summary>
        /// Câu hỏi liên kết
        /// </summary>
        public Question Question { get; set; } = null!;

        /// <summary>
        /// Thẻ phân loại liên kết
        /// </summary>
        public Tag Tag { get; set; } = null!;
    }
}
