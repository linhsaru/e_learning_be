using elearning.ContentService.Domain.Lessons;
using SharedKernel.Common;
using System;
using System.Collections.Generic;

namespace elearning.ContentService.Domain.Courses
{
    /// <summary>
    /// Chương / Mục bài học trong Khóa học
    /// </summary>
    public class Unit : AggregateRoot<Guid>
    {
        /// <summary>
        /// ID của khóa học chứa chương này
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// ID của chương cha (tùy chọn, null nếu là chương cấp cao nhất, hỗ trợ chương phụ tự tham chiếu)
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Tên chương/mục (VD: Unit 1: Greetings)
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Thứ tự xuất hiện trong khóa học
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Khóa học chứa chương này
        /// </summary>
        public Course Course { get; set; } = null!;

        /// <summary>
        /// Chương cha (nếu có)
        /// </summary>
        public Unit? Parent { get; set; }

        /// <summary>
        /// Danh sách các chương con thuộc chương này
        /// </summary>
        public ICollection<Unit> SubUnits { get; set; } = new List<Unit>();

        /// <summary>
        /// Danh sách các bài học thuộc chương này
        /// </summary>
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}
