using PetSave.Domain.Enums.v1;

namespace PetSave.Domain.Entities.v1;

public class Pet(
    string name, 
    string race, 
    Species species, 
    double weight, 
    double age, 
    string? description, 
    Guid idTutor
    ) : BaseEntity
{
    public string Name { get; private set; } = name;
    public string Race { get; private set; } = race;
    public Species Species { get; private set; } = species;
    public double Weight { get; private set; } = weight;
    public double Age { get; private set; } = age;
    public string? Description { get; private set; } = description;
    public DateTime? LastDonation { get; }
    public DonationStatus Status { get; set; } = DonationStatus.Available;
    public Guid IdTutor { get; private set; } = idTutor;
    public User Tutor { get; private set; }
    public List<PetDonation> Donations { get; set; } = [];
    
    public static Pet Create(
        string name,
        string race,
        Species species,
        double weight,
        double age,
        string? description,
        Guid idTutor)
    {
        var pet = new Pet(name, race, species, weight, age, description, idTutor);

        return pet;
    }

    public void Update(string name,
        string race,
        double weight,
        double age,
        string? description)
    {
        Name = name;
        Race = race;
        Weight = weight;
        Age = age;
        Description = description;
    }

    public void UpdateUnableStatus()
    {
        Status = DonationStatus.Unable;
    }

    public void UpdateAvailableStatus()
    {
        Status = DonationStatus.Available;
    }
}
