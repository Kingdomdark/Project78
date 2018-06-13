using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Data.Sql;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Change python string path to the one you have
            string python = @"C:\Program Files (x86)\Microsoft Visual Studio\Shared\Python36_64\python.exe";
            //Change testerino string path to the path you saved the testerino script
            string testerino = @"C:\Testerino\testerino.py";

            //Dont touch the rest trash
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(python);

            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;

            myProcessStartInfo.Arguments = testerino;
            Process myProcess = new Process();
            myProcess.StartInfo = myProcessStartInfo;
            Console.WriteLine("Calling Python script");
            Console.ReadLine();

            myProcess.Start();


            StreamReader myStreamReader = myProcess.StandardOutput;
            string myString = myStreamReader.ReadLine();

            myProcess.WaitForExit();
            myProcess.Close();
            Console.WriteLine("Value received from script: " + myString);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form1 = new Form1();
            form1.Closed += (s, args) => this.Close();
            form1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-RU5OG2S;Initial Catalog=PGA_HRO;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                con.Open();

                //new SqlCommand("SELECT casId FROM dbo.tblCasus", con);
            }

            //bool ret = false;
            ////http://csharp.net-informations.com/data-providers/csharp-sql-server-connection.htm
            //try
            //{
            string connetionString = null;
            SqlConnection cnn;
            //Change Data Source and User ID to yours.
            connetionString = "Data Source = DESKTOP-RU5OG2S; Initial Catalog = PGA_HRO; Integrated Security = True; User ID = labin;";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                MessageBox.Show("Connection Open ! ");
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }


            //}

            //catch (SqlException)
            //{
            //    ret = false;
            //}

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connectionString = null;
            SqlConnection cnn;
            //Change Data Source and User ID to yours.
            connectionString = "Data Source = DESKTOP-RU5OG2S; Initial Catalog = PGA_HRO; Integrated Security = True; User ID = labin;";
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            //IEnumerable<string>query = from itvid in





            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //try
                //{
                    //                conn.Open();
                    //                SqlCommand cmd = new SqlCommand(("select itvid,itvRegieParentId,itvTargetId,itvInterventieOptieId,itvProbleemId,probId,probLeefgebiedScoreId,probProbleemOptieId,lgscoreId,LgscoreRegieParentId,lgscoreLeefgebiedId,lgscoreScore,lgId" +
                    //"from dbo.tblInterventie inner join tblInterventieOptie on tblInterventie.itvInterventieOptieId = tblInterventieOptie.intoptId" +
                    //"inner join tblProbleem on tblInterventie.itvProbleemId = tblProbleem.probId " +
                    //"inner join tblLeefgebiedScore on tblProbleem.probLeefgebiedScoreId = tblLeefgebiedScore.lgscoreId" +
                    //"inner join tblLeefgebied on tblLeefgebiedScore.lgscoreLeefgebiedId = tblLeefgebied.lgId" +
                    //"inner join tblprobleemoptie on tblProbleemOptie.proboptId = tblProbleem.probProbleemOptieId" +
                    //"where itvProbleemId is not null"), conn);
                    //                SqlDataReader reader = cmd.ExecuteReader();
                    conn.Open();
                    string sqlstring = "select [itvid]," +
                        "[itvRegieParentId]," +
                        "[itvTargetId]," +
                        "[itvInterventieOptieId]," +
                        "[probId]," +
                        "[probLeefgebiedScoreId]," +
                        "[probProbleemOptieId]," +
                        "[lgscoreId]," +
                        "[LgscoreRegieParentId]," +
                        "[lgscoreLeefgebiedId]," +
                        "[lgscoreScore]," +
                        "[lgId]" +
                        "from[PGA_HRO].[dbo].[tblInterventie]" +
                        "inner join[tblInterventieOptie] on[tblInterventie].[itvInterventieOptieId] = [tblInterventieOptie].[intoptId]" +
                        "inner join[tblProbleem] on[tblInterventie].[itvProbleemId] = [tblProbleem].[probId]" +
                        "inner join[tblLeefgebiedScore] on[tblProbleem].[probLeefgebiedScoreId] = [tblLeefgebiedScore].[lgscoreId]" +
                        "inner join[tblLeefgebied] on[tblLeefgebiedScore].[lgscoreLeefgebiedId] = [tblLeefgebied].[lgId]" +
                        "inner join[tblprobleemoptie] on[tblProbleemOptie].[proboptId] = [tblProbleem].[probProbleemOptieId]" +
                        "where[itvProbleemId] is not null and[itvGoalReached] = 1";

                        SqlCommand cmd = new SqlCommand(sqlstring, conn);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.HasRows)
                        {
                            Console.WriteLine("\t{0}\t{1}", reader.GetString(0));
                        }
                //}

                //catch (Exception ex)
                //{
                //    MessageBox.Show("Nigga doesnt work");
                //}
                //                //HELP ME ITS NOT WORKING! YAMERO!


                //                try
                //                {
                //                    while (reader.Read())
                //                    {
                //                        Console.WriteLine(String.Format("{0}, {1}",
                //                            reader[0], reader[1]));
                //                    }
                //                    MessageBox.Show("But does it works :thinking:");
                //                }
                //                catch (Exception ex)
                //                {
                //                    MessageBox.Show("bich this is libary");
                //                }

                //                finally
                //                {
                //                    reader.Close();
                //                }
            }
        }
    }
}
