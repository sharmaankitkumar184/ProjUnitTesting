using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ProjUnitTesting.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
        }

        public virtual DbSet<ShoppingItem> ShoppingItem { get; set; } = null!;
    }
}
