
using GetAgentsStatus.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetAgentsStatus.Connectors
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<WdrAgentAvailability> WdrAgentAvailabilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<WdrAgentAvailability>(entity =>
            {
                entity.HasKey(e => e.WdrAgentAvailabilityRowguid)
                    .HasName("PK_AgentAvailability");

                entity.ToTable("WDR_AgentAvailability");

                entity.HasIndex(e => e.Extension, "FK_AgentAvailability_Extension");

                entity.HasIndex(e => e.Status, "FK_AgentAvailability_Status");

                entity.Property(e => e.WdrAgentAvailabilityRowguid)
                    .ValueGeneratedNever()
                    .HasColumnName("WDR_AgentAvailabilityROWGUID");

                entity.Property(e => e.Extension).HasMaxLength(250);

                entity.Property(e => e.FirstName).HasMaxLength(250);

                entity.Property(e => e.LastName).HasMaxLength(250);

                entity.Property(e => e.QueueName).HasMaxLength(250);

                entity.Property(e => e.Status).HasMaxLength(250);

                entity.Property(e => e.Tag).HasMaxLength(250);

                entity.Property(e => e.TimeOfCreation).HasColumnType("datetime");

                entity.Property(e => e.TimeOfModification).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
