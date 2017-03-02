using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

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

        public StoreProc RunStoreProc(string spName, List<StoreProcParam> spParrams, [Optional] List<StoreProcParam> spOutParrams)
        {
            StoreProc spReturn = new StoreProc();
            DataSet ds = new DataSet();
            List<string> outList = new List<string>();
            try
            { 
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand();
                da.SelectCommand = cmd;

                cmd.CommandText = spName;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach(StoreProcParam spParram in spParrams)
                {
                    cmd.Parameters.AddWithValue(spParram.field, spParram.value);
                }
                if (spOutParrams != null)
                {
                    foreach (StoreProcParam spOutParram in spOutParrams)
                    {
                        cmd.Parameters.Add(new SqlParameter {
                        ParameterName = spOutParram.field,
                        IsNullable = true,
                        Direction = ParameterDirection.Output,
                        DbType = DbType.String,
                        Size = 150,
                        Value = DBNull.Value,
                    });
                    }
                }
                cmd.Connection = sqlConn;

                //Fill dataset ds
                da.Fill(ds);
                spReturn.ds = ds;
                
                if (spOutParrams != null)
                {
                    foreach (StoreProcParam spOutParram in spOutParrams)
                    {
                    outList.Add(cmd.Parameters[spOutParram.field].Value.ToString());
                    }
                spReturn.outputs = outList;
                }
            }
            catch
            {
                outList.Add("DB issues");
                spReturn.outputs = outList;
            }
            
            return spReturn;
        }
    }
    public class StoreProc
    {
        public DataSet ds { get; set; }
        public List<string> outputs { get; set; }

        public string BuildOutputString(List<string> ops, string theWho, string noResponseString  )
        {
            if (theWho == null) theWho = "";
            string spStatus = "";
            if (ops == null) { spStatus = theWho+noResponseString; }
            else
            {
                foreach (string o in ops)
                {
                    if (o.Length > 0) spStatus = spStatus + o + (char)10;
                }

                if (spStatus.Replace(((char)10).ToString(),string.Empty).Length > 1) spStatus = theWho + (char)10 + spStatus;
                else spStatus = "";
            }
            return spStatus;
        }
    }

    public class StoreProcParam
    {
        public string field { get; set; }
        public string value { get; set; }
    }
    
}