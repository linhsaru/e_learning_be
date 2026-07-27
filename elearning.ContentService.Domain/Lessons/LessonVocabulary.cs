using elearning.ContentService.Domain.Knowledge.Entities;
using System;

namespace elearning.ContentService.Domain.Lessons
{
    /// <summary>
    /// Bảng liên kết Nhiều - Nhiều giữa Bài học và Từ vựng trọng tâm
    /// </summary>
    public class LessonVocabulary
    {
        /// <summary>
        /// ID của Bài học
        /// </summary>
        public Guid LessonId { get; set; }

        /// <summary>
        /// ID của Từ vựng
        /// </summary>
        public Guid VocabularyId { get; set; }

        /// <summary>
        /// Thứ tự xuất hiện của từ vựng trong bài học
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Bài học liên kết
        /// </summary>
        public Lesson Lesson { get; set; } = null!;

        /// <summary>
        /// Từ vựng liên kết
        /// </summary>
        public Vocabulary Vocabulary { get; set; } = null!;
    }
}
