using elearning.ContentService.Application.Questions.DTOs;
using elearning.ContentService.Domain.Common.Enums;
using elearning.ContentService.Domain.Questions.Entities;
using elearning.ContentService.Domain.Questions.Repositories;
using SharedKernel.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace elearning.ContentService.Application.Questions.Commands
{
    public record CreateQuestionCommand(
        Guid QuestionSetId,
        QuestionType QuestionType,
        string Content,
        SkillType? SkillType = null,
        Guid? QuestionGroupId = null,
        string? Hint = null,
        List<CreateOptionDto>? Options = null,
        List<CreateExplanationDto>? Explanations = null
    ) : IRequest<Result<Guid>>;

    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, Result<Guid>>
    {
        private readonly IQuestionRepository _repo;

        public CreateQuestionCommandHandler(IQuestionRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<Guid>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Content))
            {
                return Result.Failure<Guid>(Error.Validation("Question.ContentEmpty", "Question content cannot be empty."));
            }

            var question = Question.Create(
                request.QuestionSetId,
                request.QuestionType,
                request.Content,
                request.SkillType,
                request.QuestionGroupId,
                request.Hint
            );

            if (request.Options != null)
            {
                foreach (var opt in request.Options)
                {
                    question.AddOption(opt.Content, opt.IsCorrect, opt.OrderIndex);
                }
            }

            if (request.Explanations != null)
            {
                foreach (var exp in request.Explanations)
                {
                    question.AddExplanation(exp.ExplanationText, exp.MediaId);
                }
            }

            await _repo.AddAsync(question, cancellationToken);
            await _repo.SaveChangesAsync(cancellationToken);

            return Result.Success(question.Id);
        }
    }
}
