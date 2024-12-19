using System.ComponentModel.DataAnnotations;
using ImageRecognition.Models.Enums;

namespace ImageRecognition.Models;

public class Image{
    
    [Key]
    public required int Id {get; set; }
    
    public required string ImageBase64 {get; set; }
    
    public required Measurement MeasurementType {get; set; }
    
    public required string ImageUrl {get; set; }
    
    public required string OcrResult {get; set; }
    
    public required DateTime ProcessedAt {get; set; }
};