using elearning.ContentService.Domain.Media;
using SharedKernel.Common;
using System;
using System.Collections.Generic;

namespace elearning.ContentService.Domain.Questions.Entities
{
    /// <summary>
    /// Nhóm câu hỏi (Dùng cho dạng bài dùng chung 1 đoạn văn đọc hiểu hoặc 1 file âm thanh nghe)
    /// </summary>
    public class QuestionGroup : AuditableEntity<Guid>
    {
        /// <summary>
        /// ID của tập hợp câu hỏi chứa nhóm này
        /// </summary>
        public Guid QuestionSetId { get; set; }

        /// <summary>
        /// ID của file âm thanh/hình ảnh dùng chung cho cả nhóm (tùy chọn)
        /// </summary>
        public Guid? SharedMediaId { get; set; }

        /// <summary>
        /// Đoạn văn bản đọc hiểu dùng chung cho các câu hỏi trong nhóm (tùy chọn)
        /// </summary>
        public string? SharedContent { get; set; }

        /// <summary>
        /// Tập hợp câu hỏi chứa nhóm này
        /// </summary>
        public QuestionSet QuestionSet { get; set; } = null!;

        /// <summary>
        /// File đa phương tiện dùng chung (Audio/Image)
        /// </summary>
        public MediaAsset? SharedMedia { get; set; }

        /// <summary>
        /// Danh sách các câu hỏi con thuộc nhóm dùng chung này
        /// </summary>
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
