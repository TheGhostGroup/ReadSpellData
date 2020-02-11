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
            this.loadB = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.exportB = new System.Windows.Forms.Button();
            this.exportSQLB = new System.Windows.Forms.Button();
            this.lstSpellCasts = new System.Windows.Forms.ListView();
            this.clmCasterId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmCasterType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmSpellId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmCastFlags = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmCastFlagsEx = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmTargetId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmTargetType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkShowUnique = new System.Windows.Forms.CheckBox();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
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
            this.openFileDialog.DefaultExt = "txt";
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "Parsed Sniff File (*.txt)|*.txt";
            this.openFileDialog.Multiselect = true;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 591);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(697, 22);
            this.statusStrip.TabIndex = 11;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(77, 17);
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
            // lstSpellCasts
            // 
            this.lstSpellCasts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmCasterId,
            this.clmCasterType,
            this.clmSpellId,
            this.clmCastFlags,
            this.clmCastFlagsEx,
            this.clmTargetId,
            this.clmTargetType,
            this.clmTime});
            this.lstSpellCasts.FullRowSelect = true;
            this.lstSpellCasts.GridLines = true;
            this.lstSpellCasts.Location = new System.Drawing.Point(12, 42);
            this.lstSpellCasts.Name = "lstSpellCasts";
            this.lstSpellCasts.Size = new System.Drawing.Size(673, 536);
            this.lstSpellCasts.TabIndex = 15;
            this.lstSpellCasts.UseCompatibleStateImageBehavior = false;
            this.lstSpellCasts.View = System.Windows.Forms.View.Details;
            this.lstSpellCasts.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstSpellCasts_ColumnClick);
            // 
            // clmCasterId
            // 
            this.clmCasterId.Text = "CasterId";
            this.clmCasterId.Width = 80;
            // 
            // clmCasterType
            // 
            this.clmCasterType.Text = "CasterType";
            this.clmCasterType.Width = 80;
            // 
            // clmSpellId
            // 
            this.clmSpellId.Text = "SpellId";
            this.clmSpellId.Width = 80;
            // 
            // clmCastFlags
            // 
            this.clmCastFlags.Text = "CastFlags";
            this.clmCastFlags.Width = 80;
            // 
            // clmCastFlagsEx
            // 
            this.clmCastFlagsEx.Text = "CastFlagsEx";
            this.clmCastFlagsEx.Width = 80;
            // 
            // clmTargetId
            // 
            this.clmTargetId.Text = "TargetId";
            this.clmTargetId.Width = 80;
            // 
            // clmTargetType
            // 
            this.clmTargetType.Text = "TargetType";
            this.clmTargetType.Width = 80;
            // 
            // clmTime
            // 
            this.clmTime.Text = "Time";
            this.clmTime.Width = 90;
            // 
            // chkShowUnique
            // 
            this.chkShowUnique.AutoSize = true;
            this.chkShowUnique.Location = new System.Drawing.Point(358, 17);
            this.chkShowUnique.Name = "chkShowUnique";
            this.chkShowUnique.Size = new System.Drawing.Size(138, 17);
            this.chkShowUnique.TabIndex = 16;
            this.chkShowUnique.Text = "Show unique casts only";
            this.chkShowUnique.UseVisualStyleBackColor = true;
            this.chkShowUnique.CheckedChanged += new System.EventHandler(this.chkShowUnique_CheckedChanged);
            // 
            // Frm_ReadInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 613);
            this.Controls.Add(this.chkShowUnique);
            this.Controls.Add(this.lstSpellCasts);
            this.Controls.Add(this.exportSQLB);
            this.Controls.Add(this.exportB);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.loadB);
            this.Name = "Frm_ReadInfo";
            this.Text = "Read Spell Info";
            this.ResizeEnd += new System.EventHandler(this.Frm_ReadInfo_ResizeEnd);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button loadB;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Button exportB;
        private System.Windows.Forms.Button exportSQLB;
        private System.Windows.Forms.ListView lstSpellCasts;
        private System.Windows.Forms.ColumnHeader clmCasterId;
        private System.Windows.Forms.ColumnHeader clmCasterType;
        private System.Windows.Forms.ColumnHeader clmSpellId;
        private System.Windows.Forms.ColumnHeader clmCastFlags;
        private System.Windows.Forms.ColumnHeader clmCastFlagsEx;
        private System.Windows.Forms.ColumnHeader clmTargetId;
        private System.Windows.Forms.ColumnHeader clmTargetType;
        private System.Windows.Forms.ColumnHeader clmTime;
        private System.Windows.Forms.CheckBox chkShowUnique;
    }
}