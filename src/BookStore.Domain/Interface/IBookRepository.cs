using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interface
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBookById(int id);
        Task CreateBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBook(int id);
        bool BookExists(int id);
    }
}
