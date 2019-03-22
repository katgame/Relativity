using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using relativityCalculator.Core.Models;

namespace relativityCalculator.Infrastructure.Models
{
    public partial class RelativitiesContext : DbContext
    {
        public virtual DbSet<AreasLookUp> AreasLookUp { get; set; }
        public virtual DbSet<Assessor> Assessor { get; set; }
        public virtual DbSet<AuditLog> AuditLog { get; set; }
        public virtual DbSet<AuditTrail> AuditTrail { get; set; }
        public virtual DbSet<CompanyType> CompanyType { get; set; }
        public virtual DbSet<RelativityConfig> RelativityConfig { get; set; }
        public virtual DbSet<RelativityLookUp> RelativityLookUp { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=19289JNBMIT001\SQLEXPRESS;Database=Relativities;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AreasLookUp>(entity =>
            {
                entity.Property(e => e.Area)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Assessor>(entity =>
            {
                entity.Property(e => e.AddressLine1)
                    .HasColumnName("addressLine1")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasColumnName("addressLine2")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CellNumber)
                    .HasColumnName("cellNumber")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .HasColumnName("companyName")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyRegNo)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DeviceUsed)
                    .HasColumnName("deviceUsed")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddress)
                    .HasColumnName("emailAddress")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasColumnName("postalCode")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.WorkNumber)
                    .HasColumnName("workNumber")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.CompanyTypeNavigation)
                    .WithMany(p => p.Assessor)
                    .HasForeignKey(d => d.CompanyType)
                    .HasConstraintName("FK__Assessor__Compan__1CF15040");
            });

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.Property(e => e.AuditActionTypeEnum).HasColumnName("AuditActionTypeENUM");

                entity.Property(e => e.Changes).IsUnicode(false);

                entity.Property(e => e.DataModel)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeStamp).HasColumnType("datetime");

                entity.Property(e => e.KeyFieldId).HasColumnName("KeyFieldID");

                entity.Property(e => e.ValueAfter).IsUnicode(false);

                entity.Property(e => e.ValueBefore).IsUnicode(false);
            });

            modelBuilder.Entity<AuditTrail>(entity =>
            {
                entity.Property(e => e.AdditionalCosts)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Area)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimNumer)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Comments)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CostOfSalvage)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DifferenceInCost)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExpectedSalvageRecovery)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IncidentYear)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Recommendation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RepairCost)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TotalCostRepair)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TotalSalvageRecovery)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VehicleMake)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VehicleMass)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VehicleModel)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VehicleSumInsured)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VehicleYear)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CompanyType>(entity =>
            {
                entity.Property(e => e.CompanyType1)
                    .HasColumnName("CompanyType")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RelativityConfig>(entity =>
            {
                entity.Property(e => e.PropertyGroup)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PropertyName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PropertyValue)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RelativityLookUp>(entity =>
            {
                entity.Property(e => e.RelativityKey)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RelativityName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.RelativityValue)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
