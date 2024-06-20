using DOTA2TierList.API.ServiceExtentions;
using DOTA2TierList.Application.Services;
using DOTA2TierList.Logic.Store;
using DOTA2TierList.Persistence;
using DOTA2TierList.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using DOTA2TierList.Application.Validation;
using DOTA2TierList.API.Contracts.UserContracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<IUserRequest>();
builder.Services.AddUserService();
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


app.Run();
