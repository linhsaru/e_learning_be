using elearning.ContentService.Domain.Courses;
using SharedKernel.Common;
using System;
using System.Collections.Generic;

namespace elearning.ContentService.Domain.Lessons
{
    /// <summary>
    /// Bài học cụ thể
    /// </summary>
    public class Lesson : AggregateRoot<Guid>
    {
        /// <summary>
        /// ID của chương/mục chứa bài học này
        /// </summary>
        public Guid UnitId { get; set; }

        /// <summary>
        /// Tựa đề bài học (VD: Bài 1: Chào hỏi cơ bản)
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Thời lượng dự kiến hoàn thành bài học (tính theo phút)
        /// </summary>
        public int? DurationMinutes { get; set; }

        /// <summary>
        /// Thứ tự sắp xếp của bài học trong chương
        /// </summary>
        public required int OrderIndex { get; set; }

        /// <summary>
        /// Chương/mục chứa bài học
        /// </summary>
        public Unit Unit { get; set; } = null!;

        /// <summary>
        /// Danh sách các khối nội dung (LessonBlocks) cấu thành bài học
        /// </summary>
        public ICollection<LessonBlock> LessonBlocks { get; set; } = new List<LessonBlock>();

        /// <summary>
        /// Danh sách các từ vựng trọng tâm được giảng dạy trong bài học
        /// </summary>
        public ICollection<LessonVocabulary> LessonVocabularies { get; set; } = new List<LessonVocabulary>();

        /// <summary>
        /// Danh sách các cấu trúc ngữ pháp trọng tâm được giảng dạy trong bài học
        /// </summary>
        public ICollection<LessonGrammar> LessonGrammars { get; set; } = new List<LessonGrammar>();
    }
}
