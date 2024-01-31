using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using keyser;

namespace SQLWizards
{
    public partial class FormSQLWizards : Form
    {
        string entraDBName;
        string mainConnString;
        string entraConnString;
        public string sUbicacion;
        public static string wNomLog;
        StreamWriter Log;

        SqlConnection mainConn;
        SqlDataAdapter adaGrid;
        SqlCommandBuilder buiGrid;
        DataTable dataTableGrid;
        SqlDataAdapter adaGridD;
        SqlCommandBuilder buiGridD;
        DataTable dataTableGridD;
        String nomTabla = "";

        public FormSQLWizards()
        {
            InitializeComponent();
        }

        private void FormSQLWizards_Load(object sender, EventArgs e)
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

            String config = "";
            sUbicacion = @"C:\keyser\system\config.txt";
            String codigoEnc = Utilerias.LeerConfigEncriptado(sUbicacion);
            if (codigoEnc.Length > 0)
            {
                config = keyser.EncriptacionMax.Decriptado(codigoEnc);
            }

            string expiracion = Utilerias.ObtieneParametroConfig("@FechaExp", config);
            string aviso = Utilerias.ObtieneParametroConfig("@FechaAviso", config);

            if (Convert.ToDateTime(expiracion) < System.DateTime.Now)
            {
                MessageBox.Show("Software precated");
                Application.Exit();
            }

            if (Convert.ToDateTime(aviso) < System.DateTime.Now)
            {
                MessageBox.Show("Tool about to precated, update the version soon");
            }

            button0.Image = Image.FromFile(@"C:\keyser\Iconos\Salida.ico");
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.button0, "Exit to the Menu");

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

            mainConn = new SqlConnection(mainConnString);

