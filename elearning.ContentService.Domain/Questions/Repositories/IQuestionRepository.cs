using elearning.ContentService.Domain.Questions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace elearning.ContentService.Domain.Questions.Repositories
{
    public interface IQuestionRepository
    {
        Task<Question?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<Question?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<Question>> GetByQuestionSetIdAsync(Guid questionSetId, CancellationToken ct = default);
        Task AddAsync(Question question, CancellationToken ct = default);
        void Update(Question question);
        void Delete(Question question);
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
