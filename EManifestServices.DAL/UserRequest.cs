namespace EManifestServices.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserRequest")]
    public partial class UserRequest
    {
        public Guid UserRequestId { get; set; }

        [Required]
        [StringLength(200)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(300)]
        public string Info { get; set; }

        [Required]
        [StringLength(10)]
        public string UserName { get; set; }
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [Required]
        [StringLength(10)]
        public string Password { get; set; }

        public DateTime? RequestDate { get; set; }

        public bool? Approved { get; set; }
    }
}
