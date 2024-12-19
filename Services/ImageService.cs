using System.Text.RegularExpressions;
using ImageRecognition.Models;
using ImageRecognition.Models.Enums;


namespace ImageRecognition.Services;

public class ImageService
{
    public bool GenerateImage(string base64)
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

            return true; 
      
        } catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex}");
            return false;
        }
        
    }
}
