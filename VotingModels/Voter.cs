namespace VotingModels;

public record Voter
{
    public Voter(string id, string groupName, string surname, string name, string middleName, string email, DateTime birthdayDate)
    {
        Id = id;
        GroupName = groupName;
        Surname = surname;
        Name = name;
        MiddleName = middleName;
        Email = email;
        BirthdayDate = birthdayDate;
    }

    public Voter(string id)
    {
        Id = id;
    }

    public string Id { get; init; }
    public string? GroupName { get; init; }
    public string? Surname { get; init; }
    public string? Name { get; init; }
    public string? MiddleName { get; init; }
    public string? Email { get; init; }
    public DateTime? BirthdayDate { get; set; }

    public void Deconstruct(out string id, out string? groupName, out string? surname, out string? name,
        out string? middleName, out string? email, out DateTime? birthdayDate)
    {
        id = Id;
        groupName = GroupName;
        surname = Surname;
        name = Name;
        middleName = MiddleName;
        email = Email;
        birthdayDate = BirthdayDate;
    }
}