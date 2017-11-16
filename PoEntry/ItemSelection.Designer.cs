namespace PoEntry
{
    partial class ItemSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemSelection));
            this.cueExtender1 = new Ehs.Controls.CueExtender();
            this.cmb_Mat = new Ehs.Controls.AutoCompleteTextBox();
            this.cmb_VendorCatalog = new Ehs.Controls.AutoCompleteTextBox();
            this.cmb_MfgCatalog = new Ehs.Controls.AutoCompleteTextBox();
            this.eb_Description = new Ehs.Controls.EhsTextBox();
            this.cmb_Mfg_Name = new Ehs.Controls.AutoCompleteTextBox();
            this.b_MUOP = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmb_Mat
            // 
            this.cmb_Mat.AllowTypedIn = false;
            this.cueExtender1.SetCueText(this.cmb_Mat, "Material Code");
            this.cmb_Mat.CurrentItem = null;
            this.cmb_Mat.Items = ((System.Collections.Generic.List<Ehs.Controls.ComboBoxString>)(resources.GetObject("cmb_Mat.Items")));
            this.cmb_Mat.Location = new System.Drawing.Point(55, 24);
            this.cmb_Mat.Name = "cmb_Mat";
            this.cmb_Mat.PopupBorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cmb_Mat.PopupOffset = new System.Drawing.Point(12, 0);
            this.cmb_Mat.PopupSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.cmb_Mat.PopupSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmb_Mat.PopupWidth = 300;
            this.cmb_Mat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmb_Mat.Size = new System.Drawing.Size(400, 20);
            this.cmb_Mat.TabIndex = 5;
            this.cmb_Mat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_Mat_KeyDown);
            this.cmb_Mat.Validated += new System.EventHandler(this.cmb_Mat_Validated);
            // 
            // cmb_VendorCatalog
            // 
            this.cmb_VendorCatalog.AllowTypedIn = false;
            this.cueExtender1.SetCueText(this.cmb_VendorCatalog, "Vendor Catalog");
            this.cmb_VendorCatalog.CurrentItem = null;
            this.cmb_VendorCatalog.Items = ((System.Collections.Generic.List<Ehs.Controls.ComboBoxString>)(resources.GetObject("cmb_VendorCatalog.Items")));
            this.cmb_VendorCatalog.Location = new System.Drawing.Point(55, 44);
            this.cmb_VendorCatalog.Name = "cmb_VendorCatalog";
            this.cmb_VendorCatalog.PopupBorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cmb_VendorCatalog.PopupOffset = new System.Drawing.Point(12, 0);
            this.cmb_VendorCatalog.PopupSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.cmb_VendorCatalog.PopupSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmb_VendorCatalog.PopupWidth = 300;
            this.cmb_VendorCatalog.Size = new System.Drawing.Size(400, 20);
            this.cmb_VendorCatalog.TabIndex = 9;
            this.cmb_VendorCatalog.Validating += new System.ComponentModel.CancelEventHandler(this.cmb_VendorCatalog_Validating);
            this.cmb_VendorCatalog.Validated += new System.EventHandler(this.cmb_VendorCatalog_Validated);
            // 
            // cmb_MfgCatalog
            // 
            this.cmb_MfgCatalog.AllowTypedIn = false;
            this.cueExtender1.SetCueText(this.cmb_MfgCatalog, "Mfg Catalog");
            this.cmb_MfgCatalog.CurrentItem = null;
            this.cmb_MfgCatalog.Items = ((System.Collections.Generic.List<Ehs.Controls.ComboBoxString>)(resources.GetObject("cmb_MfgCatalog.Items")));
            this.cmb_MfgCatalog.Location = new System.Drawing.Point(55, 64);
            this.cmb_MfgCatalog.Name = "cmb_MfgCatalog";
            this.cmb_MfgCatalog.PopupBorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cmb_MfgCatalog.PopupOffset = new System.Drawing.Point(12, 0);
            this.cmb_MfgCatalog.PopupSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.cmb_MfgCatalog.PopupSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmb_MfgCatalog.PopupWidth = 300;
            this.cmb_MfgCatalog.Size = new System.Drawing.Size(400, 20);
            this.cmb_MfgCatalog.TabIndex = 13;
            this.cmb_MfgCatalog.Validating += new System.ComponentModel.CancelEventHandler(this.cmb_MfgCatalog_Validating);
            this.cmb_MfgCatalog.Validated += new System.EventHandler(this.cmb_MfgCatalog_Validated);
            // 
            // eb_Description
            // 
            this.cueExtender1.SetCueText(this.eb_Description, "Description");
            this.eb_Description.Location = new System.Drawing.Point(455, 24);
            this.eb_Description.Name = "eb_Description";
            this.eb_Description.Size = new System.Drawing.Size(350, 20);
            this.eb_Description.TabIndex = 7;
            this.eb_Description.Validating += new System.ComponentModel.CancelEventHandler(this.eb_Description_Validating);
            // 
            // cmb_Mfg_Name
            // 
            this.cmb_Mfg_Name.AllowTypedIn = false;
            this.cueExtender1.SetCueText(this.cmb_Mfg_Name, "MfgName");
            this.cmb_Mfg_Name.CurrentItem = null;
            this.cmb_Mfg_Name.Items = ((System.Collections.Generic.List<Ehs.Controls.ComboBoxString>)(resources.GetObject("cmb_Mfg_Name.Items")));
            this.cmb_Mfg_Name.Location = new System.Drawing.Point(510, 64);
            this.cmb_Mfg_Name.Name = "cmb_Mfg_Name";
            this.cmb_Mfg_Name.PopupBorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cmb_Mfg_Name.PopupOffset = new System.Drawing.Point(12, 0);
            this.cmb_Mfg_Name.PopupSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.cmb_Mfg_Name.PopupSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmb_Mfg_Name.PopupWidth = 300;
            this.cmb_Mfg_Name.Size = new System.Drawing.Size(350, 20);
            this.cmb_Mfg_Name.TabIndex = 15;
            this.cmb_Mfg_Name.Validating += new System.ComponentModel.CancelEventHandler(this.cmb_Mfg_Name_Validating);
            // 
            // b_MUOP
            // 
            this.b_MUOP.Location = new System.Drawing.Point(456, 44);
            this.b_MUOP.Name = "b_MUOP";
            this.b_MUOP.Size = new System.Drawing.Size(13, 19);
            this.b_MUOP.TabIndex = 11;
            this.b_MUOP.Text = "v";
            this.b_MUOP.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(85, 87);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(862, 24);
            this.menuStrip1.TabIndex = 33;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.toolsToolStripMenuItem.Text = "Create NonFile";
            this.toolsToolStripMenuItem.Click += new System.EventHandler(this.toolsToolStripMenuItem_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Mat Code";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-3, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Vendor Cat";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-3, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "MFG Cat";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(452, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Mfg Name";
            // 
            // ItemSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 144);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.cmb_Mfg_Name);
            this.Controls.Add(this.eb_Description);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmb_MfgCatalog);
            this.Controls.Add(this.cmb_VendorCatalog);
            this.Controls.Add(this.cmb_Mat);
            this.Controls.Add(this.b_MUOP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ItemSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ItemSelection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ItemSelection_FormClosing);
            this.Load += new System.EventHandler(this.ItemSelection_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Ehs.Controls.CueExtender cueExtender1;
        private Ehs.Controls.AutoCompleteTextBox cmb_Mat;
        private System.Windows.Forms.Button b_MUOP;
        private Ehs.Controls.AutoCompleteTextBox cmb_VendorCatalog;
        private Ehs.Controls.AutoCompleteTextBox cmb_MfgCatalog;
        private System.Windows.Forms.Button button1;
        private Ehs.Controls.EhsTextBox eb_Description;
        private Ehs.Controls.AutoCompleteTextBox cmb_Mfg_Name;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}