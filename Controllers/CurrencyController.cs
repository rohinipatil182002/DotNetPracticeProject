using dotNetPracticeProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotNetPracticeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CurrencyController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("get-all-currencies")]
        public IActionResult GetAllCurrencies()
        {
            //  var result= _appDbContext.Currencies.ToList();
            var result = (from currancies in _appDbContext.Currencies
                         select currancies).ToList();
            return Ok(result);
        }

        [HttpPost("fetchRecordByIds")]
        public async Task<IActionResult> fetchRecordByIds(List<int> ids)
        {
           //var curranciesIds = new List<int> { 1, 3, 5 };
            var curricies = await _appDbContext.Currencies.
                Where(e => ids.Contains(e.Id))
                .Select(x=>new Currency() { 
                Id=x.Id,
                Title=x.Title
                }).ToListAsync();
            return Ok(curricies);
        }
    }
}
