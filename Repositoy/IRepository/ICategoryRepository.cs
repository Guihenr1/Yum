﻿using Yum.Data;

namespace Yum.Repositoy.IRepository
{
    public interface ICategoryRepository
    {
        public Task<Category> AddAsync(Category category);
        public Task<Category> UpdateAsync(Category category);
        public Task<bool> DeleteAsync(int id);
        public Task<Category> GetAsync(int id);
        public Task<IEnumerable<Category>> GetAllAsync();
    }
}