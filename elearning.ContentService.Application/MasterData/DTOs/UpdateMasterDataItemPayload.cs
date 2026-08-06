using elearning.ContentService.Domain.Common.Enums;
using System;

namespace elearning.ContentService.Application.MasterData.DTOs
{
    public class UpdateMasterDataItemPayload
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public TagType? TagType { get; set; }
        public Guid? LanguageId { get; set; }
        public string? ShortName { get; set; }
        public string? Description { get; set; }
        public int OrderIndex { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
