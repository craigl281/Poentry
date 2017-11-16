namespace PoEntry
{
    partial class AddCatalog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddCatalog));
            this.eb_Catalog2 = new System.Windows.Forms.TextBox();
            this.eb_Catalog_Desc2 = new System.Windows.Forms.TextBox();
            this.B_OK = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_Catalog = new Ehs.Controls.AutoCompleteTextBox();
            this.SuspendLayout();
            // 
            // eb_Catalog2
            // 
            this.eb_Catalog2.Location = new System.Drawing.Point(55, 30);
            this.eb_Catalog2.Name = "eb_Catalog2";
            this.eb_Catalog2.Size = new System.Drawing.Size(125, 20);
            this.eb_Catalog2.TabIndex = 0;
            // 
            // eb_Catalog_Desc2
            // 
            this.eb_Catalog_Desc2.Location = new System.Drawing.Point(181, 30);
            this.eb_Catalog_Desc2.Name = "eb_Catalog_Desc2";
            this.eb_Catalog_Desc2.Size = new System.Drawing.Size(300, 20);
            this.eb_Catalog_Desc2.TabIndex = 1;
            // 
            // B_OK
            // 
            this.B_OK.Location = new System.Drawing.Point(125, 75);
            this.B_OK.Name = "B_OK";
            this.B_OK.Size = new System.Drawing.Size(75, 23);
            this.B_OK.TabIndex = 2;
            this.B_OK.Text = "OK";
            this.B_OK.UseVisualStyleBackColor = true;
            this.B_OK.Click += new System.EventHandler(this.B_OK_Click);
            // 
            // B_Cancel
            // 
            this.B_Cancel.Location = new System.Drawing.Point(295, 75);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(75, 23);
            this.B_Cancel.TabIndex = 3;
            this.B_Cancel.Text = "Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Catalog";
            // 
            // cmb_Catalog
            // 
            this.cmb_Catalog.AllowTypedIn = false;
            this.cmb_Catalog.CurrentItem = null;
            this.cmb_Catalog.Items = null;
            this.cmb_Catalog.Location = new System.Drawing.Point(82, 49);
            this.cmb_Catalog.Name = "cmb_Catalog";
            this.cmb_Catalog.PopupBorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cmb_Catalog.PopupOffset = new System.Drawing.Point(12, 0);
            this.cmb_Catalog.PopupSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.cmb_Catalog.PopupSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmb_Catalog.PopupWidth = 300;
            this.cmb_Catalog.Size = new System.Drawing.Size(325, 20);
            this.cmb_Catalog.TabIndex = 5;
            // 
            // AddCatalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 107);
            this.Controls.Add(this.cmb_Catalog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.B_OK);
            this.Controls.Add(this.eb_Catalog_Desc2);
            this.Controls.Add(this.eb_Catalog2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddCatalog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Catalog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox eb_Catalog2;
        private System.Windows.Forms.TextBox eb_Catalog_Desc2;
        private System.Windows.Forms.Button B_OK;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.Label label1;
        private Ehs.Controls.AutoCompleteTextBox cmb_Catalog;
    }
}