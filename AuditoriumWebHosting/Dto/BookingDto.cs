namespace AuditoriumWebHosting.Dto
{
    using System;

    public class BookingDto
    {
        public long UserId { get; set; }

        public long AuditoryId { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}