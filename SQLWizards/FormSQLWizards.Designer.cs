namespace SQLWizards
{
    partial class FormSQLWizards
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.button0 = new System.Windows.Forms.Button();
            this.buttonTablas = new System.Windows.Forms.Button();
            this.dataGridViewTablas = new System.Windows.Forms.DataGridView();
            this.richTextBoxSQL = new System.Windows.Forms.RichTextBox();
            this.textBoxSourceDataBase = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBoxConnString = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTablas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button0
            // 
            this.button0.Location = new System.Drawing.Point(684, 7);
            this.button0.Margin = new System.Windows.Forms.Padding(2);
            this.button0.Name = "button0";
            this.button0.Size = new System.Drawing.Size(38, 41);
            this.button0.TabIndex = 101;
            this.button0.UseVisualStyleBackColor = true;
            this.button0.Click += new System.EventHandler(this.button0_Click);
            // 
            // buttonTablas
            // 
            this.buttonTablas.Location = new System.Drawing.Point(12, 32);
            this.buttonTablas.Name = "buttonTablas";
            this.buttonTablas.Size = new System.Drawing.Size(265, 23);
            this.buttonTablas.TabIndex = 1;
            this.buttonTablas.Text = "Tables / Dictionary";
            this.buttonTablas.UseVisualStyleBackColor = true;
            this.buttonTablas.Click += new System.EventHandler(this.buttonTablas_Click);
            // 
            // dataGridViewTablas
            // 
            this.dataGridViewTablas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTablas.Location = new System.Drawing.Point(12, 61);
            this.dataGridViewTablas.Name = "dataGridViewTablas";
            this.dataGridViewTablas.Size = new System.Drawing.Size(265, 748);
            this.dataGridViewTablas.TabIndex = 105;
            this.dataGridViewTablas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTablas_CellClick);
            // 
            // richTextBoxSQL
            // 
            this.richTextBoxSQL.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxSQL.Location = new System.Drawing.Point(283, 274);
            this.richTextBoxSQL.Name = "richTextBoxSQL";
            this.richTextBoxSQL.Size = new System.Drawing.Size(889, 535);
            this.richTextBoxSQL.TabIndex = 107;
            this.richTextBoxSQL.Text = "";
            // 
            // textBoxSourceDataBase
            // 
            this.textBoxSourceDataBase.Location = new System.Drawing.Point(180, 9);
            this.textBoxSourceDataBase.Name = "textBoxSourceDataBase";
            this.textBoxSourceDataBase.Size = new System.Drawing.Size(97, 20);
            this.textBoxSourceDataBase.TabIndex = 0;
            this.textBoxSourceDataBase.Enter += new System.EventHandler(this.textBoxSourceDataBase_Enter);
            this.textBoxSourceDataBase.Leave += new System.EventHandler(this.textBoxSourceDataBase_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 20);
            this.label1.TabIndex = 109;
            this.label1.Text = "Source Data Base:";
            // 
            // richTextBoxConnString
            // 
            this.richTextBoxConnString.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxConnString.Location = new System.Drawing.Point(657, 344);
            this.richTextBoxConnString.Name = "richTextBoxConnString";
            this.richTextBoxConnString.Size = new System.Drawing.Size(35, 55);
            this.richTextBoxConnString.TabIndex = 110;
            this.richTextBoxConnString.Text = "";
            this.richTextBoxConnString.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(364, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Insert";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(283, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Create";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(445, 7);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Update";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(524, 7);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "Delete";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(604, 7);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 6;
            this.button5.Text = "Select";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Location = new System.Drawing.Point(657, 61);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(515, 207);
            this.richTextBoxLog.TabIndex = 111;
            this.richTextBoxLog.Text = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(283, 61);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(368, 207);
            this.dataGridView1.TabIndex = 112;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(282, 32);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 113;
            this.button6.Text = "Invest";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Consolas", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(727, 7);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(38, 41);
            this.button7.TabIndex = 114;
            this.button7.Text = "About";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // FormSQLWizards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1184, 821);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBoxSourceDataBase);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBoxSQL);
            this.Controls.Add(this.dataGridViewTablas);
            this.Controls.Add(this.buttonTablas);
            this.Controls.Add(this.button0);
            this.Controls.Add(this.richTextBoxConnString);
            this.Location = new System.Drawing.Point(1, 1);
            this.Name = "FormSQLWizards";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Keyser 1.0.0 - SQL Wizards";
            this.Load += new System.EventHandler(this.FormSQLWizards_Load);
            this.Leave += new System.EventHandler(this.FormSQLWizards_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTablas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button0;
        private System.Windows.Forms.Button buttonTablas;
        private System.Windows.Forms.DataGridView dataGridViewTablas;
        private System.Windows.Forms.RichTextBox richTextBoxSQL;
        private System.Windows.Forms.TextBox textBoxSourceDataBase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBoxConnString;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
    }
}

