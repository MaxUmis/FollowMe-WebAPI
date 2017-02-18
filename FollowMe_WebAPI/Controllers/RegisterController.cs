using FollowMe_WebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data;
using System.Web.Http;

namespace FollowMe_WebAPI.Controllers
{
    public class RegisterController : ApiController
    {
        // GET: api/Register
        public RegisterStatus GetNone()
        {
            return new RegisterStatus { success = false, message = "GET not active" };
        }

        // GET: api/Register/5
        public RegisterStatus Get(string theRegUser)
        {
            RegisterStatus reg = new RegisterStatus() { success = false, message = "GET not active for receiving variables" };
            return reg;
        }

        // POST: api/Register
        public RegisterStatus Post([FromBody]Newtonsoft.Json.Linq.JObject value)
        {
            RegisterStatus regstatus = new RegisterStatus();
            if (value == null) { regstatus.success = false ; }
            else
            {
                RegisteringUser regUser = new RegisteringUser();
                regUser.firstname = value.GetValue("firstname").ToString();
                regUser.surname = value.GetValue("surname").ToString();
                regUser.username = value.GetValue("username").ToString();
                regUser.email = value.GetValue("email").ToString();
                regUser.mobilenumber = value.GetValue("mobilenumber").ToString();
                regUser.password = value.GetValue("password").ToString();

                Register reg = new Register();
                regstatus.message = reg.RegisterUser(regUser);
                if (regstatus.message.Length < 38) // Default message length is 37 "Message from the Registration desk: " + (char)10;
                {
                    regstatus.success = true;
                }
                else regstatus.success = false ;
            }
            return regstatus;
        }

        // PUT: api/Register/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Register/5
        public void Delete(int id)
        {
        }
    }
}
