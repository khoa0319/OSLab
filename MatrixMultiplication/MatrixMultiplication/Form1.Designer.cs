namespace MatrixMultiplication
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtThreads = new System.Windows.Forms.TextBox();
            this.btnCham = new System.Windows.Forms.Button();
            this.btnNhanh = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lbKetqua = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "số thread";
            // 
            // txtThreads
            // 
            this.txtThreads.Location = new System.Drawing.Point(199, 18);
            this.txtThreads.Name = "txtThreads";
            this.txtThreads.Size = new System.Drawing.Size(117, 26);
            this.txtThreads.TabIndex = 1;
            // 
            // btnCham
            // 
            this.btnCham.Location = new System.Drawing.Point(358, 22);
            this.btnCham.Name = "btnCham";
            this.btnCham.Size = new System.Drawing.Size(108, 35);
            this.btnCham.TabIndex = 2;
            this.btnCham.Text = "ưu tiên thấp";
            this.btnCham.UseVisualStyleBackColor = true;
            this.btnCham.Click += new System.EventHandler(this.btnCham_Click);
            // 
            // btnNhanh
            // 
            this.btnNhanh.Location = new System.Drawing.Point(528, 24);
            this.btnNhanh.Name = "btnNhanh";
            this.btnNhanh.Size = new System.Drawing.Size(75, 33);
            this.btnNhanh.TabIndex = 3;
            this.btnNhanh.Text = "UT cao";
            this.btnNhanh.UseVisualStyleBackColor = true;
            this.btnNhanh.Click += new System.EventHandler(this.btnNhanh_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(698, 22);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 34);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Tich";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lbKetqua
            // 
            this.lbKetqua.FormattingEnabled = true;
            this.lbKetqua.ItemHeight = 20;
            this.lbKetqua.Location = new System.Drawing.Point(62, 124);
            this.lbKetqua.Name = "lbKetqua";
            this.lbKetqua.Size = new System.Drawing.Size(684, 264);
            this.lbKetqua.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbKetqua);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnNhanh);
            this.Controls.Add(this.btnCham);
            this.Controls.Add(this.txtThreads);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtThreads;
        private System.Windows.Forms.Button btnCham;
        private System.Windows.Forms.Button btnNhanh;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListBox lbKetqua;
    }
}

