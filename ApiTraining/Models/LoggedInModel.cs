﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTraining.Models
{
    public class LoggedInModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }

    
}
