using BitTrade;
using BitTrade.Configurations;
using BitTrade.Data;
using BitTrade.DataAccess;
using BitTrade.Identity;
using BitTrade.Methods;
using BitTrade.Model;
using BitTrade.Respository;
using BitTrade.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Radzen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRadzenComponents();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<ITransactionService, TransactionService>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddTransient<ICoinGeckoService, CoinGeckoService>();
builder.Services.AddScoped<ICoinGeckoService, CoinGeckoService>();
builder.Services.AddScoped<CurrencyInfoService>();
builder.Services.AddScoped<BitCoinConverter>();
builder.Services.AddHttpClient<CurrencyInfoService>();
builder.Services.AddDbContext<BitTradeDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("BitTradeDB")));
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
//{
//    options.Password.RequireDigit = false;
//    options.Password.RequiredLength = 5;
//    options.Password.RequireLowercase = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequireNonAlphanumeric = false;
//    options.SignIn.RequireConfirmedAccount = false;
//}).AddEntityFrameworkStores<BitTradeDbContext>();

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//}).AddCookie(options =>
//    {
//        options.Cookie.Name = "YourCookieName";
//        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
//        options.SlidingExpiration = true;
//    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = (context) =>
            {
                if (context.Request.Path.StartsWithSegments("/hubs/zaptime-chat"))
                {
                    var jwt = context.Request.Query["access_token"];
                    if (!string.IsNullOrWhiteSpace(jwt))
                    {
                        context.Token = jwt;
                    }
                }
                return Task.CompletedTask;
            }
        };
    });

//builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


//app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
