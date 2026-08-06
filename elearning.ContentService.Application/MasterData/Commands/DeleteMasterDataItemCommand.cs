using elearning.ContentService.Domain.MasterData.Repositories;
using MediatR;
using SharedKernel.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace elearning.ContentService.Application.MasterData.Commands
{
    public record DeleteMasterDataItemCommand(string Type, string Id) : IRequest<Result>;

    public class DeleteMasterDataItemCommandHandler : IRequestHandler<DeleteMasterDataItemCommand, Result>
    {
        private readonly IMasterDataRepository _repository;

        public DeleteMasterDataItemCommandHandler(IMasterDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteMasterDataItemCommand request, CancellationToken cancellationToken)
        {
            var typeNormalized = request.Type.Trim().ToLowerInvariant();

            switch (typeNormalized)
            {
                case "language":
                case "languages":
                    if (!Guid.TryParse(request.Id, out var langId))
                        return Result.Failure(Error.Validation("MasterData.InvalidId", "Invalid Guid format for Language ID."));

                    var language = await _repository.GetLanguageByIdAsync(langId, cancellationToken);
                    if (language == null)
                        return Result.Failure(Error.NotFound("MasterData.Language.NotFound", $"Language with ID '{request.Id}' not found."));

                    // Constraint Checks
                    var hasLangDeps = await _repository.HasLanguageDependenciesAsync(langId, cancellationToken);
                    if (hasLangDeps)
                        return Result.Failure(Error.Validation("MasterData.Language.HasDependencies", "Cannot delete Language because it has active linked entities (Levels, Vocabularies, or Grammars)."));

                    language.MarkAsDeleted();
                    await _repository.SaveChangesAsync(cancellationToken);
                    return Result.Success();

                case "level":
                case "levels":
                    if (!Guid.TryParse(request.Id, out var levelId))
                        return Result.Failure(Error.Validation("MasterData.InvalidId", "Invalid Guid format for Level ID."));

                    var level = await _repository.GetLevelByIdAsync(levelId, cancellationToken);
                    if (level == null)
                        return Result.Failure(Error.NotFound("MasterData.Level.NotFound", $"Level with ID '{request.Id}' not found."));

                    // Constraint Checks
                    var hasLevelDeps = await _repository.HasLevelDependenciesAsync(levelId, cancellationToken);
                    if (hasLevelDeps)
                        return Result.Failure(Error.Validation("MasterData.Level.HasDependencies", "Cannot delete Level because it is linked to active entities (Courses, LearningPaths, QuestionSets, Vocabularies, or Grammars)."));

                    level.MarkAsDeleted();
                    await _repository.SaveChangesAsync(cancellationToken);
                    return Result.Success();

                case "tag":
                case "tags":
                    if (!Guid.TryParse(request.Id, out var tagId))
                        return Result.Failure(Error.Validation("MasterData.InvalidId", "Invalid Guid format for Tag ID."));

                    var tag = await _repository.GetTagByIdAsync(tagId, cancellationToken);
                    if (tag == null)
                        return Result.Failure(Error.NotFound("MasterData.Tag.NotFound", $"Tag with ID '{request.Id}' not found."));

                    // Constraint Checks
                    var hasTagDeps = await _repository.HasTagDependenciesAsync(tagId, cancellationToken);
                    if (hasTagDeps)
                        return Result.Failure(Error.Validation("MasterData.Tag.HasDependencies", "Cannot delete Tag because it is linked to Courses, Vocabularies, Grammars, or Questions."));

                    tag.MarkAsDeleted();
                    await _repository.SaveChangesAsync(cancellationToken);
                    return Result.Success();

                case "partofspeech":
                case "parts-of-speech":
                case "part-of-speech":
                case "partofspeeches":
                    if (!int.TryParse(request.Id, out var posId))
                        return Result.Failure(Error.Validation("MasterData.InvalidId", "Invalid Integer format for PartOfSpeech ID."));

                    var pos = await _repository.GetPartOfSpeechByIdAsync(posId, cancellationToken);
                    if (pos == null)
                        return Result.Failure(Error.NotFound("MasterData.PartOfSpeech.NotFound", $"PartOfSpeech with ID '{request.Id}' not found."));

                    // Constraint Checks
                    var hasPosDeps = await _repository.HasPartOfSpeechDependenciesAsync(posId, cancellationToken);
                    if (hasPosDeps)
                        return Result.Failure(Error.Validation("MasterData.PartOfSpeech.HasDependencies", "Cannot delete PartOfSpeech because it is linked to Vocabularies."));

                    pos.MarkAsDeleted();
                    await _repository.SaveChangesAsync(cancellationToken);
                    return Result.Success();

                default:
                    return Result.Failure(Error.Validation("MasterData.UnsupportedType", $"Master data type '{request.Type}' is not supported."));
            }
        }
    }
}
