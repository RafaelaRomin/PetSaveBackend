namespace PetSave.Application.Models.InputModels.v1;

public class PetUpdateInputModel
{
    public string? Name { get; set; } = default!;
    public string? Race { get; set; }
    public double? Weight { get; set; }
    public double? Age { get; set; } 
    public string? Description { get; set; }
}