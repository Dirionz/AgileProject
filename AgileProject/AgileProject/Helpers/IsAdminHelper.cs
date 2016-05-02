using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AgileProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Principal;

namespace AgileProject.Helpers
{
    static public class IsAdminHelper
    {
        public static bool isAdmin(this HtmlHelper html)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = db.Users.FirstOrDefault(u => u.UserName == html.ViewContext.HttpContext.User.Identity.Name);
            var teacher = db.Teacher.FirstOrDefault(t => t.User.Id == user.Id);
            if (teacher != null && teacher.isAdmin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isAdminBackend(string userName)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = db.Users.FirstOrDefault(u => u.UserName == userName);
            if(user == null)
            {
                return false;
            }
            var teacher = db.Teacher.FirstOrDefault(t => t.User.Id == user.Id);
            if (teacher != null && teacher.isAdmin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}