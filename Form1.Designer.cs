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
            this.uriTextBox = new System.Windows.Forms.TextBox();
            this.labelMaxWays = new System.Windows.Forms.Label();
            this.labelCurrentWay = new System.Windows.Forms.Label();
            this.buttonTestThread = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uriTextBox
            // 
            this.uriTextBox.Location = new System.Drawing.Point(12, 101);
            this.uriTextBox.Name = "uriTextBox";
            this.uriTextBox.Size = new System.Drawing.Size(474, 20);
            this.uriTextBox.TabIndex = 1;
            this.uriTextBox.Text = "http://www.openstreetmap.org/api/0.6/map?bbox=-1.080351,50.895238,-1.054516,50.90" +
                "7688";
            // 
            // labelMaxWays
            // 
            this.labelMaxWays.AutoSize = true;
            this.labelMaxWays.Location = new System.Drawing.Point(209, 206);
            this.labelMaxWays.Name = "labelMaxWays";
            this.labelMaxWays.Size = new System.Drawing.Size(35, 13);
            this.labelMaxWays.TabIndex = 4;
            this.labelMaxWays.Text = "label1";
            // 
            // labelCurrentWay
            // 
            this.labelCurrentWay.AutoSize = true;
            this.labelCurrentWay.Location = new System.Drawing.Point(130, 206);
            this.labelCurrentWay.Name = "labelCurrentWay";
            this.labelCurrentWay.Size = new System.Drawing.Size(35, 13);
            this.labelCurrentWay.TabIndex = 5;
            this.labelCurrentWay.Text = "label2";
            // 
            // buttonTestThread
            // 
            this.buttonTestThread.Location = new System.Drawing.Point(195, 269);
            this.buttonTestThread.Name = "buttonTestThread";
            this.buttonTestThread.Size = new System.Drawing.Size(75, 23);
            this.buttonTestThread.TabIndex = 6;
            this.buttonTestThread.Text = "Test Thread";
            this.buttonTestThread.UseVisualStyleBackColor = true;
            this.buttonTestThread.Click += new System.EventHandler(this.buttonTestThread_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 392);
            this.Controls.Add(this.buttonTestThread);
            this.Controls.Add(this.labelCurrentWay);
            this.Controls.Add(this.labelMaxWays);
            this.Controls.Add(this.uriTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox uriTextBox;
        private System.Windows.Forms.Label labelMaxWays;
        private System.Windows.Forms.Label labelCurrentWay;
        private System.Windows.Forms.Button buttonTestThread;
    }
}

