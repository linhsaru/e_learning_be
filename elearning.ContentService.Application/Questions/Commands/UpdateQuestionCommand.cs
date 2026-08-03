using elearning.ContentService.Application.Questions.DTOs;
using elearning.ContentService.Domain.Common.Enums;
using elearning.ContentService.Domain.Questions.Entities;
using elearning.ContentService.Domain.Questions.Repositories;
using SharedKernel.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace elearning.ContentService.Application.Questions.Commands
{
    public record UpdateQuestionCommand(
        Guid Id,
        QuestionType QuestionType,
        string Content,
        SkillType? SkillType = null,
        Guid? QuestionGroupId = null,
        string? Hint = null,
        List<UpdateOptionDto>? Options = null,
        List<UpdateExplanationDto>? Explanations = null
    ) : IRequest<Result>;

    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, Result>
    {
        private readonly IQuestionRepository _repo;

        public UpdateQuestionCommandHandler(IQuestionRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _repo.GetByIdWithDetailsAsync(request.Id, cancellationToken);
            if (question == null)
            {
                return Result.Failure(Error.NotFound("Question.NotFound", $"Question with Id '{request.Id}' was not found."));
            }

            if (string.IsNullOrWhiteSpace(request.Content))
            {
                return Result.Failure(Error.Validation("Question.ContentEmpty", "Question content cannot be empty."));
            }

            question.Update(
                request.QuestionType,
                request.Content,
                request.SkillType,
                request.QuestionGroupId,
                request.Hint
            );

            // Synchronize Options
            if (request.Options != null)
            {
                question.Options.Clear();
                foreach (var opt in request.Options)
                {
                    question.AddOption(opt.Content, opt.IsCorrect, opt.OrderIndex);
                }
            }

            // Synchronize Explanations
            if (request.Explanations != null)
            {
                question.Explanations.Clear();
                foreach (var exp in request.Explanations)
                {
                    question.AddExplanation(exp.ExplanationText, exp.MediaId);
                }
            }

            _repo.Update(question);
            await _repo.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
