using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkSchedulingSystem.Domain.Entities;

public class ScheduleEntityConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable("Schedules");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedNever();      

        builder.Property(s => s.UserId)
            .IsRequired();

        builder.Property(s => s.Date)
            .IsRequired();

        builder.Property(s => s.Status)
            .IsRequired()
            .HasConversion<int>();      

       
        builder.HasMany(s => s.Jobs)
               .WithOne()
               .HasForeignKey(j => j.AssignedUserId)   
               .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(s => s.Jobs)
               .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
