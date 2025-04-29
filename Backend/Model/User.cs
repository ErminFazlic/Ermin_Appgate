namespace Backend.Model
{
    public class User(string username, string password)
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = username;
        public string Password { get; set; } = password;
    }
}
