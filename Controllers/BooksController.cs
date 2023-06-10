using AutoMapper;
using GenericRepositoryAndUnitofWork.Entities;
using GenericRepositoryAndUnitofWork.Models;
using GenericRepositoryAndUnitofWork.UnitofWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenericRepositoryAndUnitofWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly IMapper _mapper;

        public BooksController(IUnitofWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBook()
        {
            var books = await _unitOfWork.BookRepository.GetAllBooksAsync();
            if (books == null)
            {
                return NotFound();
            }
            var booksModel = _mapper.Map<List<BookModel>>(books);
            return Ok(booksModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _unitOfWork.BookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            var bookModel = _mapper.Map<BookModel>(book);
            return Ok(bookModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookModel bookModel)
        {
            var book = _mapper.Map<Book>(bookModel);
            try
            {
                await _unitOfWork.BookRepository.AddBookAsync(book);
                await _unitOfWork.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }
            return CreatedAtAction("GetBookById", new { id = book.Id }, _mapper.Map<BookModel>(book));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookModel bookModel)
        {
            var book = _mapper.Map<Book>(bookModel);
            try
            {
                await _unitOfWork.BookRepository.UpdateBookAsync(id, book);
                await _unitOfWork.SaveChanges();
            }
            catch
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _unitOfWork.BookRepository.DeleteBookAsync(id);
                await _unitOfWork.SaveChanges();
            }
            catch
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
