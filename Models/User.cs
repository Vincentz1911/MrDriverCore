using System.Collections.Generic;

namespace MrDriverCore.Models
{
    public class User
    {
        public User()
        {
            Locations = new List<Locations>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public List<Locations> Locations { get; set; }
    }
}
