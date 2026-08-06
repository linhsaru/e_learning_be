using elearning.ContentService.Application.MasterData.DTOs;
using elearning.ContentService.Domain.MasterData.Repositories;
using MediatR;
using SharedKernel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace elearning.ContentService.Application.MasterData.Commands
{
    public record ReorderMasterDataItemsCommand(string Type, IReadOnlyList<MasterDataReorderItemDto> OrderList) : IRequest<Result>;

    public class ReorderMasterDataItemsCommandHandler : IRequestHandler<ReorderMasterDataItemsCommand, Result>
    {
        private readonly IMasterDataRepository _repository;

        public ReorderMasterDataItemsCommandHandler(IMasterDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(ReorderMasterDataItemsCommand request, CancellationToken cancellationToken)
        {
            if (request.OrderList == null || request.OrderList.Count == 0)
                return Result.Failure(Error.Validation("MasterData.Reorder.EmptyList", "Order list cannot be empty."));

            var typeNormalized = request.Type.Trim().ToLowerInvariant();

            switch (typeNormalized)
            {
                case "language":
                case "languages":
                    var langIds = request.OrderList
                        .Where(x => Guid.TryParse(x.Id, out _))
                        .Select(x => Guid.Parse(x.Id))
                        .ToList();

                    var languages = await _repository.GetLanguagesByIdsAsync(langIds, cancellationToken);
                    foreach (var item in request.OrderList)
                    {
                        if (Guid.TryParse(item.Id, out var gId))
                        {
                            var lang = languages.FirstOrDefault(x => x.Id == gId);
                            if (lang != null)
                            {
                                lang.OrderIndex = item.OrderIndex;
                                lang.MarkAsUpdated();
                            }
                        }
                    }
                    await _repository.SaveChangesAsync(cancellationToken);
                    return Result.Success();

                case "level":
                case "levels":
                    var levelIds = request.OrderList
                        .Where(x => Guid.TryParse(x.Id, out _))
                        .Select(x => Guid.Parse(x.Id))
                        .ToList();

                    var levels = await _repository.GetLevelsByIdsAsync(levelIds, cancellationToken);
                    foreach (var item in request.OrderList)
                    {
                        if (Guid.TryParse(item.Id, out var gId))
                        {
                            var lvl = levels.FirstOrDefault(x => x.Id == gId);
                            if (lvl != null)
                            {
                                lvl.OrderIndex = item.OrderIndex;
                                lvl.MarkAsUpdated();
                            }
                        }
                    }
                    await _repository.SaveChangesAsync(cancellationToken);
                    return Result.Success();

                case "tag":
                case "tags":
                    var tagIds = request.OrderList
                        .Where(x => Guid.TryParse(x.Id, out _))
                        .Select(x => Guid.Parse(x.Id))
                        .ToList();

                    var tags = await _repository.GetTagsByIdsAsync(tagIds, cancellationToken);
                    foreach (var item in request.OrderList)
                    {
                        if (Guid.TryParse(item.Id, out var gId))
                        {
                            var tag = tags.FirstOrDefault(x => x.Id == gId);
                            if (tag != null)
                            {
                                tag.OrderIndex = item.OrderIndex;
                                tag.MarkAsUpdated();
                            }
                        }
                    }
                    await _repository.SaveChangesAsync(cancellationToken);
                    return Result.Success();

                case "partofspeech":
                case "parts-of-speech":
                case "part-of-speech":
                case "partofspeeches":
                    var posIds = request.OrderList
                        .Where(x => int.TryParse(x.Id, out _))
                        .Select(x => int.Parse(x.Id))
                        .ToList();

                    var posList = await _repository.GetPartsOfSpeechByIdsAsync(posIds, cancellationToken);
                    foreach (var item in request.OrderList)
                    {
                        if (int.TryParse(item.Id, out var pId))
                        {
                            var pos = posList.FirstOrDefault(x => x.Id == pId);
                            if (pos != null)
                            {
                                pos.OrderIndex = item.OrderIndex;
                                pos.MarkAsUpdated();
                            }
                        }
                    }
                    await _repository.SaveChangesAsync(cancellationToken);
                    return Result.Success();

                default:
                    return Result.Failure(Error.Validation("MasterData.UnsupportedType", $"Master data type '{request.Type}' is not supported."));
            }
        }
    }
}
