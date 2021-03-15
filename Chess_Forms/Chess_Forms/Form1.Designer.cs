namespace Chess_Forms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonOmstart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(42, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(520, 520);
            this.panel1.TabIndex = 0;
            // 
            // buttonOmstart
            // 
            this.buttonOmstart.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonOmstart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOmstart.Location = new System.Drawing.Point(578, 28);
            this.buttonOmstart.Name = "buttonOmstart";
            this.buttonOmstart.Size = new System.Drawing.Size(75, 23);
            this.buttonOmstart.TabIndex = 1;
            this.buttonOmstart.Text = "Starta Om";
            this.buttonOmstart.UseVisualStyleBackColor = true;
            this.buttonOmstart.Click += new System.EventHandler(this.buttonOmstart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(592, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 550);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOmstart);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonOmstart;
        private System.Windows.Forms.Label label1;
    }
}

