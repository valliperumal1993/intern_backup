using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {

            Int32 column_count;//for calculating  the columns 
        
            List<string> cdm_list ;//list of cdm description
            List<string> commalist_cdm_list = new List<string>();
            List<string[]> cdm_list_splitted = new List<string[]>();//temporary cdm_list splitted
            Dictionary<string, string[]> cdm_desc_splitted = new Dictionary<string, string[]>();//temporary cdm_list splitted
            Dictionary<string, string> commalist_cdm_desc = new Dictionary<string, string>();
            Dictionary<string, string> cdm_desc = new Dictionary<string, string>();
            List<int> countmax_cdm_list_splitted = new List<int>();

            string[] comma_array_commalist_distinct_cdm_list;
            string[] values1 = null;
            string column_name = null;
            string final_column_name = null;
            string temp;

            Databaseacces db = new Databaseacces();
            cdm_desc = db.FetchCdm();
            cdm_list = db.FetchCdmTerms();

            cdm_list = cdm_list.Where(s => !string.IsNullOrWhiteSpace(s)).ToList(); // cdm_list.RemoveAll(y => y == null);
            var nullKeys = cdm_desc.Where(pair => pair.Value == "")
                         .Select(pair => pair.Key)
                         .ToList();
            foreach (var badKey in nullKeys)
            {
                //Console.WriteLine(badKey);
                cdm_desc.Remove(badKey);
            }
             
            //foreach (var items in cdm_list)
            //{
            //    values1 = items.Split(' ');
            //    cdm_list_splitted.Add(values1);
            //}

            foreach (KeyValuePair<string,string> items in cdm_desc)
            {
                values1 = items.Value.Split(' ');
                cdm_desc_splitted.Add(items.Key,values1);
            }

            //foreach (var items in cdm_list)
            //{
            //    temp = Regex.Replace(items.Trim(), @"\s+", "','");
            //    temp = "'" + temp + "'";
            //    commalist_cdm_list.Add(temp);
            //}
            foreach (KeyValuePair<string,string> items in cdm_desc)
            {
                temp = Regex.Replace(items.Value.Trim(), @"\s+", "','");
                temp = "'" + temp + "'";
                commalist_cdm_desc.Add(items.Key,temp);
            }

            //foreach (var items in cdm_list_splitted)
            //{
            //    countmax_cdm_list_splitted.Add(items.Count());
            //}
            foreach (var items in cdm_desc_splitted)
            {
                countmax_cdm_list_splitted.Add(items.Value.Count());
            }
            column_count = db.CountColumns();
            
            for (int i = column_count; i < countmax_cdm_list_splitted.Max()+1; i++)
            {
                    db.AlterColumns(i);
            }

            //foreach (var items in commalist_cdm_list)
            //{
            //    comma_array_commalist_distinct_cdm_list = items.Split(',');

            //    for (int i = 1; i <= comma_array_commalist_distinct_cdm_list.Count(); i++)
            //    {
            //        column_name = column_name + "column" + i + ",";
            //    }
                
            //    final_column_name = column_name.TrimEnd(',');
            //    db.InsertRows(final_column_name, items);
            //    column_name = null;
            //}

            foreach (var items in commalist_cdm_desc)
            {
                comma_array_commalist_distinct_cdm_list = items.Value.Split(',');

                for (int i = 1; i <= comma_array_commalist_distinct_cdm_list.Count()+1; i++)
                {
                    column_name = column_name + "column" + i + ",";
                }

                final_column_name = column_name.TrimEnd(',');
                //db.InsertRows(final_column_name, items.Value);
                db.InsertRowsdict(final_column_name,items.Key, items.Value);
                column_name = null;
            }
         
        }
    }
}