using FollowMe_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace FollowMe_WebAPI
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            DB db = new DB();
            DataSet ds = new DataSet();
            ds = db.GetDataSetFromString("select * from tblSites", "");

            if (ds.Tables.Count > 0) { messageLabel.Text = "Successful db connection :)"; }
            else { messageLabel.Text = "Nothing returned. Unsuccessful db connection :("; }

            DataSet ds2 = new DataSet();
            List<StoreProcParam> spParramList = new List<StoreProcParam>();
            spParramList.Add(new StoreProcParam { field = "id", value = "0" });
            spParramList.Add(new StoreProcParam { field = "Firstname", value = "Mark"});

            ds2 = db.RunStoreProc("sp_RegisterUser", spParramList);

            if (ds2.Tables.Count > 0)
            {
                spMessageLabel.Text = "The firstname is: " + ds2.Tables[0].Rows[0].ItemArray[1].ToString();
            }
            else spMessageLabel.Text = "Uh the horror.";
            */
        }
    }
}