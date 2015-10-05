using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using cms.Models;

namespace cms.Contexts.Mappings
{
    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Lastname)
                .HasMaxLength(100);

            this.Property(t => t.Firstname)
                .HasMaxLength(100);

            this.Property(t => t.Street)
                .HasMaxLength(100);

            this.Property(t => t.StreetNumber)
                .HasMaxLength(10);

            this.Property(t => t.Area)
                .HasMaxLength(100);

            this.Property(t => t.PostalCode)
                .HasMaxLength(10);

            this.Property(t => t.HomePhone)
                .HasMaxLength(10);

            this.Property(t => t.Mobile)
                .HasMaxLength(10);

            this.Property(t => t.OtherPhone)
                .HasMaxLength(10);

            this.Property(t => t.Fax)
                .HasMaxLength(10);

            this.Property(t => t.Email)
                .HasMaxLength(100);

            this.Property(t => t.Remarks)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("People");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Lastname).HasColumnName("Lastname");
            this.Property(t => t.Firstname).HasColumnName("Firstname");
            this.Property(t => t.Street).HasColumnName("Street");
            this.Property(t => t.StreetNumber).HasColumnName("StreetNumber");
            this.Property(t => t.Area).HasColumnName("Area");
            this.Property(t => t.PostalCode).HasColumnName("PostalCode");
            this.Property(t => t.HomePhone).HasColumnName("HomePhone");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.OtherPhone).HasColumnName("OtherPhone");
            this.Property(t => t.Fax).HasColumnName("Fax");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
        }
    }
}
