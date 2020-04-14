using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomePotato.Models
{
    public class AwesomePotatoContext : DbContext
    {
        public AwesomePotatoContext(DbContextOptions<AwesomePotatoContext> builder) : base(builder)
        {

        }

        public DbSet<ErrorLogData> ErrorLogData { get; set; }
    }
}
