namespace WindowsFormsApplication6
{
    partial class Form2
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
            this.p_new1 = new System.Windows.Forms.Label();
            this.p_new2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.reset = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // p_new1
            // 
            this.p_new1.AutoSize = true;
            this.p_new1.Location = new System.Drawing.Point(26, 36);
            this.p_new1.Name = "p_new1";
            this.p_new1.Size = new System.Drawing.Size(103, 13);
            this.p_new1.TabIndex = 1;
            this.p_new1.Text = "Enter new password";
            this.p_new1.Click += new System.EventHandler(this.label2_Click);
            // 
            // p_new2
            // 
            this.p_new2.AutoSize = true;
            this.p_new2.Location = new System.Drawing.Point(26, 82);
            this.p_new2.Name = "p_new2";
            this.p_new2.Size = new System.Drawing.Size(119, 13);
            this.p_new2.TabIndex = 2;
            this.p_new2.Text = "Re-enter new password";
            this.p_new2.Click += new System.EventHandler(this.p_new2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(151, 36);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '•';
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 4;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(151, 82);
            this.textBox3.Name = "textBox3";
            this.textBox3.PasswordChar = '•';
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 5;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // reset
            // 
            this.reset.Enabled = false;
            this.reset.Location = new System.Drawing.Point(29, 128);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(75, 23);
            this.reset.TabIndex = 6;
            this.reset.Text = "Reset";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(151, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(355, 188);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.p_new2);
            this.Controls.Add(this.p_new1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label p_new1;
        private System.Windows.Forms.Label p_new2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button reset;
        public System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button1;
    }
}