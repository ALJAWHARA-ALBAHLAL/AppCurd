using AppCurd.Controllers;
using AppCurd.Models;
using AppCurd.Repositories.Contract;
using AppCurd.Repositories.Implementation;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IGenericRepository<Employees>,EmployeeRepository>();
builder.Services.AddScoped<IGenericRepository<Departments>, DepartmentRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
