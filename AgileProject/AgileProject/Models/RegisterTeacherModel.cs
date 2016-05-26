using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgileProject.Models
{
    public class RegisterTeacherModel
    {
        public IEnumerable<SelectListItem> getCorridors { get; set; }
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string LastName { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Phone { get; set; }
        public int corridorId { get; set; }
        public ApplicationUser User { get; set; }
        public bool isAdmin { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string PadNumber { get; set; }
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}