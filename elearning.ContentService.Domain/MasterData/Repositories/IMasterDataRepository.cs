using elearning.ContentService.Domain.Common.Enums;
using elearning.ContentService.Domain.MasterData.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace elearning.ContentService.Domain.MasterData.Repositories
{
    public interface IMasterDataRepository
    {
        // Languages
        Task<Language?> GetLanguageByIdAsync(Guid id, CancellationToken ct = default);
        Task<bool> IsLanguageCodeExistsAsync(string code, Guid? excludeId = null, CancellationToken ct = default);
        Task AddLanguageAsync(Language language, CancellationToken ct = default);
        Task<(IReadOnlyList<Language> Items, int TotalCount)> ListLanguagesAsync(string? search, int pageNumber, int pageSize, CancellationToken ct = default);
        Task<bool> HasLanguageDependenciesAsync(Guid languageId, CancellationToken ct = default);

        // Levels
        Task<Level?> GetLevelByIdAsync(Guid id, CancellationToken ct = default);
        Task AddLevelAsync(Level level, CancellationToken ct = default);
        Task<(IReadOnlyList<Level> Items, int TotalCount)> ListLevelsAsync(string? search, Guid? languageId, int pageNumber, int pageSize, CancellationToken ct = default);
        Task<bool> HasLevelDependenciesAsync(Guid levelId, CancellationToken ct = default);

        // Tags
        Task<Tag?> GetTagByIdAsync(Guid id, CancellationToken ct = default);
        Task<bool> IsTagSlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken ct = default);
        Task AddTagAsync(Tag tag, CancellationToken ct = default);
        Task<(IReadOnlyList<Tag> Items, int TotalCount)> ListTagsAsync(string? search, TagType? tagType, int pageNumber, int pageSize, CancellationToken ct = default);
        Task<bool> HasTagDependenciesAsync(Guid tagId, CancellationToken ct = default);

        // PartsOfSpeech
        Task<PartOfSpeech?> GetPartOfSpeechByIdAsync(int id, CancellationToken ct = default);
        Task<bool> IsPartOfSpeechCodeExistsAsync(string code, int? excludeId = null, CancellationToken ct = default);
        Task AddPartOfSpeechAsync(PartOfSpeech pos, CancellationToken ct = default);
        Task<(IReadOnlyList<PartOfSpeech> Items, int TotalCount)> ListPartsOfSpeechAsync(string? search, int pageNumber, int pageSize, CancellationToken ct = default);
        Task<bool> HasPartOfSpeechDependenciesAsync(int posId, CancellationToken ct = default);

        // Reorder Helpers
        Task<List<Language>> GetLanguagesByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default);
        Task<List<Level>> GetLevelsByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default);
        Task<List<Tag>> GetTagsByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default);
        Task<List<PartOfSpeech>> GetPartsOfSpeechByIdsAsync(IEnumerable<int> ids, CancellationToken ct = default);

        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
