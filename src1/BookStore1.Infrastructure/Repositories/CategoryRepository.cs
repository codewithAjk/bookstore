using BookStore1.Domain.Interfaces;
using BookStore1.Domain.Models;
using BookStore1.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore1.Infrastructure.Repositories
{

    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookStoreDbContext _context;
        public CategoryRepository(BookStoreDbContext context)
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

            if (!(category is null))
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
