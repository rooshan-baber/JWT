namespace JWT
{
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        public string refreshToken { get; set; } = string.Empty;
        public DateTime tokenCreated { get; set; } = DateTime.Now;
        public DateTime tokenExpired { get; set; }
    }
}
