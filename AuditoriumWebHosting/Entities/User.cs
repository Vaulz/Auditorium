namespace AuditoriumWebHosting.Entities
{
    public class User : BaseEntity
    {
        public AccountData AccountData { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }
    }
}