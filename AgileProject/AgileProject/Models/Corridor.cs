using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgileProject.Models
{
    public class Corridor
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 1)]
        public String Name { get; set; }
    }
}