namespace AuditoriumWebHosting.Entities
{
    using System;

    public class KeyJournal : BaseEntity
    {
        public DateTime StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        public Booking Booking { get; set; }
    }
}