using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileProject.Models
{
    public class Status
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public Teacher Teacher { get; set; }
        public DateTime Date { get; set; }
    }
}