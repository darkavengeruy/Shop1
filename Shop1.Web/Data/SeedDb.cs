﻿namespace Shop1.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using Helpers;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            var user = await this.userHelper.GetUserByEmailAsync("ferfranco@hotmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Fernando",
                    LastName = "Franco",
                    Email = "ferfranco@hotmail.com",
                    UserName = "ferfranco@hotmail.com",
                    PhoneNumber = "+56 984787772"
                };

                var result = await this.userHelper.AddUserAsync(user, "123456789");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

                if (!this.context.Products.Any())
            {
                this.AddProduct("iPhone X", user);
                this.AddProduct("Magic Mouse", user);
                this.AddProduct("iWatch Series 4", user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(10000),
                IsAvailabe = true,
                Stock = this.random.Next(100)
            });
        }
    }

}
