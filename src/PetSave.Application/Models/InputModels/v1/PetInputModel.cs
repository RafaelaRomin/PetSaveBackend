using PetSave.Domain.Enums.v1;

namespace PetSave.Application.Models.InputModels.v1;

public class PetInputModel
{
    public required string Name { get; set; } = default!;
    public required string Race { get; set; }
    public Species Species { get; set; } = default!;
    public double Weight { get; set; }
    public double Age { get; set; } 
    public string? Description { get; set; }
    public Guid IdTutor { get; set; }
}