using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces.Models
{
    public interface ILogin
    {
        public string Email { get; }
        public string Password { get; }
    }
}
