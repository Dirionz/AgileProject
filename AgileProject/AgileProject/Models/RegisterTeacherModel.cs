using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgileProject.Models
{
    public class RegisterTeacherModel
    {
        public List<SelectListItem> getCorridors()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "test", Value = "tesing" }
            };
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public Corridor corridor { get; set; }
        public ApplicationUser User { get; set; }
        public bool isAdmin { get; set; }
    }
}