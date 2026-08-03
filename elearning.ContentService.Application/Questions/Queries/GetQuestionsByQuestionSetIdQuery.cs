using elearning.ContentService.Application.Questions.DTOs;
using elearning.ContentService.Domain.Questions.Repositories;
using SharedKernel.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace elearning.ContentService.Application.Questions.Queries
{
    public record GetQuestionsByQuestionSetIdQuery(Guid QuestionSetId) : IRequest<Result<IReadOnlyList<QuestionDto>>>;

    public class GetQuestionsByQuestionSetIdQueryHandler : IRequestHandler<GetQuestionsByQuestionSetIdQuery, Result<IReadOnlyList<QuestionDto>>>
    {
        private readonly IQuestionRepository _repo;

        public GetQuestionsByQuestionSetIdQueryHandler(IQuestionRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<IReadOnlyList<QuestionDto>>> Handle(GetQuestionsByQuestionSetIdQuery request, CancellationToken cancellationToken)
        {
            var questions = await _repo.GetByQuestionSetIdAsync(request.QuestionSetId, cancellationToken);

            var dtos = questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                QuestionSetId = q.QuestionSetId,
                QuestionGroupId = q.QuestionGroupId,
                QuestionType = q.QuestionType,
                SkillType = q.SkillType,
                Content = q.Content,
                Hint = q.Hint,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
                Options = q.Options.Select(o => new OptionDto
                {
                    Id = o.Id,
                    QuestionId = o.QuestionId,
                    Content = o.Content,
                    IsCorrect = o.IsCorrect,
                    OrderIndex = o.OrderIndex
                }).ToList(),
                Explanations = q.Explanations.Select(e => new ExplanationDto
                {
                    Id = e.Id,
                    QuestionId = e.QuestionId,
                    MediaId = e.MediaId,
                    ExplanationText = e.ExplanationText
                }).ToList()
            }).ToList();

            return Result.Success<IReadOnlyList<QuestionDto>>(dtos);
        }
    }
}
