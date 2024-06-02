namespace PetSave.Domain.Entities.v1;

public class User(
    string fullName, 
    string city, 
    string state, 
    string email, 
    string phoneNumber,
    string password
    ) : BaseEntity
{
    public string FullName { get; private set; } = fullName;
    public string City { get; private set; } = city;
    public string State { get; private set; } = state;
    public string Email { get; private set; } = email;
    public string PhoneNumber { get; private set; } = phoneNumber;
    public string Password { get; private set; } = password;
    public List<Pet> Pets { get; }

    public static User Create(
        string fullName, 
        string city, 
        string state, 
        string email, 
        string phoneNumber,
        string password)
    {
        var user = new User(fullName, city, state, email, phoneNumber, password);

        return user;
    }

    public void Update(
        string fullName, 
        string city, 
        string state, 
        string email, 
        string phoneNumber,
        string password)
    {
        FullName = fullName;
        City = city;
        State = state;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
    }
}