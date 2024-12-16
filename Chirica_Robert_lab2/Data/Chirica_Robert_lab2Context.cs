using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Chirica_Robert_lab2.Models;

namespace Chirica_Robert_lab2.Data
{
    public class Chirica_Robert_lab2Context : DbContext
    {
        public Chirica_Robert_lab2Context (DbContextOptions<Chirica_Robert_lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Chirica_Robert_lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Chirica_Robert_lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Chirica_Robert_lab2.Models.Author> Author { get; set; } = default!;
        public DbSet<Chirica_Robert_lab2.Models.Category> Category { get; set; } = default!;
    }
}
