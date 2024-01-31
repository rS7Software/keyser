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
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace keyser
{
    public partial class FormMenu : Form
    {
        public string sUbicacion;
        public static string wNomLog;
        StreamWriter Log;

        public FormMenu()
        {
            InitializeComponent();
        }

        private void salidaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // AfectaKardex(Mainconn) // Afectación a Kardex Pendiente

            //TareasProgramadas.Show;

            this.Close();
        }

        private void FormMenu_Load(object sender, EventArgs e)
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

                Log.WriteLine(strDate + " Entró al Menú principal");
                Log.Close();
            }
            // Termina Log
        }

        private void EjecutaPrograma(string pNomPrograma)
        {
            try { Process.Start(@"C:\keyser\system\" + pNomPrograma); }
            catch (Exception err) { MessageBox.Show("Error de Ejecución: " + err, "keyser"); }
        }
        private void salidaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void loadDataDictionaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EjecutaPrograma("LoadDataDictionary");
            this.Close();
        }

        private void menuStrip1_ItemClicked(object sender, EventArgs e)
        {

        }

        private void DBMMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mainConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
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
            }
            catch (Exception err)
            {
                //richTextBoxLog.Text += "[Error] Script: " + pNomScript + Environment.NewLine; Thread.Sleep(1000); richTextBoxLog.Refresh();
                //richTextBoxLog.Text += err + Environment.NewLine; Thread.Sleep(1000); richTextBoxLog.Refresh();

                pConn.Close();
                return false;
            }
            pConn.Close();
            return true;
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

        private Boolean EjecutarComandoSQL(SqlConnection pConn, string pComando, string pMsgDeError)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(pComando, pConn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 3600;
                cmd.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                //richTextBoxLog.Text += Environment.NewLine + pMsgDeError + err + Environment.NewLine; Thread.Sleep(1000); richTextBoxLog.Refresh();
                return false;
            }
            return true;
        }

        private void sQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EjecutaPrograma("SQLWizards");
            this.Close();
        }

        private void initialSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EjecutaPrograma("MainConnection");
            this.Close();
        }

        private void contactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!richTextBox1.Visible) { richTextBox1.Visible = true; }
            else
            { richTextBox1.Visible = false; }   
        }
    }
}
