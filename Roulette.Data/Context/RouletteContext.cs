using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roulette.Data.Context
{
    public class RouletteContext : DbContext
    {
        public RouletteContext(DbContextOptions<RouletteContext> options)
           : base(options)
        {
        }
        public DbSet<Entities.Roulette> Roulette { get; set; }
        public DbSet<Entities.Bet> Bet { get; set; }
        public DbSet<Entities.Client> Client { get; set; }
        public DbSet<Entities.User> User { get; set; }
    }
}
