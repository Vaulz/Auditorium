namespace AuditoriumWebHosting.Entities
{
    public class AccountData : BaseEntity
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}