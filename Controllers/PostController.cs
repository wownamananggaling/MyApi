using Microsoft.AspNetCore.Mvc;
using MyApi.Models;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private static List<Post> _posts = new()
        {
            new Post { Id = 1, Title = "Hello", Content = "First post" },
            new Post { Id = 2, Title = "World", Content = "Second post" }
        };

        [HttpGet]
        public IActionResult GetAll() => Ok(_posts);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var post = _posts.FirstOrDefault(p => p.Id == id);
            return post == null ? NotFound() : Ok(post);
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            post.Id = _posts.Count + 1;
            _posts.Add(post);
            return CreatedAtAction(nameof(GetById), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Post updated)
        {
            var post = _posts.FirstOrDefault(p => p.Id == id);
            if (post == null) return NotFound();
            post.Title = updated.Title;
            post.Content = updated.Content;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var post = _posts.FirstOrDefault(p => p.Id == id);
            if (post == null) return NotFound();
            _posts.Remove(post);
            return NoContent();
        }
    }
}