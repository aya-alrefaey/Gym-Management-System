using Gym.BusinessLogic.Attachment;
using Gym.BusinessLogic.Mapper;
using Gym.BusinessLogic.Services;
using Gym.Data.contexts;
using Gym.DataAccess.Data.Identity;
using Gym.DataAccess.Repositories;
using Gym.DataAccess.UnitOfWork;
using Gym.DataSeeder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<ITrainerService, TrainerService>();
//builder.Services.AddScoped<IPlanRepository, PlanRepository>();
//builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();
//builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IHealthRecordService, HealthRecordService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(config => { }, typeof(MemberProfile).Assembly);
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IMembershipService, MembershipService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IAttachment, Attachment>();

builder.Services.AddDbContext<GymDbcontext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
  
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
   
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<GymDbcontext>()
    .AddDefaultTokenProviders(); ;
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(24);
    options.SlidingExpiration = true;
});
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

//var uploadsPath = Path.Combine(app.Environment.ContentRootPath, "Uploads");
//Directory.CreateDirectory(uploadsPath);
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(uploadsPath),
//    RequestPath = "/Uploads"
//});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await using (var scope = app.Services.CreateAsyncScope())
{
var context = scope.ServiceProvider.GetRequiredService<GymDbcontext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    await DataSeeder.SeedAllData(context, userManager, roleManager, config);
}


app.Run();
