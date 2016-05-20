using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleApplication3
{
    class Databaseacces
    {    
            string sql_cdm = "Select * from cdm_new";
            string sql_cdm_terms;
            string sql_insert_cdm_terms = null;
            string sql_alter_cdm_terms = null;          
           
            string connectionString = "server=.;database=db1;Trusted_Connection=true";

        SqlConnection connection;
        SqlCommand command_cdm;
        SqlCommand command_cdm_terms;
        SqlCommand command_insert_cdm_terms;
        SqlCommand command_alter_cdm_terms;
        SqlDataReader dataReader;
 
     public  List<String> FetchCdmTerms()
        {
           connection = new SqlConnection(connectionString);
           connection.Open();
           List<String> cdm_list=new List<string>();
            
            try
            {
                command_cdm = new SqlCommand(sql_cdm, connection);
                dataReader = command_cdm.ExecuteReader();
                while (dataReader.Read())
                {
                    cdm_list.Add(dataReader["cdmdes"].ToString());//cdm description
                }
                dataReader.Close();
                command_cdm.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not open connection !" + ex);
            }
            connection.Close();
            return cdm_list;
        }

     public Dictionary<String, String> FetchCdm()
     {
         connection = new SqlConnection(connectionString);
         connection.Open();
         //List<String> cdm_list = new List<string>();
         Dictionary<String, String> cdm_desc = new Dictionary<string, string>();
         try
         {
             command_cdm = new SqlCommand("Select * from cdm", connection);
             dataReader = command_cdm.ExecuteReader();
             while (dataReader.Read())
             {
                 cdm_desc.Add(dataReader["cdmcode"].ToString(),dataReader["cdmwithoutspec"].ToString());//cdm description
             }
             dataReader.Close();
             command_cdm.Dispose();
         }
         catch (Exception ex)
         {
             Console.WriteLine("Can not open connection !" + ex);
         }
         connection.Close();
         return cdm_desc;
     }

     public  int CountColumns()
     {
         connection = new SqlConnection(connectionString);
         int count=0;
            connection.Open();
            sql_cdm_terms = "SELECT COUNT(*) FROM db1.sys.columns WHERE object_id = OBJECT_ID('db1.dbo.cdm_terms')";
            command_cdm_terms = new SqlCommand(sql_cdm_terms, connection);
         count=(Int32)command_cdm_terms.ExecuteScalar();
            connection.Close();
            return count;

        }

     public  void AlterColumns(int i)
        {

            connection = new SqlConnection(connectionString);
            connection.Open();
            sql_alter_cdm_terms = "ALTER TABLE cdm_terms ADD column" + (i + 1) + " varchar(50)";
            command_alter_cdm_terms = new SqlCommand(sql_alter_cdm_terms, connection);
            command_alter_cdm_terms.ExecuteNonQuery();
            connection.Close();
        }

     public void InsertRows(string final_column_name, string items)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            sql_insert_cdm_terms = "INSERT INTO db1.dbo.cdm_terms (" + final_column_name + ") VALUES (" + items + ")";//dnt include \'\' single quotes
            command_insert_cdm_terms = new SqlCommand(sql_insert_cdm_terms, connection);
            command_insert_cdm_terms.ExecuteNonQuery();
            connection.Close();
        }
     public void InsertRowsdict(string final_column_name,string key,string items)
     {
         connection = new SqlConnection(connectionString);
         connection.Open();
         sql_insert_cdm_terms = "INSERT INTO db1.dbo.cdm_terms (" +final_column_name +") VALUES (" + "'" + key + "'," + items + ")";//dnt include \'\' single quotes
         command_insert_cdm_terms = new SqlCommand(sql_insert_cdm_terms, connection);
         command_insert_cdm_terms.ExecuteNonQuery();
         connection.Close();
     }
    }
}
