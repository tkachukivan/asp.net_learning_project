using ContactManagerBackend.Models;
using System;
using System.Data.Entity;

namespace ContactManagerBackend
{
    public class ApiContext : DbContext
    {
        public ApiContext() : base("ApiContext") { }

        public DbSet<Contact> Contacts { get; set; }
    }
}
