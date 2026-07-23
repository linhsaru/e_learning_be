using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SharedKernel.Specifications
{
    public abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; protected set; }

        public List<Expression<Func<TEntity, object>>> Includes { get; } = [];

        public Expression<Func<TEntity, object>>? OrderBy { get; protected set; }

        public Expression<Func<TEntity, object>>? OrderByDescending { get; protected set; }

        public int Skip { get; protected set; }

        public int Take { get; protected set; }

        public bool IsPagingEnabled { get; protected set; }

        protected BaseSpecification()
        {
        }

        protected BaseSpecification(
            Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected void AddInclude(
            Expression<Func<TEntity, object>> include)
        {
            Includes.Add(include);
        }

        protected void ApplyPaging(
            int skip,
            int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        protected void ApplyOrderBy(
            Expression<Func<TEntity, object>> orderBy)
        {
            OrderBy = orderBy;
        }

        protected void ApplyOrderByDescending(
            Expression<Func<TEntity, object>> orderByDescending)
        {
            OrderByDescending = orderByDescending;
        }
    }
}
