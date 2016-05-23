namespace Esperanto
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openROMsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.createComparisonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tvMessages = new Esperanto.TreeViewEx();
            this.ofdRomFirst = new System.Windows.Forms.OpenFileDialog();
            this.ofdRomSecond = new System.Windows.Forms.OpenFileDialog();
            this.previewControlSecondary = new Esperanto.PreviewControl();
            this.previewControlPrimary = new Esperanto.PreviewControl();
            this.fbdCreateCompare = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1134, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openROMsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.createComparisonToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openROMsToolStripMenuItem
            // 
            this.openROMsToolStripMenuItem.Name = "openROMsToolStripMenuItem";
            this.openROMsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openROMsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.openROMsToolStripMenuItem.Text = "&Open ROMs...";
            this.openROMsToolStripMenuItem.Click += new System.EventHandler(this.openROMsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(189, 6);
            // 
            // createComparisonToolStripMenuItem
            // 
            this.createComparisonToolStripMenuItem.Enabled = false;
            this.createComparisonToolStripMenuItem.Name = "createComparisonToolStripMenuItem";
            this.createComparisonToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.createComparisonToolStripMenuItem.Text = "&Create Comparison...";
            this.createComparisonToolStripMenuItem.Click += new System.EventHandler(this.createComparisonToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(189, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 540);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1134, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip";
            // 
            // tvMessages
            // 
            this.tvMessages.Location = new System.Drawing.Point(12, 27);
            this.tvMessages.Name = "tvMessages";
            this.tvMessages.Size = new System.Drawing.Size(150, 510);
            this.tvMessages.TabIndex = 1;
            // 
            // ofdRomFirst
            // 
            this.ofdRomFirst.Filter = "Game Boy Advance ROMs (*.gba;*.agb)|*.gba;*.agb|All Files (*.*)|*.*";
            this.ofdRomFirst.Title = "Open First ROM";
            // 
            // ofdRomSecond
            // 
            this.ofdRomSecond.Filter = "Game Boy Advance ROMs (*.gba;*.agb)|*.gba;*.agb|All Files (*.*)|*.*";
            this.ofdRomSecond.Title = "Open Second ROM";
            // 
            // previewControlSecondary
            // 
            this.previewControlSecondary.Enabled = false;
            this.previewControlSecondary.Location = new System.Drawing.Point(652, 27);
            this.previewControlSecondary.Name = "previewControlSecondary";
            this.previewControlSecondary.Size = new System.Drawing.Size(470, 510);
            this.previewControlSecondary.TabIndex = 3;
            // 
            // previewControlPrimary
            // 
            this.previewControlPrimary.Enabled = false;
            this.previewControlPrimary.Location = new System.Drawing.Point(168, 27);
            this.previewControlPrimary.Name = "previewControlPrimary";
            this.previewControlPrimary.Size = new System.Drawing.Size(470, 510);
            this.previewControlPrimary.TabIndex = 2;
            // 
            // fbdCreateCompare
            // 
            this.fbdCreateCompare.Description = "Select directory to save comparison to.";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 562);
            this.Controls.Add(this.tvMessages);
            this.Controls.Add(this.previewControlSecondary);
            this.Controls.Add(this.previewControlPrimary);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openROMsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createComparisonToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private PreviewControl previewControlPrimary;
        private PreviewControl previewControlSecondary;
        private TreeViewEx tvMessages;
        private System.Windows.Forms.OpenFileDialog ofdRomFirst;
        private System.Windows.Forms.OpenFileDialog ofdRomSecond;
        private System.Windows.Forms.FolderBrowserDialog fbdCreateCompare;
    }
}

