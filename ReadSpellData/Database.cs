using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
namespace ReadSpellData
{
    class Database
    {
        public static void WriteDB(string sqlstring)
        {
            MySqlConnection conn = new MySqlConnection();
            MySqlCommand myCommand = new MySqlCommand();
            conn.ConnectionString = "server=" + ConfigurationManager.AppSettings["host"].ToString() + "; port=" + ConfigurationManager.AppSettings["port"].ToString() + "; user id=" + ConfigurationManager.AppSettings["username"].ToString() + "; password=" + ConfigurationManager.AppSettings["password"].ToString() + "; database=" + ConfigurationManager.AppSettings["database"].ToString() + ";Connect Timeout=300";
            myCommand.Connection = conn;
            myCommand.CommandText = sqlstring;
            Utility.WriteLog(sqlstring);
            try
            {
                conn.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException myerror)
            {
                Console.WriteLine("Error updating the database: " + myerror.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
