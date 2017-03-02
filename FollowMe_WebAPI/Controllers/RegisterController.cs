using FollowMe_WebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Text;

namespace FollowMe_WebAPI.Controllers
{
    public class RegisterController : ApiController
    {
        // GET: api/Register
        public HttpResponseMessage GetNone()
        {
            RegisterStatus reg = new RegisterStatus() { success = false, message = "GET not active for this URI" };
            var jObject = JObject.FromObject(reg);
            var response = Request.CreateResponse(HttpStatusCode.NotFound);
            response.Content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
            return response;
            
        }

        // GET: api/Register/5
        public HttpResponseMessage Get(string theUser)
        {
            RegisterStatus reg = new RegisterStatus() { success = false, message = "GET not active for this URI" };
            var jObject = JObject.FromObject(reg);
            var response = Request.CreateResponse(HttpStatusCode.NotFound);
            response.Content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
            return response;
        }

        // POST: api/Register
        public HttpResponseMessage Post([FromBody]Newtonsoft.Json.Linq.JObject value)
        {
            RegisterStatus regstatus = new RegisterStatus();
            if (value == null) { regstatus.success = false ; }
            else
            {
                Utilities utils = new Utilities();
                RegisteringUser regUser = new RegisteringUser();
                regUser.firstname = utils.HTTPSafe(value.GetValue("firstname").ToString());
                regUser.surname = utils.HTTPSafe(value.GetValue("surname").ToString());
                regUser.username = utils.HTTPSafe(value.GetValue("username").ToString());
                regUser.email = utils.SingleEmail(value.GetValue("email").ToString());
                regUser.mobilenumber = utils.HTTPSafe(value.GetValue("mobilenumber").ToString().Replace(" ",""));
                regUser.password = value.GetValue("password").ToString();
                regstatus.success = false;
                if (regUser.firstname.Length < 1) regstatus.message = "Message from the Registration desk: Please provide your firstname.";
                else if (regUser.surname.Length < 1) regstatus.message = "Message from the Registration desk: Please provide your surname.";
                else if (regUser.username.Length < 1) regstatus.message = "Message from the Registration desk: Please provide a nickname.";
                else if (regUser.email == null) regstatus.message = "Message from the Registration desk: There seems to be something wrong with your email. Please check it and try again.";
                else if (regUser.mobilenumber.Length > 12) regstatus.message = "Message from the Registration desk: We only accept contact numbers of 12 characters or less.";
                else if (regUser.password.Length < 6) regstatus.message = "Message from the Registration desk: That password is a bit short. Choose a password with 6 to 8 characters.";
                else if (regUser.password.Length > 8) regstatus.message = "Message from the Registration desk: I don't think I'll remeber a password that long. Choose a password with 6 to 8 characters.";
                else
                {
                    Register reg = new Register();
                    regstatus.message = reg.RegisterUser(regUser);
                    if (regstatus.message.Length < 38) // Default message length is 37 "Message from the Registration desk: " + (char)10;
                    {
                        regstatus.success = true;
                    }
                }
            }

            var jObject = JObject.FromObject(regstatus);
            var response = Request.CreateResponse(HttpStatusCode.BadRequest);
            if (regstatus.success) response = Request.CreateResponse(HttpStatusCode.Created);
            response.Content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
            return response;

            
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
