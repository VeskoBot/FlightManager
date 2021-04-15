using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManager.Models;

namespace FlightManager.Data
{
    public class FlightsManagerDBContext : DbContext
    {
        public FlightsManagerDBContext(DbContextOptions<FlightsManagerDBContext> options) : base(options)
        {

        }
        public DbSet<User> UsersSet { get; set; }
        public DbSet<Flights> FlightsSet { get; set; }
        public DbSet<Reservations> ReservationsSet { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservations>()
                .HasOne(p => p.Flight)
                .WithMany(b => b.ReservationsList)
                .HasForeignKey(p => p.FlightId)
                .HasConstraintName("ForeignKey_Flights_Reservations");
        }
    }
}
