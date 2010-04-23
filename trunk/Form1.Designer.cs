namespace OSM2TAB
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
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.buttonTestThread = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPrefix = new System.Windows.Forms.TextBox();
            this.labelCurrentWay = new System.Windows.Forms.TextBox();
            this.labelMaxWays = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // inputTextBox
            // 
            this.inputTextBox.Location = new System.Drawing.Point(12, 29);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(474, 20);
            this.inputTextBox.TabIndex = 1;
            this.inputTextBox.Text = "http://www.openstreetmap.org/api/0.6/map?bbox=-1.080351,50.895238,-1.054516,50.90" +
                "7688";
            // 
            // buttonTestThread
            // 
            this.buttonTestThread.Location = new System.Drawing.Point(207, 228);
            this.buttonTestThread.Name = "buttonTestThread";
            this.buttonTestThread.Size = new System.Drawing.Size(75, 23);
            this.buttonTestThread.TabIndex = 6;
            this.buttonTestThread.Text = "Go!";
            this.buttonTestThread.UseVisualStyleBackColor = true;
            this.buttonTestThread.Click += new System.EventHandler(this.buttonTestThread_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Input";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Output";
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(12, 77);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(474, 20);
            this.outputTextBox.TabIndex = 9;
            this.outputTextBox.Text = "c:\\out";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Processing Status:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Total Nodes:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "TAB Prefix";
            // 
            // tabPrefix
            // 
            this.tabPrefix.Location = new System.Drawing.Point(75, 112);
            this.tabPrefix.Name = "tabPrefix";
            this.tabPrefix.Size = new System.Drawing.Size(104, 20);
            this.tabPrefix.TabIndex = 13;
            // 
            // labelCurrentWay
            // 
            this.labelCurrentWay.Enabled = false;
            this.labelCurrentWay.Location = new System.Drawing.Point(113, 150);
            this.labelCurrentWay.Name = "labelCurrentWay";
            this.labelCurrentWay.Size = new System.Drawing.Size(100, 20);
            this.labelCurrentWay.TabIndex = 14;
            // 
            // labelMaxWays
            // 
            this.labelMaxWays.Enabled = false;
            this.labelMaxWays.Location = new System.Drawing.Point(113, 186);
            this.labelMaxWays.Name = "labelMaxWays";
            this.labelMaxWays.Size = new System.Drawing.Size(100, 20);
            this.labelMaxWays.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 277);
            this.Controls.Add(this.labelMaxWays);
            this.Controls.Add(this.labelCurrentWay);
            this.Controls.Add(this.tabPrefix);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonTestThread);
            this.Controls.Add(this.inputTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.Button buttonTestThread;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tabPrefix;
        private System.Windows.Forms.TextBox labelCurrentWay;
        private System.Windows.Forms.TextBox labelMaxWays;
    }
}

