using elearning.ContentService.Domain.Common.Enums;
using elearning.ContentService.Domain.Knowledge.Entities;
using elearning.ContentService.Domain.Questions;
using SharedKernel.Common;
using System;
using System.Collections.Generic;

namespace elearning.ContentService.Domain.MasterData.Entities
{
    /// <summary>
    /// Thẻ phân loại và chủ đề (Tag/Topic)
    /// </summary>
    public class Tag : BaseEntity<Guid>
    {
        /// <summary>
        /// Tên thẻ (VD: IT, Business, Daily Life)
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Chuỗi định dạng thân thiện với URL (VD: business-chinese)
        /// </summary>
        public required string Slug { get; set; }

        /// <summary>
        /// Phân loại thẻ (Topic, General Tag, Grammar Category...)
        /// </summary>
        public TagType Type { get; set; }

        /// <summary>
        /// Liên kết thẻ với các khóa học
        /// </summary>
        public ICollection<CourseTag> CourseTags { get; set; } = new List<CourseTag>();

        /// <summary>
        /// Liên kết thẻ với từ vựng
        /// </summary>
        public ICollection<VocabularyTag> VocabularyTags { get; set; } = new List<VocabularyTag>();

        /// <summary>
        /// Liên kết thẻ với ngữ pháp
        /// </summary>
        public ICollection<GrammarTag> GrammarTags { get; set; } = new List<GrammarTag>();

        /// <summary>
        /// Liên kết thẻ với câu hỏi
        /// </summary>
        public ICollection<QuestionTag> QuestionTags { get; set; } = new List<QuestionTag>();
    }
}
