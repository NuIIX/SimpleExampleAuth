using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimpleExampleAuth.Controllers;
using SimpleExampleAuth.Models;


namespace SimpleExampleAuth
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionCar = builder.Configuration.GetConnectionString("DefaultConnectionCar");
            string connectionUser = builder.Configuration.GetConnectionString("DefaultConnectionUser");

            builder.Services.AddDbContext<CarContext>(options => options.UseSqlServer(connectionCar));
            builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionUser));

            builder.Services.AddTransient<AccountController>();

            builder.Services.AddMvc();

            builder.Services.AddSingleton<IRedisCacheService, RedisCacheService>();

            builder.Services.AddAuthorization(options =>
            {
                var defaultPolicy = new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme, "JwtBearer")
                    .RequireAuthenticatedUser()
                    .Build();

                options.DefaultPolicy = defaultPolicy;
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = JWTAuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = JWTAuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = JWTAuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            })
            .AddGitHub(options =>
            {
                options.ClientId = builder.Configuration["Authentication:GitHub:ClientId"];
                options.ClientSecret = builder.Configuration["Authentication:GitHub:ClientSecret"];
                options.CallbackPath = "/signin-github";
            });

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            using var serviceScope = app.Services.CreateScope();

            #region Заполнить БД

            //var contextCar = serviceScope.ServiceProvider.GetRequiredService<CarContext>();
            //await CarsGenerator.GenerateRandomCarsAsync(contextCar, 1000);

            //var contextUser = serviceScope.ServiceProvider.GetRequiredService<UserContext>();
            //await UsersGenerator.GenerateRandomUsersAsync(contextUser, 1000);

            #endregion

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            var accountController = serviceScope.ServiceProvider.GetRequiredService<AccountController>();
            app.MapPost("/Account/LoginAccses", accountController.LoginAccses);

            app.Map("/data", [Authorize] () => new { message = "Авторизация пройдена!" });

            app.Run();
        }
    }
}