﻿using Microsoft.EntityFrameworkCore;
using StavkiWebApi.Models.Entites;

namespace StavkiWebApi.Models.EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Request> Requests => Set<Request>();
        public ApplicationContext() 
        {
            Database.EnsureDeleted();   
            Database.EnsureCreated();
        } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\Users\sozon\Desktop\Stavki\StavkiWebApi\StavkiWebApi\bin\Debug\net6.0\StvavkiDB.db");
        }
    }
}
