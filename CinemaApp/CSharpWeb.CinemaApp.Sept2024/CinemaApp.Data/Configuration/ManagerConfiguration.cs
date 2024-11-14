using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Data.Configuration
{
    using static Common.EntityValidationConstants.Manager;
    public class ManagerConfiguration:IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
           builder.HasKey(m => m.Id);  
           
           builder.Property(m => m.WorkPhoneNumber)
                .IsRequired()
                .HasMaxLength(WorkPhoneNumberMaxlLength);
            
            builder.Property(m => m.UserId)
                .IsRequired();

            builder.HasOne(m => m.User)
                .WithOne()
                .HasForeignKey<Manager>(m => m.UserId);
        }
    }
}
