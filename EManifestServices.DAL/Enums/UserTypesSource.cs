using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EManifestServices.DAL.Enums
{
    public class UserTypesSource
    {
        public List<UserTypeComboItemSource> UserTypes { get; set; }
        public List<UserTypeComboItemSource> ClientTypes { get; set; }
        public UserTypesSource()
        {
            UserTypes = new List<UserTypeComboItemSource>();
            UserTypes.Add(new UserTypeComboItemSource
            {
                Value = 1,
                Text = "Admin"
            });
            UserTypes.Add(new UserTypeComboItemSource
            {
                Value = 2,
                Text = "Agent User"
            });
            UserTypes.Add(new UserTypeComboItemSource
            {
                Value = 3,
                Text = "Port Management User"
            });
            //clients
            ClientTypes = new List<UserTypeComboItemSource>();
            ClientTypes.Add(new UserTypeComboItemSource { Value = 1, Text = "Admin" });
            ClientTypes.Add(new UserTypeComboItemSource { Value = 2, Text = "IQManClient" });
            ClientTypes.Add(new UserTypeComboItemSource { Value = 3, Text = "PortClient" });
        }
    }
    public class UserTypeComboItemSource
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }
}
