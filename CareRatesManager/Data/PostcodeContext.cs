using Microsoft.EntityFrameworkCore;
using CareRatesManager.Models;

namespace CareRatesManager.Data
{
    public class PostcodeContext : DbContext
    {
        public PostcodeContext(DbContextOptions<PostcodeContext> options) : base(options)
        { }

        public PostcodeContext() : base()
        { }

        public DbSet<PostcodeModel> Postcodes {get;set;}
        public DbSet<RatesModel> Rates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WCCDataDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            base.OnConfiguring(builder);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<PostcodeModel>().ToTable("PostcodeZones");
        //    modelBuilder.Entity<RatesModel>().ToTable("RateDetails");

        //    modelBuilder.Entity<RatesModel>()
        //        .HasKey(t => t.RateId);

        //    modelBuilder.Entity<PostcodeModel>()
        //        .HasOne(p => p.RateDetails)
        //        .WithMany(r => r.PostCodes)
        //        .IsRequired();

        //}


    }
}
