using System.Text.Json.Serialization;
using DotNetEnv;
using DotnetGeminiSDK;
using ImageRecognition.Config;
using ImageRecognition.Entity;
using ImageRecognition.Services;
using Microsoft.EntityFrameworkCore;

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
        });

builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseNpgsql(Settings.Connection));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// using (var scope = app.Services.CreateScope())
// {
//     var geminiService = scope.ServiceProvider.GetRequiredService<GeminiService>();
//     await geminiService.Example();
// }
app.MapControllers();
app.Run();