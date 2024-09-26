using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context:IdentityDbContext<Staff,AppRole,int>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server=LAPTOP-DEQQ0OF8\\SQLEXPRESS;database=TravelStaffDB;integrated security=true;");
        //}

        public DbSet<Travel> Travels { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Staff>()
               .HasMany(s => s.Travels)
               .WithOne(t => t.Staff)
               .HasForeignKey(t => t.StaffID)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Staff>()
                .HasOne(s => s.Admin)
                .WithMany(a => a.Staffs)
                .HasForeignKey(s => s.AdminID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Travel>()
                .HasOne(t => t.Admin)
                .WithMany(a => a.TravelAdmins)
                .HasForeignKey(t => t.AdminID)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<Travel>()
                .HasOne(t => t.Status)
                .WithMany(s => s.Travels)
                .HasForeignKey(t => t.StatusID)
                .OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Travel>()
			   .HasMany(t => t.Messages)
			   .WithOne(m => m.Travel)
			   .HasForeignKey(m => m.TravelID)
			   .OnDelete(DeleteBehavior.NoAction);
		}
    }
}