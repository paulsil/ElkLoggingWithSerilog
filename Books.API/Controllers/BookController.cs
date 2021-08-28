using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Books.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public object Get()
        {
            return new  { Name = "Bob" };
        }
    }
}
