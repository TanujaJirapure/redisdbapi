using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedisAPI.Models
{
    public class Platform
    {
        [Required]
        public string Id { get; set; } = $"platform:{Guid.NewGuid().ToString()}";
        [Required]
        public string Name { get; set; }
    }
}
