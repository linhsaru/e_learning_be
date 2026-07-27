using elearning.ContentService.Domain.Courses;
using System;

namespace elearning.ContentService.Domain.MasterData.Entities
{
    /// <summary>
    /// Bảng liên kết Nhiều - Nhiều giữa Khóa học và Thẻ phân loại
    /// </summary>
    public class CourseTag
    {
        /// <summary>
        /// ID của Khóa học
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// ID của Thẻ phân loại
        /// </summary>
        public Guid TagId { get; set; }

        /// <summary>
        /// Khóa học liên kết
        /// </summary>
        public Course Course { get; set; } = null!;

        /// <summary>
        /// Thẻ phân loại liên kết
        /// </summary>
        public Tag Tag { get; set; } = null!;
    }
}
