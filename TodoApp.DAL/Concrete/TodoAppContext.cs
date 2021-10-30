using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Entities.Concrete;

namespace TodoApp.DAL.Concrete
{
    public class TodoAppContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=TodoAppDb;integrated security=true");

            

        }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }

        
    }
}
