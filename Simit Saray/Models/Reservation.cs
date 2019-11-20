namespace Simit_Saray.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Reservation")]
    public partial class Reservation
    {
        public int ID { get; set; }

        public int? UserID { get; set; }

        public int? Amount { get; set; }

        public DateTime? ComeDate { get; set; }

        public TimeSpan? ComingHours { get; set; }

        public virtual Profil Profil { get; set; }
    }
}
