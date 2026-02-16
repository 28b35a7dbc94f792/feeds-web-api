using FeedsWebApi.Data;
using FeedsWebApi.Factories;
using FeedsWebApi.Helpers;
using FeedsWebApi.Services;
using FeedsWebApi.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("SqliteDb")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFeedService, FeedService>();
builder.Services.AddScoped<IUserDtoValidator,UserDtoValidator>();
builder.Services.AddScoped<IFeedDtoValidator,FeedDtoValidator>();
builder.Services.AddScoped<IUserResponseDtoFactory, UserResponseDtoFactory>();
builder.Services.AddScoped<IFeedResponseDtoFactory, FeedResponseDtoFactory>();
builder.Services.AddScoped<IImageHelper, ImageHelper>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
