using elearning.ContentService.Domain.Common.Enums;
using elearning.ContentService.Domain.MasterData.Entities;
using elearning.ContentService.Domain.MasterData.Repositories;
using elearning.ContentService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace elearning.ContentService.Infrastructure.Persistence.Repositories
{
    public class MasterDataRepository : IMasterDataRepository
    {
        private readonly ContentDbContext _dbContext;

        public MasterDataRepository(ContentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Languages

        public async Task<Language?> GetLanguageByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _dbContext.Languages.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<bool> IsLanguageCodeExistsAsync(string code, Guid? excludeId = null, CancellationToken ct = default)
        {
            return await _dbContext.Languages.AnyAsync(x => x.Code == code && (!excludeId.HasValue || x.Id != excludeId.Value), ct);
        }

        public async Task AddLanguageAsync(Language language, CancellationToken ct = default)
        {
            await _dbContext.Languages.AddAsync(language, ct);
        }

        public async Task<(IReadOnlyList<Language> Items, int TotalCount)> ListLanguagesAsync(string? search, int pageNumber, int pageSize, CancellationToken ct = default)
        {
            var query = _dbContext.Languages.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                query = query.Where(x => x.Code.ToLower().Contains(s) || x.Name.ToLower().Contains(s));
            }

            var total = await query.CountAsync(ct);
            var items = await query
                .OrderBy(x => x.OrderIndex)
                .ThenBy(x => x.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task<bool> HasLanguageDependenciesAsync(Guid languageId, CancellationToken ct = default)
        {
            var hasLevels = await _dbContext.Levels.AnyAsync(x => x.LanguageId == languageId, ct);
            if (hasLevels) return true;

            var hasVocab = await _dbContext.Vocabularies.AnyAsync(x => x.LanguageId == languageId, ct);
            if (hasVocab) return true;

            var hasGrammar = await _dbContext.Grammars.AnyAsync(x => x.LanguageId == languageId, ct);
            if (hasGrammar) return true;

            return false;
        }

        #endregion

        #region Levels

        public async Task<Level?> GetLevelByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _dbContext.Levels
                .Include(x => x.Language)
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task AddLevelAsync(Level level, CancellationToken ct = default)
        {
            await _dbContext.Levels.AddAsync(level, ct);
        }

        public async Task<(IReadOnlyList<Level> Items, int TotalCount)> ListLevelsAsync(string? search, Guid? languageId, int pageNumber, int pageSize, CancellationToken ct = default)
        {
            var query = _dbContext.Levels.AsNoTracking().Include(x => x.Language).AsQueryable();

            if (languageId.HasValue && languageId.Value != Guid.Empty)
            {
                query = query.Where(x => x.LanguageId == languageId.Value);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                query = query.Where(x => x.Code.ToLower().Contains(s) || x.Name.ToLower().Contains(s));
            }

            var total = await query.CountAsync(ct);
            var items = await query
                .OrderBy(x => x.OrderIndex)
                .ThenBy(x => x.Code)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task<bool> HasLevelDependenciesAsync(Guid levelId, CancellationToken ct = default)
        {
            var hasCourses = await _dbContext.Courses.AnyAsync(x => x.LevelId == levelId, ct);
            if (hasCourses) return true;

            var hasLearningPaths = await _dbContext.LearningPaths.AnyAsync(x => x.TargetLevelId == levelId, ct);
            if (hasLearningPaths) return true;

            var hasQuestionSets = await _dbContext.QuestionSets.AnyAsync(x => x.LevelId == levelId, ct);
            if (hasQuestionSets) return true;

            var hasVocab = await _dbContext.Vocabularies.AnyAsync(x => x.LevelId == levelId, ct);
            if (hasVocab) return true;

            var hasGrammar = await _dbContext.Grammars.AnyAsync(x => x.LevelId == levelId, ct);
            if (hasGrammar) return true;

            return false;
        }

        #endregion

        #region Tags

        public async Task<Tag?> GetTagByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _dbContext.Tags.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<bool> IsTagSlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken ct = default)
        {
            return await _dbContext.Tags.AnyAsync(x => x.Slug == slug && (!excludeId.HasValue || x.Id != excludeId.Value), ct);
        }

        public async Task AddTagAsync(Tag tag, CancellationToken ct = default)
        {
            await _dbContext.Tags.AddAsync(tag, ct);
        }

        public async Task<(IReadOnlyList<Tag> Items, int TotalCount)> ListTagsAsync(string? search, TagType? tagType, int pageNumber, int pageSize, CancellationToken ct = default)
        {
            var query = _dbContext.Tags.AsNoTracking().AsQueryable();

            if (tagType.HasValue)
            {
                query = query.Where(x => x.Type == tagType.Value);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(s) || x.Slug.ToLower().Contains(s));
            }

            var total = await query.CountAsync(ct);
            var items = await query
                .OrderBy(x => x.OrderIndex)
                .ThenBy(x => x.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task<bool> HasTagDependenciesAsync(Guid tagId, CancellationToken ct = default)
        {
            var hasCourseTags = await _dbContext.CourseTags.AnyAsync(x => x.TagId == tagId, ct);
            if (hasCourseTags) return true;

            var hasVocabTags = await _dbContext.VocabularyTags.AnyAsync(x => x.TagId == tagId, ct);
            if (hasVocabTags) return true;

            var hasGrammarTags = await _dbContext.GrammarTags.AnyAsync(x => x.TagId == tagId, ct);
            if (hasGrammarTags) return true;

            var hasQuestionTags = await _dbContext.QuestionTags.AnyAsync(x => x.TagId == tagId, ct);
            if (hasQuestionTags) return true;

            return false;
        }

        #endregion

        #region PartsOfSpeech

        public async Task<PartOfSpeech?> GetPartOfSpeechByIdAsync(int id, CancellationToken ct = default)
        {
            return await _dbContext.PartsOfSpeech.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<bool> IsPartOfSpeechCodeExistsAsync(string code, int? excludeId = null, CancellationToken ct = default)
        {
            return await _dbContext.PartsOfSpeech.AnyAsync(x => x.Code == code && (!excludeId.HasValue || x.Id != excludeId.Value), ct);
        }

        public async Task AddPartOfSpeechAsync(PartOfSpeech pos, CancellationToken ct = default)
        {
            await _dbContext.PartsOfSpeech.AddAsync(pos, ct);
        }

        public async Task<(IReadOnlyList<PartOfSpeech> Items, int TotalCount)> ListPartsOfSpeechAsync(string? search, int pageNumber, int pageSize, CancellationToken ct = default)
        {
            var query = _dbContext.PartsOfSpeech.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                query = query.Where(x => x.Code.ToLower().Contains(s) || (x.Name != null && x.Name.ToLower().Contains(s)));
            }

            var total = await query.CountAsync(ct);
            var items = await query
                .OrderBy(x => x.OrderIndex)
                .ThenBy(x => x.Code)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task<bool> HasPartOfSpeechDependenciesAsync(int posId, CancellationToken ct = default)
        {
            var pos = await GetPartOfSpeechByIdAsync(posId, ct);
            if (pos == null) return false;

            return await _dbContext.Vocabularies.AnyAsync(
                x => (pos.Code != null && x.PartOfSpeech == pos.Code) ||
                     (pos.Name != null && x.PartOfSpeech == pos.Name) ||
                     (pos.ShortName != null && x.PartOfSpeech == pos.ShortName),
                ct);
        }

        #endregion

        #region Reorder Helpers

        public async Task<List<Language>> GetLanguagesByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default)
        {
            return await _dbContext.Languages.Where(x => ids.Contains(x.Id)).ToListAsync(ct);
        }

        public async Task<List<Level>> GetLevelsByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default)
        {
            return await _dbContext.Levels.Where(x => ids.Contains(x.Id)).ToListAsync(ct);
        }

        public async Task<List<Tag>> GetTagsByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default)
        {
            return await _dbContext.Tags.Where(x => ids.Contains(x.Id)).ToListAsync(ct);
        }

        public async Task<List<PartOfSpeech>> GetPartsOfSpeechByIdsAsync(IEnumerable<int> ids, CancellationToken ct = default)
        {
            return await _dbContext.PartsOfSpeech.Where(x => ids.Contains(x.Id)).ToListAsync(ct);
        }

        #endregion

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return await _dbContext.SaveChangesAsync(ct);
        }
    }
}
