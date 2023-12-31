﻿using _13_12_23.Entities.Base;
using System.Linq.Expressions;

namespace _13_12_23.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAllAsync(Expression<Func<T, bool>>? expression=null, 
            int skip = 0, 
            int take = 0,
            bool IsTracking = true,
            params string[] includes);

        IQueryable<T> GetAllByOrderAsync(Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? orderException = null,
            bool IsDescending = false,
            int skip = 0,
            int take = 0,
            bool IsTracking = true,
            params string[] includes);

        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChanceAsync();
    }
}
