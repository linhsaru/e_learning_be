using elearning.ContentService.Domain.Media;
using SharedKernel.Common;
using System;
using System.Diagnostics.CodeAnalysis;

namespace elearning.ContentService.Domain.Questions.Entities
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

        public Explanation() { }

        [SetsRequiredMembers]
        public Explanation(Guid id, Guid questionId, string explanationText, Guid? mediaId = null)
        {
            Id = id;
            QuestionId = questionId;
            ExplanationText = explanationText;
            MediaId = mediaId;
        }
    }
}
