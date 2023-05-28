using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2_Rpm
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }  
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
