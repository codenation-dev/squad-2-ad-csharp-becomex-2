using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomePotato.Models
{
    public class AwesomePotatoContext : DbContext
    {
        public DbSet<ErrorLogData> ErrorLogData { get; set; }
    }
}
