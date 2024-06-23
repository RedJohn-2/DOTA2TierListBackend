using DOTA2TierList.API.ServiceExtentions;
using DOTA2TierList.Persistence;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using DOTA2TierList.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DOTA2TierList.API.ServiceExtention;
using DOTA2TierList.API.Validation;
using DOTA2TierList.API.Mapping;
using AutoMapper;
using DOTA2TierList.Persistence.Mapping;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(typeof(DtoMappingProfile).Assembly, typeof(DaoMappingProfile).Assembly);
// Add services to the container.
var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));


builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddAuthenticationServices(builder.Configuration);

builder.Services.AddAutoMapper(typeof(DtoMappingProfile).Assembly, typeof(DaoMappingProfile).Assembly);

builder.Services.AddValidatorsFromAssemblyContaining<UserRequestValidator>();

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

app.UseExceptionHandling();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
