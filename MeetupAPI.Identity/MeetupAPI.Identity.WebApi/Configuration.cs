using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace MeetupAPI.Identity.WebApi;

public static class Configuration
{
    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>()
        {
            new ApiResource("MeetupWebAPI", "Web API", new [] {JwtClaimTypes.Name})
            {
                Scopes = {"MeetupWebAPI"}
            }
        };
    
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>()
        {
            new ApiScope("MeetupWebAPI", "Web API")
        };
    
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>()
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>()
        {
            new Client()
            {
                ClientId = "meetup-web-api",
                ClientName = "MeetupAPI",
                ClientSecrets =
                {
                    new Secret("ggggggggggggggggg".Sha256())
                },
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                AllowedScopes =
                {   
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "MeetupWebAPI"
                },
                
                RedirectUris =
                {
                    "http://localhost:5156/"
                },
                    /*
                PostLogoutRedirectUris =
                {
                    "https://localhost:7200/auth/login"
                },
                */
                AllowAccessTokensViaBrowser = true
            }
        };
}