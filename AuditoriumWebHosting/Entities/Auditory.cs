namespace AuditoriumWebHosting.Entities
{
    using System;

    public class Auditory : BaseEntity
    {
        public string Number { get; set; }

        public int Capacity { get; set; }

        public string AdditionalInfo { get; set; }

        public Guid Key { get; set; }
    }
}