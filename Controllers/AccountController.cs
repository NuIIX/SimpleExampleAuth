using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleExampleAuth.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleExampleAuth.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserContext _db;
        private const string _passwordHash = "=5rfQA31gutUm?Cq%HFjRHwk2!>7*>q.C7Hm2*qM(c:ue?.wfX[=m<mB^TX6)^?@";

        public AccountController(UserContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<IActionResult> Registration()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public IResult LoginAccses(User loginData)
        {
            var passwordHash = CreatePasswordHash(loginData.Password);

            User? person = _db.Users.FirstOrDefault(p => p.Email == loginData.Email && p.Password == passwordHash);
            if (person is null) return Results.Unauthorized();

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Email) };
            var jwt = new JwtSecurityToken(
                    issuer: JWTAuthOptions.ISSUER,
                    audience: JWTAuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(JWTAuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = person.Email
            };

            return Results.Json(response);
        }

        [HttpPost]
        public IActionResult GitHubLogin()
        {
            var redirectUrl = Url.Action("GitHubResponse", "Account");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, "GitHub");
        }

        public async Task<IActionResult> GitHubResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (result?.Principal != null)
            {
                var claims = result.Principal.Identities
                                .FirstOrDefault()?.Claims.Select(claim => new
                                {
                                    claim.Type,
                                    claim.Value
                                });

                var nicknameClaim = claims?.FirstOrDefault(c => c.Type == "urn:github:name");
                var nickname = nicknameClaim?.Value;

                if (!string.IsNullOrEmpty(nickname))
                {
                    Response.Cookies.Append("GitHubNickname", nickname, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict
                    });
                }

                return RedirectToAction("Login", "Account");
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult CheckGitHubSSO()
        {
            var githubCookie = Request.Cookies[".AspNetCore.Cookies"];
            var githubNickname = Request.Cookies["GitHubNickname"];

            if (!string.IsNullOrEmpty(githubCookie))
            {
                return Json(new { isAuthenticated = true, nickname = githubNickname, message = "User is authenticated via GitHub SSO." });
            }

            return Json(new { isAuthenticated = false, message = "User is not authenticated via GitHub SSO." });
        }

        [HttpPost]
        public IActionResult LogoutGitHubSSO()
        {
            if (Request.Cookies.ContainsKey(".AspNetCore.Cookies"))
            {
                Response.Cookies.Delete(".AspNetCore.Cookies");
                Response.Cookies.Delete("GitHubNickname");
            }

            return Json(new { success = true, message = "User logged out successfully." });
        }


        [HttpPost]
        public async Task<IActionResult> RegSumbit(User user)
        {
            user.Password = CreatePasswordHash(user.Password);

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return RedirectToAction("Login", "Account");
        }

        public static string CreatePasswordHash(string password)
        {
            byte[] passwordHashByte;
            using (var hmac = new System.Security.Cryptography.HMACSHA512(Encoding.UTF8.GetBytes(_passwordHash)))
            {
                passwordHashByte = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            return BitConverter.ToString(passwordHashByte).Replace("-", "").ToLower();
        }
    }
}
