using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FollowMe_WebAPI.Models
{
    public class Register
    {
        public bool success { get; set; }
    }
    public class RegisterUser
    {
        public string firstname { get; set; }
        public string surname { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string mobileNumber { get; set; }
        public string password { get; set; }
    }
}