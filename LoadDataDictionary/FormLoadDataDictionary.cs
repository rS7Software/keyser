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
using keyser;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;

namespace LoadDataDictionary
{
    public partial class FormLoadDataDictionary : Form
    {
        string entraDBName;
        string mainConnString;
        string entraConnString;
        public string sUbicacion;
        public static string wNomLog;
        StreamWriter Log;

        public FormLoadDataDictionary()
        {
            InitializeComponent();
        }

        private void FormLoadDataDictionary_Load(object sender, EventArgs e)
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

                Log.WriteLine(strDate + " Entró a LoadDataDictionary");
                Log.Close();
            }
            // Termina Log

            button0.Image = Image.FromFile(@"C:\keyser\Iconos\Salida.ico");

            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\keyser\system\keyser.config");
            while ((line = file.ReadLine()) != null)
            { richTextBoxConnString.Text = line; }
            file.Close();

            string server = Utilerias.ObtieneDataSource(richTextBoxConnString.Text);
            string database = Utilerias.ObtieneInitialCatalog(richTextBoxConnString.Text);
            string username = Utilerias.ObtieneUserID(richTextBoxConnString.Text);
            string password = Utilerias.ObtienePassword(richTextBoxConnString.Text);
            string resto = Utilerias.ObtieneResto(richTextBoxConnString.Text);
            mainConnString = "Data Source=" + server + "; Initial Catalog=" + database + "; User ID=" + username + "; Password=" + password + "; " + resto;
            richTextBoxConnString.Text = mainConnString;

            if (server == "" || database == "" || username == "" || password == "")
            { MessageBox.Show("Incorrect data. You may need to run the setup main connection test.", "keyser"); }

