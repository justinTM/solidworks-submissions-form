namespace WindowsFormsApplication1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.startSolidworks = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.nextSubmission = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.ScanDirectoriesButton = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.LoadSelectionButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Select root folder of downloaded Canvas submissions";
            this.folderBrowserDialog1.SelectedPath = "Z:\\Windows.Documents\\Downloads";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(296, 37);
            this.button2.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(72, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(187, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Select folder for student submissions...";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(27, 39);
            this.textBox4.MaximumSize = new System.Drawing.Size(400, 20);
            this.textBox4.MinimumSize = new System.Drawing.Size(180, 20);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(256, 20);
            this.textBox4.TabIndex = 1;
            this.textBox4.Text = "Z:\\Windows.Documents\\My Documents\\ENGR 248\\STUDENTS (TA)\\STUDENT Lab 4 Submission" +
    "s";
            // 
            // startSolidworks
            // 
            this.startSolidworks.Location = new System.Drawing.Point(26, 127);
            this.startSolidworks.Name = "startSolidworks";
            this.startSolidworks.Size = new System.Drawing.Size(96, 26);
            this.startSolidworks.TabIndex = 3;
            this.startSolidworks.Text = "Start Solidworks";
            this.startSolidworks.UseMnemonic = false;
            this.startSolidworks.UseVisualStyleBackColor = true;
            this.startSolidworks.Click += new System.EventHandler(this.button3_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "2)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(149, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Run Solidworks iteration script";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "1)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Control script iterations";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "3)";
            // 
            // button5
            // 
            this.button5.Cursor = System.Windows.Forms.Cursors.Default;
            this.button5.Enabled = false;
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.Location = new System.Drawing.Point(27, 202);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(77, 54);
            this.button5.TabIndex = 23;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Cursor = System.Windows.Forms.Cursors.Default;
            this.button6.Location = new System.Drawing.Point(293, 233);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 24;
            this.button6.Text = "Next sketch";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(293, 262);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 27;
            this.button7.Text = "Next part";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Enabled = false;
            this.button8.Location = new System.Drawing.Point(27, 262);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(77, 30);
            this.button8.TabIndex = 28;
            this.button8.Text = "Previous part";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // nextSubmission
            // 
            this.nextSubmission.Location = new System.Drawing.Point(293, 291);
            this.nextSubmission.Name = "nextSubmission";
            this.nextSubmission.Size = new System.Drawing.Size(75, 23);
            this.nextSubmission.TabIndex = 29;
            this.nextSubmission.Text = "Next student";
            this.nextSubmission.UseVisualStyleBackColor = true;
            this.nextSubmission.Click += new System.EventHandler(this.nextSubmission_Click_1);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(27, 298);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(77, 21);
            this.button10.TabIndex = 30;
            this.button10.Text = "button10";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // ScanDirectoriesButton
            // 
            this.ScanDirectoriesButton.Location = new System.Drawing.Point(27, 66);
            this.ScanDirectoriesButton.Name = "ScanDirectoriesButton";
            this.ScanDirectoriesButton.Size = new System.Drawing.Size(75, 23);
            this.ScanDirectoriesButton.TabIndex = 33;
            this.ScanDirectoriesButton.Text = "Scan folder";
            this.ScanDirectoriesButton.UseVisualStyleBackColor = true;
            this.ScanDirectoriesButton.Click += new System.EventHandler(this.ScanDirectoriesButton_Click);
            // 
            // treeView1
            // 
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.HotTracking = true;
            this.treeView1.Location = new System.Drawing.Point(12, 409);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(368, 307);
            this.treeView1.TabIndex = 34;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(StudentInfo);
            // 
            // LoadSelectionButton
            // 
            this.LoadSelectionButton.Location = new System.Drawing.Point(13, 380);
            this.LoadSelectionButton.Name = "LoadSelectionButton";
            this.LoadSelectionButton.Size = new System.Drawing.Size(89, 23);
            this.LoadSelectionButton.TabIndex = 7;
            this.LoadSelectionButton.Text = "Load selection";
            this.LoadSelectionButton.UseVisualStyleBackColor = true;
            this.LoadSelectionButton.Click += new System.EventHandler(this.LoadSelectionButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 730);
            this.Controls.Add(this.LoadSelectionButton);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.ScanDirectoriesButton);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.nextSubmission);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.startSolidworks);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button startSolidworks;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button nextSubmission;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button ScanDirectoriesButton;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button LoadSelectionButton;
    }
}

