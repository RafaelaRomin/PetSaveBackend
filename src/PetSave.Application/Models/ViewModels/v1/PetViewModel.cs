namespace PetSave.Application.Models.ViewModels.v1;

public class PetViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Specie { get; set; }
    public string Race { get; set; }
    public double Age { get; set; }
    public string Description { get; set; }
    public double Weight { get; set; }
    public string DonationStatus { get; set; }
    public string TutorName { get; set; }
    public string TutorPhoneNumber { get; set; }
}