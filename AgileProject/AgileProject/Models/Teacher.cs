using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileProject.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public Corridor Corridor { get; set; }
        public ApplicationUser User { get; set; }

    }
}