using elearning.ContentService.Domain.Knowledge.Entities;
using System;

namespace elearning.ContentService.Domain.Lessons
{
    /// <summary>
    /// Bảng liên kết Nhiều - Nhiều giữa Bài học và Cấu trúc ngữ pháp trọng tâm
    /// </summary>
    public class LessonGrammar
    {
        /// <summary>
        /// ID của Bài học
        /// </summary>
        public Guid LessonId { get; set; }

        /// <summary>
        /// ID của Ngữ pháp
        /// </summary>
        public Guid GrammarId { get; set; }

        /// <summary>
        /// Thứ tự xuất hiện của điểm ngữ pháp trong bài học
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Bài học liên kết
        /// </summary>
        public Lesson Lesson { get; set; } = null!;

        /// <summary>
        /// Cấu trúc ngữ pháp liên kết
        /// </summary>
        public Grammar Grammar { get; set; } = null!;
    }
}
