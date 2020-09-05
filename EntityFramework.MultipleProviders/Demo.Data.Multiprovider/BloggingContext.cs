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
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    //EntityFrameworkCore\Add-Migration InitialCreate
    //EntityFrameworkCore\update-database
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public BloggingContext() :
            base(new DbContextOptionsBuilder<BloggingContext>()
                .UseSqlite("Data Source=Blogging.sqlite").Options)
        {
        }

        public BloggingContext(DbContextOptions<BloggingContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", true)
                    .Build();

                options.UseSqlite(configuration.GetConnectionString("sqlite"));
            }
        }
    }
}