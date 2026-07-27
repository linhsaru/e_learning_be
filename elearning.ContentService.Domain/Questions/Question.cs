using elearning.ContentService.Domain.Common.Enums;
using SharedKernel.Common;
using System;
using System.Collections.Generic;

namespace elearning.ContentService.Domain.Questions
{
    /// <summary>
    /// Thực thể Câu hỏi đơn
    /// </summary>
    public class Question : AuditableEntity<Guid>
    {
        /// <summary>
        /// ID của tập hợp câu hỏi chứa câu hỏi này
        /// </summary>
        public Guid QuestionSetId { get; set; }

        /// <summary>
        /// ID của nhóm câu hỏi dùng chung (tùy chọn, null nếu là câu hỏi độc lập)
        /// </summary>
        public Guid? QuestionGroupId { get; set; }

        /// <summary>
        /// Dạng câu hỏi (SingleChoice, MultipleChoice, TrueFalse, FillInBlank, Matching, Ordering, Essay...)
        /// </summary>
        public QuestionType QuestionType { get; set; }

        /// <summary>
        /// Kỹ năng đánh giá cụ thể (Listening, Reading, Writing, Speaking...)
        /// </summary>
        public SkillType? SkillType { get; set; }

        /// <summary>
        /// Nội dung câu hỏi cụ thể
        /// </summary>
        public required string Content { get; set; }

        /// <summary>
        /// Gợi ý làm bài nếu học viên chọn sai (tùy chọn)
        /// </summary>
        public string? Hint { get; set; }

        /// <summary>
        /// Tập hợp câu hỏi sở hữu
        /// </summary>
        public QuestionSet QuestionSet { get; set; } = null!;

        /// <summary>
        /// Nhóm câu hỏi dùng chung (nếu thuộc một nhóm)
        /// </summary>
        public QuestionGroup? QuestionGroup { get; set; }

        /// <summary>
        /// Danh sách phương án lựa chọn đáp án
        /// </summary>
        public ICollection<Option> Options { get; set; } = new List<Option>();

        /// <summary>
        /// Danh sách phần lời giải thích chi tiết đáp án
        /// </summary>
        public ICollection<Explanation> Explanations { get; set; } = new List<Explanation>();

        /// <summary>
        /// Danh sách thẻ chủ đề phân loại gắn cho câu hỏi
        /// </summary>
        public ICollection<QuestionTag> QuestionTags { get; set; } = new List<QuestionTag>();
    }
}
