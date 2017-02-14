using FollowMe_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FollowMe_WebAPI.Controllers
{
    public class RegisterController : ApiController
    {
        // GET: api/Register
        public Register GetNone()
        {
            return new Register { success = false };
        }

        // GET: api/Register/5
        public Register Get(string theRegUser)
        {
            Register reg = new Register() { success = false };
            return reg;
        }

        // POST: api/Register
        public Register Post([FromBody]string value)
        {
            Register reg = new Register() { success = true };
            return reg;
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
