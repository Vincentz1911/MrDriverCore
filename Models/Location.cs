namespace MrDriverCore.Models
{
    public class Location
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        //public float Distance { get; set; }
        //public int Stored { get; set; }
    }
}
