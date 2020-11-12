using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EManifestServices.DAL
{
    public class Notifications
    {
        [Key]
        public Guid NotificationId { get; set; }
        [Required]
        [StringLength(100)]
        public string Header { get; set; }
        [Required]
        public string Details { get; set; }
        public bool Status { get; set; }
        public bool? Read { get; set; }
        [Required]
        public DateTime? NotificationDate { get; set; }
        [Required]
        public Guid? UserId { get; set; }
        public Guid? CarrierId { get; set; }

        public virtual Users Users { get; set; }
        public virtual Carriers Carriers { get; set; }

    }
}
