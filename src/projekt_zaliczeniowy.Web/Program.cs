using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using projekt_zaliczeniowy.Core;
using projekt_zaliczeniowy.Infrastructure;
using projekt_zaliczeniowy.Infrastructure.Data;
using projekt_zaliczeniowy.Web;
using FastEndpoints;
using FastEndpoints.Swagger.Swashbuckle;
using FastEndpoints.ApiExplorer;
using Microsoft.OpenApi.Models;
using Serilog;
using Microsoft.AspNetCore.Identity;
using Autofac.Core;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
  options.CheckConsentNeeded = context => true;
  options.MinimumSameSitePolicy = SameSiteMode.None;
});

string? connectionString = builder.Configuration.GetConnectionString("SqliteConnection");  //Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext(connectionString!);

builder.Services.AddDbContext<IdentityDataContext>(options =>
          options.UseSqlite(connectionString));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<IdentityDataContext>();

//builder.Services.AddAuthentication(o =>
//{
//  o.DefaultScheme = IdentityConstants.ApplicationScheme;
//  o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
//})
//  .AddIdentityCookies(o => { });

//builder.Services.AddIdentityCore<ApplicationUser>(o =>
//{
//  o.Stores.MaxLengthForKeys = 128;
//})
//  .AddUserManager<UserManager<ApplicationUser>>()
//  .AddSignInManager<SignInManager<ApplicationUser>>()
//  .AddDefaultTokenProviders()
//  .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.ConfigureApplicationCookie(options =>
//{
//  // Cookie settings
//  options.Cookie.HttpOnly = true;
//  options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

//  options.LoginPath = "/Identity/Account/Login";
//  options.AccessDeniedPath = "/Identity/Account/AccessDenied";
//  options.SlidingExpiration = true;
//});


//builder.Services.AddControllersWithViews().AddNewtonsoftJson();
//builder.Services.AddRazorPages();
builder.Services.AddFastEndpoints();
builder.Services.AddFastEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
  c.EnableAnnotations();
  c.OperationFilter<FastEndpointsOperationFilter>();
});

// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
builder.Services.Configure<ServiceConfig>(config =>
{
  config.Services = new List<ServiceDescriptor>(builder.Services);

  // optional - default path to view services is /listallservices - recommended to choose your own path
  config.Path = "/listservices";
});


builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterModule(new DefaultCoreModule());
  containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
});

//builder.Logging.AddAzureWebAppDiagnostics(); add this if deploying to Azure

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseShowAllServicesMiddleware();
}
else
{
  app.UseExceptionHandler("/Home/Error");
  app.UseHsts();
}
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

//app.MapDefaultControllerRoute();
//app.MapRazorPages();

// Seed Database
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;

  try
  {
    var context = services.GetRequiredService<ApplicationDbContext>();
    //                    context.Database.Migrate();
    context.Database.EnsureCreated();
    SeedData.Initialize(services);
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
  }
}

app.Run();

// Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
public partial class Program
{
}
