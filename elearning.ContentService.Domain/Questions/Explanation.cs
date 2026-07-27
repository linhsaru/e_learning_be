using elearning.ContentService.Domain.Media;
using SharedKernel.Common;
using System;

namespace elearning.ContentService.Domain.Questions
{
    /// <summary>
    /// Lời giải thích đáp án chi tiết cho Câu hỏi
    /// </summary>
    public class Explanation : BaseEntity<Guid>
    {
        /// <summary>
        /// ID của câu hỏi sở hữu phần giải thích này
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// ID của file hình ảnh/video/audio minh họa cho lời giải thích (tùy chọn)
        /// </summary>
        public Guid? MediaId { get; set; }

        /// <summary>
        /// Văn bản giải thích lý do vì sao đáp án đó đúng hoặc sai
        /// </summary>
        public required string ExplanationText { get; set; }

        /// <summary>
        /// Câu hỏi sở hữu
        /// </summary>
        public Question Question { get; set; } = null!;

        /// <summary>
        /// File đa phương tiện giải thích đính kèm
        /// </summary>
        public MediaAsset? Media { get; set; }
    }
}
