using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Models;

namespace NewsPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _repository;

        public NewsController(INewsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/news
        [HttpGet]
        public IActionResult GetAll()
        {
            var news = _repository.News.ToList();
            return Ok(news);
        }

        // GET: api/news/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var newsItem = _repository.News.FirstOrDefault(n => n.NewsID == id);
            if (newsItem == null)
                return NotFound();
            return Ok(newsItem);
        }

        // POST: api/news
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create([FromBody] News newsItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repository.CreateNews(newsItem);
            return CreatedAtAction(nameof(GetById), new { id = newsItem.NewsID }, newsItem);
        }

        // PUT: api/news/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Update(int id, [FromBody] News newsItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = _repository.News.FirstOrDefault(n => n.NewsID == id);
            if (existing == null)
                return NotFound();

            newsItem.NewsID = id;
            _repository.UpdateNews(newsItem);

            return NoContent();
        }

        // DELETE: api/news/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            var newsItem = _repository.News.FirstOrDefault(n => n.NewsID == id);
            if (newsItem == null)
                return NotFound();

            _repository.DeleteNews(newsItem);
            return NoContent();
        }
    }
}
