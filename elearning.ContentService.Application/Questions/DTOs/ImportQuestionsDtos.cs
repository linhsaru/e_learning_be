using System;
using System.Collections.Generic;

namespace elearning.ContentService.Application.Questions.DTOs
{
    public class ImportQuestionsResultDto
    {
        public int TotalImported { get; set; }
        public int TotalFailed { get; set; }
        public List<Guid> ImportedQuestionIds { get; set; } = new();
        public List<string> ErrorMessages { get; set; } = new();
    }
}
