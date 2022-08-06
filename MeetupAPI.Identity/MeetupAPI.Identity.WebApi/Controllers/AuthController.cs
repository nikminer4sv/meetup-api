using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityServer4.Services;
using MeetupAPI.Identity.Domain;
using MeetupAPI.Identity.WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MeetupAPI.Identity.WebApi.Controllers;

public class AuthController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IIdentityServerInteractionService _interactionService;

    public AuthController(SignInManager<ApplicationUser> signInManager, 
        UserManager<ApplicationUser> userManager,
        IIdentityServerInteractionService interactionService) => 
        (_signInManager, _userManager, _interactionService) = 
        (signInManager, userManager, interactionService);

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet]
    [Authorize]
    public IActionResult Token()
    {
        return Content(Request.Cookies["access_token"]);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var user = await _userManager.FindByNameAsync(viewModel.Username);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "User not found");
            return View(viewModel); 
        }

        var result = await _signInManager.PasswordSignInAsync(viewModel.Username,
            viewModel.Password, false, false);

        if (result.Succeeded)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GetToken(authClaims);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            Response.Cookies.Append("access_token", jwtToken);
            return Content($"Your token is: {jwtToken}");
        }

        ModelState.AddModelError(string.Empty, "Login failed");
        return View(viewModel);

    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var user = new ApplicationUser()
        {
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            UserName = viewModel.Username,
        };

        var result = await _userManager.CreateAsync(user, viewModel.Password);
        if (result.Succeeded)
        {
            LoginViewModel loginViewModel = new LoginViewModel()
            {
                Username = viewModel.Username,
                Password = viewModel.Password
            };
            return await Login(loginViewModel);
        }

        ModelState.AddModelError(string.Empty, "Error occured");
        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/auth/Login");
    }
    
    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ggggggggggggggggg"));

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            audience: "MeetupWebAPI",
            issuer: "https://localhost:7206",
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
    
}