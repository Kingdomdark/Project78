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
            string python = @"C:\Users\JasonTjauw\Anaconda3\envs\test\python.exe";
            //Change testerino string path to the path you saved the testerino script
            string testerino = @"C:\Users\JasonTjauw\Desktop\Project78\WindowsFormsApp1\test\Untitled.py";

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
            ////http://csharp.net-informations.com/data-providers/csharp-sql-server-connection.htm
            string connetionString = null;
            SqlConnection cnn;
            //Change Data Source and User ID to yours.
            connetionString = "Data Source = DESKTOP-6TMDVN3; Initial Catalog = PGA_HRO; Integrated Security = True; User ID = JasonTjauw;";
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
            connectionString = "Data Source = DESKTOP-6TMDVN3; Initial Catalog = PGA_HRO; Integrated Security = True; User ID = JasonTjauw;";
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            //IEnumerable<string>query = from itvid in
            string sqlstring = "select [itvid]," +
                "[itvRegieParentId]," +
                "[itvTargetId]," +
                "[itvInterventieOptieId]," +
                "[probId]," +
                "[lgscoreId]," +
                "[LgscoreRegieParentId]," +
                "[casId]," +
                "[casTargetId]," +
                "[ptid]," +
                "[sclSubjectId]," +
                "[sclId]," +
                "[sclCollectionId]" +
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
            
            while (reader.Read())
            {

                DataTable Table = new DataTable("TestTable");
                Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}", 
                    reader["itvid"], reader["itvRegieParentId"], reader["itvTargetId"], reader["itvInterventieOptieId"], reader["probId"],
                     reader["lgscoreId"], reader["LgscoreRegieParentId"], reader["casId"], reader["casTargetId"], reader["ptid"],
                     reader["sclSubjectId"], reader["sclId"], reader["sclCollectionId"]);
            }
        }

        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void addNewCriminal(object sender, EventArgs e)
        {
            int itvid = Int32.Parse(comboBox1.Text);
            int itvRegieParentId = Int32.Parse(comboBox2.Text);
            int itvTargetId = Int32.Parse(comboBox3.Text);
            int probId = Int32.Parse(comboBox4.Text);
            int lgscoreId = Int32.Parse(comboBox5.Text);
            int lgId = Int32.Parse(comboBox7.Text);
            int casId = Int32.Parse(comboBox8.Text);
            int tgId = Int32.Parse(comboBox9.Text);
            int casTargetId = Int32.Parse(comboBox10.Text);
            int ptid = Int32.Parse(comboBox11.Text);
            int sclSubjectId = Int32.Parse(comboBox12.Text);
            int sclId = Int32.Parse(comboBox13.Text);
            int sclCollectionId = Int32.Parse(comboBox14.Text);
            int probprobleemoptieId = Int32.Parse(comboBox15.Text);

            string connectionString = null;
            SqlConnection cnn;
            connectionString = "Data Source = DESKTOP-6TMDVN3; Initial Catalog = Test; Integrated Security = True; User ID = JasonTjauw;";
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            String sqlString = "Select * from TestTable";
            SqlCommand cmd = new SqlCommand(sqlString, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            int count = 1;
            
            while (reader.Read())
            {
                count++;
                DataTable Table = new DataTable("TestTable");
                int TestTable_Id = Int32.Parse(reader["Id"].ToString());
            }
            
            reader.Close();
           

            int Id = count++;
            Console.WriteLine("The data has been added");
            string CreateNewCriminal = "INSERT INTO TestTable (Id, itvid,itvRegieParentId,itvTargetId,probId,lgscoreId,lgId,"+ 
                "casId,tgId,casTargetId,ptid,sclSubjectId,sclId,sclCollectionId,probprobleemoptieId) " +
                "VALUES ("+Id+","+itvid+","+itvRegieParentId+","+itvTargetId+","+probId+","+
                lgscoreId+","+lgId+","+casId+","+tgId+","+casTargetId+","+ptid+","+sclSubjectId+","+
                sclId+","+sclCollectionId+","+probprobleemoptieId+");";

            SqlCommand insert = new SqlCommand(CreateNewCriminal, cnn);
            SqlDataReader reader2 = insert.ExecuteReader();

            while (reader2.Read())
            {
                Console.WriteLine("The Data has been inserted");
            }
            reader2.Close();

            SqlCommand WriteCsv = new SqlCommand(sqlString, cnn);
            SqlDataReader reader3 = cmd.ExecuteReader();
            var csv = new StringBuilder();
            var Headers = string.Format("'Id', 'itvid', 'itvRegieParentId', 'itvTargetId', 'itvInterventieOptieId', 'probId', 'lgscoreId', 'lgId', 'casId', 'tgId', 'casTargetId', 'ptid','sclSubjectId', 'sclId', 'sclCollectionId', 'probprobleemoptieId'");
            csv.AppendLine(Headers);
            while (reader3.Read())
            {
                count++;
                var First = reader3["Id"];
                var Second = reader3["itvid"];
                var Third = reader3["itvRegieParentId"];
                var Fourth = reader3["itvTargetId"];
                string Fifth = "'NULL'";
                var Sixth = reader3["probId"];
                var Seventh = reader3["lgscoreId"];
                var Eight = reader3["lgId"];
                var Nine = reader3["casId"];
                var Ten = reader3["tgId"];
                var Eleven = reader3["casTargetId"];
                var Twelve = reader3["ptid"];
                var Thirteen = reader3["sclSubjectId"];
                var Fourteen = reader3["sclId"];
                var Fifteen = reader3["sclCollectionId"];
                var Sixteen = reader3["probprobleemoptieId"];

                //ProcessStartInfo start = new ProcessStartInfo();
                //start.FileName = @"C:\Users\JasonTjauw\Desktop\Project78\WindowsFormsApp1\test\Untitled.py";
                //start.Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\" \"{5}\" \"{6}\" \"{7}\" \"{8}\" \"{9}\" \"{10}\" \"{11}\" \"{12}\" \"{13}\" \"{14}\" \"{15}\"", First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eight, Nine, Ten, Eleven, Twelve, Thirteen, Fourteen, Fifteen, Sixteen);
                //start.UseShellExecute = false;// Do not use OS shell
                //start.CreateNoWindow = true; // We don't need new window
                //start.RedirectStandardOutput = true;// Any output, generated by application will be redirected back
                //start.RedirectStandardError = true; // Any error in standard output will be redirected back (for example exceptions)
                //using (Process process = Process.Start(start))
                //{
                //    using (StreamReader reader4 = process.StandardOutput)
                //    {
                //        string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
                //        string result = reader4.ReadToEnd(); // Here is the result of StdOut(for example: print "test")
                //    }
                //}
                var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}", First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eight, Nine, Ten, Eleven, Twelve, Thirteen, Fourteen, Fifteen, Sixteen, Environment.NewLine);
                csv.AppendLine(newLine);
                File.WriteAllText(@"C:\Users\JasonTjauw\Documents\Project 78\FinalTest.csv", csv.ToString());
            }
            
            //var csv = new StringBuilder();

            //for (int id = 0; id < Id; id++)
            //{
            //    var first = reader2["Id"];
            //    var second = reader2[2];
            //    var newLine = string.Format("{0},{1}", first, second);
            //    csv.AppendLine(newLine);
            //}

        }
    }
}
