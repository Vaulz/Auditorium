namespace AuditoriumWebHosting.Dto
{
    public class UserBookingDto
    {
        public int Id { get; set; }
        
        public string AuditoryNumber { get; set; }
        
        public string Date { get; set; }

        public string Time { get; set; }
    }
}