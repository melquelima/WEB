using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace APIAUTH.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public String Query2Obj(string SERV, string BD, string USER, string PWD,string query)
        {

            //string SERV = "chrisbot.database.windows.net";
            //string BD = "BTD";
            //string USER = "resbot";
            //string PWD = "ResMi@mi2019";
            //"PROVIDER=SQLOLEDB;DATA SOURCE=" + SERV + ";INITIAL CATALOG=" + BD + "; User Id=" + USER + "; Password=" + PWD
            try
            {
                SqlConnection conn = new SqlConnection("SERVER=" + SERV + ";DataBase=" + BD + "; User Id=" + USER + "; Password=" + PWD);  //+ ";Connect Timeout=1");
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandTimeout = 600;
                SqlDataReader dr = command.ExecuteReader();
                List<Dictionary<string, string>> Table = rdr2dictonary(dr);
                conn.Close();
                string json = JsonConvert.SerializeObject(Table);
          
                return Ret("Success","",json);
            }
            catch (Exception e)
            {
                return Ret("Failed", e.Message, "[]");
            }
        }
        public String Execute(string SERV, string BD, string USER, string PWD, string query)
        {
            try
            {
                SqlConnection conn = new SqlConnection("SERVER=" + SERV + ";DataBase=" + BD + "; User Id=" + USER + "; Password=" + PWD);  //+ ";Connect Timeout=1");
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandTimeout = 600;
                command.ExecuteNonQuery();
            
                return Ret("Success", "", "[]");
            }
            catch(Exception e)
            {
                return Ret("Failed", e.Message, "[]");
            }
        }
        private String Ret(string sts,string msg,string resp)
        {
            Dictionary<string, string> Retorno = new Dictionary<string, string>();
            Retorno.Add("Status", sts);
            Retorno.Add("Message", msg);
            Retorno.Add("Response", resp);
            string json = JsonConvert.SerializeObject(Retorno, Formatting.Indented);
            json = json.Replace("\"[", "[").Replace("]\"", "]").Replace("\\", "");
            return json;
        }
        private new List<Dictionary<string, string>> rdr2dictonary(SqlDataReader dr)
        {
            List<Dictionary<string, string>> Table = new List<Dictionary<string, string>>();
            int count = 0;
            while (dr.Read())
            {
                Table.Add(new Dictionary<string, string>());
                var end = dr.FieldCount;
                for (var i = 0; i < end; i++)
                {
                    Table[count].Add(dr.GetName(i), dr[i].ToString());
                }
                count++;
            }
            return Table;
        }
    }
}
