namespace EManifestServices.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ports
    {
        [Key]
        public Guid PortId { get; set; }

        [Required]
        [StringLength(100)]
        public string PortName { get; set; }

        public bool? IsActive { get; set; }

        public int? Number { get; set; }
    }
}
