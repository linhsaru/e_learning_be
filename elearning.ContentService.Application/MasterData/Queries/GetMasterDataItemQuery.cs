using elearning.ContentService.Application.MasterData.DTOs;
using elearning.ContentService.Domain.MasterData.Repositories;
using MediatR;
using SharedKernel.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace elearning.ContentService.Application.MasterData.Queries
{
    public record GetMasterDataItemQuery(string Type, string Id) : IRequest<Result<MasterDataItemDto>>;

    public class GetMasterDataItemQueryHandler : IRequestHandler<GetMasterDataItemQuery, Result<MasterDataItemDto>>
    {
        private readonly IMasterDataRepository _repository;

        public GetMasterDataItemQueryHandler(IMasterDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<MasterDataItemDto>> Handle(GetMasterDataItemQuery request, CancellationToken cancellationToken)
        {
            var typeNormalized = request.Type.Trim().ToLowerInvariant();

            switch (typeNormalized)
            {
                case "language":
                case "languages":
                    if (!Guid.TryParse(request.Id, out var langId))
                        return Result.Failure<MasterDataItemDto>(Error.Validation("MasterData.InvalidId", "Invalid Guid format for Language ID."));

                    var language = await _repository.GetLanguageByIdAsync(langId, cancellationToken);
                    if (language == null)
                        return Result.Failure<MasterDataItemDto>(Error.NotFound("MasterData.Language.NotFound", $"Language with ID '{request.Id}' not found."));

                    return Result.Success(new MasterDataItemDto
                    {
                        Id = language.Id.ToString(),
                        Type = "Language",
                        Code = language.Code,
                        Name = language.Name,
                        OrderIndex = language.OrderIndex,
                        CreatedAt = language.CreatedAt,
                        UpdatedAt = language.UpdatedAt
                    });

                case "level":
                case "levels":
                    if (!Guid.TryParse(request.Id, out var levelId))
                        return Result.Failure<MasterDataItemDto>(Error.Validation("MasterData.InvalidId", "Invalid Guid format for Level ID."));

                    var level = await _repository.GetLevelByIdAsync(levelId, cancellationToken);
                    if (level == null)
                        return Result.Failure<MasterDataItemDto>(Error.NotFound("MasterData.Level.NotFound", $"Level with ID '{request.Id}' not found."));

                    return Result.Success(new MasterDataItemDto
                    {
                        Id = level.Id.ToString(),
                        Type = "Level",
                        Code = level.Code,
                        Name = level.Name,
                        LanguageId = level.LanguageId,
                        LanguageName = level.Language?.Name,
                        OrderIndex = level.OrderIndex,
                        CreatedAt = level.CreatedAt,
                        UpdatedAt = level.UpdatedAt
                    });

                case "tag":
                case "tags":
                    if (!Guid.TryParse(request.Id, out var tagId))
                        return Result.Failure<MasterDataItemDto>(Error.Validation("MasterData.InvalidId", "Invalid Guid format for Tag ID."));

                    var tag = await _repository.GetTagByIdAsync(tagId, cancellationToken);
                    if (tag == null)
                        return Result.Failure<MasterDataItemDto>(Error.NotFound("MasterData.Tag.NotFound", $"Tag with ID '{request.Id}' not found."));

                    return Result.Success(new MasterDataItemDto
                    {
                        Id = tag.Id.ToString(),
                        Type = "Tag",
                        Name = tag.Name,
                        Slug = tag.Slug,
                        TagType = tag.Type,
                        OrderIndex = tag.OrderIndex,
                        CreatedAt = tag.CreatedAt,
                        UpdatedAt = tag.UpdatedAt
                    });

                case "partofspeech":
                case "parts-of-speech":
                case "part-of-speech":
                case "partofspeeches":
                    if (!int.TryParse(request.Id, out var posId))
                        return Result.Failure<MasterDataItemDto>(Error.Validation("MasterData.InvalidId", "Invalid Integer format for PartOfSpeech ID."));

                    var pos = await _repository.GetPartOfSpeechByIdAsync(posId, cancellationToken);
                    if (pos == null)
                        return Result.Failure<MasterDataItemDto>(Error.NotFound("MasterData.PartOfSpeech.NotFound", $"PartOfSpeech with ID '{request.Id}' not found."));

                    return Result.Success(new MasterDataItemDto
                    {
                        Id = pos.Id.ToString(),
                        Type = "PartOfSpeech",
                        Code = pos.Code,
                        Name = pos.Name,
                        ShortName = pos.ShortName,
                        Description = pos.Description,
                        OrderIndex = pos.OrderIndex,
                        IsActive = pos.IsActive,
                        CreatedAt = pos.CreatedAt,
                        UpdatedAt = pos.UpdatedAt
                    });

                default:
                    return Result.Failure<MasterDataItemDto>(Error.Validation("MasterData.UnsupportedType", $"Master data type '{request.Type}' is not supported."));
            }
        }
    }
}
