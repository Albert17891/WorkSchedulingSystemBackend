using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkSchedulingSystem.Domain.Entities;

public class JobEntityConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("Jobs");

        builder.HasKey(j => j.Id);

        builder.Property(j => j.Id)
            .ValueGeneratedNever();

        builder.Property(j => j.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(j => j.Description)
            .HasMaxLength(2000);

        builder.Property(j => j.Duration)
            .IsRequired();

        builder.Property(j => j.ScheduledDate)
            .IsRequired();

        builder.Property(j => j.AssignedUserId)
            .IsRequired();
    }
}
