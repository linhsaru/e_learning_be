using elearning.ContentService.Domain.Common.Enums;
using System;

namespace elearning.ContentService.Application.MasterData.DTOs
{
    public class MasterDataItemDto
    {
        public string Id { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public TagType? TagType { get; set; }
        public Guid? LanguageId { get; set; }
        public string? LanguageName { get; set; }
        public string? ShortName { get; set; }
        public string? Description { get; set; }
        public int OrderIndex { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
