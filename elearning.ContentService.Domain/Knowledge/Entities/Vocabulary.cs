using elearning.ContentService.Domain.MasterData.Entities;
using SharedKernel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace elearning.ContentService.Domain.Knowledge.Entities
{
    public class Vocabulary : AggregateRoot<Guid>
    {
        public Guid LanguageId { get; set; }
        public Guid LevelId { get; set; }
        public required string Word { get; set; }
        public string? Phonetic { get; set; } //Phiên âm
        public string? PartOfSpeech { get; set; } //Từ loại
        public Guid? AudioMediaId { get; set; }

        public Language Language { get; private set; } = null!;
        public Level Level { get; private set; } = null!;
    }
}
