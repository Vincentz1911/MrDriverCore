using System.Collections.Generic;

namespace MrDriverCore.Models
{
    public class User
    {
        public User()
        {
            Location = new List<Location>();
        }

        public User(int id, string name, string password)
        {
            Id = id;
            Name = name;
            Password = password;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public List<Location> Location { get; set; }
        public string Name { get; }
    }
}
