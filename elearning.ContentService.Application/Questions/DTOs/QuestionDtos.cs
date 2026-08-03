using elearning.ContentService.Domain.Common.Enums;
using System;
using System.Collections.Generic;

namespace elearning.ContentService.Application.Questions.DTOs
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public Guid QuestionSetId { get; set; }
        public Guid? QuestionGroupId { get; set; }
        public QuestionType QuestionType { get; set; }
        public SkillType? SkillType { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? Hint { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<OptionDto> Options { get; set; } = new();
        public List<ExplanationDto> Explanations { get; set; } = new();
    }

    public class OptionDto
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public int OrderIndex { get; set; }
    }

    public class ExplanationDto
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid? MediaId { get; set; }
        public string ExplanationText { get; set; } = string.Empty;
    }

    public class CreateOptionDto
    {
        public string Content { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public int OrderIndex { get; set; }
    }

    public class CreateExplanationDto
    {
        public string ExplanationText { get; set; } = string.Empty;
        public Guid? MediaId { get; set; }
    }

    public class UpdateOptionDto
    {
        public Guid? Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public int OrderIndex { get; set; }
    }

    public class UpdateExplanationDto
    {
        public Guid? Id { get; set; }
        public string ExplanationText { get; set; } = string.Empty;
        public Guid? MediaId { get; set; }
    }
}
