using MVC_Code_First.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Lets Register the UCompanyContext in DI and Read the Connection String
// SO that dotnet cli will generate the Database and Tables in Database Server

builder.Services.AddDbContext<UCompanyContext>(options =>
{
    // UseSqlServer() will accept the ConnecitonString for SQL Server Only
    // builder.Configuration: WIll Read appSettings.json file
    // GetConnectionString() method will read the 'ConnectionStrings' section
    // from appSettings.json
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnString"));
});


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

