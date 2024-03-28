namespace PetSave.Domain.Entities.v1;

public class User(string fullName, string city, string state, string email, string phoneNumber)
    : BaseEntity
{
    public string FullName { get; private set; } = fullName;
    public string City { get; private set; } = city;
    public string State { get; private set; } = state;
    public string Email { get; private set; } = email;
    public string PhoneNumber { get; private set; } = phoneNumber;
    public List<Pet> Pets { get; private set; }

    public static User Create(
        string fullName, 
        string city, 
        string state, 
        string email, 
        string phoneNumber)
    {
        var user = new User(fullName, city, state, email, phoneNumber);

        return user;
    }

    public void Update(
        string fullName, 
        string city, 
        string state, 
        string email, 
        string phoneNumber)
    {
        FullName = fullName;
        City = city;
        State = state;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}