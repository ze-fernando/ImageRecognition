using ImageRecognition.Models.Enums;

namespace ImageRecognition.Models.Dtos;

public record ImageDto(
    string Image64,
    string CostumerCode,
    Measurement MeasurementType
);