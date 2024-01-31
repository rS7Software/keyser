
namespace LoadDataDictionary
{
    partial class FormLoadDataDictionary
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSourceDataBase = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBoxConnString = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button0
            // 
            this.button0.Location = new System.Drawing.Point(654, 9);
            this.button0.Margin = new System.Windows.Forms.Padding(2);
            this.button0.Name = "button0";
            this.button0.Size = new System.Drawing.Size(38, 41);
            this.button0.TabIndex = 100;
            this.button0.UseVisualStyleBackColor = true;
            this.button0.Click += new System.EventHandler(this.button0_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Connection String:";
            // 
            // textBoxSourceDataBase
            // 
            this.textBoxSourceDataBase.Location = new System.Drawing.Point(179, 32);
            this.textBoxSourceDataBase.Name = "textBoxSourceDataBase";
            this.textBoxSourceDataBase.Size = new System.Drawing.Size(250, 20);
            this.textBoxSourceDataBase.TabIndex = 0;
            this.textBoxSourceDataBase.TextChanged += new System.EventHandler(this.textBoxSourceDataBase_TextChanged);
            this.textBoxSourceDataBase.Enter += new System.EventHandler(this.textBoxSourceDataBase_Enter);
            this.textBoxSourceDataBase.Leave += new System.EventHandler(this.textBoxSourceDataBase_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Source Data Base:";
            // 
            // richTextBoxConnString
            // 
            this.richTextBoxConnString.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxConnString.Location = new System.Drawing.Point(16, 92);
            this.richTextBoxConnString.Name = "richTextBoxConnString";
            this.richTextBoxConnString.Size = new System.Drawing.Size(678, 55);
            this.richTextBoxConnString.TabIndex = 1;
            this.richTextBoxConnString.Text = "";
            this.richTextBoxConnString.Leave += new System.EventHandler(this.richTextBoxConnString_Leave);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(369, 154);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(324, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Update Data Dictionary";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxLog.Location = new System.Drawing.Point(15, 183);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(678, 215);
            this.richTextBoxLog.TabIndex = 3;
            this.richTextBoxLog.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(16, 154);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(324, 23);
            this.button2.TabIndex = 101;
            this.button2.Text = "Load Tables";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormLoadDataDictionary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(706, 410);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBoxConnString);
            this.Controls.Add(this.textBoxSourceDataBase);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button0);
            this.Location = new System.Drawing.Point(10, 60);
            this.Name = "FormLoadDataDictionary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Keyser 1.0.0 - Load Data Dictionary";
            this.Load += new System.EventHandler(this.FormLoadDataDictionary_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button0;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSourceDataBase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBoxConnString;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.Button button2;
    }
}

