using dotNetPracticeProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotNetPracticeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(AppDbContext appDbContext) : ControllerBase
    {
        [HttpPost("AddBooks")]
        public async Task<IActionResult> addBooks([FromBody] Book book)
        {
            appDbContext.Books.Add(book);
            await appDbContext.SaveChangesAsync();
            return Ok("Data Inserted...");
        }

        [HttpPost("AddMultipleBooks")]
        public async Task<IActionResult> addMultipleBooks([FromBody] List<Book> books)
        {
            appDbContext.Books.AddRange(books);
            await appDbContext.SaveChangesAsync();
            return Ok("Record Inserted");
        }

        [HttpPost("AddBooksWithAuthor")]
        public async Task<IActionResult> addBooksWithAuthor([FromBody] Book book)
        {
            appDbContext.Books.Add(book);
            await appDbContext.SaveChangesAsync();
            return Ok("Data Inserted...");
        }

        [HttpGet("AllBooks")]
        public async Task<IActionResult> getallbooksAsync()
        {
            var books = await appDbContext.Books.AsNoTracking().ToListAsync();
            return Ok(books);
        }


        [HttpGet("AllBooksNavigation")]
        public async Task<IActionResult> getallbooksNavigationAsync()
        {
            var books = await appDbContext.Books.AsNoTracking().Select(x => new 
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                NoOfpages = x.NoOfpages,
                IsActive = x.IsActive,
                CreatedOn = x.CreatedOn,
                LanguageId = x.LanguageId,
                AuthorId = x.AuthorId,
                Author=x.Author, 
                Language=x.Language.Title!=null? x.Language.Title: "NA",
            }).ToListAsync();
            return Ok(books);
        }

        [HttpGet("AllBooksEagerLoading")]
        public async Task<IActionResult> getallbooksEagerLoadingAsync()
        {
            var books = await appDbContext.Books.Include(x=>x.Author)
                .Include(x=>x.Language).ToListAsync();
            return Ok(books);
        }

        [HttpPut("UpdateBooks/{id}")]
        public async Task<IActionResult> updateBooks([FromRoute] int id, [FromBody] Book book)
        {
            var resultBook = await appDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (resultBook == null)
            {
                return NotFound("Book is not available");
            }

            resultBook.Title= book.Title ;
            resultBook.Description = book.Description ;

            await appDbContext.SaveChangesAsync();
            return Ok(book);
        }

        [HttpPut("UpdateBooksInSingleQuery")]
        public async Task<IActionResult> updateBooksinSingleQuery( [FromBody] Book book)
        {
            appDbContext.Books.Update(book);

            await appDbContext.SaveChangesAsync();
            return Ok(book);
        }

        [HttpPut("UpdateBooksBulk")]
        public async Task<IActionResult> updateBooksBulk()
        {
            await appDbContext.Books.Where(x=>x.NoOfpages==200).
           ExecuteUpdateAsync(x => x.SetProperty(p => p.Description, 
           p => p.Title + "This is description")
            .SetProperty(p => p.Title, p => p.Title + " Updated 2"));
            return Ok();
        }

        // hard delete
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> deletebookbyId([FromRoute] int id)
        {
            var book = await appDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (book == null)
            {
                return NotFound("Book is not present");
            }

            appDbContext.Books.Remove(book);
            await appDbContext.SaveChangesAsync();
            return Ok();
        }

        //only one database calll
        [HttpDelete("Deleteonecall/{id}")]
        public async Task<IActionResult> deletebookbyIdonecall([FromRoute] int id)
        {
            var book = new Book { Id = id };
            appDbContext.Entry(book).State = EntityState.Deleted;
            await appDbContext.SaveChangesAsync();
          
            return Ok();
        }


        //Bulk record deleted multi time databse call

        [HttpDelete("DeleteRecordsInBulk")]
        public async Task <IActionResult> deleteRecordsInBulk()
        {
            var books = await appDbContext.Books.Where(x => x.Id > 7).ToListAsync();
            appDbContext.Books.RemoveRange(books);
             await appDbContext.SaveChangesAsync();
            return Ok();
        }

        //bulk delete in one databse call
        [HttpDelete("DeleteInBulk")]
        public async Task<IActionResult> deleteBookInBulkoneDatabaseAsync()
        {
            var books = await appDbContext.Books.Where(x => x.Id < 8).ExecuteDeleteAsync();
            return Ok();
        }
    }
}
