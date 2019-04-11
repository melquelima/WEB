using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using RGiesecke.DllExport;

namespace minhaDll
{
    public static class MqlSqlDemo
    {
        public static string ConnectionString = "",Erro = "",Retorno="";
        private static SqlConnection Cn = null;
        private static SqlCommand Com = null;

        [DllExport("Config", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPWStr)]
        public static string Config([MarshalAs(UnmanagedType.LPWStr)] string SERV_, [MarshalAs(UnmanagedType.LPWStr)] string BD_, [MarshalAs(UnmanagedType.LPWStr)] string USER_, [MarshalAs(UnmanagedType.LPWStr)] string PWD_)
        {
            return ConnectionString = "Server=tcp:" + SERV_ + ",1433;Initial Catalog=" + BD_ + ";Persist Security Info=False;User ID=" + USER_ + ";Password=" + PWD_ + ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }

        [DllExport("OpenConnection", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.VariantBool)]
        public static bool OpenConnection()
        {
            Erro = "";
            try
            {
                Cn = new SqlConnection();
                Cn.ConnectionString = ConnectionString;
                Com = new SqlCommand();
                Com.Connection = Cn;
                Cn.Open();
                return true;
            }
            catch (Exception e)
            {
                Erro = e.Message;
                Cn.Dispose();
                Com.Dispose();
                Cn = null;
                Com = null;
                return false;
            }
        }

        [DllExport("RunQuery", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.VariantBool)]
        public static bool RunQuery([MarshalAs(UnmanagedType.LPWStr)] string Query)
        {
            Erro = "";
            Retorno = "";
            try
            {
                Com.CommandText = Query;
                SqlDataAdapter sda = new SqlDataAdapter(Query, Cn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                
                SqlDataReader rdr = Com.ExecuteReader();
                Retorno = toJson(rdr);
                rdr.Close();
                return true;
            }
            catch (Exception e)
            {
                Erro = e.Message;
                return false;
                throw;
            }
        }

        [DllExport("GetError", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPWStr)]
        public static string GetError()
        {
            return Erro;
        }

        [DllExport("GetReturn", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPWStr)]
        public static string GetReturn()
        {
            return Retorno;
        }


        public static string toJson(SqlDataReader rdr)
        {
            var json = "";
            float n;
            int count = 0;

            while (rdr.Read())
            {
                json += "{";
                var end = rdr.FieldCount;
                for (var i = 0; i < end; i++)
                {
                    json += "\"" + rdr.GetName(i) + "\":" + (float.TryParse(rdr[i].ToString().Replace(",", "."), out n) ? "" : "\"") + rdr[i].ToString().Replace(",", ".") + (float.TryParse(rdr[i].ToString().Replace(",", "."), out n) ? "" : "\"") + (i == (end - 1) ? "" : ",");

                }
                json += "},";
                count++;
            }
            json = (count > 0 ? "[" : "") + json.Substring(0, json.Length - 1) + (count > 0 ? "]" : "");
            return json;
        }



    }



}
