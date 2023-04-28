namespace Database.Data
{
    public sealed class User : Entity
    {
        public string Subject { get; private set; }
        public string Name { get; private set; }

        public User(
            string subject,
            string name
        )
        {
            Subject = subject;
            Name = name;
        }

        public void Update(
            string name
        )
        {
            Name = name;
         }
    }
}