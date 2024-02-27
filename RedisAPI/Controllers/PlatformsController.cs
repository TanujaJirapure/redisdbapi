using Microsoft.AspNetCore.Mvc;
using RedisAPI.Data;
using RedisAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisAPI.Controllers
{
    [Route("api/[controller]")] //equivalent to [Route("api/platforms")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repo;

        public PlatformsController(IPlatformRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}", Name ="GetPlatformById")]
        public ActionResult<Platform> GetPlatformById(string id)
        {
            var platform = _repo.GetPlatformById(id);
            if(platform != null)
            {
                return Ok(platform);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<Platform> CreatePlatform(Platform platform)
        {
            _repo.CreatePlatform(platform);

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platform.Id }, platform);

        }

        [HttpGet]
        public ActionResult<IEnumerable<Platform>> GetAllplatforms()
        {
            return Ok(_repo.GetAllPlatforms());
        }

        [HttpPost("{id}")]
        public ActionResult<bool> DeletePlatformById(string id)
        {
            if (_repo.DeletePlatformById(id))
            {
                return Ok(true);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public ActionResult<Platform> UpdatePlatformById(Platform plat,string id)
        {
            var platform = _repo.UpdatePlatformById(plat,id);
            if (platform != null)
            {
                return Ok(platform);
            }
            return NotFound();
        }
    }
}
