using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CLStudentsDb
{
    /// <summary>
    /// Summary description for DelRecords
    /// </summary>
    public class DelRecords
    {
        public DelRecords()
        {

        }

        /// <summary>
        /// truncate defined table - delete all records
        /// </summary>
        public void DeleteAllRecords(string tablename)
        {
            ConToDb connstr = new ConToDb("ProjectStudentsDB");

            using (SqlConnection sqlconn = new SqlConnection(connstr.GetConnectionString()))
            {
                SqlCommand sqlquery = new SqlCommand();
                sqlquery.CommandText = "DelProc";
                sqlquery.CommandType = System.Data.CommandType.StoredProcedure;

                //declaring procedure parameter tablename
                sqlquery.Parameters.Add(new SqlParameter("@table", SqlDbType.VarChar, 50));
                sqlquery.Parameters["@table"].Value = tablename;

                //openning connection and executing procedure
                sqlquery.Connection = sqlconn;
                sqlconn.Open();
                sqlquery.ExecuteNonQuery();
                sqlconn.Close();
            }
        }
    }
}
