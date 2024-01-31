using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MainConnection
{
    public partial class FormMainConnection : Form
    {
        SqlConnection masterConn;
        SqlConnection mainConn;
        string Server;
        string Database = "master";
        string User;
        string Password;
        string connStringmaster;
        string connStringkeyser;
        Boolean swDBkeyser = false;

        public string sUbicacion;
        public static string wNomLog;
        StreamWriter Log;

        public FormMainConnection()
        {
            InitializeComponent();
        }

        private void FormMainConnection_Load(object sender, EventArgs e)
        {
            // Log
            int wFolio = 0;
            int wTamaño = 0;
            string wsFolio = "";
            string wsTamaño = "";
            string wDirLog = @"C:\keyser\system\Logs\";
            DirectoryInfo dir = new DirectoryInfo(wDirLog);
            foreach (var files in dir.GetFiles())
            {
                //MessageBox.Show(files.Name);
                wsFolio = files.Name.Substring(3, 5);
                wsTamaño = files.Length.ToString();
            }
            if (wsFolio == "") { wsFolio = "0"; }
            if (wsTamaño == "") { wsTamaño = "0"; }

            wFolio = Convert.ToInt32(wsFolio);
            wTamaño = Convert.ToInt32(wsTamaño);

            if (wTamaño > 400000) { wFolio += 1; }

            wsFolio = "00000" + wFolio.ToString();
            int wLen = wsFolio.Length;
            wsFolio = wsFolio.Substring(wLen - 5, 5);
            wNomLog = wDirLog + "Log" + wsFolio + ".txt";

            //MessageBox.Show(wNomLog);
            using (Log = File.AppendText(wNomLog))
            {
                DateTime dateTime = new DateTime();
                dateTime = DateTime.Now;
                string strDate = Convert.ToDateTime(dateTime).ToString("dd-MM-yyyy HH:mm:ss");

                Log.WriteLine(strDate + " Entró a Initial Setup");
                Log.Close();
            }
            // Termina Log
            button0.Image = Image.FromFile(@"C:\keyser\Iconos\Salida.ico");

        }

        private string RegresaConnStringmaster()
        {
            Server = Server.Replace("/", @"\");
            connStringmaster = @"Data Source=" + Server + "; Initial Catalog=" + Database + "; User ID=" + User + "; Password=" + Password + "; Integrated Security=True; Connect Timeout=30; MultipleActiveResultSets=True;";
            return connStringmaster;
        }

        private string RegresaConnStringkeyser()
        {
            connStringkeyser = @"Data Source=" + Server + "; Initial Catalog=keyser; User ID=" + User + "; Password=" + Password + "; Integrated Security=True; Connect Timeout=30; MultipleActiveResultSets=True;";
            return connStringkeyser;
        }

        private void textBoxServerInstance_Leave(object sender, EventArgs e)
        {
            Server = @textBoxServerInstance.Text;
            richTextBoxConnString.Text = RegresaConnStringmaster();
        }

        private void textBoxUsername_Leave(object sender, EventArgs e)
        {
            User = textBoxUsername.Text;
            richTextBoxConnString.Text = RegresaConnStringmaster();
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            Password = textBoxPassword.Text;
            richTextBoxConnString.Text = RegresaConnStringmaster();
        }

        private void EjecutaPrograma(string pNomPrograma)
        {
            try { Process.Start(@"C:\keyser\system\" + pNomPrograma); }
            catch (Exception err) { MessageBox.Show("Error de Ejecución: " + err, "Keyser"); }
        }
        private void button0_Click(object sender, EventArgs e)
        {
            EjecutaPrograma("keyser");
            this.Close();
        }

        //SqlCommand cmd = new SqlCommand("Select * FROM [master].[INFORMATION_SCHEMA].[TABLES]", mainConn);                
        //SqlDataReader rdr = cmd.ExecuteReader();
        //rdr.Close();

        //cmd = new SqlCommand("SELECT name, database_id, create_date FROM sys.databases", mainConn);
        //                while (rdr.Read())
        //                {
        //                    if (rdr["name"].ToString() == "keyser") { swExiste = true; break; }
        //                }
        private void button1_Click(object sender, EventArgs e)
        {
            connStringmaster = richTextBoxConnString.Text;
            labelApproved.Visible = false;
            labelFailed.Visible = false;
            try
            {
                Boolean swExiste = false;
                connStringkeyser = RegresaConnStringkeyser();
                mainConn = new SqlConnection(connStringkeyser);
                mainConn.Open();
                
                SqlCommand cmd = new SqlCommand("Select * from dbms", mainConn);
                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Close();
                mainConn.Close();
                
                labelApproved.Visible = true;
                if (!swExiste) { buttonDB.Visible = true; } else { buttonDB.Visible = false; }
            }
            catch (SqlException ex) { 
                MessageBox.Show(ex.ToString()); 
                labelFailed.Visible = true;
                if (swDBkeyser)
                {
                    MessageBox.Show("Credentials error.", "CONNECTION ERROR");
                }
                else
                {
                    MessageBox.Show("PROBABLE FAILURES: " + Environment.NewLine + " (1) Database keyser not installed or missing, " + Environment.NewLine + " (2) Credentials error," + Environment.NewLine + " (3) Network communication failed," + Environment.NewLine + " (4) SQL Service down.", "CONNECTION ERROR");
                }
                buttonDB.Visible = true;
                folderBrowserDialog1 = new FolderBrowserDialog();
                folderBrowserDialog1.Description = "Select DATA folder and clic on Create Database:";
                folderBrowserDialog1.SelectedPath = "C:\\Program Files\\Microsoft SQL Server\\";
                textBox1.Visible = true;
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = folderBrowserDialog1.SelectedPath;
                }
                string text = File.ReadAllText(@"C:\keyser\system\CreateDB.sql");
                text = text.Replace("RUTA", textBox1.Text + @"\");
                File.WriteAllText(@"C:\keyser\system\CreateDBkeyser.sql", text);
            }
        }

        private void GrabaConfig()
        {
            try
            {
                StreamWriter sw = new StreamWriter(@"C:\keyser\system\keyser.config");
                sw.WriteLine(RegresaConnStringkeyser());
                sw.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter sw = new StreamWriter(@"C:\keyser\system\keyser.config");
                sw.WriteLine(RegresaConnStringkeyser());
                sw.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            MessageBox.Show("Saved correctly", "Keyser");
        }

        private void buttonDB_Click(object sender, EventArgs e)
        {
            SqlConnection masterConn = new SqlConnection(connStringmaster);
            SqlConnection mainConn = new SqlConnection(connStringkeyser);

            if (EjecutarScriptSQL(masterConn, @"C:\keyser\system\CreateDBkeyser.sql", "[Error]"))
            {
                MessageBox.Show("A Database was created.", "Keyser");

                if (EjecutarScriptSQL(mainConn, @"C:\keyser\system\CreateTabledbms.sql", "[Error]"))
                {
                    if (EjecutarScriptSQL(mainConn, @"C:\keyser\system\CreateTabledbm_bases_datos.sql", "[Error]"))
                    {
                        if (EjecutarScriptSQL(mainConn, @"C:\keyser\system\CreateTabledbmtipos_archivos.sql", "[Error]"))
                        {
                            if (EjecutarScriptSQL(mainConn, @"C:\keyser\system\CreateTabledbmarchivos.sql", "[Error]"))
                            {
                                if (EjecutarScriptSQL(mainConn, @"C:\keyser\system\CreateTabledbmtipos_datos.sql", "[Error]"))
                                {
                                    if (EjecutarScriptSQL(mainConn, @"C:\keyser\system\CreateTabledbmdiccionario.sql", "[Error]"))
                                    {
                                        if (EjecutarScriptSQL(mainConn, @"C:\keyser\system\InsertaEndbmarchivos.sql", "[Error]"))
                                        {
                                            if (EjecutarScriptSQL(mainConn, @"C:\keyser\system\InsertaEndbmdiccionario.sql", "[Error]"))
                                            {
                                                if (EjecutarScriptSQL(mainConn, @"C:\keyser\system\ActualizaMarcaEndbmdiccionario.sql", "[Error]"))
                                                {
                                                    if (EjecutarScriptSQL(mainConn, @"C:\keyser\system\InsertaEndbmbases_datos.sql", "[Error]"))
                                                    {
                                                        if (EjecutarScriptSQL(mainConn, @"C:\keyser\system\ActualizaEndbmbases_datos.sql", "[Error]"))
                                                        {
                                                            MessageBox.Show("Click on Test Connection again", "Keyser");
                                                            swDBkeyser = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("PROBABLE FAILURES: " + Environment.NewLine + " (1) Database keyser not installed or missing, " + Environment.NewLine + " (2) Credentials error," + Environment.NewLine + " (3) Network communication failed," + Environment.NewLine + " (4) SQL Service down.", "CONNECTION ERROR");
                goto Exit;
            }

            if (EjecutarScriptSQL(masterConn, @"C:\keyser\system\InsertaValoresDBMS.sql", "[Error]"))
            { }

            GrabaConfig();

        Exit: { }
        }

        private Boolean EjecutarScriptSQL(SqlConnection pConn, string pNomScript, string pMsgDeError)
        {
            pConn.Open();
            try
            {
                FileInfo file = new FileInfo(pNomScript);
                string script = file.OpenText().ReadToEnd();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = pConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = script;
                cmd.CommandTimeout = 3600;
                cmd.ExecuteNonQuery();
                EscribeLog("[Info] Ejecutó Script: " + pNomScript);
                pConn.Close();
                return true;
            }
            catch (Exception err)
            {
                EscribeLog("[Error] Script: " + pNomScript);
                EscribeLog("[Error] " + err);
                MessageBox.Show(err.ToString(), pNomScript);

                pConn.Close();
                return false;
            }
        }

        private void EscribeLog(string pTexto)
        {
            using (Log = File.AppendText(wNomLog))
            {
                DateTime dateTime = new DateTime();
                dateTime = DateTime.Now;
                string strDate = Convert.ToDateTime(dateTime).ToString("dd-MM-yyyy HH:mm:ss");

                Log.WriteLine(strDate + " " + pTexto);
                Log.Close();
            }
        }

        private void textBoxServerInstance_TextChanged(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }
    }
}
