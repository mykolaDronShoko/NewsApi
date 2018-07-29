using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseSPA.Controllers;
using DbContextSPA.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModelsSPA.Models;
using Newtonsoft.Json;

namespace ExamplePrj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : BaseController
    {
        public NewsController(
             ExampleDbContext context,
             RoleManager<IdentityRole> roleManager,
             UserManager<ApplicationUser> userManager,
             IConfiguration configuration
            ) : base(context, roleManager, userManager, configuration)
        { }

        //This method takes a list of the latest news

        [HttpGet("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<News>>> GetNewest()
        {
            var news = await _context.News
                .Include(img => img.Images)
                .OrderByDescending(d => d.DateAdded)
                .Take(5)
                .AsNoTracking()
                .ToListAsync();
            if (news == null)
            {
                return NotFound();
            }

            return new JsonResult(news, new JsonSerializerSettings()
            {
                Formatting = Formatting.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                TypeNameHandling = TypeNameHandling.None
            });
        }

        //This method takes the news by id parameter

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<News>> GetNewsById([FromRoute]int id)
        {
            var news = await _context.News
                .Include(img => img.Images)
                .Include(com => com.Comments)
                .FirstOrDefaultAsync(x => x.Id_news == id);
            news.Comments = news.Comments.Where(c => c.Active == true).ToList();
            news.NumberViews++;
            _context.News.Update(news);
            await _context.SaveChangesAsync();
            if (news == null)
            {
                return NotFound();
            }
            return new JsonResult(news, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                TypeNameHandling = TypeNameHandling.None
            });
        }

        //This method takes the news list by url parameter

        [HttpGet("[action]/{dataModel}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetNewsByUrl([FromRoute]string dataModel, [FromQuery]int page)
        {
            var news = await _context.News
              .Include(img => img.Images)
              .Include(com => com.Comments)
              .Include(c => c.Categories)
              .Where(z => (z.Categories.Name.Contains(dataModel) ||
                          z.Rank.Contains(dataModel)) && z.Active == true)
              .OrderBy(d => d.DateAdded)
              .Skip((page - 1) * 8)
              .Take(8)
              .AsNoTracking()
              .ToListAsync();
            return new JsonResult(news, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                TypeNameHandling = TypeNameHandling.None
            });
        }

        //This method takes a list of most viewed news

        [HttpGet("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetMostViewedNews()
        {
            var news = await _context.News
              .Include(img => img.Images)
              .OrderByDescending(d => d.NumberViews)
              .Take(10)
              .AsNoTracking()
              .ToListAsync();
            return new JsonResult(news, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                TypeNameHandling = TypeNameHandling.None
            });
        }

        //This method takes a list of most discussed news
        [HttpGet("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetMostDiscussedNews()
        {
            var news = await _context.News
              .Select(n => new {
                  Id_news = n.Id_news,
                  Title = n.Title,
                  Description = n.Description,
                  NumberViews = n.NumberViews,
                  DateAdded = n.DateAdded,
                  Comments = n.Comments.Where(c => c.Active == true)
              })
              .OrderByDescending(d => d.Comments.Count())
              .Take(10)
              .AsNoTracking()
              .ToListAsync();


            return new JsonResult(news, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                TypeNameHandling = TypeNameHandling.None
            });
        }

        //This method takes a list of random news
        [HttpGet("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetRandomNews()
        {
            // int newsCount = _context.News.Count();

            var news = await _context.News
              .Include(img => img.Images)
              .Include(com => com.Comments)
              // .Include(c => c.Categories)
              // .OrderByDescending(d => d.NumberViews)
              .AsNoTracking()
              .ToListAsync();
            List<News> randomList = new List<News>();
            Random rnd = new Random();
            while (randomList.Count() < 10)
            {
                int newsIndex = rnd.Next(0, news.Count());
                if (!randomList.Contains(news[newsIndex]))
                {
                    randomList.Add(news[newsIndex]);
                }
            }

            return new JsonResult(randomList, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                TypeNameHandling = TypeNameHandling.None
            });
        }


        //This method is looking for news by title

        [HttpGet("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Search([FromQuery]string s, [FromQuery]int page)
        {

            var news = await _context.News
              .Include(img => img.Images)
              .Where(z => z.Title.Contains(s) && z.Active == true)
              .OrderBy(d => d.DateAdded)
              .Skip((page - 1) * 8)
              .Take(8)
              .AsNoTracking()
              .ToListAsync();
            return new JsonResult(news, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                TypeNameHandling = TypeNameHandling.None
            });
        }
        //This method пуеі all comments
        [HttpGet("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllComments([FromQuery]int page)
        {

            var comments = await _context.Comments
              .Include(u => u.IdNews)
              .OrderByDescending(d => d.DateAdded)
              .Skip((page - 1) * 8)
              .Take(20)
              .AsNoTracking()
              .ToListAsync();
            return new JsonResult(comments, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                TypeNameHandling = TypeNameHandling.None
            });
        }

        //This method adds a comment

        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AddComment([FromBody]CommentsViewModel model)
        {
            var news = await _context.News.FindAsync(model.Id_news);
            if (news == null)
            {
                return NotFound();
            }
            var newComment = new Comments
            {
                Text = model.Text,
                DateAdded = DateTime.Now,
                Active = false,
                Id_news = model.Id_news,
                User_name = model.User_name,
                User_Img = model.User_img
            };
            news.Comments.Add(newComment);
            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }


        //This method changes comment status

        [HttpGet("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ChangeCommentStatus([FromQuery]int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return BadRequest();
            }
            comment.Active = !comment.Active;
            _context.Comments.Update(comment);
            try
            {
                await _context.SaveChangesAsync();
                return new JsonResult(comment, new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    PreserveReferencesHandling = PreserveReferencesHandling.None,
                    TypeNameHandling = TypeNameHandling.None
                });
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
