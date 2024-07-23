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
using Microsoft.Owin.Host.SystemWeb;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(typeof(DtoUserMappingProfile).Assembly, typeof(DaoMappingProfile).Assembly);
// Add services to the container.
var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));


builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.Configure<SteamAuthOptions>(builder.Configuration.GetSection(nameof(SteamAuthOptions)));

builder.Services.AddAuthenticationServices(builder.Configuration);

builder.Services.AddAntiforgery();

builder.Services.AddAutoMapper(typeof(DtoUserMappingProfile).Assembly, typeof(DaoMappingProfile).Assembly);

builder.Services.AddValidatorsFromAssemblyContaining<UserRequestValidator>();

builder.Services.AddUserService();
builder.Services.AddTierListService();

builder.Services.AddControllers();

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policy => {
        policy.WithOrigins("http://localhost:5173");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});
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

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();


app.MapControllers();


app.Run();
