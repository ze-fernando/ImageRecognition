using System.Linq;
using System.Text.RegularExpressions;
using ImageRecognition.Entity;
using ImageRecognition.Models;
using ImageRecognition.Models.Dtos;


namespace ImageRecognition.Services;

public class ImageService(GeminiService service, AppDbContext context)
{
    private readonly GeminiService _service = service;
    private readonly AppDbContext _context = context;

    public string? GenerateImage(string base64)
    {
        try
        {
            string fileName = $"{Guid.NewGuid().ToString()[..6]}.png";
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            string filePath = Path.Combine(directoryPath, fileName);
            
            string data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64, "");
            data = new Regex(@"\s+").Replace(data, "");

            byte[] imageBytes = Convert.FromBase64String(data);
            File.WriteAllBytes(filePath, imageBytes);

            return fileName;
      
        } catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex}");
            return null;
        }        
    }

    public async Task<Image> SendToGeminiAsync(string nameFile, ImageDto dto)
    {
        string result = await _service.SendImageAsync(nameFile, dto.MeasurementType);

        var img = new Image{
            CostumerCode = dto.CostumerCode,
            MeasurementType = dto.MeasurementType,
            ImageUrl = $"http://localhost:5066/images/{nameFile}",
            OcrResult = result,
            ProcessedAt = DateTime.Now
        };

        _context.Images.Add(img);
        await _context.SaveChangesAsync();

        return img;
    }

    public async Task<ICollection<Image>> GetImagesAsync()
    {
        ICollection<Image> images = _context.Images.ToList();

        return images;
    }
}
