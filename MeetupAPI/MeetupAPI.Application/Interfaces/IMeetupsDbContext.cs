using MeetupAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace MeetupAPI.Application.Interfaces;

public interface IMeetupsDbContext
{
    DbSet<Meetup> Meetups { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}