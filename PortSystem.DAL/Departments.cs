using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortSystem.DAL
{
    public class Departments
    {
        [Key]
        public Guid DepartmentId { get; set; }
        [Required]
        [StringLength(30)]
        public string DepartmentName { get; set; }
        [Required]
        public Guid PortId { get; set; }

        public virtual Ports Ports { get; set; }

    }
}
