using GenericRepositoryAndUnitofWork.Entities;
using GenericRepositoryAndUnitofWork.Models;
using Microsoft.EntityFrameworkCore;

namespace GenericRepositoryAndUnitofWork.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;

        public BookRepository(BookStoreContext context) 
        { 
            _context = context;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task AddBookAsync(Book book)
        {
            var id = book.CategoryId;
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new Exception("Category Not Found!");
            }
            _context.Books.Add(book);
        }

        public async Task UpdateBookAsync(int id, Book book )
        {
            if(id != book.Id)
            {
                throw new Exception("Book Id not found.");
            }
            _context.Books.Update(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                throw new Exception("Book Id not found.");
            }
             _context.Books.Remove(book);
        }

    }
}
