﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.CoreDomains.SystemAccessing.UserAggregate
{
    public interface UserFactory
    {
        User Create(string username, string password, string email);
    }
}
