using MeetupAPI.Identity.Domain;
using MeetupAPI.Identity.Persistence;
using MeetupAPI.Identity.WebApi;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 4;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<ApplicationUser>()
    .AddInMemoryApiResources(Configuration.ApiResources)
    .AddInMemoryApiScopes(Configuration.ApiScopes)
    .AddInMemoryIdentityResources(Configuration.IdentityResources)
    .AddInMemoryClients(Configuration.Clients)
    .AddDeveloperSigningCredential();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "MeetupApi.Identity.Cookie";
    config.LoginPath = "/auth/Login";
    config.LogoutPath = "/auth/Logout";
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<AuthDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception exception)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, "An error occured while application initializing.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    IdentityModelEventSource.ShowPII = true; 
}

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Auth}/{action=Login}/{id?}"
        );
});

app.Run();