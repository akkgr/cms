using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using cms.Models;

namespace cms.Contexts.Mappings
{
    public class JobMap : EntityTypeConfiguration<Job>
    {
        public JobMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.PersonId)
                .HasMaxLength(100);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            this.Property(t => t.Remarks)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Jobs");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PersonId).HasColumnName("PersonId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Implemented).HasColumnName("Implemented");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.Remarks).HasColumnName("Remarks");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(t => t.Jobs)
                .HasForeignKey(d => d.PersonId);
        }
    }
}
