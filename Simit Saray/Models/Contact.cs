namespace Simit_Saray.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Adress { get; set; }

        [Column("Contact")]
        [StringLength(150)]
        public string Contact1 { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Phone2 { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Email2 { get; set; }

        [StringLength(100)]
        public string Worktime { get; set; }

        [StringLength(300)]
        public string Map { get; set; }
    }
}
