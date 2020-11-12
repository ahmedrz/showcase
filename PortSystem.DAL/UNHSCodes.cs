using Emanifest.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortSystem.DAL
{
    public class UNHSCodes : IUNHSCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UNHSCodeId { get; set; }
        [StringLength(2)]
        public string Classification { get; set; }
        [StringLength(10)]
        public string Code { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        [StringLength(10)]
        public string ParentCode { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public bool IsLeaf { get; set; }
        [Required]
        public int Serial { get; set; }
    }
}
