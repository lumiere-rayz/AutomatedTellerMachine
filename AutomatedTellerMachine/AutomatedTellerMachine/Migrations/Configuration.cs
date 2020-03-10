namespace AutomatedTellerMachine.Migrations
{
    using AutomatedTellerMachine.Models;
    using AutomatedTellerMachine.Services;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AutomatedTellerMachine.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "AutomatedTellerMachine.Models.ApplicationDbContext";
        }

        protected override void Seed(AutomatedTellerMachine.Models.ApplicationDbContext context)
        {
            //set up roles and asign users quickly
            //create an instance of a user store
            var userStore = new UserStore<ApplicationUser>(context);
            // create a user manager by specifying the application user class and passing in the store we just created
            var userManager = new UserManager<ApplicationUser>(userStore);

            if(!context.Users.Any(t=>t.UserName == "admin@mvcatm.com"))
            {
                var user = new ApplicationUser { UserName = "admin@mvcatm.com", Email = "admin@mvcatm.com" };
                userManager.Create(user, "passW0rd!");

                //after creating the admin user. set it up with a checking account using checking account service
                var service = new CheckingAccountService(context);
                service.CreateCheckingAccount("admin", "user", user.Id, 1000);

                //now to create the admin role and give it a new identity role called admin
                //the lambda expression tell it to look for existing record using the name property
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Admin" });
                context.SaveChanges(); //to ensure the role exist in the database
                //finally use the userManager AddToRole() to assign the new user to the admin role
                userManager.AddToRole(user.Id, "Admin"); //done run Update-Database command and test
            }

            //context.Transactions.Add(new Transaction { Amount = 75, CheckingAccountId = 6 });
            //context.Transactions.Add(new Transaction { Amount = -25, CheckingAccountId = 6 });
            //context.Transactions.Add(new Transaction { Amount = 100000, CheckingAccountId = 6 });
            //context.Transactions.Add(new Transaction { Amount = 19.99m, CheckingAccountId = 6 });
            //context.Transactions.Add(new Transaction { Amount = 64.40m, CheckingAccountId = 6 });
            //context.Transactions.Add(new Transaction { Amount = 100, CheckingAccountId = 6 });
            //context.Transactions.Add(new Transaction { Amount = -300, CheckingAccountId = 6 });
            //context.Transactions.Add(new Transaction { Amount = 255.75m, CheckingAccountId = 6 });
            //context.Transactions.Add(new Transaction { Amount = 198, CheckingAccountId = 6 });
            //context.Transactions.Add(new Transaction { Amount = 2, CheckingAccountId = 6 });
            //context.Transactions.Add(new Transaction { Amount = 50, CheckingAccountId = 6 });
            //context.Transactions.Add(new Transaction { Amount = -1.50m, CheckingAccountId = 6 });
            //context.Transactions.Add(new Transaction { Amount = 6100, CheckingAccountId = 6 });
            //context.Transactions.Add(new Transaction { Amount = 164.84m, CheckingAccountId = 6 });
            //context.Transactions.Add(new Transaction { Amount = .01m, CheckingAccountId = 6 });

            //context.Transactions.Add(new Transaction { Amount = 75, CheckingAccountId = 5 });
            //context.Transactions.Add(new Transaction { Amount = -25, CheckingAccountId = 5 });
            //context.Transactions.Add(new Transaction { Amount = 100000, CheckingAccountId = 5 });
            //context.Transactions.Add(new Transaction { Amount = 19.99m, CheckingAccountId = 5 });
            //context.Transactions.Add(new Transaction { Amount = 64.40m, CheckingAccountId = 5 });
            //context.Transactions.Add(new Transaction { Amount = 100, CheckingAccountId = 5 });
            //context.Transactions.Add(new Transaction { Amount = -300, CheckingAccountId = 5 });
            //context.Transactions.Add(new Transaction { Amount = 255.75m, CheckingAccountId = 5 });
            //context.Transactions.Add(new Transaction { Amount = 198, CheckingAccountId = 5 });
            //context.Transactions.Add(new Transaction { Amount = 2, CheckingAccountId = 5 });
            //context.Transactions.Add(new Transaction { Amount = 50, CheckingAccountId = 5 });
            //context.Transactions.Add(new Transaction { Amount = -1.50m, CheckingAccountId = 5 });
            //context.Transactions.Add(new Transaction { Amount = 6100, CheckingAccountId = 5 });
            //context.Transactions.Add(new Transaction { Amount = 164.84m, CheckingAccountId = 5 });
            //context.Transactions.Add(new Transaction { Amount = .01m, CheckingAccountId = 5 });

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
