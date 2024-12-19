using System.Buffers.Text;
using DotnetGeminiSDK.Client.Interfaces;
using DotnetGeminiSDK.Model;
using ImageRecognition.Models.Enums;
using Sprache;

namespace ImageRecognition.Services;

public class GeminiService(IGeminiClient geminiClient)
{
    private readonly IGeminiClient _geminiClient = geminiClient;

    public async Task<string> SendImageAsync(string nameFile, Measurement type)
    {
        string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
        string image = Path.Combine(directoryPath, nameFile);

        var bytes = File.ReadAllBytes(image);
        string message = @$"
                this image is a picture of a measuring equipment for {type} consumption,
                take care to get only the numbers of consumption window, do not get the numbers of the serial number or any other number,
                if has more than one number, get only the biggest number,
                all number must be in the same color, if the number is in a different color, ignore it,
                only numbers; no spaces, no commas, no dots, no letters, no symbols, no units, no other characters,
                if you cannot read the numbers, write 'unreadable'
        ";
        var res = await _geminiClient.ImagePrompt(message, bytes, ImageMimeType.Png);
        
        
        return res.Candidates[0].Content.Parts[0].Text;
    }
}
