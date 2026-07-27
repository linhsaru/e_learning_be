using SharedKernel.Common;
using System;

namespace elearning.ContentService.Domain.Questions
{
    /// <summary>
    /// Các lựa chọn phương án đáp án cho Câu hỏi
    /// </summary>
    public class Option : BaseEntity<Guid>
    {
        /// <summary>
        /// ID của câu hỏi sở hữu phương án này
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Nội dung phương án lựa chọn (VD: A. Hello, B. Goodbye)
        /// </summary>
        public required string Content { get; set; }

        /// <summary>
        /// Cờ xác định đây có phải là đáp án đúng hay không
        /// </summary>
        public bool IsCorrect { get; set; }

        /// <summary>
        /// Thứ tự xuất hiện của phương án (A, B, C, D...)
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Câu hỏi sở hữu
        /// </summary>
        public Question Question { get; set; } = null!;
    }
}
