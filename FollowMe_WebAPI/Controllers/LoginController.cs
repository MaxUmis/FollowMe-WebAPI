using FollowMe_WebAPI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace FollowMe_WebAPI.Controllers
{
    public class LoginController : ApiController
    {
        // GET: api/Login
        public HttpResponseMessage GetNone()
        {
            LoginStatus ls = new LoginStatus() { success = false, message = "GET not active for this URI" };
            var jObject = JObject.FromObject(ls);
            var response = Request.CreateResponse(HttpStatusCode.NotFound);
            response.Content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
            return response;
        }

        // GET: api/Login/5
        public HttpResponseMessage Get(string theUser)
        {
            LoginStatus ls = new LoginStatus() { success = false, message = "GET not active for this URI" };
            var jObject = JObject.FromObject(ls);
            var response = Request.CreateResponse(HttpStatusCode.NotFound);
            response.Content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
            return response;
        }

        // POST: api/Login
        public HttpResponseMessage Post([FromBody]Newtonsoft.Json.Linq.JObject value)
        {
            LoginStatus loginStatus = new LoginStatus();
            if (value == null) { loginStatus.success = false; }
            else
            {
                Utilities utils = new Utilities();
                if(utils.SingleEmail(value.GetValue("email").ToString()) != null)
                {
                    if(value.GetValue("password").ToString().Length < 6)
                    {
                        loginStatus.success = false;
                        loginStatus.message = "Message from the gatekeeper: That password is a bit short. Choose a password with 6 to 8 characters.";
                        loginStatus.loggedinuser = null;
                    }//if
                    else if (value.GetValue("password").ToString().Length > 8)
                    {
                        loginStatus.success = false;
                        loginStatus.message = "Message from the gatekeeper: I don't think I'll remeber a password that long. Choose a password with 6 to 8 characters.";
                        loginStatus.loggedinuser = null;
                    }//else if
                    else
                    {
                        LoggingInUser liUser = new LoggingInUser();
                        liUser.email = utils.SingleEmail(value.GetValue("email").ToString());
                        liUser.password = value.GetValue("password").ToString();

                        Login li = new Login();
                        try
                        {
                            loginStatus = li.LoginUser(liUser);
                            if (loginStatus.message.Length < 1) loginStatus.success = true;
                        }
                        catch
                        {
                            loginStatus.success = false;
                            loginStatus.message = "Message from the gatekeeper: He's all broken. I couldn't fetch him :(";
                            loginStatus.loggedinuser = null;
                        }
                    }//else
                    
                }//if
                else
                {
                    loginStatus.success = false;
                    loginStatus.message = "Message from the gatekeeper: There seems to be a problem with your email address. Please check it and try again.";
                    loginStatus.loggedinuser = null;
                }
                
            }
            var jObject = JObject.FromObject(loginStatus);
            var response = Request.CreateResponse(HttpStatusCode.BadRequest);
            if (loginStatus.success) response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
            return response;
            
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
