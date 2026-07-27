using elearning.ContentService.Domain.Common.Enums;
using SharedKernel.Common;
using System;
using System.Text.Json;

namespace elearning.ContentService.Domain.Lessons
{
    /// <summary>
    /// Khối nội dung trong Bài học (Text, Video, Audio, Image, Exercise...)
    /// </summary>
    public class LessonBlock : AggregateRoot<Guid>
    {
        /// <summary>
        /// ID của bài học chứa khối nội dung này
        /// </summary>
        public Guid LessonId { get; set; }

        /// <summary>
        /// Phân loại khối (Text = 1, Video = 2, Audio = 3, Image = 4, Exercise = 5...)
        /// </summary>
        public BlockType BlockType { get; set; }

        /// <summary>
        /// Dữ liệu nội dung chi tiết dạng JSON linh hoạt (VD: Video -> {"MediaId": "..."})
        /// </summary>
        public JsonDocument ContentPayload { get; set; } = default!;

        /// <summary>
        /// Thứ tự trình bày của khối từ trên xuống dưới trong bài học
        /// </summary>
        public required int OrderIndex { get; set; }

        /// <summary>
        /// Bài học chứa khối này
        /// </summary>
        public Lesson Lesson { get; set; } = null!;
    }
}
