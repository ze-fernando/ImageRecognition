using ImageRecognition.Models.Dtos;
using ImageRecognition.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImageRecognition.Controllers;


[ApiController]
[Route("api")]
public class ImageController(ImageService service) : ControllerBase
{
    private readonly ImageService _service = service;

    [HttpPost]
    public async Task<IActionResult> SendImageAsync([FromBody] ImageDto img)
    {
        var fileName = _service.GenerateImage(img.Image64);

        if(string.IsNullOrEmpty(fileName))
            return BadRequest();

            
        var resultAnalysis = await _service.SendToGeminiAsync(fileName, img);
        
        return Ok(new {data = resultAnalysis});
    }

    [HttpGet("ocrs")]
    public async Task<IActionResult> GetImages()
    {
        var img = await _service.GetImagesAsync();

        return Ok(new {data = img});
    }
}