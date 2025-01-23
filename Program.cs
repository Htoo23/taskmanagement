using Microsoft.EntityFrameworkCore;
using TaskmanagementSystem.Application.UseCases;
using TaskmanagementSystem.Core.Entities;
using TaskmanagementSystem.Infrastructure.Data;
using TaskmanagementSystem.Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);






builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepository<TaskItem>, TaskRepository>();
builder.Services.AddScoped<UpdateTaskProgressUseCase>();



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRepository<TaskItem>, TaskRepository>();

builder.Services.AddScoped<GetTasksUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();
app.MapControllerRoute(
     name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
