using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using URLShortener1.Data;
using URLShortener1.Dto;
using URLShortener1.Entities;

namespace URLShortener1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {

        private readonly DataContext _ctx;
      

        public UrlController(DataContext dataContext)
        {

            _ctx = dataContext;
        }

        [HttpGet]
      
        public async Task<ActionResult<List<Url>>> GetAllUrls()
        {
            var urlsQuery = _ctx.Urls.Include(url => url.User); 

            var urlDtos = await urlsQuery.Select(url => new UrlAllInfoDto
            {
                LongUrl = url.LongUrl,
                ShortUrl = url.ShortUrl,
                UserId = url.UserId,
                CreatedDate = url.CreatedDate,
                UserName = url.User != null ? url.User.Username : string.Empty
            }).ToListAsync();

            return Ok(urlDtos);
        }
       

        [HttpGet("getById/{urlId}")]
        public async Task<ActionResult<UrlAllInfoDto>> GetUrlById(int urlId)
        {
            var urlDetails = await _ctx.Urls
                .Where(url => url.Id == urlId)  
                .Include(url => url.User) 
                .Select(url => new UrlAllInfoDto
                {
                    LongUrl = url.LongUrl,
                    ShortUrl = url.ShortUrl,
                    UserId = url.UserId,
                    CreatedDate = url.CreatedDate,
                    UserName = url.User != null ? url.User.Username : string.Empty
                })
                .FirstOrDefaultAsync();

            if (urlDetails == null)
            {
                return NotFound();  
            }

            return Ok(urlDetails);
        }

        [HttpGet("getById1/{urlId}")]
        public async Task<ActionResult<string>> GetUrlById1(int urlId)
        {
            var url = _ctx.Urls.FirstOrDefaultAsync(u=>u.Id==urlId).Result;
            if (url == null)
            {
                return NotFound();
            }
            var longUrl = url.LongUrl;

            string shortUrl = UrlShortener.ShortenUrl(longUrl, urlId);

            url.ShortUrl = shortUrl;
            _ctx.SaveChangesAsync();

         

            return Ok(url);
        }

        [HttpPost]        
        public async Task<ActionResult<Url>> AddUrl(UrlDto request)
        {
            var url = new Url();
            if (_ctx.Urls.Any(u => u.LongUrl == request.LongUrl))
            {
                return BadRequest("Url is already taken");

            }
            url.LongUrl = request.LongUrl;
            url.ShortUrl = request.ShortUrl;
            url.UserId = request.UserId;

            await _ctx.AddAsync(url);
           
                await _ctx.SaveChangesAsync();
                    
            
            
            
            return Ok(url);

        }

        [HttpDelete("id")]
        public async Task<ActionResult<List<Url>>> DeleteUrl(int id)
        {
            var url = await _ctx.Urls.FindAsync(id);

            if (url == null)
            {
                return NotFound();  
            }

            _ctx.Urls.Remove(url);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }



    }
}
