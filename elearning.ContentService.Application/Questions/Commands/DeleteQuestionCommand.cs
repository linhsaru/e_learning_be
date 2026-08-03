using elearning.ContentService.Domain.Questions.Repositories;
using SharedKernel.Common;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace elearning.ContentService.Application.Questions.Commands
{
    public record DeleteQuestionCommand(Guid Id) : IRequest<Result>;

    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, Result>
    {
        private readonly IQuestionRepository _repo;

        public DeleteQuestionCommandHandler(IQuestionRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (question == null)
            {
                return Result.Failure(Error.NotFound("Question.NotFound", $"Question with Id '{request.Id}' was not found."));
            }

            _repo.Delete(question);
            await _repo.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
