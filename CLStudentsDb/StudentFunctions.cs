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
    /// Summary description for StudentFunctions
    /// </summary>
    public class StudentFunctions
    {
        private string name;
        private string surname;
        private short grade;
        private string subject;
        ConToDb connstr = new ConToDb("ProjectStudentsDB");


        public StudentFunctions(string name, string surname, short grade, string subject)
        {
            this.name = name;
            this.surname = surname;
            this.grade = grade;
            this.subject = subject;
        }



        /// <summary>
        /// inserts student in to database with provided values name, surname, grade and subject 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="grade"></param>
        /// <param name="subject"></param>
        public void InsertStudent()
        {

            using (SqlConnection sqlconn = new SqlConnection(connstr.GetConnectionString()))
            {
                SqlCommand sqlquery = new SqlCommand();
                sqlquery.CommandText = string.Format("InsStudent");
                sqlquery.CommandType = CommandType.StoredProcedure;
                sqlquery.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar, 50));
                sqlquery.Parameters["@name"].Value = name;
                sqlquery.Parameters.Add(new SqlParameter("@surname", SqlDbType.VarChar, 50));
                sqlquery.Parameters["@surname"].Value = surname;
                sqlquery.Parameters.Add(new SqlParameter("@grade", SqlDbType.SmallInt));
                sqlquery.Parameters["@grade"].Value = grade;
                sqlquery.Parameters.Add(new SqlParameter("@subject", SqlDbType.VarChar, 50));
                sqlquery.Parameters["@subject"].Value = subject;
                sqlconn.Open();
                sqlquery.Connection = sqlconn;
                sqlquery.ExecuteNonQuery();
                sqlconn.Close();
            }
        }
    }
}
