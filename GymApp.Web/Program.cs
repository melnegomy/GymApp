using GymApp.BLL.Contracts;
using GymApp.BLL.Services;
using GymApp.DAL.Context;
using GymApp.DAL.Contracts;
using GymApp.DAL.Models;
using GymApp.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


/*
//builder.Services.AddKeyedScoped<IPlansRepository, PlansMockRepository>("MOCK");
//builder.Services.AddScoped<IPlansRepository, PlansMockRepository>();
//builder.Services.AddScoped<IPlansRepository, PlansRepository>();

//builder.Services.AddScoped<GymDbContext>();
//builder.Services.AddKeyedScoped<IPlansRepository, PlansRepository>("REAL");
*/

builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPlansService, PlansService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddDbContext<GymDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
