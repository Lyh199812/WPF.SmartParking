﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.Entity
{
    public class PayResultEntity
    {
        public bool result { get; set; }
        public string message { get; set; }
        public int state { get; set; }
        public string url { get; set; }
    }
}
