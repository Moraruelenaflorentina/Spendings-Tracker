using DocumentFormat.OpenXml.Office.Word;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingTracker
{
    internal class CategoryData
    {
        SqlConnection c = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpendingsDB;Integrated Security=True;");
        public int RowNum { get; set; }

        public int CategoryID { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }


        public List<CategoryData> categoryListDatas(int userId)
        {
            List<CategoryData> listData = new List<CategoryData>();
            c.Open();
            string selectData = @"SELECT Id, ROW_NUMBER() OVER (ORDER BY Id) AS RowNum, 
                      category, type, status, date_insert 
                      FROM category
                      WHERE user_id = @user_id";

            using (SqlCommand cmd = new SqlCommand(selectData, c))
            {

                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = userId;  

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CategoryData cData = new CategoryData();
                    cData.CategoryID = Convert.ToInt32(reader["Id"]);
                    cData.RowNum = Convert.ToInt32(reader["RowNum"]);
                    cData.Category = reader["category"].ToString();
                    cData.Type = reader["type"].ToString();
                    cData.Status = reader["status"].ToString();
                    cData.Date = Convert.ToDateTime(reader["date_insert"]).ToString("MM-dd-yyyy");
                    listData.Add(cData);
                }
            }

            c.Close();  
            return listData;


        }
    }
}
