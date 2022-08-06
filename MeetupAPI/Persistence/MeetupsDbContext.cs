using MeetupAPI.Application.Interfaces;
using MeetupAPI.Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityTypeConfigurations;

namespace Persistence;

public sealed class MeetupsDbContext : DbContext, IMeetupsDbContext
{
    public DbSet<Meetup> Meetups { get; set; }

    public MeetupsDbContext(DbContextOptions<MeetupsDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new MeetupConfiguration());
        base.OnModelCreating(builder);
    }
}