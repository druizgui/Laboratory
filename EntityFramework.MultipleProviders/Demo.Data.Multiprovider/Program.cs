// -----------------------------------------------------------------
// <copyright>Copyright (C) 2020, David Ruiz.</copyright>
// Licensed under the Apache License, Version 2.0.
// You may not use this file except in compliance with the License:
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Software is distributed on an "AS IS", WITHOUT WARRANTIES
// OR CONDITIONS OF ANY KIND, either express or implied.
// -----------------------------------------------------------------

namespace Demo.Data.Multiprovider
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    internal class Program
    {
        //https://docs.microsoft.com/es-es/ef/core/what-is-new/ef-core-3.0/
        private static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            DbContextOptions<BloggingContext> options;

            var databaseType = configuration.GetValue<string>("Provider", "memory");
            Console.WriteLine($"EntityFramework provider: {databaseType}");

            var connectionString = configuration.GetValue<string>("ConnectionStrings:" + databaseType);
            Console.WriteLine($"Connection string: {connectionString}");

            switch (databaseType.ToLowerInvariant())
            {
                case "sqlite":
                    options = new DbContextOptionsBuilder<BloggingContext>()
                        .UseSqlite(connectionString)
                        .Options;
                    break;
                case "sqlserver":
                    options = new DbContextOptionsBuilder<BloggingContext>()
                        .UseSqlServer(connectionString)
                        .Options;
                    break;
                default:
                    options = new DbContextOptionsBuilder<BloggingContext>()
                        .UseInMemoryDatabase(connectionString)
                        .Options;
                    break;
            }

            using (var db = new BloggingContext(options))
            {
                db.Database.EnsureCreated();

                // Insert 
                Console.WriteLine("Inserting a new blog");
                db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                db.SaveChanges();

                // Read
                Console.WriteLine("Querying for a blog");
                var blog = db.Blogs
                    .OrderBy(b => b.BlogId)
                    .First();

                // Update
                Console.WriteLine("Updating the blog and adding a post");
                blog.Url = "https://devblogs.microsoft.com/dotnet";
                blog.Posts.Add(
                    new Post
                    {
                        Title = "Hello World",
                        Content = "I wrote an app using EF Core!"
                    });
                db.SaveChanges();

                // Delete
                Console.WriteLine("Delete the blog");
                db.Remove(blog);
                db.SaveChanges();
            }
        }
    }
}