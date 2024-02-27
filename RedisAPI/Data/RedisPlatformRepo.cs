using RedisAPI.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RedisAPI.Data
{
    public class RedisPlatformRepo : IPlatformRepo
    {
        private readonly IConnectionMultiplexer _redis;
        public RedisPlatformRepo(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }
        public void CreatePlatform(Platform plat)
        {
            if(plat == null)
            {
                throw new ArgumentOutOfRangeException(nameof(plat));
            }
            var db = _redis.GetDatabase();
            var serialPlat = JsonSerializer.Serialize(plat);
            //db.StringSet(plat.Id, serialPlat);
            //db.SetAdd("Platforms", serialPlat);

            db.HashSet("HashPlatform", new HashEntry[]
            {new HashEntry(plat.Id, serialPlat)});
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            var db = _redis.GetDatabase();
            //var completeSet = db.SetMembers("Platforms");

            var completeHash = db.HashGetAll("HashPlatform");
            if (completeHash.Length > 0)
            {
                var obj = Array.ConvertAll(completeHash, val => JsonSerializer.Deserialize<Platform>(val.Value)).ToList();

                return obj;
            }
            return null;
        }

        public Platform GetPlatformById(string id)
        {
            var db = _redis.GetDatabase();
            //var plat = db.StringGet(id);
            var hashPlat = db.HashGet("HashPlatform", id);
            if (!string.IsNullOrEmpty(hashPlat))
            {
                return JsonSerializer.Deserialize<Platform>(hashPlat);
            }

            return null;
        }

        public bool DeletePlatformById(string id)
        {
            var db = _redis.GetDatabase();
            var hashPlat = db.HashGet("HashPlatform", id);
            if (!string.IsNullOrEmpty(hashPlat))
            {
                db.HashDelete("HashPlatform", id);
                return true;
            }
            return false;
        }

        public Platform UpdatePlatformById(Platform plat, string id)
        {
            var db = _redis.GetDatabase();
            var serialPlat = JsonSerializer.Serialize(plat);
            //var hashPlat = db.HashGet("HashPlatform", plat.Id);
            
            if (db.HashExists("HashPlatform", id))
            {
                db.HashSet("HashPlatform", new HashEntry[]
                {new HashEntry(id, serialPlat) });
                var hashPlat = db.HashGet("HashPlatform", id);
                if (!string.IsNullOrEmpty(hashPlat))
                {
                    return JsonSerializer.Deserialize<Platform>(hashPlat);
                }
            }
            return null;
        }
    }
}
