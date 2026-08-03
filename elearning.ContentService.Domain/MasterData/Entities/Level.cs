using elearning.ContentService.Domain.Courses;
using elearning.ContentService.Domain.Knowledge.Entities;
using elearning.ContentService.Domain.Questions.Entities;
using SharedKernel.Common;
using System;
using System.Collections.Generic;

namespace elearning.ContentService.Domain.MasterData.Entities
{
    /// <summary>
    /// Cấp độ trình độ học (CEFR, HSK, TOPIK, JLPT...)
    /// </summary>
    public class Level : BaseEntity<Guid>
    {
        /// <summary>
        /// Mã định danh cấp độ (VD: HSK4, B2, TOPIK3)
        /// </summary>
        public required string Code { get; set; }

        /// <summary>
        /// Tên hiển thị của cấp độ (VD: HSK Cấp 4, Trung cấp 2)
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Thứ tự sắp xếp độ khó từ thấp đến cao
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Mã ID ngôn ngữ sở hữu cấp độ này
        /// </summary>
        public Guid LanguageId { get; set; }

        /// <summary>
        /// Ngôn ngữ sở hữu
        /// </summary>
        public Language Language { get; set; } = null!;

        /// <summary>
        /// Danh sách các khóa học yêu cầu/đạt được cấp độ này
        /// </summary>
        public ICollection<Course> Courses { get; set; } = new List<Course>();

        /// <summary>
        /// Danh sách các lộ trình học hướng tới cấp độ này
        /// </summary>
        public ICollection<LearningPath> LearningPaths { get; set; } = new List<LearningPath>();

        /// <summary>
        /// Danh sách các tập câu hỏi phù hợp với cấp độ này
        /// </summary>
        public ICollection<QuestionSet> QuestionSets { get; set; } = new List<QuestionSet>();

        /// <summary>
        /// Danh sách từ vựng thuộc cấp độ này
        /// </summary>
        public ICollection<Vocabulary> Vocabularies { get; set; } = new List<Vocabulary>();

        /// <summary>
        /// Danh sách ngữ pháp thuộc cấp độ này
        /// </summary>
        public ICollection<Grammar> Grammars { get; set; } = new List<Grammar>();
    }
}
