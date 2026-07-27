using elearning.ContentService.Domain.Lessons;
using elearning.ContentService.Domain.MasterData.Entities;
using elearning.ContentService.Domain.Media;
using SharedKernel.Common;
using System;
using System.Collections.Generic;

namespace elearning.ContentService.Domain.Knowledge.Entities
{
    /// <summary>
    /// Thực thể Từ vựng (Kho Tri Thức Ngôn Ngữ)
    /// </summary>
    public class Vocabulary : AggregateRoot<Guid>
    {
        /// <summary>
        /// ID của ngôn ngữ thuộc về
        /// </summary>
        public Guid LanguageId { get; set; }

        /// <summary>
        /// ID của cấp độ khó tương ứng (HSK1, B1, Topik 2...)
        /// </summary>
        public Guid LevelId { get; set; }

        /// <summary>
        /// Từ gốc (VD: 你好, Hello, 안녕하세요)
        /// </summary>
        public required string Word { get; set; }

        /// <summary>
        /// Phiên âm chuẩn (VD: nǐ hǎo, IPA phonetic, Pinyin)
        /// </summary>
        public string? Phonetic { get; set; }

        /// <summary>
        /// Từ loại (Danh từ, Động từ, Tính từ...)
        /// </summary>
        public string? PartOfSpeech { get; set; }

        /// <summary>
        /// Định nghĩa / Dịch nghĩa chi tiết của từ
        /// </summary>
        public required string Meaning { get; set; }

        /// <summary>
        /// ID của file âm thanh phát âm chuẩn (tùy chọn)
        /// </summary>
        public Guid? AudioMediaId { get; set; }

        /// <summary>
        /// Âm Hán Việt (Áp dụng đặc thù cho Tiếng Trung/Hàn/Nhật tại VN, VD: NPHỤ HẢO)
        /// </summary>
        public string? SinoVietnamese { get; set; }

        /// <summary>
        /// Bộ thủ Hán tự (VD: 亻 nhân đứng)
        /// </summary>
        public string? Radical { get; set; }

        /// <summary>
        /// Tổng số nét viết của từ
        /// </summary>
        public int? StrokeCount { get; set; }

        /// <summary>
        /// Chuỗi JSON/SVG định nghĩa thứ tự các nét viết để vẽ động trên ứng dụng
        /// </summary>
        public string? StrokeOrderJson { get; set; }

        /// <summary>
        /// Ngôn ngữ sở hữu
        /// </summary>
        public Language Language { get; set; } = null!;

        /// <summary>
        /// Cấp độ khó của từ
        /// </summary>
        public Level Level { get; set; } = null!;

        /// <summary>
        /// File âm thanh phát âm chuẩn
        /// </summary>
        public MediaAsset? AudioMedia { get; set; }

        /// <summary>
        /// Danh sách các câu ví dụ minh họa cách dùng từ
        /// </summary>
        public ICollection<VocabularyExample> Examples { get; set; } = new List<VocabularyExample>();

        /// <summary>
        /// Danh sách thẻ chủ đề phân loại từ vựng
        /// </summary>
        public ICollection<VocabularyTag> VocabularyTags { get; set; } = new List<VocabularyTag>();

        /// <summary>
        /// Danh sách các bài học có chứa từ vựng này
        /// </summary>
        public ICollection<LessonVocabulary> LessonVocabularies { get; set; } = new List<LessonVocabulary>();
    }
}
