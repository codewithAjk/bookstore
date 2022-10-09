using BookStore1.Domain.Interfaces;
using BookStore1.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookRepository.GetBooks();
            return Ok(books);
        }

        [HttpGet("{id}", Name = "GetBook")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookRepository.GetBookById(id);
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book book)
        {
            await _bookRepository.CreateBook(book);
            return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }
            try
            {
                await _bookRepository.UpdateBook(book);

                return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepository.GetBookById(id);

            if (book is null)
            {
                return NotFound();
            }

            await _bookRepository.DeleteBook(id);

            return Ok();

        }
        private bool BookExists(int id)
        {
            return _bookRepository.BookExists(id);
        }
    }
}
