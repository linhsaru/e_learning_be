using elearning.ContentService.Domain.Media;
using SharedKernel.Common;
using System;

namespace elearning.ContentService.Domain.Knowledge.Entities
{
    /// <summary>
    /// Câu ví dụ minh họa cách dùng cho Từ vựng
    /// </summary>
    public class VocabularyExample : BaseEntity<Guid>
    {
        /// <summary>
        /// ID của từ vựng sở hữu câu ví dụ này
        /// </summary>
        public Guid VocabularyId { get; set; }

        /// <summary>
        /// Câu ví dụ bằng ngôn ngữ gốc (VD: 你好吗？)
        /// </summary>
        public required string Sentence { get; set; }

        /// <summary>
        /// Bản dịch nghĩa của câu ví dụ (VD: Bạn khỏe không?)
        /// </summary>
        public required string Translation { get; set; }

        /// <summary>
        /// Phiên âm cho cả câu ví dụ (VD: Nǐ hǎo ma?)
        /// </summary>
        public string? Phonetic { get; set; }

        /// <summary>
        /// ID của file phát âm âm thanh mẫu cho cả câu ví dụ (tùy chọn)
        /// </summary>
        public Guid? AudioMediaId { get; set; }

        /// <summary>
        /// Từ vựng sở hữu
        /// </summary>
        public Vocabulary Vocabulary { get; set; } = null!;

        /// <summary>
        /// File âm thanh phát âm câu mẫu
        /// </summary>
        public MediaAsset? AudioMedia { get; set; }
    }
}
