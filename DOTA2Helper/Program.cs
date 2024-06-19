using DOTA2TierList.Application.Mappers;
using DOTA2TierList.Application.Services;
using DOTA2TierList.Logic.Store;
using DOTA2TierList.Persistence;
using DOTA2TierList.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));
builder.Services.AddScoped<IUserStore, UserRepository>();
builder.Services.AddSingleton<UserMapper>();
builder.Services.AddScoped<UserService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(name: "default", pattern: "{controller}/{action}/{id?}");

app.Run();
