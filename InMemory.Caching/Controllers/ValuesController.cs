using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        #region Set and Get

        //[HttpGet("/get")]
        //public string GetName()
        //{
        //    if (_memoryCache.TryGetValue<string>("name", out string name))
        //    {
        //        return _memoryCache.Get<string>("name");
        //    }

        //    return null;
        //}

        //[HttpGet("/set/{name}")]
        //public void SetName(string name)
        //{
        //    _memoryCache.Set("name", name);
        //}

        #endregion

        #region Sliding - Absolute Expiration

        [HttpGet("/set")]
        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
        }

        [HttpGet("get")]
        public DateTime GetDate()
        {
            return _memoryCache.Get<DateTime>("date");
        }

        #endregion



    }
}
