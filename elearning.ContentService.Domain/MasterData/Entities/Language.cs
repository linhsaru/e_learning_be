using SharedKernel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace elearning.ContentService.Domain.MasterData.Entities
{
    /// <summary>
    /// Ngôn ngữ
    /// </summary>
    public class Language : BaseEntity<Guid>
    {
        public required string Code { get; set; } //Code: KO, VI, JP,...

        public string? Name { get; set; } //Name: Korean, Vietnamese, Japanese

        public ICollection<Level> Levels { get; set; } = new List<Level>();
    }
}
