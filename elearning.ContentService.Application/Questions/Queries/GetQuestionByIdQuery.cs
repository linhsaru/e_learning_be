using elearning.ContentService.Application.Questions.DTOs;
using elearning.ContentService.Domain.Questions.Repositories;
using SharedKernel.Common;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace elearning.ContentService.Application.Questions.Queries
{
    public record GetQuestionByIdQuery(Guid Id) : IRequest<Result<QuestionDto>>;

    public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, Result<QuestionDto>>
    {
        private readonly IQuestionRepository _repo;

        public GetQuestionByIdQueryHandler(IQuestionRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<QuestionDto>> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var question = await _repo.GetByIdWithDetailsAsync(request.Id, cancellationToken);
            if (question == null)
            {
                return Result.Failure<QuestionDto>(Error.NotFound("Question.NotFound", $"Question with Id '{request.Id}' was not found."));
            }

            var dto = new QuestionDto
            {
                Id = question.Id,
                QuestionSetId = question.QuestionSetId,
                QuestionGroupId = question.QuestionGroupId,
                QuestionType = question.QuestionType,
                SkillType = question.SkillType,
                Content = question.Content,
                Hint = question.Hint,
                CreatedAt = question.CreatedAt,
                UpdatedAt = question.UpdatedAt,
                Options = question.Options.Select(o => new OptionDto
                {
                    Id = o.Id,
                    QuestionId = o.QuestionId,
                    Content = o.Content,
                    IsCorrect = o.IsCorrect,
                    OrderIndex = o.OrderIndex
                }).ToList(),
                Explanations = question.Explanations.Select(e => new ExplanationDto
                {
                    Id = e.Id,
                    QuestionId = e.QuestionId,
                    MediaId = e.MediaId,
                    ExplanationText = e.ExplanationText
                }).ToList()
            };

            return Result.Success(dto);
        }
    }
}
