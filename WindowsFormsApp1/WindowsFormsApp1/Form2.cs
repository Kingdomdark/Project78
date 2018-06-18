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
            string python = @"C:\Users\labin\Anaconda3\envs\test\python.exe";
            //Change testerino string path to the path you saved the testerino script
            string testerino = @"C:\Users\labin\Documents\GitHub\Project78\WindowsFormsApp1\jason_is_kech_1.py";

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

            //using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-RU5OG2S;Initial Catalog=PGA_HRO;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            //{
            //    con.Open();

            //    //new SqlCommand("SELECT casId FROM dbo.tblCasus", con);
            //}

            //bool ret = false;
            ////http://csharp.net-informations.com/data-providers/csharp-sql-server-connection.htm
            string connetionString = null;
            SqlConnection cnn;
            //Change Data Source and User ID to yours.
            connetionString = "Data Source = DESKTOP-RU5OG2S; Initial Catalog = PGA_HRO; Integrated Security = True; User ID = labin;";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                MessageBox.Show("Connection Open! ");
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection! ");
            }

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
            string sqlstring = "select [itvid]," +
                "[itvRegieParentId]," +
                "[itvTargetId]," +
                "[itvInterventieOptieId]," +
                "[probId]," +
                "[lgscoreId]," +
                "[lgId]," +
                "[casId]," +
                "[tgId]," +
                "[casTargetId]," +
                "[ptid]," +
                "[sclSubjectId]," +
                "[sclId]," +
                "[sclCollectionId]," +
                "[probprobleemoptieId]" +
                "" +
                "from[PGA_HRO].[dbo].[tblInterventie]" +
                "inner join[tblInterventieOptie] on[tblInterventie].[itvInterventieOptieId] = [tblInterventieOptie].[intoptId]" +
                "inner join[tblProbleem] on[tblInterventie].[itvProbleemId] = [tblProbleem].[probId]" +
                "inner join[tblLeefgebiedScore] on[tblProbleem].[probLeefgebiedScoreId] = [tblLeefgebiedScore].[lgscoreId]" +
                "inner join[tblLeefgebied] on[tblLeefgebiedScore].[lgscoreLeefgebiedId] = [tblLeefgebied].[lgId]" +
                "inner join[tblprobleemoptie] on[tblProbleemOptie].[proboptId] = [tblProbleem].[probProbleemOptieId]" +
                "inner join[tblCasus] on[tblInterventie].[itvRegieParentId] = [tblCasus].[casId] and[tblInterventie].[itvRegieParentType] = 'casus'" +
                "inner join[tblPGAThemaGebied] on[tblPGAThemaGebied].[tgId] = [tblCasus].[casThemaGebiedId]" +
                "inner join[tblPGAThema] on[tblPGAThema].[ptId] = [tblPGAThemaGebied].[tgThemaId]" +
                "inner join[tblSubjectCollection] on[tblInterventie].[itvRegieParentId] = [tblSubjectCollection].[scTopicId]" +
                "inner join[tblSubjectCollectionLid] on[tblSubjectCollection].[scId] = [tblSubjectCollectionLid].[sclCollectionId]" +
                "inner join[tblSubject] on[tblSubject].[sjId] = [tblSubjectCollectionLid].[sclSubjectId]" +
                "where[itvProbleemId] is not null and[itvGoalReached] = 1 and[sclFunctie] = 'primairsubject'";

            SqlCommand cmd = new SqlCommand(sqlstring, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {

                DataTable Table = new DataTable("TestTable");
                Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}", 
                    reader["itvid"], reader["itvRegieParentId"], reader["itvTargetId"], reader["itvInterventieOptieId"], reader["probId"],
                     reader["lgscoreId"], reader["LgscoreRegieParentId"], reader["casId"], reader["casTargetId"], reader["ptid"],
                     reader["sclSubjectId"], reader["sclId"], reader["sclCollectionId"]);
                //foreach (DataRow dataRow in Table.Rows)
                //{
                //    foreach (var item in dataRow.ItemArray)
                //    {
                //        Console.WriteLine(item);
                //    }
                //}

            }





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
                    //conn.Open();
                    //string sqlstring = "select [itvid]," +
                    //    "[itvRegieParentId]," +
                    //    "[itvTargetId]," +
                    //    "[itvInterventieOptieId]," +
                    //    "[probId]," +
                    //    "[probLeefgebiedScoreId]," +
                    //    "[probProbleemOptieId]," +
                    //    "[lgscoreId]," +
                    //    "[LgscoreRegieParentId]," +
                    //    "[lgscoreLeefgebiedId]," +
                    //    "[lgscoreScore]," +
                    //    "[lgId]" +
                    //    "from[PGA_HRO].[dbo].[tblInterventie]" +
                    //    "inner join[tblInterventieOptie] on[tblInterventie].[itvInterventieOptieId] = [tblInterventieOptie].[intoptId]" +
                    //    "inner join[tblProbleem] on[tblInterventie].[itvProbleemId] = [tblProbleem].[probId]" +
                    //    "inner join[tblLeefgebiedScore] on[tblProbleem].[probLeefgebiedScoreId] = [tblLeefgebiedScore].[lgscoreId]" +
                    //    "inner join[tblLeefgebied] on[tblLeefgebiedScore].[lgscoreLeefgebiedId] = [tblLeefgebied].[lgId]" +
                    //    "inner join[tblprobleemoptie] on[tblProbleemOptie].[proboptId] = [tblProbleem].[probProbleemOptieId]" +
                    //    "where[itvProbleemId] is not null and[itvGoalReached] = 1";

                    //    SqlCommand cmd = new SqlCommand(sqlstring, conn);
                    //    SqlDataReader reader = cmd.ExecuteReader();
                    //    while (reader.HasRows)
                    //    {
                    //        Console.WriteLine("\t{0}\t{1}", reader.GetInt64(0), reader.GetInt64(1));
                    //    }
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
