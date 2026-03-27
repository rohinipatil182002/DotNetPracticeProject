using dotNetPracticeProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
