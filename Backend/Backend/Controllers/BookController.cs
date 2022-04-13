using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistence.Repository;
using Persistence.Models;
using Microsoft.AspNetCore.Http;

namespace Backend.Controllers
{
    [Route("api/Books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private UnitOfWork _unitOfWork;
        private Repository<Book> _bookRepo;


        public BookController()
        {
            _unitOfWork = new UnitOfWork();
            _bookRepo = _unitOfWork.Repository<Book>();
        }

        // Method to get all books 
        /// <summary></summary>
        /// <returns></returns>
        [HttpGet(Name = "GetBooks")]
        [ProducesResponseType(200, Type = typeof(List<Book>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                return Ok(await _bookRepo.GetAll());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
                return StatusCode(400, ModelState);
            }
        }

        // Method to get only one book
        /// <summary></summary>
        /// <returns></returns>
        [HttpGet("{Id:int}", Name = "GetBook")]
        [ProducesResponseType(200, Type = typeof(Book))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetOneBook(int Id)
        {
            try
            {
                Book book = await _bookRepo.GetOneById(Id);

                if (book == null)
                {
                    return NotFound("Book not found.");
                }
                else
                {
                    return Ok(book);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
                return StatusCode(400, ModelState);
            }
        }

        // Method to add one book
        /// <summary></summary>
        /// <returns></returns>
        [HttpPost(Name = "AddBook")]
        [ProducesResponseType(201, Type = typeof(Book))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOneBook([FromBody] Book book)
        {
            try
            {
                // If object comes null, return Bad Request
                if (book == null)
                {
                    return BadRequest(ModelState);
                }

                if (await _bookRepo.AddOne(book))
                {
                    return Created("~api/Book", new { book = book });
                }

                ModelState.AddModelError("", $"An error occurred while trying to save the book {book.Title}");
                return StatusCode(500, ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
                return StatusCode(400, ModelState);
            }
        }

        // Method to update one book
        /// <summary></summary>
        /// <returns></returns>
        [HttpPut("{Id:int}", Name = "UpdateBook")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOneBook(int Id, [FromBody] Book book)
        {
            try
            {
                if (book == null || Id != book.Id)
                {
                    return BadRequest(ModelState);
                }

                if (!await _bookRepo.UpdateOne(Id, book))
                {
                    ModelState.AddModelError("", $"An error occurred while trying to update the book {book.Title}");
                    return StatusCode(500, ModelState);
                }

                return Content("Book updated succesfully.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
                return StatusCode(400, ModelState);
            }
        }

        // Method to delete one book
        /// <summary></summary>
        /// <returns></returns>
        [HttpDelete("{Id:int}", Name = "DeleteBook")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteOneBook(int Id)
        {
            try
            {
                Book book = await _bookRepo.GetOneById(Id);
                if (book == null)
                {
                    return NotFound("Book not found.");
                }

                if (!await _bookRepo.DeleteOne(Id))
                {
                    ModelState.AddModelError("", $"An error occurred while trying to delete the book {book.Title}");
                    return StatusCode(500, ModelState);
                }

                return Content("Book deleted succesfully.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
                return StatusCode(400, ModelState);
            }
        }
    }
}
