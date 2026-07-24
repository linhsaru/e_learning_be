using SharedKernel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace elearning.ContentService.Domain.MasterData.Entities
{
    /// <summary>
    /// Từ loại: Động từ, danh từ, tính từ,...
    /// </summary>
    public class PartOfSpeech : BaseEntity<int>
    {
        public required string Code { get; set; } //NOUN, VERB,...
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Description { get; set; }
        public int OrderIndex { get; set; }
        public bool IsActive { get; set; }
    }
}