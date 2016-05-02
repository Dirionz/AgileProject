using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgileProject.Models
{
    public class PostStatusModel
    {
        public IEnumerable<SelectListItem> getStatus { get; set; }
        public int statusId { get; set; }
        public string date { get; set; }
    }
}