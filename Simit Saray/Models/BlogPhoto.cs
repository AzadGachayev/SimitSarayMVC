using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Simit_Saray.Models
{
    [Table("BlogPhoto")]
    public class BlogPhoto
    {
        public int ID { get; set; }

        [StringLength(800)]
        public string Photo { get; set; }
    }
}