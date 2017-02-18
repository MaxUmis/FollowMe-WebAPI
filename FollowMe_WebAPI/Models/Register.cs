using FollowMe_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace FollowMe_WebAPI.Models
{
    public class RegisterStatus
    {
        public bool success { get; set; }
        public string message { get; set; }
    }
    public class RegisteringUser
    {
        public string firstname { get; set; }
        public string surname { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string mobilenumber { get; set; }
        public string password { get; set; }
    }
    public class Register
    {
        public string RegisterUser(RegisteringUser rUser)
        {
            Utilities utils = new Utilities();
            DB db = new DB();
            StoreProc sp = new StoreProc();
            DataSet ds = new DataSet();
            List<StoreProcParam> spParramList = new List<StoreProcParam>();
            spParramList.Add(new StoreProcParam { field = "id", value = "0" });
            spParramList.Add(new StoreProcParam { field = "firstname", value = rUser.firstname });
            spParramList.Add(new StoreProcParam { field = "surname", value = rUser.surname });
            spParramList.Add(new StoreProcParam { field = "username", value = rUser.username });
            spParramList.Add(new StoreProcParam { field = "email", value = rUser.email });
            spParramList.Add(new StoreProcParam { field = "mobilenumber", value = rUser.mobilenumber });
            spParramList.Add(new StoreProcParam { field = "password", value = utils.EncryptString(rUser.password) });

            List<StoreProcParam> spOutParramList = new List<StoreProcParam>();
            spOutParramList.Add(new StoreProcParam { field = "@OutPutMessage", value = "" });

            sp = db.RunStoreProc("sp_RegisterUser", spParramList, spOutParramList);
            ds = sp.ds;

            string spStatus = "";
            if (sp.outputs == null) { spStatus = "Message from the Registration desk: Registration desk closed."; }
            else
            {
                spStatus = "Message from the Registration desk: " + (char)10;
                foreach (string o in sp.outputs)
                {
                    if (o.Length > 0) spStatus = spStatus + o + (char)10;
                }
                
            }
            return spStatus;
        }
    }
    
}