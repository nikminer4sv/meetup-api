using MeetupAPI.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigurations;

public class MeetupConfiguration : IEntityTypeConfiguration<Meetup>
{
    public void Configure(EntityTypeBuilder<Meetup> builder)
    {
        builder.HasKey(meetup => meetup.Id);
        builder.HasIndex(meetup => meetup.Id).IsUnique();
        builder.Property(meetup => meetup.Title).HasMaxLength(128);
        builder.Property(meetup => meetup.Description).HasMaxLength(256);
        builder.Property(meetup => meetup.Organizer).HasMaxLength(64);
        builder.Property(meetup => meetup.Place).HasMaxLength(64);
    }
}