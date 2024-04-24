namespace Database.Data;

public sealed class User : Entity
{
    public User(
        string subject,
        string name
    )
    {
        Subject = subject;
        Name = name;
    }

    public string Subject { get; private set; }
    public string Name { get; private set; }

    public void Update(
        string name
    )
    {
        Name = name;
    }
}