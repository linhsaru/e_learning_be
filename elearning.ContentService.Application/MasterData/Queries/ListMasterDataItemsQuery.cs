using elearning.ContentService.Application.MasterData.DTOs;
using elearning.ContentService.Domain.Common.Enums;
using elearning.ContentService.Domain.MasterData.Repositories;
using MediatR;
using SharedKernel.Common;
using SharedKernel.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace elearning.ContentService.Application.MasterData.Queries
{
    public record ListMasterDataItemsQuery(
        string Type,
        string? SearchTerm = null,
        Guid? LanguageId = null,
        TagType? TagType = null,
        int PageNumber = 1,
        int PageSize = 20) : IRequest<Result<PagedResult<MasterDataItemDto>>>;

    public class ListMasterDataItemsQueryHandler : IRequestHandler<ListMasterDataItemsQuery, Result<PagedResult<MasterDataItemDto>>>
    {
        private readonly IMasterDataRepository _repository;

        public ListMasterDataItemsQueryHandler(IMasterDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<PagedResult<MasterDataItemDto>>> Handle(ListMasterDataItemsQuery request, CancellationToken cancellationToken)
        {
            var typeNormalized = request.Type.Trim().ToLowerInvariant();
            var pageNumber = Math.Max(1, request.PageNumber);
            var pageSize = Math.Clamp(request.PageSize, 1, 100);

            switch (typeNormalized)
            {
                case "language":
                case "languages":
                    var (langItems, langTotal) = await _repository.ListLanguagesAsync(request.SearchTerm, pageNumber, pageSize, cancellationToken);
                    var mappedLangs = langItems.Select(x => new MasterDataItemDto
                    {
                        Id = x.Id.ToString(),
                        Type = "Language",
                        Code = x.Code,
                        Name = x.Name,
                        OrderIndex = x.OrderIndex,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt
                    }).ToList();

                    return Result.Success(new PagedResult<MasterDataItemDto>(mappedLangs, langTotal, pageNumber, pageSize));

                case "level":
                case "levels":
                    var (levelItems, levelTotal) = await _repository.ListLevelsAsync(request.SearchTerm, request.LanguageId, pageNumber, pageSize, cancellationToken);
                    var mappedLevels = levelItems.Select(x => new MasterDataItemDto
                    {
                        Id = x.Id.ToString(),
                        Type = "Level",
                        Code = x.Code,
                        Name = x.Name,
                        LanguageId = x.LanguageId,
                        LanguageName = x.Language?.Name,
                        OrderIndex = x.OrderIndex,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt
                    }).ToList();

                    return Result.Success(new PagedResult<MasterDataItemDto>(mappedLevels, levelTotal, pageNumber, pageSize));

                case "tag":
                case "tags":
                    var (tagItems, tagTotal) = await _repository.ListTagsAsync(request.SearchTerm, request.TagType, pageNumber, pageSize, cancellationToken);
                    var mappedTags = tagItems.Select(x => new MasterDataItemDto
                    {
                        Id = x.Id.ToString(),
                        Type = "Tag",
                        Name = x.Name,
                        Slug = x.Slug,
                        TagType = x.Type,
                        OrderIndex = x.OrderIndex,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt
                    }).ToList();

                    return Result.Success(new PagedResult<MasterDataItemDto>(mappedTags, tagTotal, pageNumber, pageSize));

                case "partofspeech":
                case "parts-of-speech":
                case "part-of-speech":
                case "partofspeeches":
                    var (posItems, posTotal) = await _repository.ListPartsOfSpeechAsync(request.SearchTerm, pageNumber, pageSize, cancellationToken);
                    var mappedPos = posItems.Select(x => new MasterDataItemDto
                    {
                        Id = x.Id.ToString(),
                        Type = "PartOfSpeech",
                        Code = x.Code,
                        Name = x.Name,
                        ShortName = x.ShortName,
                        Description = x.Description,
                        OrderIndex = x.OrderIndex,
                        IsActive = x.IsActive,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt
                    }).ToList();

                    return Result.Success(new PagedResult<MasterDataItemDto>(mappedPos, posTotal, pageNumber, pageSize));

                default:
                    return Result.Failure<PagedResult<MasterDataItemDto>>(Error.Validation("MasterData.UnsupportedType", $"Master data type '{request.Type}' is not supported."));
            }
        }
    }
}
