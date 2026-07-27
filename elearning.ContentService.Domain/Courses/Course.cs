using elearning.ContentService.Domain.MasterData.Entities;
using SharedKernel.Common;
using System;
using System.Collections.Generic;

namespace elearning.ContentService.Domain.Courses
{
    /// <summary>
    /// Thực thể Khóa học
    /// </summary>
    public class Course : AggregateRoot<Guid>
    {
        /// <summary>
        /// ID của lộ trình học chứa khóa này (tùy chọn, null nếu là khóa học độc lập)
        /// </summary>
        public Guid? LearningPathId { get; set; }

        /// <summary>
        /// ID của cấp độ tương ứng với khóa học
        /// </summary>
        public Guid LevelId { get; set; }

        /// <summary>
        /// Tên khóa học (VD: Tiếng Trung Sơ Cấp 1)
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Đường dẫn ảnh đại diện của khóa học
        /// </summary>
        public string? ThumbnailUrl { get; set; }

        /// <summary>
        /// Mô tả chi tiết nội dung khóa học
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Lộ trình học chứa khóa này
        /// </summary>
        public LearningPath? LearningPath { get; set; }

        /// <summary>
        /// Cấp độ trình độ của khóa học
        /// </summary>
        public Level Level { get; set; } = null!;

        /// <summary>
        /// Danh sách các chương/mục bài học trong khóa
        /// </summary>
        public ICollection<Unit> Units { get; set; } = new List<Unit>();

        /// <summary>
        /// Danh sách các thẻ phân loại được gán cho khóa học
        /// </summary>
        public ICollection<CourseTag> CourseTags { get; set; } = new List<CourseTag>();
    }
}
