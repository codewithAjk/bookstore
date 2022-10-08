using BookStore.Domain.Interface;
using BookStore.Domain.Models;
using BookStore.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookStoreDBContext _context;
        public CategoryRepository(BookStoreDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var catgories = await _context.Categories.ToListAsync();
            return catgories;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);            
        }

        public async Task CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            _context.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(x => x.Id == id);

            if(!(category is null))
            {
                _context.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public bool CategoryExists(int id)
        {
            return _context.Categories.Find(id) != null;
        }

    }
}
