using Model.UserClass;
using Shared.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Helpers
{
    public class UserTheoryData : TheoryData<IUser> { 
        public UserTheoryData()
        {
            Add(new User
            {
                Id = 1,
                Name = "Andjela Test",
                Email = "andjela@gmail.com",
                Password = "oRdR0yIrrbZFdjs/3mJJ6Qi1/5E=",
                Salt = "1111",
                AccountConfirmed = true

            }); ;
            Add(new User
            {
                Id = 2,
                Name = "Petar Test",
                Email = "petar@gmail.com",
                Password = "0000",
                Salt = "1111",
                AccountConfirmed = false
            });
        }
    }
}
