namespace Persistence;

public class DbInitializer
{
    public static void Initialize(MeetupsDbContext context) => context.Database.EnsureCreated();
}