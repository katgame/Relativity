using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using relativityCalculator.Core.Models;
using relativityCalculator.Core.Contracts;
using Microsoft.Extensions.Options;
using System.Linq;

namespace relativityCalculator.Infrastructure.Models
{
    public partial class RelativitiesContext : DbContext
    {
		private readonly string _connectionString;
		private readonly IOptions<AppSettings> _config;

		public RelativitiesContext(IOptions<AppSettings> config)
		{
			_config = config;
			_connectionString = config.Value.SQLocal;
		}

		public virtual DbSet<AreasLookUp> AreasLookUp { get; set; }
        public virtual DbSet<Assessor> Assessor { get; set; }
        public virtual DbSet<AuditLog> AuditLog { get; set; }
        public virtual DbSet<AuditTrail> AuditTrail { get; set; }
        public virtual DbSet<CompanyType> CompanyType { get; set; }
        public virtual DbSet<Relativity> Relativity { get; set; }
        public virtual DbSet<RelativityLookUp> RelativityLookUp { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

			if (!optionsBuilder.IsConfigured)
            {
				#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.          
				optionsBuilder.UseSqlServer(_connectionString);
		
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
            });

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.Property(e => e.Changes).IsUnicode(false);

                entity.Property(e => e.ItemId)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeStamp).HasColumnType("datetime");

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

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DifferenceInCost)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExpectedSalvageRecovery)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IncidentYear)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PolicyNumber)
                    .HasMaxLength(50)
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

				entity.Property(e => e.Assessor)
				  .HasMaxLength(50)
				  .IsUnicode(false);
			});

            modelBuilder.Entity<CompanyType>(entity =>
            {
                entity.Property(e => e.CompanyTypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Relativity>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

				entity.Property(e => e.Active)
				  .HasMaxLength(1)
				  .IsUnicode(false);
			});

            modelBuilder.Entity<RelativityLookUp>(entity =>
            {
                entity.Property(e => e.RelativityId)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.RelativityKey)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RelativityValue)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }

		public override int SaveChanges()
		{
			var modifiedEntities = ChangeTracker.Entries()
				.Where(p => p.State == EntityState.Modified).ToList();
			var now = DateTime.UtcNow;
			string Id = string.Empty;
			foreach (var change in modifiedEntities)
			{
				var entityName = change.Entity.GetType().Name;
				foreach (var prop in change.OriginalValues.Properties)
				{
					
					if(getColumnName(prop.GetFieldName()) ==  "Id")
						Id = change.GetDatabaseValues().GetValue<object>(prop.Name).ToString();
					var originalValue = change.GetDatabaseValues().GetValue<object>(prop.Name).ToString();
					var currentValue = change.CurrentValues[prop].ToString();
					if (!originalValue.Equals(currentValue))
					{
						//var reason = change.OriginalValues.Properties.Where(x => x.Name == "UpdateReason").Select(y => y.ValueGenerated);
						var log = new AuditLog()
						{
							ItemId = Convert.ToInt32(Id),
							TableName = entityName,
							Changes = getColumnName(prop.GetFieldName()),
							ValueBefore = originalValue.ToString(),
							ValueAfter = currentValue,
							DateTimeStamp = now
						};
						AuditLog.Add(log);
						EfRepository<AuditLog>._auditLog = log;
					}
				}
			}
			return base.SaveChanges();
		}

		internal String getColumnName(string value)
		{
			int startPos = value.IndexOf('<') +1 ;
			int end = value.IndexOf('>') - 1;
			return value.Substring(startPos, end);
		}

	}

}
