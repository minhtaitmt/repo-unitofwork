using GenericRepositoryAndUnitofWork.Entities;

namespace GenericRepositoryAndUnitofWork.Repositories
{
    public interface IBookRepository
    {
        Task <List<Book>> GetAllBooksAsync ();
        Task<Book> GetBookByIdAsync (int id);
        Task AddBookAsync (Book book);
        Task DeleteBookAsync (int id);
        Task UpdateBookAsync (int id, Book book);
    }
}
