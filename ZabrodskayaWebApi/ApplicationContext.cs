using Microsoft.EntityFrameworkCore;
using ZabrodskayaWebApi.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZabrodskayaWebApi
{
    public class ApplicationContext : DbContext
    {

        public DbSet<StandartType> StandartTypes { get; set; }

        public DbSet<Standart> Standarts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<XrefUserStandart> XrefUserStandarts { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<XrefUserStandart>()
                .HasKey(x => new { x.UserId, x.StandartId });

            modelBuilder.Entity<XrefUserStandart>()
                .HasOne(x => x.User)
                .WithMany(xus => xus.XrefUserStandarts)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<XrefUserStandart>()
                .HasOne(x => x.Standart)
                .WithMany(xus => xus.XrefUserStandarts)
                .HasForeignKey(x => x.StandartId);
        }
    }
}
