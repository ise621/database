namespace Database.Data
{
    public sealed class User : Entity
    {
        public string Subject { get; private set; }
        public string Email { get; private set; }
        public string Name { get; private set; }

        public User(
            string subject,
            string email,
            string name
        )
        {
            Subject = subject;
            Email = email;
            Name = name;
        }

        public void Update(
            string email,
            string name
        )
        {
            Email = email;
            Name = name;
        }
    }
}