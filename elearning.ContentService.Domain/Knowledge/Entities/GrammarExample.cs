using elearning.ContentService.Domain.Media;
using SharedKernel.Common;
using System;

namespace elearning.ContentService.Domain.Knowledge.Entities
{
    /// <summary>
    /// Câu ví dụ minh họa cấu trúc Ngữ pháp
    /// </summary>
    public class GrammarExample : BaseEntity<Guid>
    {
        /// <summary>
        /// ID của cấu trúc ngữ pháp sở hữu
        /// </summary>
        public Guid GrammarId { get; set; }

        /// <summary>
        /// Câu ví dụ bằng ngôn ngữ gốc chứa mẫu ngữ pháp
        /// </summary>
        public required string Sentence { get; set; }

        /// <summary>
        /// Bản dịch nghĩa của câu ví dụ
        /// </summary>
        public required string Translation { get; set; }

        /// <summary>
        /// ID của file âm thanh phát âm câu ví dụ mẫu (tùy chọn)
        /// </summary>
        public Guid? AudioMediaId { get; set; }

        /// <summary>
        /// Cấu trúc ngữ pháp sở hữu
        /// </summary>
        public Grammar Grammar { get; set; } = null!;

        /// <summary>
        /// File âm thanh đọc câu ví dụ
        /// </summary>
        public MediaAsset? AudioMedia { get; set; }
    }
}
