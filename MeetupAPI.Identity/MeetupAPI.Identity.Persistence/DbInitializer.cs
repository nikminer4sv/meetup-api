using Microsoft.EntityFrameworkCore;

namespace MeetupAPI.Identity.Persistence;

public class DbInitializer
{
    public static void Initialize(DbContext context)
    {
        context.Database.EnsureCreated();
    }
}