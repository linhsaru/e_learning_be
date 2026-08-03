using elearning.ContentService.Domain.Questions.Entities;
using elearning.ContentService.Domain.Questions.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace elearning.ContentService.Infrastructure.Persistence.Repositories.Questions
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ContentDbContext _dbContext;

        public QuestionRepository(ContentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Question?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _dbContext.Questions.FirstOrDefaultAsync(q => q.Id == id, ct);
        }

        public async Task<Question?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default)
        {
            return await _dbContext.Questions
                .Include(q => q.Options)
                .Include(q => q.Explanations)
                .Include(q => q.QuestionTags)
                .FirstOrDefaultAsync(q => q.Id == id, ct);
        }

        public async Task<IReadOnlyList<Question>> GetByQuestionSetIdAsync(Guid questionSetId, CancellationToken ct = default)
        {
            return await _dbContext.Questions
                .Include(q => q.Options)
                .Include(q => q.Explanations)
                .Where(q => q.QuestionSetId == questionSetId)
                .ToListAsync(ct);
        }

        public async Task AddAsync(Question question, CancellationToken ct = default)
        {
            await _dbContext.Questions.AddAsync(question, ct);
        }

        public void Update(Question question)
        {
            _dbContext.Questions.Update(question);
        }

        public void Delete(Question question)
        {
            _dbContext.Questions.Remove(question);
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return await _dbContext.SaveChangesAsync(ct);
        }
    }
}
