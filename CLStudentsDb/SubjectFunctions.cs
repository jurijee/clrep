using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CLStudentsDb
{
    /// <summary>
    /// Summary description for SubjectFunctions
    /// </summary>
    public class SubjectFunctions
    {
        private string subject;
        private string teacher;
        private string examdate;
        ConToDb connstr = new ConToDb("ProjectStudentsDB");

        public SubjectFunctions()
        {
            examdate = "";
        }

        public SubjectFunctions(string subject, string teacher) : this()
        {
            this.subject = subject;
            this.teacher = teacher;
        }

        public SubjectFunctions(string subject, string teacher, string examdate) : this()
        {
            this.subject = subject;
            this.teacher = teacher;
            this.examdate = examdate;
        }

        /// <summary>
        /// inserts subject in to database based on provided values subject, teacher and examdate
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="teacher"></param>
        /// <param name="date"></param>
        public void InsertSubject()
        {

            using (SqlConnection sqlconn = new SqlConnection(connstr.GetConnectionString()))
            {
                SqlCommand sqlquery = new SqlCommand();
                sqlquery.CommandText = "[dbo].[InsSub]";
                sqlquery.CommandType = CommandType.StoredProcedure;
                sqlquery.Parameters.Add(new SqlParameter("@subject", SqlDbType.VarChar, 50));
                sqlquery.Parameters["@subject"].Value = subject;
                sqlquery.Parameters.Add(new SqlParameter("@teacher", SqlDbType.VarChar, 150));
                sqlquery.Parameters["@teacher"].Value = teacher;
                if (string.Compare(examdate, "") != 0)
                {
                    sqlquery.Parameters.Add(new SqlParameter("@examdate", SqlDbType.Date));
                    sqlquery.Parameters["@examdate"].Value = examdate;
                }
                sqlconn.Open();
                sqlquery.Connection = sqlconn;
                sqlquery.ExecuteNonQuery();
                sqlconn.Close();
            }
        }

        /// <summary>
        /// returns subject summary for all subjects
        /// </summary>
        /// <returns></returns>
        public DataTable SelectSubjectInfo()
        {
            using (SqlConnection sqlconn = new SqlConnection(connstr.GetConnectionString()))
            {
                string sqlquery = "SELECT Subject, Teacher, ExamDate FROM Subjects";
                DataTable dt = new DataTable();

                try
                {
                    SqlDataAdapter sda = new SqlDataAdapter(sqlquery, connstr.GetConnectionString());
                    sqlconn.Open();
                    sda.Fill(dt);
                    return dt;
                }

                finally
                {
                    sqlconn.Close();
                    sqlconn.Dispose();
                }
            }
        }

        /// <summary>
        /// Returns distinct list of subjects
        /// </summary>
        /// <returns></returns>
        public DataTable SelectDistinctSubject()
        {
            using (SqlConnection sqlconn = new SqlConnection(connstr.GetConnectionString()))
            {
                string sqlquery = "SELECT Subject FROM SelDistSubjects";
                DataTable dt = new DataTable();

                try
                {
                    SqlDataAdapter sda = new SqlDataAdapter(sqlquery, connstr.GetConnectionString());
                    sqlconn.Open();
                    sda.Fill(dt);
                    return dt;
                }

                finally
                {
                    sqlconn.Close();
                    sqlconn.Dispose();
                }
            }
        }
    }
}
