﻿using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {

        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppUser2> Users2 { get; set; }


    }
}
