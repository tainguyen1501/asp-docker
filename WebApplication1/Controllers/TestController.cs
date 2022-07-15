using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        //private readonly IDatabase _database;
        public TestController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            //_database = database;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Test...");
        }

        [HttpGet("test2")]
        public IActionResult GetTest2()
        {
            return Ok("Tests...");
        }

        [HttpGet("Set")]
        public async Task<IActionResult> Set(string key, string value)
        {
            //_database.StringSet(key, value);
            var options = new DistributedCacheEntryOptions()
                   .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                   .SetSlidingExpiration(TimeSpan.FromMinutes(2));
            //await _database.SetAddAsync(key, value);
            await _distributedCache.SetAsync(key, Encoding.UTF8.GetBytes(value), options);
            return Ok("Set ok...");
        }
        [HttpGet("Get")]
        public async Task<IActionResult> Get(string key)
        {
            //var sadf = _database.StringGet(key);
            var bytes = await _distributedCache.GetAsync(key);
            if (bytes != null)
            {
                string str = Encoding.UTF8.GetString(bytes);
                return Ok(str);

            }
            return Ok("Not found...");
        }
    }
}
