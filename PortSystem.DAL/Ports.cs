using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortSystem.DAL
{
    public class Ports
    {
        public Ports()
        {
            Departments = new ObservableCollection<Departments>();
        }
        [Key]
        public Guid PortId { get; set; }
        [Required]
        [StringLength(30)]
        public string PortName { get; set; }
        [Required]
        [StringLength(5)]
        public string PortCode { get; set; }

        public virtual ICollection<Departments> Departments { get; set; }

    }
}
