﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsContext.ReadModel
{
    public class ContactInfo
    {
        public Guid ContactId { get; set; }

        public string Code { get; set; }
        public string Title { get; set; }
    }
}