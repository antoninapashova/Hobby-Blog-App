﻿using Application.Repositories;
using Hobby_Project;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly HobbyDbContext _context;

        public CategoryRepository(HobbyDbContext context)
        {
            _context = context;
        }

        public async Task<Category> Add(Category entity)
        {
            await _context.Categories.AddAsync(entity);
            return entity;
        }

        public void Delete(Category category)
        {
            _context.Remove(category);
        }

        public IEnumerable<Category> GetAllEntities()
        {
            return _context.Categories;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public IQueryable<Category> GetAllNames() 
        {
            return _context.Categories.AsNoTracking().AsQueryable();
        }

        public async Task<bool> CheckCategoryExists(string name)
        {
            return await _context.Categories.AnyAsync(c => c.Name == name);
        }

        public async Task<bool> FindById(int id)
        {
           return await _context.Categories.AsNoTracking().AnyAsync(c => c.Id == id);
        }

        public Category Update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
