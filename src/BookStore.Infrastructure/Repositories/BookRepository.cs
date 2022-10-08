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
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreDBContext _context;
        public BookRepository(BookStoreDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            var books = await _context.Books.ToListAsync();
            return books;
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBook(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            _context.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBook(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

            if(!(book is null))
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public bool BookExists(int id)
        {
            return _context.Books.Find(id) != null;
        }

    }
}
