using Gym_Project.IService;
using Gym_Project.Models;
using Gym_Project.Models.Dao;
using Microsoft.EntityFrameworkCore;
using ProGCoder_MomoAPI.Models.Momo;
using ProGCoder_MomoAPI.Services;

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
builder.Services.AddScoped<IMemberPackageService, MemberPackageDao>();
builder.Services.AddScoped<IMemberService, MemberDao>();
builder.Services.AddScoped<IPaymentService, PaymentDao>();
builder.Services.AddScoped<IClassPackService, ClassPackDao>();
builder.Services.AddScoped<IFeedBackService, FeedBackDao>();

builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<IMomoService, MomoService>();
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
