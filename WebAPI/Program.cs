using Microsoft.EntityFrameworkCore;
using WebAPI.Abstractions;
using WebAPI.DataAccess;
using WebAPI.Entities;
using WebAPI.Repositories;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);


builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        // options.UseNpgsql(@"host=localhost;port=5432;database=users_wpf_webapi;username=postgres;password=1234");
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(AppDbContext)));
    }
);
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRepository<Company>, CompanyRepository>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
