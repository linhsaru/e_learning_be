using SharedKernel.Common;
using System;

namespace elearning.ContentService.Domain.MasterData.Entities
{
    /// <summary>
    /// Từ loại trong ngôn ngữ (Danh từ, Động từ, Tính từ...)
    /// </summary>
    public class PartOfSpeech : AuditableEntity<int>
    {
        /// <summary>
        /// Mã từ loại (VD: NOUN, VERB, ADJ)
        /// </summary>
        public required string Code { get; set; }

        /// <summary>
        /// Tên từ loại hiển thị (VD: Danh từ, Động từ)
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Tên viết tắt (VD: n., v., adj.)
        /// </summary>
        public string? ShortName { get; set; }

        /// <summary>
        /// Mô tả chi tiết cách dùng từ loại
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Thứ tự hiển thị
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Trạng thái hoạt động
        /// </summary>
        public bool IsActive { get; set; } = true;

        public PartOfSpeech() { }

        public static PartOfSpeech Create(string code, string? name, string? shortName, string? description, int orderIndex = 0, bool isActive = true)
        {
            var entity = new PartOfSpeech
            {
                Code = code,
                Name = name,
                ShortName = shortName,
                Description = description,
                OrderIndex = orderIndex,
                IsActive = isActive
            };
            entity.MarkAsCreated();
            return entity;
        }

        public void Update(string code, string? name, string? shortName, string? description, int orderIndex, bool isActive)
        {
            Code = code;
            Name = name;
            ShortName = shortName;
            Description = description;
            OrderIndex = orderIndex;
            IsActive = isActive;
            MarkAsUpdated();
        }
    }
}