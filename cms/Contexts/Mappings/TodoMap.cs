using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using cms.Models;

namespace cms.Contexts.Mappings
{
    public class ToDoMap : EntityTypeConfiguration<ToDo>
    {
        public ToDoMap()
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
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ToDoes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PersonId).HasColumnName("PersonId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ToDoDate).HasColumnName("ToDoDate");
            this.Property(t => t.Done).HasColumnName("Done");

            // Relationships
            this.HasOptional(t => t.Person)
                .WithMany(t => t.ToDoes)
                .HasForeignKey(d => d.PersonId);
        }
    }
}
