using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Domain.Base;

#nullable disable

namespace Model.Domain
{
    [Table("MyDb")]
    public partial class MyDb : ModelBase
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [StringLength(30)]
        public string VehicleMake { get; set; }
        [StringLength(30)]
        public string VehicleModel { get; set; }
    }
}
