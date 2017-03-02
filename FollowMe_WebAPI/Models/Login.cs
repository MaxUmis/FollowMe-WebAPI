using FollowMe_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace FollowMe_WebAPI.Models
{
    public class LoginStatus
    {
        public bool success { get; set; }
        public string message { get; set; }
        public LoggedInUser loggedinuser { get; set; }

        public class LoggedInUser
        {
            public string followmeGUID { get; set; }
            public string firstname { get; set; }
            public string surname { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public string mobilenumber { get; set; }
        }
    }
    
    public class LoggingInUser
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class Login
    {
        public LoginStatus LoginUser(LoggingInUser rUser)
        {
            LoginStatus liStatus = new LoginStatus();
            Utilities utils = new Utilities();
            DB db = new DB();
            StoreProc sp = new StoreProc();
            DataSet ds = new DataSet();
            List<StoreProcParam> spParramList = new List<StoreProcParam>();
            spParramList.Add(new StoreProcParam { field = "email", value = rUser.email });
            spParramList.Add(new StoreProcParam { field = "password", value = utils.EncryptString(rUser.password) });

            List<StoreProcParam> spOutParramList = new List<StoreProcParam>();
            spOutParramList.Add(new StoreProcParam { field = "@OutPutMessage", value = "" });

            sp = db.RunStoreProc("sp_CheckLogin", spParramList, spOutParramList);
            ds = sp.ds;

            liStatus.success = false;
            liStatus.message = sp.BuildOutputString(sp.outputs, "Message from the gatekeeper: ", "Login desk closed.");

            if (ds == null) liStatus.loggedinuser = null;
            else if(ds.Tables.Count < 1) liStatus.loggedinuser = null;
            else if(ds.Tables[0].Rows.Count < 1) liStatus.loggedinuser = null;
            else
            {
                liStatus.loggedinuser = new LoginStatus.LoggedInUser()
                {
                    followmeGUID = ds.Tables[0].Rows[0].ItemArray[0].ToString(),
                    firstname = ds.Tables[0].Rows[0].ItemArray[1].ToString(),
                    surname = ds.Tables[0].Rows[0].ItemArray[2].ToString(),
                    username = ds.Tables[0].Rows[0].ItemArray[3].ToString(),
                    email = ds.Tables[0].Rows[0].ItemArray[4].ToString(),
                    mobilenumber = ds.Tables[0].Rows[0].ItemArray[5].ToString()
                };
            }
            return liStatus;
        }
    }
    
}