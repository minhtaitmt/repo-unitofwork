﻿using GenericRepositoryAndUnitofWork.Entities;
using Microsoft.EntityFrameworkCore;

namespace GenericRepositoryAndUnitofWork.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookStoreContext _context;

        public CategoryRepository(BookStoreContext context) 
        {
            _context = context;
        }
        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var book = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(book);
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task UpdateCategoryAsync(int id, Category category)
        {
            if(id != category.Id)
            {
                throw new Exception("Category Id not found.");
            }
            _context.Categories.Update(category);
        }
    }
}
