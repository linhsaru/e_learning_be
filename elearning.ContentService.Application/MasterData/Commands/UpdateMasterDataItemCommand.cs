using elearning.ContentService.Application.MasterData.DTOs;
using elearning.ContentService.Domain.MasterData.Repositories;
using MediatR;
using SharedKernel.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace elearning.ContentService.Application.MasterData.Commands
{
    public record UpdateMasterDataItemCommand(string Type, string Id, UpdateMasterDataItemPayload Payload) : IRequest<Result<MasterDataItemDto>>;

    public class UpdateMasterDataItemCommandHandler : IRequestHandler<UpdateMasterDataItemCommand, Result<MasterDataItemDto>>
    {
        private readonly IMasterDataRepository _repository;

        public UpdateMasterDataItemCommandHandler(IMasterDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<MasterDataItemDto>> Handle(UpdateMasterDataItemCommand request, CancellationToken cancellationToken)
        {
            var typeNormalized = request.Type.Trim().ToLowerInvariant();
            var payload = request.Payload;

            switch (typeNormalized)
            {
                case "language":
                case "languages":
                    if (!Guid.TryParse(request.Id, out var langId))
                        return Result.Failure<MasterDataItemDto>(Error.Validation("MasterData.InvalidId", "Invalid Guid format for Language ID."));

                    var language = await _repository.GetLanguageByIdAsync(langId, cancellationToken);
                    if (language == null)
                        return Result.Failure<MasterDataItemDto>(Error.NotFound("MasterData.Language.NotFound", $"Language with ID '{request.Id}' not found."));

                    if (!string.IsNullOrWhiteSpace(payload.Code) && payload.Code != language.Code)
                    {
                        var codeExists = await _repository.IsLanguageCodeExistsAsync(payload.Code, langId, cancellationToken);
                        if (codeExists)
                            return Result.Failure<MasterDataItemDto>(Error.Conflict("MasterData.Language.CodeExists", $"Language code '{payload.Code}' already exists."));
                    }

                    language.Update(
                        payload.Code ?? language.Code,
                        payload.Name ?? language.Name,
                        payload.OrderIndex
                    );

                    await _repository.SaveChangesAsync(cancellationToken);

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

                    var targetLangId = payload.LanguageId ?? level.LanguageId;
                    if (payload.LanguageId.HasValue && payload.LanguageId.Value != level.LanguageId)
                    {
                        var langExists = await _repository.GetLanguageByIdAsync(payload.LanguageId.Value, cancellationToken);
                        if (langExists == null)
                            return Result.Failure<MasterDataItemDto>(Error.NotFound("MasterData.Language.NotFound", $"Language with ID '{payload.LanguageId}' not found."));
                    }

                    level.Update(
                        payload.Code ?? level.Code,
                        payload.Name ?? level.Name,
                        targetLangId,
                        payload.OrderIndex
                    );

                    await _repository.SaveChangesAsync(cancellationToken);

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

                    var slug = !string.IsNullOrWhiteSpace(payload.Slug)
                        ? payload.Slug
                        : (!string.IsNullOrWhiteSpace(payload.Name) ? payload.Name.ToLowerInvariant().Replace(" ", "-") : tag.Slug);

                    if (slug != tag.Slug)
                    {
                        var slugExists = await _repository.IsTagSlugExistsAsync(slug, tagId, cancellationToken);
                        if (slugExists)
                            return Result.Failure<MasterDataItemDto>(Error.Conflict("MasterData.Tag.SlugExists", $"Tag slug '{slug}' already exists."));
                    }

                    tag.Update(
                        payload.Name ?? tag.Name,
                        slug,
                        payload.TagType ?? tag.Type,
                        payload.OrderIndex
                    );

                    await _repository.SaveChangesAsync(cancellationToken);

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

                    if (!string.IsNullOrWhiteSpace(payload.Code) && payload.Code != pos.Code)
                    {
                        var codeExists = await _repository.IsPartOfSpeechCodeExistsAsync(payload.Code, posId, cancellationToken);
                        if (codeExists)
                            return Result.Failure<MasterDataItemDto>(Error.Conflict("MasterData.PartOfSpeech.CodeExists", $"PartOfSpeech code '{payload.Code}' already exists."));
                    }

                    pos.Update(
                        payload.Code ?? pos.Code,
                        payload.Name ?? pos.Name,
                        payload.ShortName ?? pos.ShortName,
                        payload.Description ?? pos.Description,
                        payload.OrderIndex,
                        payload.IsActive
                    );

                    await _repository.SaveChangesAsync(cancellationToken);

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
