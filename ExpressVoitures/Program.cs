using ExpressVoitures;
using ExpressVoitures.DbInitializer;
using ExpressVoitures.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Setup DB connexion
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ExpressVoituresContext>(options =>
    options.UseSqlServer(connectionString));

// Setup Error Page from DB <--> EFCore
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ExpressVoituresContext>();

// Setup Razor Pages
builder.Services.AddControllersWithViews();

// Setup Services Scope
builder.Services.AddScoped<IExpressVoituresService, ExpressVoituresService>();

var app = builder.Build();

/**************************************************************/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Seed the database with initial data
app.SeedDataBase();

// Seed the admin user
await app.SeedAdmin();

// Setup HTTPS redirection and static files
app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
