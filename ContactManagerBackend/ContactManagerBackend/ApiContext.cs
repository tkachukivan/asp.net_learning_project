using System;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerBackend
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<Models.Contact> Contacts { get; set; }

        public DbSet<Models.User> Users { get; set; }
    }
}
