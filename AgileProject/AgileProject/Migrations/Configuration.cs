using System.Collections.Generic;
using AgileProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AgileProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AgileProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "AgileProject.Models.ApplicationDbContext";
        }

        protected override void Seed(AgileProject.Models.ApplicationDbContext context)
        {

            var corridors = new List<Corridor>
            {
                new Corridor() { Name = "Corridor1"},
                new Corridor() { Name = "Corridor2"},
                new Corridor() { Name = "Corridor3"},
            };
            corridors.ForEach(s => context.Corridors.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();


            if (!(context.Users.Any(u => u.UserName == "u@u.uu")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser { UserName = "u@u.uu", PhoneNumber = "0797697898" };
                userManager.Create(userToInsert, "Hejhej123!");
            }


            var user = context.Users.FirstOrDefault(u => u.UserName == "u@u.uu");
            var corridor = context.Corridors.FirstOrDefault(c => c.Name == "Corridor1");
            var teacher = new Teacher() { FirstName = "Test", LastName = "Testsson", Phone = "0797697898", imageURL = "../images/default.jpg", isAdmin = true, Corridor = corridor, User = user};
            context.Teacher.Add(teacher);
            context.SaveChanges();

        }
    }
}
