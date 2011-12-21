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
         this.buttonGo = new System.Windows.Forms.Button();
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.outputTextBox = new System.Windows.Forms.TextBox();
         this.label3 = new System.Windows.Forms.Label();
         this.label4 = new System.Windows.Forms.Label();
         this.label5 = new System.Windows.Forms.Label();
         this.tabPrefix = new System.Windows.Forms.TextBox();
         this.labelCurrentWay = new System.Windows.Forms.TextBox();
         this.labelMaxWays = new System.Windows.Forms.TextBox();
         this.outputFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
         this.buttonSelectOutFolder = new System.Windows.Forms.Button();
         this.buttonSelectInFile = new System.Windows.Forms.Button();
         this.openOSMFileDialog = new System.Windows.Forms.OpenFileDialog();
         this.themeTextBox = new System.Windows.Forms.TextBox();
         this.label6 = new System.Windows.Forms.Label();
         this.debugTextBox = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // inputTextBox
         // 
         this.inputTextBox.Location = new System.Drawing.Point(12, 29);
         this.inputTextBox.Name = "inputTextBox";
         this.inputTextBox.Size = new System.Drawing.Size(465, 20);
         this.inputTextBox.TabIndex = 1;
         this.inputTextBox.Text = "c:\\osm\\luxembourg.osm";
         // 
         // buttonGo
         // 
         this.buttonGo.Location = new System.Drawing.Point(207, 228);
         this.buttonGo.Name = "buttonGo";
         this.buttonGo.Size = new System.Drawing.Size(75, 23);
         this.buttonGo.TabIndex = 6;
         this.buttonGo.Text = "Go!";
         this.buttonGo.UseVisualStyleBackColor = true;
         this.buttonGo.Click += new System.EventHandler(this.buttonTestThread_Click);
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
         this.outputTextBox.Size = new System.Drawing.Size(270, 20);
         this.outputTextBox.TabIndex = 9;
         this.outputTextBox.Text = "c:\\osm";
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
         this.tabPrefix.Text = "mytab";
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
         // outputFolderBrowserDialog
         // 
         this.outputFolderBrowserDialog.HelpRequest += new System.EventHandler(this.outputFolderBrowserDialog1_HelpRequest);
         // 
         // buttonSelectOutFolder
         // 
         this.buttonSelectOutFolder.Location = new System.Drawing.Point(288, 75);
         this.buttonSelectOutFolder.Name = "buttonSelectOutFolder";
         this.buttonSelectOutFolder.Size = new System.Drawing.Size(26, 23);
         this.buttonSelectOutFolder.TabIndex = 16;
         this.buttonSelectOutFolder.Text = "...";
         this.buttonSelectOutFolder.UseVisualStyleBackColor = true;
         this.buttonSelectOutFolder.Click += new System.EventHandler(this.buttonSelectOutFolder_Click);
         // 
         // buttonSelectInFile
         // 
         this.buttonSelectInFile.Location = new System.Drawing.Point(483, 27);
         this.buttonSelectInFile.Name = "buttonSelectInFile";
         this.buttonSelectInFile.Size = new System.Drawing.Size(26, 23);
         this.buttonSelectInFile.TabIndex = 17;
         this.buttonSelectInFile.Text = "...";
         this.buttonSelectInFile.UseVisualStyleBackColor = true;
         this.buttonSelectInFile.Click += new System.EventHandler(this.buttonSelectInFile_Click);
         // 
         // openOSMFileDialog
         // 
         this.openOSMFileDialog.FileName = "openInputFileDialog";
         // 
         // themeTextBox
         // 
         this.themeTextBox.Location = new System.Drawing.Point(12, 277);
         this.themeTextBox.Name = "themeTextBox";
         this.themeTextBox.Size = new System.Drawing.Size(270, 20);
         this.themeTextBox.TabIndex = 18;
         this.themeTextBox.Text = "C:\\osm2tab\\theme.xml";
         // 
         // label6
         // 
         this.label6.AutoSize = true;
         this.label6.Location = new System.Drawing.Point(12, 261);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(40, 13);
         this.label6.TabIndex = 19;
         this.label6.Text = "Theme";
         // 
         // debugTextBox
         // 
         this.debugTextBox.Location = new System.Drawing.Point(333, 77);
         this.debugTextBox.Multiline = true;
         this.debugTextBox.Name = "debugTextBox";
         this.debugTextBox.Size = new System.Drawing.Size(263, 220);
         this.debugTextBox.TabIndex = 20;
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(625, 321);
         this.Controls.Add(this.debugTextBox);
         this.Controls.Add(this.label6);
         this.Controls.Add(this.themeTextBox);
         this.Controls.Add(this.buttonSelectInFile);
         this.Controls.Add(this.buttonSelectOutFolder);
         this.Controls.Add(this.labelMaxWays);
         this.Controls.Add(this.labelCurrentWay);
         this.Controls.Add(this.tabPrefix);
         this.Controls.Add(this.label5);
         this.Controls.Add(this.label4);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.outputTextBox);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.buttonGo);
         this.Controls.Add(this.inputTextBox);
         this.Name = "Form1";
         this.Text = "OSM2TAB";
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tabPrefix;
        private System.Windows.Forms.TextBox labelCurrentWay;
        private System.Windows.Forms.TextBox labelMaxWays;
        private System.Windows.Forms.FolderBrowserDialog outputFolderBrowserDialog;
        private System.Windows.Forms.Button buttonSelectOutFolder;
        private System.Windows.Forms.Button buttonSelectInFile;
        private System.Windows.Forms.OpenFileDialog openOSMFileDialog;
        private System.Windows.Forms.TextBox themeTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox debugTextBox;
    }
}

