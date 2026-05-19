namespace Amazon.core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string street { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string AppUserId { get; set; }
       // public AppUser AppUser { get; set; }
    }
}