using Microsoft.EntityFrameworkCore;
using Practical_17.Data;
using Practical_17.Mappings;
using Practical_17.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddTransient<IStudentRepository, StudentRepository>(); // New instance every time
builder.Services.AddScoped<IStudentRepository, StudentRepository>(); // Same instance per request
//builder.Services.AddSingleton<IStudentRepository, StudentRepository>(); // Single instance for api


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
    pattern: "{controller=Students}/{action=Index}/{id?}");

app.Run();
