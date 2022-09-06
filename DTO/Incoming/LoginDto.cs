﻿using Shared.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Incoming
{
    public class LoginDto : ILogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
