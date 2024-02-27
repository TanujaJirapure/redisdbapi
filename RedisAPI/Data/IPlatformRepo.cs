using RedisAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisAPI.Data
{
    public interface IPlatformRepo
    {
        void CreatePlatform(Platform plat);
        Platform GetPlatformById(string id);
        IEnumerable<Platform> GetAllPlatforms();
        bool DeletePlatformById(string id);
        Platform UpdatePlatformById(Platform plat, string id);
    }
}
