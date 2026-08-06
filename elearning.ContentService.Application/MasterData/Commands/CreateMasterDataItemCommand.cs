using elearning.ContentService.Application.MasterData.DTOs;
using elearning.ContentService.Domain.MasterData.Entities;
using elearning.ContentService.Domain.MasterData.Repositories;
using MediatR;
using SharedKernel.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace elearning.ContentService.Application.MasterData.Commands
{
    public record CreateMasterDataItemCommand(string Type, CreateMasterDataItemPayload Payload) : IRequest<Result<MasterDataItemDto>>;

    public class CreateMasterDataItemCommandHandler : IRequestHandler<CreateMasterDataItemCommand, Result<MasterDataItemDto>>
    {
        private readonly IMasterDataRepository _repository;

        public CreateMasterDataItemCommandHandler(IMasterDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<MasterDataItemDto>> Handle(CreateMasterDataItemCommand request, CancellationToken cancellationToken)
        {
            var typeNormalized = request.Type.Trim().ToLowerInvariant();
            var payload = request.Payload;

            switch (typeNormalized)
            {
                case "language":
                case "languages":
                    if (string.IsNullOrWhiteSpace(payload.Code))
                        return Result.Failure<MasterDataItemDto>(Error.Validation("MasterData.Language.CodeRequired", "Language code is required."));
                    if (string.IsNullOrWhiteSpace(payload.Name))
                        return Result.Failure<MasterDataItemDto>(Error.Validation("MasterData.Language.NameRequired", "Language name is required."));

                    var existingLang = await _repository.IsLanguageCodeExistsAsync(payload.Code, null, cancellationToken);
                    if (existingLang)
                        return Result.Failure<MasterDataItemDto>(Error.Conflict("MasterData.Language.CodeExists", $"Language code '{payload.Code}' already exists."));

                    var language = Language.Create(payload.Code, payload.Name, payload.OrderIndex);
                    await _repository.AddLanguageAsync(language, cancellationToken);
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
                    if (string.IsNullOrWhiteSpace(payload.Code))
                        return Result.Failure<MasterDataItemDto>(Error.Validation("MasterData.Level.CodeRequired", "Level code is required."));
                    if (string.IsNullOrWhiteSpace(payload.Name))
                        return Result.Failure<MasterDataItemDto>(Error.Validation("MasterData.Level.NameRequired", "Level name is required."));
                    if (!payload.LanguageId.HasValue || payload.LanguageId.Value == Guid.Empty)
                        return Result.Failure<MasterDataItemDto>(Error.Validation("MasterData.Level.LanguageIdRequired", "LanguageId is required for Level."));

                    var lang = await _repository.GetLanguageByIdAsync(payload.LanguageId.Value, cancellationToken);
                    if (lang == null)
                        return Result.Failure<MasterDataItemDto>(Error.NotFound("MasterData.Language.NotFound", $"Language with ID '{payload.LanguageId}' not found."));

                    var level = Level.Create(payload.Code, payload.Name, payload.LanguageId.Value, payload.OrderIndex);
                    await _repository.AddLevelAsync(level, cancellationToken);
                    await _repository.SaveChangesAsync(cancellationToken);

                    return Result.Success(new MasterDataItemDto
                    {
                        Id = level.Id.ToString(),
                        Type = "Level",
                        Code = level.Code,
                        Name = level.Name,
                        LanguageId = level.LanguageId,
                        LanguageName = lang.Name,
                        OrderIndex = level.OrderIndex,
                        CreatedAt = level.CreatedAt,
                        UpdatedAt = level.UpdatedAt
                    });

                case "tag":
                case "tags":
                    if (string.IsNullOrWhiteSpace(payload.Name))
                        return Result.Failure<MasterDataItemDto>(Error.Validation("MasterData.Tag.NameRequired", "Tag name is required."));

                    var slug = !string.IsNullOrWhiteSpace(payload.Slug)
                        ? payload.Slug
                        : payload.Name.ToLowerInvariant().Replace(" ", "-");

                    var tagType = payload.TagType ?? Domain.Common.Enums.TagType.General;

                    var existingTag = await _repository.IsTagSlugExistsAsync(slug, null, cancellationToken);
                    if (existingTag)
                        return Result.Failure<MasterDataItemDto>(Error.Conflict("MasterData.Tag.SlugExists", $"Tag slug '{slug}' already exists."));

                    var tag = Tag.Create(payload.Name, slug, tagType, payload.OrderIndex);
                    await _repository.AddTagAsync(tag, cancellationToken);
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
                    if (string.IsNullOrWhiteSpace(payload.Code))
                        return Result.Failure<MasterDataItemDto>(Error.Validation("MasterData.PartOfSpeech.CodeRequired", "PartOfSpeech code is required."));

                    var existingPos = await _repository.IsPartOfSpeechCodeExistsAsync(payload.Code, null, cancellationToken);
                    if (existingPos)
                        return Result.Failure<MasterDataItemDto>(Error.Conflict("MasterData.PartOfSpeech.CodeExists", $"PartOfSpeech code '{payload.Code}' already exists."));

                    var pos = PartOfSpeech.Create(payload.Code, payload.Name, payload.ShortName, payload.Description, payload.OrderIndex, payload.IsActive);
                    await _repository.AddPartOfSpeechAsync(pos, cancellationToken);
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
