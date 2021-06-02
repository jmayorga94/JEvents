using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JEvents.API.Dtos
{
    public class EventCreateDto
    {
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
        [Required]
        public string StartDate { get; set; }
        [Required]
        public int Duration { get; set; }
    }
}
