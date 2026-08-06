using elearning.ContentService.Domain.Knowledge.Entities;
using SharedKernel.Common;
using System;
using System.Collections.Generic;

namespace elearning.ContentService.Domain.MasterData.Entities
{
    /// <summary>
    /// Thực thể Ngôn ngữ (Master Data)
    /// </summary>
    public class Language : AuditableEntity<Guid>
    {
        /// <summary>
        /// Mã ngôn ngữ chuẩn ISO (VD: zh, en, vi, ko, ja)
        /// </summary>
        public required string Code { get; set; }

        /// <summary>
        /// Tên ngôn ngữ hiển thị (VD: Tiếng Trung, English, Tiếng Việt)
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Thứ tự hiển thị
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Danh sách các cấp độ học thuộc ngôn ngữ này
        /// </summary>
        public ICollection<Level> Levels { get; set; } = new List<Level>();

        /// <summary>
        /// Danh sách các từ vựng thuộc ngôn ngữ này
        /// </summary>
        public ICollection<Vocabulary> Vocabularies { get; set; } = new List<Vocabulary>();

        /// <summary>
        /// Danh sách các cấu trúc ngữ pháp thuộc ngôn ngữ này
        /// </summary>
        public ICollection<Grammar> Grammars { get; set; } = new List<Grammar>();

        public Language() { }

        public static Language Create(string code, string name, int orderIndex = 0)
        {
            var entity = new Language
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                OrderIndex = orderIndex
            };
            entity.MarkAsCreated();
            return entity;
        }

        public void Update(string code, string name, int orderIndex)
        {
            Code = code;
            Name = name;
            OrderIndex = orderIndex;
            MarkAsUpdated();
        }
    }
}
