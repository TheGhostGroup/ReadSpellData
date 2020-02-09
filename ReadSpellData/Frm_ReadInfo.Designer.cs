namespace ReadSpellData
{
    partial class Frm_ReadInfo
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
            this.entryListBox = new System.Windows.Forms.ListBox();
            this.spellRichTextBox = new System.Windows.Forms.RichTextBox();
            this.loadB = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.spellListBox = new System.Windows.Forms.ListBox();
            this.entryLabel = new System.Windows.Forms.Label();
            this.spellLabel = new System.Windows.Forms.Label();
            this.spellInfoLable = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.exportB = new System.Windows.Forms.Button();
            this.exportSQLB = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // entryListBox
            // 
            this.entryListBox.FormattingEnabled = true;
            this.entryListBox.Location = new System.Drawing.Point(12, 80);
            this.entryListBox.Name = "entryListBox";
            this.entryListBox.Size = new System.Drawing.Size(162, 498);
            this.entryListBox.TabIndex = 1;
            this.entryListBox.SelectedIndexChanged += new System.EventHandler(this.entryListBox_SelectedIndexChanged);
            // 
            // spellRichTextBox
            // 
            this.spellRichTextBox.Location = new System.Drawing.Point(346, 80);
            this.spellRichTextBox.Name = "spellRichTextBox";
            this.spellRichTextBox.Size = new System.Drawing.Size(343, 498);
            this.spellRichTextBox.TabIndex = 2;
            this.spellRichTextBox.Text = "";
            // 
            // loadB
            // 
            this.loadB.Location = new System.Drawing.Point(13, 13);
            this.loadB.Name = "loadB";
            this.loadB.Size = new System.Drawing.Size(92, 23);
            this.loadB.TabIndex = 3;
            this.loadB.Text = "Load";
            this.loadB.UseVisualStyleBackColor = true;
            this.loadB.Click += new System.EventHandler(this.loadB_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // spellListBox
            // 
            this.spellListBox.FormattingEnabled = true;
            this.spellListBox.Location = new System.Drawing.Point(180, 80);
            this.spellListBox.Name = "spellListBox";
            this.spellListBox.Size = new System.Drawing.Size(160, 498);
            this.spellListBox.TabIndex = 6;
            this.spellListBox.SelectedIndexChanged += new System.EventHandler(this.spellListBox_SelectedIndexChanged);
            // 
            // entryLabel
            // 
            this.entryLabel.AutoSize = true;
            this.entryLabel.Location = new System.Drawing.Point(70, 57);
            this.entryLabel.Name = "entryLabel";
            this.entryLabel.Size = new System.Drawing.Size(31, 13);
            this.entryLabel.TabIndex = 8;
            this.entryLabel.Text = "Entry";
            // 
            // spellLabel
            // 
            this.spellLabel.AutoSize = true;
            this.spellLabel.Location = new System.Drawing.Point(239, 61);
            this.spellLabel.Name = "spellLabel";
            this.spellLabel.Size = new System.Drawing.Size(44, 13);
            this.spellLabel.TabIndex = 9;
            this.spellLabel.Text = "Spell ID";
            // 
            // spellInfoLable
            // 
            this.spellInfoLable.AutoSize = true;
            this.spellInfoLable.Location = new System.Drawing.Point(495, 61);
            this.spellInfoLable.Name = "spellInfoLable";
            this.spellInfoLable.Size = new System.Drawing.Size(51, 13);
            this.spellInfoLable.TabIndex = 10;
            this.spellInfoLable.Text = "Spell Info";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 592);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(702, 22);
            this.statusStrip.TabIndex = 11;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(86, 17);
            this.toolStripStatusLabel.Text = "No File Loaded";
            // 
            // exportB
            // 
            this.exportB.Location = new System.Drawing.Point(111, 13);
            this.exportB.Name = "exportB";
            this.exportB.Size = new System.Drawing.Size(117, 23);
            this.exportB.TabIndex = 12;
            this.exportB.Text = "Export To Excel";
            this.exportB.UseVisualStyleBackColor = true;
            this.exportB.Click += new System.EventHandler(this.exportB_Click);
            // 
            // exportSQLB
            // 
            this.exportSQLB.Location = new System.Drawing.Point(235, 13);
            this.exportSQLB.Name = "exportSQLB";
            this.exportSQLB.Size = new System.Drawing.Size(117, 23);
            this.exportSQLB.TabIndex = 13;
            this.exportSQLB.Text = "Export To Database";
            this.exportSQLB.UseVisualStyleBackColor = true;
            this.exportSQLB.Click += new System.EventHandler(this.exportSQLB_Click);
            // 
            // Frm_ReadInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 614);
            this.Controls.Add(this.exportSQLB);
            this.Controls.Add(this.exportB);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.spellInfoLable);
            this.Controls.Add(this.spellLabel);
            this.Controls.Add(this.entryLabel);
            this.Controls.Add(this.spellListBox);
            this.Controls.Add(this.loadB);
            this.Controls.Add(this.spellRichTextBox);
            this.Controls.Add(this.entryListBox);
            this.Name = "Frm_ReadInfo";
            this.Text = "Read Spell Info";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox entryListBox;
        private System.Windows.Forms.Button loadB;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ListBox spellListBox;
        private System.Windows.Forms.Label entryLabel;
        private System.Windows.Forms.Label spellLabel;
        private System.Windows.Forms.Label spellInfoLable;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Button exportB;
        public System.Windows.Forms.RichTextBox spellRichTextBox;
        private System.Windows.Forms.Button exportSQLB;
    }
}