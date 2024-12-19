using System.Text.Json.Serialization;
using DotNetEnv;
using DotnetGeminiSDK;
using ImageRecognition.Config;
using ImageRecognition.Entity;
using ImageRecognition.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
              options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<GeminiService>();

builder.Services.AddGeminiClient(config =>
    {
        config.ApiKey = Settings.GeminiKey;
        config.ImageBaseUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash"; 
    });

builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseNpgsql(Settings.Connection));
    
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles(
    new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
        RequestPath = "/images"
    });
app.MapControllers();
app.Run();