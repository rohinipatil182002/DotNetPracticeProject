using dotNetPracticeProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotNetPracticeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public LanguageController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("get-all-languages")]
        public async Task<IActionResult> GetLanguage()
        {
            // var result = _appDbContext.Languages.ToList();
            //var result = (from languages in _appDbContext.Languages select languages).ToList();

            var result = await _appDbContext.Languages.ToListAsync();
            //var result = await (from languages in _appDbContext.Languages select languages).ToListAsync();


            return Ok(result);
        }
    }
}
