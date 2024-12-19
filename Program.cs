using System.Text.Json.Serialization;
using DotNetEnv;
using DotnetGeminiSDK;
using ImageRecognition.Config;
using ImageRecognition.Entity;
using ImageRecognition.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

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

builder.Services.AddSwaggerGen(sg =>
{
    sg.SwaggerDoc("v1", new OpenApiInfo { Title = "Measurement Recognition", Version = "v1" });

});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(sg =>
    {
        sg.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        sg.RoutePrefix = "";
    });
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