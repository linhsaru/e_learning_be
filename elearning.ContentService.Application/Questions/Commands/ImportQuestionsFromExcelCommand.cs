using elearning.ContentService.Application.Questions.DTOs;
using elearning.ContentService.Domain.Common.Enums;
using elearning.ContentService.Domain.Questions.Entities;
using elearning.ContentService.Domain.Questions.Repositories;
using ExcelDataReader;
using MediatR;
using SharedKernel.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace elearning.ContentService.Application.Questions.Commands
{
    public record ImportQuestionsFromExcelCommand(
        Guid QuestionSetId,
        Stream FileStream
    ) : IRequest<Result<ImportQuestionsResultDto>>;

    public class ImportQuestionsFromExcelCommandHandler : IRequestHandler<ImportQuestionsFromExcelCommand, Result<ImportQuestionsResultDto>>
    {
        private readonly IQuestionRepository _repo;

        public ImportQuestionsFromExcelCommandHandler(IQuestionRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<ImportQuestionsResultDto>> Handle(ImportQuestionsFromExcelCommand request, CancellationToken cancellationToken)
        {
            if (request.FileStream == null || request.FileStream.Length == 0)
            {
                return Result.Failure<ImportQuestionsResultDto>(Error.Validation("Import.FileEmpty", "Import file stream is empty."));
            }

            // Register encoding provider for ExcelDataReader support
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var resultDto = new ImportQuestionsResultDto();
            int rowIndex = 1;

            using (var reader = ExcelReaderFactory.CreateReader(request.FileStream))
            {
                // Skip header row
                if (!reader.Read())
                {
                    return Result.Failure<ImportQuestionsResultDto>(Error.Validation("Import.ExcelEmpty", "Excel file has no content."));
                }

                while (reader.Read())
                {
                    rowIndex++;

                    string? typeStr = reader.GetValue(0)?.ToString()?.Trim();
                    string? skillStr = reader.GetValue(1)?.ToString()?.Trim();
                    string? content = reader.GetValue(2)?.ToString()?.Trim();
                    string? hint = reader.GetValue(3)?.ToString()?.Trim();

                    string? optionA = reader.GetValue(4)?.ToString()?.Trim();
                    string? optionB = reader.GetValue(5)?.ToString()?.Trim();
                    string? optionC = reader.GetValue(6)?.ToString()?.Trim();
                    string? optionD = reader.GetValue(7)?.ToString()?.Trim();

                    string? correctOptionsStr = reader.GetValue(8)?.ToString()?.Trim();
                    string? explanationText = reader.GetValue(9)?.ToString()?.Trim();

                    // Ignore completely empty rows
                    if (string.IsNullOrWhiteSpace(content) && string.IsNullOrWhiteSpace(typeStr))
                    {
                        continue;
                    }

                    // Validation: Content is required
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        resultDto.ErrorMessages.Add($"Dòng {rowIndex}: Nội dung câu hỏi (Content) không được để trống.");
                        resultDto.TotalFailed++;
                        continue;
                    }

                    // Parse QuestionType
                    if (!TryParseQuestionType(typeStr, out QuestionType questionType))
                    {
                        resultDto.ErrorMessages.Add($"Dòng {rowIndex}: Dạng câu hỏi '{typeStr}' không hợp lệ.");
                        resultDto.TotalFailed++;
                        continue;
                    }

                    // Parse SkillType
                    SkillType? skillType = ParseSkillType(skillStr);

                    // Create Question Entity
                    var question = Question.Create(
                        request.QuestionSetId,
                        questionType,
                        content,
                        skillType,
                        questionGroupId: null,
                        hint: hint
                    );

                    // Parse Options
                    var correctKeys = ParseCorrectKeys(correctOptionsStr);
                    int orderIndex = 0;

                    if (!string.IsNullOrWhiteSpace(optionA))
                    {
                        bool isCorrect = correctKeys.Contains("A") || correctKeys.Contains("1");
                        question.AddOption(optionA, isCorrect, orderIndex++);
                    }

                    if (!string.IsNullOrWhiteSpace(optionB))
                    {
                        bool isCorrect = correctKeys.Contains("B") || correctKeys.Contains("2");
                        question.AddOption(optionB, isCorrect, orderIndex++);
                    }

                    if (!string.IsNullOrWhiteSpace(optionC))
                    {
                        bool isCorrect = correctKeys.Contains("C") || correctKeys.Contains("3");
                        question.AddOption(optionC, isCorrect, orderIndex++);
                    }

                    if (!string.IsNullOrWhiteSpace(optionD))
                    {
                        bool isCorrect = correctKeys.Contains("D") || correctKeys.Contains("4");
                        question.AddOption(optionD, isCorrect, orderIndex++);
                    }

                    // Add Explanation if available
                    if (!string.IsNullOrWhiteSpace(explanationText))
                    {
                        question.AddExplanation(explanationText);
                    }

                    await _repo.AddAsync(question, cancellationToken);
                    resultDto.ImportedQuestionIds.Add(question.Id);
                    resultDto.TotalImported++;
                }
            }

            if (resultDto.TotalImported > 0)
            {
                await _repo.SaveChangesAsync(cancellationToken);
            }

            return Result.Success(resultDto);
        }

        private static bool TryParseQuestionType(string? str, out QuestionType questionType)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                questionType = QuestionType.SingleChoice;
                return true;
            }

            if (Enum.TryParse<QuestionType>(str, true, out var parsed))
            {
                questionType = parsed;
                return true;
            }

            if (int.TryParse(str, out int val) && Enum.IsDefined(typeof(QuestionType), val))
            {
                questionType = (QuestionType)val;
                return true;
            }

            questionType = QuestionType.SingleChoice;
            return false;
        }

        private static SkillType? ParseSkillType(string? str)
        {
            if (string.IsNullOrWhiteSpace(str)) return null;

            if (Enum.TryParse<SkillType>(str, true, out var parsed))
            {
                return parsed;
            }

            if (int.TryParse(str, out int val) && Enum.IsDefined(typeof(SkillType), val))
            {
                return (SkillType)val;
            }

            return null;
        }

        private static HashSet<string> ParseCorrectKeys(string? correctStr)
        {
            var keys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (string.IsNullOrWhiteSpace(correctStr)) return keys;

            var parts = correctStr.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                keys.Add(part.Trim().ToUpperInvariant());
            }

            return keys;
        }
    }
}
