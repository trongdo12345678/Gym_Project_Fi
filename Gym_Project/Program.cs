using Gym_Project.IService;
using Gym_Project.Models;
using Gym_Project.Models.Dao;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSession();

builder.Services.AddDbContext<GymProjectContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ITrainerService, TrainerDao>();
builder.Services.AddScoped<IAccountService, AccountDao>();
builder.Services.AddScoped<IPackageService, PackageDao>();
builder.Services.AddScoped<IAttendanceService, AttendanceDao>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
	name: "Admin",
	pattern: "{area:exists}/{controller}/{action}");
app.MapControllerRoute(
	name: "default",
	pattern: "{controller}/{action}");
app.MapControllerRoute(
  name: "default",
  pattern: "{controller}/{action}/{page?}"
);
app.Run();
