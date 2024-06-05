using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PetSave.Application.Services.Interfaces;
using PetSave.Application.Services.v1;
using PetSave.Domain.Repositories.Interfaces;
using PetSave.Infra;
using PetSave.Infra.Persistence.Repositories.v1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PetSaveDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PetSaveConnectionString")));

builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPetDonationRepository, PetDonationRepository>();
builder.Services.AddScoped<IPetDonationService, PetDonationService>();

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();  

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
    {
        new OpenApiSecurityScheme{
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
    }});
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();