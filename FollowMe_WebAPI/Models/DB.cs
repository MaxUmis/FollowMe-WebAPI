using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace FollowMe_WebAPI.Models
{
    public class DB
    {
        public SqlConnection sqlConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["FollowMeDataConnectionString"].ConnectionString);
        
        public DB()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSet GetDataSetFromString(string SQL, string SearchStr)//takes a sql command, adds the search variable to the end and then generates/returns a dataset.
        {
            DataSet ds = new DataSet();
            try
            {

                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand();

                cmd = sqlConn.CreateCommand();
                cmd.CommandText = SQL + " " + SearchStr;
                da.SelectCommand = cmd;
                ds = new DataSet();

                sqlConn.Open();
                da.Fill(ds);
                sqlConn.Close();
                cmd.Parameters.Clear();
            }
            catch
            {
                /*ds not populated thus catch returns empty dataset*/
            }
            return ds;
        }

        public DataSet RunStoreProc(string spName, List<StoreProcParam> spParrams)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();
            da.SelectCommand = cmd;

            cmd.CommandText = spName;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach(StoreProcParam spParram in spParrams)
            {
                cmd.Parameters.AddWithValue(spParram.field, spParram.value);
            }
            cmd.Connection = sqlConn;

            //Fill dataset ds
            da.Fill(ds);

            return ds;
        }
    }
    public class StoreProcParam
    {
        public string field { get; set; }
        public string value { get; set; }
    }
}