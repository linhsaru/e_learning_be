using elearning.ContentService.Domain.Common.Enums;
using elearning.ContentService.Domain.MasterData.Entities;
using SharedKernel.Common;
using System;
using System.Collections.Generic;

namespace elearning.ContentService.Domain.Questions
{
    /// <summary>
    /// Tập hợp các câu hỏi (Bộ đề / Bộ câu hỏi, VD: Reading Part 1, HSK4 Listening Test)
    /// </summary>
    public class QuestionSet : AuditableEntity<Guid>
    {
        /// <summary>
        /// ID của cấp độ tương đương (tùy chọn, null nếu áp dụng đa cấp độ)
        /// </summary>
        public Guid? LevelId { get; set; }

        /// <summary>
        /// Tên tập hợp câu hỏi (VD: Reading Part 1, TOPIK I Practice Test 1)
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Kỹ năng đánh giá chính của tập câu hỏi (Listening, Reading, Writing, Speaking, Grammar, Vocabulary)
        /// </summary>
        public SkillType? SkillType { get; set; }

        /// <summary>
        /// Cấp độ tương ứng
        /// </summary>
        public Level? Level { get; set; }

        /// <summary>
        /// Danh sách các nhóm câu hỏi dùng chung đoạn văn/audio thuộc tập này
        /// </summary>
        public ICollection<QuestionGroup> QuestionGroups { get; set; } = new List<QuestionGroup>();

        /// <summary>
        /// Danh sách các câu hỏi đơn thuộc tập này
        /// </summary>
        public ICollection<Question> Questions { get; set; } = new List<Question>();

        /// <summary>
        /// Danh sách các đề thi có đưa tập câu hỏi này vào làm bài
        /// </summary>
        public ICollection<AssessmentQuestionSet> AssessmentQuestionSets { get; set; } = new List<AssessmentQuestionSet>();
    }
}
