namespace IQManClient.DAL
{
    using Emanifest.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users : IUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            VoyageDetails = new ObservableCollection<VoyageDetails>();
            Notifications = new ObservableCollection<Notifications>();
        }

        [Key]
        public Guid UserId { get; set; }

        [StringLength(10)]
        public string UserName { get; set; }
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(10)]
        public string Password { get; set; }

        public bool? IsActive { get; set; }

        public Guid? CarrierId { get; set; }

        public virtual Carriers Carriers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VoyageDetails> VoyageDetails { get; set; }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notifications> Notifications { get; set; }
    }
}
