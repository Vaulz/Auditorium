namespace AuditoriumWebHosting.Entities
{
    using System;

    public class Booking : BaseEntity
    {
        public User User { get; set; }
        
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public Auditory Auditory { get; set; }
    }
}