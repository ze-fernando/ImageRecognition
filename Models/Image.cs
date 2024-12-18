namespace ImageRecognition.Models;

public class Image{
    public required int Id {get; set; }
    public required string ImageBase64 {get; set; }
    public required string CustomerCode {get; set; }
    public required string ImageUrl {get; set; }
    public required string OcrResult {get; set; }
    public required DateTime ProcessedA {get; set; }
};