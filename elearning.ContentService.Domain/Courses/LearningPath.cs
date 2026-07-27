using elearning.ContentService.Domain.MasterData.Entities;
using SharedKernel.Common;
using System;
using System.Collections.Generic;

namespace elearning.ContentService.Domain.Courses
{
    /// <summary>
    /// Lộ trình học (Tập hợp gồm nhiều khóa học theo tiến trình)
    /// </summary>
    public class LearningPath : AuditableEntity<Guid>
    {
        /// <summary>
        /// Tên lộ trình (VD: Lộ trình chinh phục HSK 4)
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Mô tả tổng quan về lộ trình học
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// ID của cấp độ mục tiêu đầu ra của lộ trình
        /// </summary>
        public Guid TargetLevelId { get; set; }

        /// <summary>
        /// Cấp độ mục tiêu đầu ra
        /// </summary>
        public Level TargetLevel { get; set; } = null!;

        /// <summary>
        /// Danh sách các khóa học nằm trong lộ trình này
        /// </summary>
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