            if (EjecutarScriptSQL(mainConn, @"C:\keyser\system\InsertaEndbmbases_datos.sql", "[Error]"))
            { }
            if (EjecutarScriptSQL(mainConn, @"C:\keyser\system\ActualizaEndbmbases_datos.sql", "[Error]"))
            { }
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
        private void textBoxSourceDataBase_Leave(object sender, EventArgs e)
        {
            entraDBName = textBoxSourceDataBase.Text.ToUpper();
            textBoxSourceDataBase.Text = entraDBName;
            string server = Utilerias.ObtieneDataSource(richTextBoxConnString.Text);
            string database = Utilerias.ObtieneInitialCatalog(richTextBoxConnString.Text);
            string username = Utilerias.ObtieneUserID(richTextBoxConnString.Text);
            string password = Utilerias.ObtienePassword(richTextBoxConnString.Text);
            string resto = Utilerias.ObtieneResto(richTextBoxConnString.Text);
            entraConnString = "Data Source=" + server + "; Initial Catalog=" + textBoxSourceDataBase.Text + "; User ID=" + username + "; Password=" + password + "; " + resto;
            richTextBoxConnString.Text = entraConnString;

            if (server == "" || database == "" || username == "" || password == "")
            { MessageBox.Show("Incorrect data. You may need to run the setup main connection test.", "keyser"); }
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

        private void buttonTablas_Click(object sender, EventArgs e)
        {
            string wSelect = "SELECT DISTINCT cve_base_datos, nom_tabla FROM dbmdiccionario WHERE cve_base_datos = '" + entraDBName + "'";
            adaGrid = new SqlDataAdapter(wSelect, mainConn);
            dataTableGrid = new DataTable();
            adaGrid.Fill(dataTableGrid);

            dataGridViewTablas.Visible = true;
            dataGridViewTablas.DataSource = dataTableGrid;
            dataGridViewTablas.Columns[0].Visible = false;
            dataGridViewTablas.Columns[1].HeaderText = "Tables";
            dataGridViewTablas.Columns[1].DefaultCellStyle.BackColor = SystemColors.Control;
            dataGridViewTablas.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewTablas.Columns[1].Width = 200;
            dataGridViewTablas.Columns[1].ReadOnly = true;

            wSelect = "SELECT num_ordinal, nom_campo, cve_tipo_datos, tam, cve_base_datos, nom_tabla, marca FROM dbmdiccionario WHERE cve_base_datos = '" + entraDBName + "' AND nom_tabla = 'sysconstantes' ORDER BY num_ordinal";
            adaGridD = new SqlDataAdapter(wSelect, mainConn);
            dataTableGridD = new DataTable();
            adaGridD.Fill(dataTableGridD);
            dataGridView1.Visible = true;
            dataGridView1.DataSource = dataTableGridD;
            dataGridView1.Columns[0].HeaderText = "Ord";
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = SystemColors.Control;
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns[0].Width = 30;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].HeaderText = "Field";
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[1].DefaultCellStyle.BackColor = SystemColors.Control;
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].HeaderText = "Type";
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[2].DefaultCellStyle.BackColor = SystemColors.Control;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns[2].Width = 70;
            dataGridView1.Columns[3].HeaderText = "Size";
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[3].DefaultCellStyle.BackColor = SystemColors.Control;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns[3].Width = 30;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].HeaderText = "";
            dataGridView1.Columns[6].Width = 20;

            mainConn.Open();
            Boolean swExiste = false;
            SqlCommand cmdU = new SqlCommand("SELECT * FROM dbmbases_datos", mainConn);
            SqlDataReader rdrU = cmdU.ExecuteReader();
            while (rdrU.Read())
            {
                if (rdrU["cve_base_datos"].ToString() == textBoxSourceDataBase.Text.ToUpper().Trim()) { swExiste = true; break; }
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
                    MessageBox.Show("Error: " + err);
                    this.Close();
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
                    MessageBox.Show("Error: " + err);
                    this.Close();
                }
            }

            String wsComandoSQL = "UPDATE [dbo].[dbmbases_datos] SET enlinea = 0 WHERE cve_base_datos != '" + entraDBName + "'";
            EjecutarComandoSQL(mainConn, wsComandoSQL, "[Err] SQL Command: ");

            mainConn.Close();
        }

        private void FormSQLWizards_Leave(object sender, EventArgs e)
        {
            mainConn.Close();
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridViewTablas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            nomTabla = dataGridViewTablas.CurrentRow.Cells[1].Value.ToString();
            String wSelect = "SELECT num_ordinal, nom_campo, cve_tipo_datos, tam, cve_base_datos, nom_tabla, marca FROM dbmdiccionario WHERE cve_base_datos = '" + entraDBName + "' AND nom_tabla = '" + nomTabla + "' ORDER BY num_ordinal";
            adaGridD = new SqlDataAdapter(wSelect, mainConn);
            dataTableGridD = new DataTable();
            adaGridD.Fill(dataTableGridD);
            dataGridView1.Visible = true;
            dataGridView1.DataSource = dataTableGridD;
        }

        //Insert
        private void button2_Click(object sender, EventArgs e)
        {
            if (nomTabla == "") {  goto Exit; }

            String nL = Environment.NewLine;
            String script = "CREATE PROCEDURE [dbo].[InsertaEn" + nomTabla + "]";

            mainConn.Open();
            SqlCommand cmdR = new SqlCommand("SELECT num_ordinal, nom_campo, cve_tipo_datos, tam, cve_base_datos, nom_tabla, marca FROM dbmdiccionario WHERE cve_base_datos = '" + entraDBName + "' AND nom_tabla = '" + nomTabla + "' ORDER BY num_ordinal", mainConn);
            SqlDataReader rdr = cmdR.ExecuteReader();
            while (rdr.Read())
            {
                String ord = "0" + rdr["num_ordinal"].ToString();
                ord = ord.ToString().Substring(ord.Length - 2, 2);
                String cve_td = rdr["cve_tipo_datos"].ToString();
                String tam = rdr["tam"].ToString();

                byte o = Convert.ToByte(ord);
                if (Convert.ToBoolean(dataGridView1[6, o].Value))
                {
                    script += nL + "    @param" + ord + " " + cve_td;
                    if (cve_td == "varchar" || cve_td == "nvarchar") { script += "(" + tam + "),"; }
                    else { script += ","; }
                }
            }
            rdr.Close();

            script = script.Substring(0, script.Length - 1) + nL + nL  + "AS BEGIN";
            script += nL + nL + "INSERT INTO [dbo].[" + nomTabla + "] (";

            rdr = cmdR.ExecuteReader();
            while (rdr.Read())
            {
                byte o = Convert.ToByte(rdr["num_ordinal"].ToString());
                if (Convert.ToBoolean(dataGridView1[6, o].Value))
                {
                    String nomCampo = rdr["nom_campo"].ToString();
                    script += nL + "    [" + nomCampo + "],";
                }
            }
            rdr.Close();

            script = script.Substring(0, script.Length - 1) + ")" +nL + "VALUES (";

            rdr = cmdR.ExecuteReader();
            while (rdr.Read())
            {
                byte o = Convert.ToByte(rdr["num_ordinal"].ToString());
                if (Convert.ToBoolean(dataGridView1[6, o].Value))
                {
                    String ord = "0" + o.ToString();
                    ord = ord.ToString().Substring(ord.Length - 2, 2);
                    script += nL + "    @param" + ord + ",";
                }
            }
            rdr.Close();
            mainConn.Close();

            script = script.Substring(0, script.Length - 1) + ")" + nL + nL + "END";
            richTextBoxSQL.Text = script;

            Exit: { }
        }

        //Create
        private void button1_Click(object sender, EventArgs e)
        {
            if (nomTabla == "") { goto Exit; }

            byte o = 0;
            int r = 0;
            String nL = Environment.NewLine;
            String script = "USE [" + entraDBName + "]" + nL + "GO" + nL + nL; 
            script += "CREATE TABLE [dbo].[" + nomTabla + "](";

            mainConn.Open();
            SqlCommand cmdR = new SqlCommand("SELECT num_ordinal, nom_campo, cve_tipo_datos, tam, cve_base_datos, nom_tabla, marca FROM dbmdiccionario WHERE cve_base_datos = '" + entraDBName + "' AND nom_tabla = '" + nomTabla + "' ORDER BY num_ordinal", mainConn);
            SqlDataReader rdr = cmdR.ExecuteReader();
            while (rdr.Read())
            {
                String nomCampo = rdr["nom_campo"].ToString();
                String cve_td = rdr["cve_tipo_datos"].ToString();
                String tam = rdr["tam"].ToString();
                o = Convert.ToByte(rdr["num_ordinal"].ToString());

                script += nL + "    [" + nomCampo + "] [" + cve_td + "]";
                if (cve_td == "varchar" || cve_td == "nvarchar") { script += "(" + tam + ") NOT NULL,"; }
                else { script += " NOT NULL,"; }
            }
            rdr.Close();
            mainConn.Close();

            script = script.Substring(0, script.Length - 1) + nL + "PRIMARY KEY CLUSTERED";
            script += nL + "(";

            while (r <= o)
            {
                String nomCampo = dataGridView1[1, r].Value.ToString();

                if (Convert.ToBoolean(dataGridView1[6, r].Value))
                {
                    script += nL + "    [" + nomCampo + "] ASC,";
                }
                r += 1;
            }

            script = script.Substring(0, script.Length - 1);
            script += nL + ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]";
            script += nL + ") ON [PRIMARY]";
            script += nL + "GO";

            richTextBoxSQL.Text = script;

        Exit: { }
        }

        //Update
        private void button3_Click(object sender, EventArgs e)
        {
            if (nomTabla == "") { goto Exit; }

            String nL = Environment.NewLine;
            String script = "CREATE PROCEDURE [dbo].[ActualizaEn" + nomTabla + "]";

            mainConn.Open();
            SqlCommand cmdR = new SqlCommand("SELECT num_ordinal, nom_campo, cve_tipo_datos, tam, cve_base_datos, nom_tabla, marca FROM dbmdiccionario WHERE cve_base_datos = '" + entraDBName + "' AND nom_tabla = '" + nomTabla + "' ORDER BY num_ordinal", mainConn);
            SqlDataReader rdr = cmdR.ExecuteReader();
            while (rdr.Read())
            {
                String ord = "0" + rdr["num_ordinal"].ToString();
                ord = ord.ToString().Substring(ord.Length - 2, 2);
                String cve_td = rdr["cve_tipo_datos"].ToString();
                String tam = rdr["tam"].ToString();

                script += nL + "    @param" + ord + " " + cve_td;
                if (cve_td == "varchar" || cve_td == "nvarchar") { script += "(" + tam + "),"; }
                else { script += ","; }
            }
            rdr.Close();

            script = script.Substring(0, script.Length - 1) + nL + nL + "AS BEGIN";
            script += nL + nL + "UPDATE [dbo].[" + nomTabla + "] SET";

            rdr = cmdR.ExecuteReader();
            while (rdr.Read())
            {
                byte o = Convert.ToByte(rdr["num_ordinal"].ToString());
                if (Convert.ToBoolean(dataGridView1[6, o].Value) == false)
                {
                    String ord = "0" + o.ToString();
                    ord = ord.Substring(ord.Length - 2, 2);
                    String nomCampo = rdr["nom_campo"].ToString();
                    script += nL + "    [" + nomCampo + "] = @param" + ord + ",";
                }
            }
            rdr.Close();

            script = script.Substring(0, script.Length - 1) + nL + "WHERE ";

            rdr = cmdR.ExecuteReader();
            while (rdr.Read())
            {
                byte o = Convert.ToByte(rdr["num_ordinal"].ToString());
                if (Convert.ToBoolean(dataGridView1[6, o].Value))
                {
                    String ord = "0" + o.ToString();
                    ord = ord.ToString().Substring(ord.Length - 2, 2);
                    String nomCampo = rdr["nom_campo"].ToString();
                    script += nL + "    " + nomCampo + " = @param" + ord + " AND";
                }
            }
            rdr.Close();
            mainConn.Close();

            script = script.Substring(0, script.Length - 3) + nL + nL + "END";
            richTextBoxSQL.Text = script;

        Exit: { }
        }

        //Delete
        private void button4_Click(object sender, EventArgs e)
        {
            if (nomTabla == "") { goto Exit; }

            String nL = Environment.NewLine;
            String script = "CREATE PROCEDURE [dbo].[BorraEn" + nomTabla + "]";

            mainConn.Open();
            SqlCommand cmdR = new SqlCommand("SELECT num_ordinal, nom_campo, cve_tipo_datos, tam, cve_base_datos, nom_tabla, marca FROM dbmdiccionario WHERE cve_base_datos = '" + entraDBName + "' AND nom_tabla = '" + nomTabla + "' ORDER BY num_ordinal", mainConn);
            SqlDataReader rdr = cmdR.ExecuteReader();
            while (rdr.Read())
            {
                String ord = "0" + rdr["num_ordinal"].ToString();
                ord = ord.ToString().Substring(ord.Length - 2, 2);
                String cve_td = rdr["cve_tipo_datos"].ToString();
                String tam = rdr["tam"].ToString();

                byte o = Convert.ToByte(ord);
                if (Convert.ToBoolean(dataGridView1[6, o].Value))
                {
                    script += nL + "    @param" + ord + " " + cve_td;
                    if (cve_td == "varchar" || cve_td == "nvarchar") { script += "(" + tam + "),"; }
                    else { script += ","; }
                }
            }
            rdr.Close();

            script = script.Substring(0, script.Length - 1) + nL + nL + "AS BEGIN";
            script += nL + nL + "DELETE FROM [dbo].[" + nomTabla + "] WHERE ";

            rdr = cmdR.ExecuteReader();
            while (rdr.Read())
            {
                byte o = Convert.ToByte(rdr["num_ordinal"].ToString());
                if (Convert.ToBoolean(dataGridView1[6, o].Value))
                {
                    String ord = "0" + o.ToString();
                    ord = ord.ToString().Substring(ord.Length - 2, 2);
                    String nomCampo = rdr["nom_campo"].ToString();
                    script += nL + "    " + nomCampo + " = @param" + ord + " AND";
                }
            }
            rdr.Close();
            mainConn.Close();

            script = script.Substring(0, script.Length - 3) + nL + nL + "END";
            richTextBoxSQL.Text = script;

        Exit: { }
        }

        //Select
        private void button5_Click(object sender, EventArgs e)
        {
            if (nomTabla == "") { goto Exit; }

            String nL = Environment.NewLine;
            String script = "SELECT ";

            mainConn.Open();
            SqlCommand cmdR = new SqlCommand("SELECT num_ordinal, nom_campo, cve_tipo_datos, tam, cve_base_datos, nom_tabla, marca FROM dbmdiccionario WHERE cve_base_datos = '" + entraDBName + "' AND nom_tabla = '" + nomTabla + "' ORDER BY num_ordinal", mainConn);
            SqlDataReader rdr = cmdR.ExecuteReader();
            while (rdr.Read())
            {
                String nomCampo = rdr["nom_campo"].ToString();
                script += nL + "    [" + nomCampo + "],";
            }
            rdr.Close();

            script = script.Substring(0, script.Length - 1);
            script += nL + "FROM [dbo].[" + nomTabla + "]";
            script += nL + "WHERE ";

            rdr = cmdR.ExecuteReader();
            while (rdr.Read())
            {
                byte o = Convert.ToByte(rdr["num_ordinal"].ToString());
                if (Convert.ToBoolean(dataGridView1[6, o].Value))
                {
                    String ord = "0" + o.ToString();
                    ord = ord.ToString().Substring(ord.Length - 2, 2);
                    String nomCampo = rdr["nom_campo"].ToString();
                    script += nL + "    " + nomCampo + " = @param" + ord + " AND";
                }
            }
            rdr.Close();
            mainConn.Close();

            script = script.Substring(0, script.Length - 3) + nL + nL + "END";
            richTextBoxSQL.Text = script;

        Exit: { }
        }

        //Invest
        private void button6_Click(object sender, EventArgs e)
        {
            if (nomTabla == "") { goto Exit; }
            mainConn.Open();
            SqlCommand cmdR = new SqlCommand("SELECT num_ordinal, nom_campo, cve_tipo_datos, tam, cve_base_datos, nom_tabla, marca FROM dbmdiccionario WHERE cve_base_datos = '" + entraDBName + "' AND nom_tabla = '" + nomTabla + "' ORDER BY num_ordinal", mainConn);
            SqlDataReader rdr = cmdR.ExecuteReader();
            while (rdr.Read())
            {
                String ord = rdr["num_ordinal"].ToString();
                Byte r = Convert.ToByte(ord);
                string nomTabla = dataGridView1[5, r].Value.ToString();
                Byte marca;
                if (Convert.ToBoolean(dataGridView1[6, r].Value.ToString())) { marca = 0; }
                else { marca = 1; }

                try
                {
                    SqlCommand cmdA = new SqlCommand("ActualizaMarcaEndbmdiccionario", mainConn);
                    cmdA.CommandType = CommandType.StoredProcedure;
                    cmdA.Parameters.Add(new SqlParameter("@param0", SqlDbType.VarChar));
                    cmdA.Parameters.Add(new SqlParameter("@param1", SqlDbType.VarChar));
                    cmdA.Parameters.Add(new SqlParameter("@param2", SqlDbType.VarChar));
                    cmdA.Parameters.Add(new SqlParameter("@param3", SqlDbType.Bit));
                    cmdA.Parameters["@param0"].Value = entraDBName;
                    cmdA.Parameters["@param1"].Value = nomTabla;
                    cmdA.Parameters["@param2"].Value = Convert.ToByte(ord);
                    cmdA.Parameters["@param3"].Value = marca;
                    cmdA.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    mainConn.Close();

                    MessageBox.Show("Error: " + err);
                    Form form = FormSQLWizards.ActiveForm;
                    form.Close();
                }
            }
            mainConn.Close();

            nomTabla = dataGridViewTablas.CurrentRow.Cells[1].Value.ToString();
            String wSelect = "SELECT num_ordinal, nom_campo, cve_tipo_datos, tam, cve_base_datos, nom_tabla, marca FROM dbmdiccionario WHERE cve_base_datos = '" + entraDBName + "' AND nom_tabla = '" + nomTabla + "' ORDER BY num_ordinal";
            adaGridD = new SqlDataAdapter(wSelect, mainConn);
            dataTableGridD = new DataTable();
            adaGridD.Fill(dataTableGridD);
            dataGridView1.Visible = true;
            dataGridView1.DataSource = dataTableGridD;

        Exit: { }
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

        private void button7_Click(object sender, EventArgs e)
        {
            System.DateTime now = DateTime.Now;

            if (now > Convert.ToDateTime("2024-03-01"))
            {
                MessageBox.Show("Desarrollado por richardsalinas662@gmail.com" + Environment.NewLine + "Para mas herramientas gratis: @ricardosalinas.software en YouTube" + Environment.NewLine + "Generadores de código para c#, sql, etc.", "rS7 keyser");
            }
        }
    }
    
}
