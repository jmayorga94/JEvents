using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JEvents.API.Dtos
{
    public class EventUpdateDto
    {
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

    }
}
