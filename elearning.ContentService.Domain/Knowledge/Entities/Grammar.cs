using elearning.ContentService.Domain.Lessons;
using elearning.ContentService.Domain.MasterData.Entities;
using SharedKernel.Common;
using System;
using System.Collections.Generic;

namespace elearning.ContentService.Domain.Knowledge.Entities
{
    /// <summary>
    /// Thực thể Cấu trúc Ngữ pháp (Kho Tri Thức Ngôn Ngữ)
    /// </summary>
    public class Grammar : AggregateRoot<Guid>
    {
        /// <summary>
        /// ID của ngôn ngữ áp dụng cấu trúc ngữ pháp này
        /// </summary>
        public Guid LanguageId { get; set; }

        /// <summary>
        /// ID của cấp độ khó của ngữ pháp (VD: HSK3, B2)
        /// </summary>
        public Guid LevelId { get; set; }

        /// <summary>
        /// Tên hoặc mẫu câu cấu trúc (VD: Cấu trúc Shi... De 是...的, Subject + Verb + Object)
        /// </summary>
        public required string StructureName { get; set; }

        /// <summary>
        /// Phần giải thích chi tiết ý nghĩa và ngữ cảnh sử dụng cấu trúc
        /// </summary>
        public required string Explanation { get; set; }

        /// <summary>
        /// Ngôn ngữ sở hữu
        /// </summary>
        public Language Language { get; set; } = null!;

        /// <summary>
        /// Cấp độ trình độ của điểm ngữ pháp
        /// </summary>
        public Level Level { get; set; } = null!;

        /// <summary>
        /// Danh sách các câu ví dụ minh họa ngữ pháp
        /// </summary>
        public ICollection<GrammarExample> Examples { get; set; } = new List<GrammarExample>();

        /// <summary>
        /// Danh sách các thẻ phân loại gắn với ngữ pháp
        /// </summary>
        public ICollection<GrammarTag> GrammarTags { get; set; } = new List<GrammarTag>();

        /// <summary>
        /// Danh sách các bài học giảng dạy cấu trúc ngữ pháp này
        /// </summary>
        public ICollection<LessonGrammar> LessonGrammars { get; set; } = new List<LessonGrammar>();
    }
}