            SqlConnection mainConn = new SqlConnection(mainConnString);
            if (EjecutarScriptSQL(mainConn, @"C:\keyser\system\InsertaEndbmarchivos.sql", "[Error]"))
            { }
            if (EjecutarScriptSQL(mainConn, @"C:\keyser\system\InsertaEndbmdiccionario.sql", "[Error]"))
            { }
            if (EjecutarScriptSQL(mainConn, @"C:\keyser\system\InsertaEndbmbases_datos.sql", "[Error]"))
            { }
            if (EjecutarScriptSQL(mainConn, @"C:\keyser\system\ActualizaEndbmbases_datos.sql", "[Error]"))
            { }
        }

        private void EjecutaPrograma(string pNomPrograma)
        {
            try { Process.Start(@"C:\keyser\system\" + pNomPrograma); }
            catch (Exception err) { MessageBox.Show("Error de Ejecución: " + err, "keyser"); }
        }
        private void button0_Click(object sender, EventArgs e)
        {
            EjecutaPrograma("keyser");
            this.Close();
        }

        private void textBoxSourceDataBase_Leave(object sender, EventArgs e)
        {

            entraDBName = textBoxSourceDataBase.Text.ToUpper();
            textBoxSourceDataBase.Text = entraDBName;
            string server = Utilerias.ObtieneDataSource(richTextBoxConnString.Text);
            string database = Utilerias.ObtieneInitialCatalog(richTextBoxConnString.Text);
            string username = Utilerias.ObtieneUserID(richTextBoxConnString.Text);
            string password = Utilerias.ObtienePassword(richTextBoxConnString.Text);
            string resto = Utilerias.ObtieneResto(richTextBoxConnString.Text);
            entraConnString = "Data Source=" + server + "; Initial Catalog=" + entraDBName + "; User ID=" + username + "; Password=" + password + "; " + resto;
            richTextBoxConnString.Text = entraConnString;

            if (server == "" || database == "" || username == "" || password == "")
            { MessageBox.Show("Incorrect data. You may need to run the setup main connection test.", "keyser"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection mainConn = new SqlConnection(mainConnString);
            SqlConnection entraConn = new SqlConnection(entraConnString);

            int cont = 0;
            string wsComandoSQL;
            mainConn.Open();          
            entraConn.Open();
            wsComandoSQL = "Delete from [dbo].[dbmdiccionario] where cve_base_datos = '" + entraDBName + "'";
            EjecutarComandoSQL(mainConn, wsComandoSQL, "[Err] SQL Command: ");

            SqlCommand cmdR = new SqlCommand("Select * from dbmarchivos order by num_archivo", mainConn);
            SqlDataReader rdr = cmdR.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            rdr.Close();
            foreach (DataRow row in dt.Rows)
            {
                CargarSchema(entraConn, mainConn, row[1].ToString());
                richTextBoxLog.Text += row[1].ToString() + " - ";
                cont += 1;
            }

            richTextBoxLog.Text += "Proceso terminado. Se cargaron " + cont.ToString() + " tablas." + Environment.NewLine; Thread.Sleep(1000); richTextBoxLog.Refresh();
            EscribeLog("Proceso terminado. Se cargaron \" + cont.ToString() + \" tablas.");
            mainConn.Close();
            entraConn.Close();

        Exit: { }
        }

        private void richTextBoxConnString_Leave(object sender, EventArgs e)
        {
            entraConnString = richTextBoxConnString.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection mainConn = new SqlConnection(mainConnString);
            SqlConnection entraConn = new SqlConnection(entraConnString);

            int cont = 0;
            string wsComandoSQL;
            mainConn.Open();
            entraConn.Open();

            wsComandoSQL = "DELETE FROM [dbo].[dbmarchivos]";
            EjecutarComandoSQL(mainConn, wsComandoSQL, "[Err] SQL Command: ");

            SqlCommand cmdR = new SqlCommand("SELECT object_id, name, max_column_id_used, type, type_desc, create_date, modify_date, lock_escalation_desc \r\nFROM sys.tables order by name", entraConn);
            SqlDataReader rdr = cmdR.ExecuteReader();
            while (rdr.Read())
            {
                Boolean sw = true;
                if (rdr["name"].ToString().Length > 2)
                {
                    if (rdr["name"].ToString().Substring(0, 3) == "dbm") { sw = false; }
                }

                if (rdr["name"].ToString() == "sysdiagrams") { sw = false; }

                if (sw)
                {
                    cont += 1;
                    richTextBoxLog.Text += rdr["name"].ToString() + " - ";

                    try
                    {
                        SqlCommand cmdI = new SqlCommand("InsertaEndbmarchivos", mainConn);
                        cmdI.CommandType = CommandType.StoredProcedure;
                        cmdI.Parameters.Add(new SqlParameter("@param0", SqlDbType.Int));
                        cmdI.Parameters.Add(new SqlParameter("@param1", SqlDbType.VarChar));
                        cmdI.Parameters.Add(new SqlParameter("@param2", SqlDbType.VarChar));
                        cmdI.Parameters.Add(new SqlParameter("@param3", SqlDbType.Bit));
                        cmdI.Parameters.Add(new SqlParameter("@param4", SqlDbType.Bit));
                        cmdI.Parameters["@param0"].Value = cont;
                        cmdI.Parameters["@param1"].Value = rdr["name"].ToString();
                        cmdI.Parameters["@param2"].Value = "Master";
                        cmdI.Parameters["@param3"].Value = 0;
                        cmdI.Parameters["@param4"].Value = 0;
                        cmdI.ExecuteNonQuery();
                    }
                    catch (Exception err)
                    {
                        mainConn.Close();
                        entraConn.Close();
                        EscribeLog("[Error] Script: InsertaEndbmArchivos");
                        EscribeLog("[Error] " + err);

                        MessageBox.Show("Error: " + err);
                        Form form = FormLoadDataDictionary.ActiveForm;
                        form.Close();
                    }
                }
            }
            rdr.Close();
            richTextBoxLog.Text += "Proceso terminado. Se cargaron " + cont.ToString() + " tablas." + Environment.NewLine; Thread.Sleep(1000); richTextBoxLog.Refresh();
            EscribeLog("Proceso terminado. Se cargaron \" + cont.ToString() + \" tablas.");

            Boolean swExiste = false;
            SqlCommand cmdU = new SqlCommand("SELECT * FROM dbmbases_datos", mainConn);
            SqlDataReader rdrU = cmdU.ExecuteReader();
            while (rdrU.Read())
            {
                if (rdrU["cve_base_datos"].ToString() == textBoxSourceDataBase.Text) { swExiste = true; break; }
            }
            rdrU.Close();

            if (swExiste)
            {
                try
                {
                    SqlCommand cmdI = new SqlCommand("ActualizaEndbmbases_datos", mainConn);
                    cmdI.CommandType = CommandType.StoredProcedure;
                    cmdI.Parameters.Add(new SqlParameter("@param0", SqlDbType.VarChar));
                    cmdI.Parameters.Add(new SqlParameter("@param1", SqlDbType.VarChar));
                    cmdI.Parameters.Add(new SqlParameter("@param2", SqlDbType.VarChar));
                    cmdI.Parameters.Add(new SqlParameter("@param3", SqlDbType.Int));
                    cmdI.Parameters["@param0"].Value = entraDBName;
                    cmdI.Parameters["@param1"].Value = entraDBName;
                    cmdI.Parameters["@param2"].Value = "SQL Server";
                    cmdI.Parameters["@param3"].Value = 1;
                    cmdI.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    EscribeLog("[Error] Script: ActualizaEndbmbases_datos");
                    EscribeLog("[Error] " + err);

                    mainConn.Close();
                    entraConn.Close();
                    MessageBox.Show("Error: " + err);
                    Form form = FormLoadDataDictionary.ActiveForm;
                    form.Close();
                }
            }
            else
            {
                try
                {
                    SqlCommand cmdI = new SqlCommand("InsertaEndbmbases_datos", mainConn);
                    cmdI.CommandType = CommandType.StoredProcedure;
                    cmdI.Parameters.Add(new SqlParameter("@param0", SqlDbType.VarChar));
                    cmdI.Parameters.Add(new SqlParameter("@param1", SqlDbType.VarChar));
                    cmdI.Parameters.Add(new SqlParameter("@param2", SqlDbType.VarChar));
                    cmdI.Parameters.Add(new SqlParameter("@param3", SqlDbType.Int));
                    cmdI.Parameters["@param0"].Value = entraDBName;
                    cmdI.Parameters["@param1"].Value = entraDBName;
                    cmdI.Parameters["@param2"].Value = "SQL Server";
                    cmdI.Parameters["@param3"].Value = 1;
                    cmdI.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    EscribeLog("[Error] Script: InsertaEndbmbases_datos");
                    EscribeLog("[Error] " + err);

                    mainConn.Close();
                    entraConn.Close();
                    MessageBox.Show("Error: " + err);
                    Form form = FormLoadDataDictionary.ActiveForm;
                    form.Close();
                }
            }

            wsComandoSQL = "UPDATE [dbo].[dbmbases_datos] SET enlinea = 0 WHERE cve_base_datos != '" + entraDBName + "'";
            EjecutarComandoSQL(mainConn, wsComandoSQL, "[Err] SQL Command: ");

            mainConn.Close();
            entraConn.Close();

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
            }
            catch (Exception err)
            {
                EscribeLog("[Error] Script: " + pNomScript);
                EscribeLog("[Error] " + err);

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
                richTextBoxLog.Text += Environment.NewLine + pMsgDeError + err + Environment.NewLine; Thread.Sleep(1000); richTextBoxLog.Refresh();
                return false;
            }
            return true;
        }

        private void CargarSchema(SqlConnection pentraConn, SqlConnection pmainConn, string pNomTabla)
        {
            SqlCommand cmdE = new SqlCommand("Select * from " + pNomTabla, pentraConn);
            SqlDataReader rdrE = cmdE.ExecuteReader(CommandBehavior.KeyInfo);
            DataTable schemaTable = rdrE.GetSchemaTable();
            rdrE.Close();

            try
            {
                string wColumnName = "";
                string wColumnOrdinal = "";
                string wColumnSize = "";
                string wColumnDataTypeName = "";
                foreach (DataRow row in schemaTable.Rows)
                {
                    foreach (DataColumn Propiedad in schemaTable.Columns)
                    {
                        if (Propiedad.ColumnName == "ColumnName") { wColumnName = row[Propiedad].ToString(); }
                        if (Propiedad.ColumnName == "ColumnOrdinal") { wColumnOrdinal = row[Propiedad].ToString(); }
                        if (Propiedad.ColumnName == "ColumnSize") { wColumnSize = row[Propiedad].ToString(); }
                        if (Propiedad.ColumnName == "DataTypeName") { wColumnDataTypeName = row[Propiedad].ToString(); }
                    }

                    SqlCommand cmdI = new SqlCommand("InsertaEndbmdiccionario", pmainConn);
                    cmdI.CommandType = CommandType.StoredProcedure;
                    cmdI.Parameters.Add(new SqlParameter("@param0", SqlDbType.VarChar));
                    cmdI.Parameters.Add(new SqlParameter("@param1", SqlDbType.VarChar));
                    cmdI.Parameters.Add(new SqlParameter("@param2", SqlDbType.TinyInt));
                    cmdI.Parameters.Add(new SqlParameter("@param3", SqlDbType.VarChar));
                    cmdI.Parameters.Add(new SqlParameter("@param4", SqlDbType.VarChar));
                    cmdI.Parameters.Add(new SqlParameter("@param5", SqlDbType.Int));
                    cmdI.Parameters["@param0"].Value = entraDBName;
                    cmdI.Parameters["@param1"].Value = pNomTabla;
                    cmdI.Parameters["@param2"].Value = Convert.ToByte(wColumnOrdinal);
                    cmdI.Parameters["@param3"].Value = wColumnName;
                    cmdI.Parameters["@param4"].Value = wColumnDataTypeName;
                    cmdI.Parameters["@param5"].Value = Convert.ToInt32(wColumnSize);
                    cmdI.ExecuteNonQuery();
                }
            }
            catch (Exception err)
            {
                EscribeLog("[Error] Script: InsertaEndbmdiccionario");
                EscribeLog("[Error] " + err);

                pmainConn.Close();
                pentraConn.Close();
                MessageBox.Show("Error: " + err);
                Form form = FormLoadDataDictionary.ActiveForm;
                form.Close();
            }
        }

        private void textBoxSourceDataBase_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxSourceDataBase_Enter(object sender, EventArgs e)
        {
            SqlConnection mainConn = new SqlConnection(mainConnString);
            mainConn.Open();
            SqlCommand cmdR = new SqlCommand("SELECT * FROM dbmbases_datos", mainConn);
            SqlDataReader rdr = cmdR.ExecuteReader();
            while (rdr.Read()) 
            { 
                if (Convert.ToBoolean(rdr["enlinea"].ToString()))
                {
                    textBoxSourceDataBase.Text = rdr["cve_base_datos"].ToString();
                    break;
                }
            }
            rdr.Close();
            mainConn.Close();
        }
    }
}
