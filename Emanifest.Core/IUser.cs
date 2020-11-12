﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emanifest.Core
{
    public interface IUser
    {
        Guid UserId { get; set; }

        string UserName { get; set; }

        string Email { get; set; }

        string Password { get; set; }

        bool? IsActive { get; set; }
        
        Guid? CarrierId { get; set; }
    }
}
