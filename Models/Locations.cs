namespace MrDriverCore.Models
{
    public class Locations
    {
        public int Id { get; set; }
        public int Driver { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public LatLng LatLng { get; set; }
        public float Distance { get; set; }
        public int Stored { get; set; }
    }
}
