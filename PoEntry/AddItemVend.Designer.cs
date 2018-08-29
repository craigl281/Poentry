namespace PoEntry
{
    partial class AddItemVend
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddItemVend));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmb_Mat = new Ehs.Controls.AutoCompleteTextBox();
            this.cmb_Vendor = new Ehs.Controls.AutoCompleteTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmb_Purchase = new Ehs.Controls.AutoCompleteTextBox();
            this.eb_comment = new System.Windows.Forms.TextBox();
            this.eb_cost = new System.Windows.Forms.TextBox();
            this.eb_conversion = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.eb_mfg_code = new System.Windows.Forms.TextBox();
            this.eb_mfg_name = new System.Windows.Forms.TextBox();
            this.eb_mfg_cat = new System.Windows.Forms.TextBox();
            this.eb_vendor_cat = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.q_Command = new System.Data.SqlClient.SqlCommand();
            this.sqlConnection7 = new System.Data.SqlClient.SqlConnection();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmb_Mat);
            this.panel1.Controls.Add(this.cmb_Vendor);
            this.panel1.Location = new System.Drawing.Point(0, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(541, 47);
            this.panel1.TabIndex = 0;
            // 
            // cmb_Mat
            // 
            this.cmb_Mat.AllowTypedIn = false;
            this.cmb_Mat.CurrentItem = null;
            this.cmb_Mat.Location = new System.Drawing.Point(9, 3);
            this.cmb_Mat.Name = "cmb_Mat";
            this.cmb_Mat.PopupBorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cmb_Mat.PopupOffset = new System.Drawing.Point(12, 0);
            this.cmb_Mat.PopupSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.cmb_Mat.PopupSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmb_Mat.PopupWidth = 300;
            this.cmb_Mat.Size = new System.Drawing.Size(531, 20);
            this.cmb_Mat.TabIndex = 21;
            // 
            // cmb_Vendor
            // 
            this.cmb_Vendor.AllowTypedIn = false;
            this.cmb_Vendor.CurrentItem = null;
            this.cmb_Vendor.Location = new System.Drawing.Point(9, 23);
            this.cmb_Vendor.Name = "cmb_Vendor";
            this.cmb_Vendor.PopupBorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cmb_Vendor.PopupOffset = new System.Drawing.Point(12, 0);
            this.cmb_Vendor.PopupSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.cmb_Vendor.PopupSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmb_Vendor.PopupWidth = 300;
            this.cmb_Vendor.Size = new System.Drawing.Size(531, 20);
            this.cmb_Vendor.TabIndex = 20;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(169, 120);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(298, 120);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmb_Purchase);
            this.panel2.Controls.Add(this.eb_comment);
            this.panel2.Controls.Add(this.eb_cost);
            this.panel2.Controls.Add(this.eb_conversion);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.eb_mfg_code);
            this.panel2.Controls.Add(this.eb_mfg_name);
            this.panel2.Controls.Add(this.eb_mfg_cat);
            this.panel2.Controls.Add(this.eb_vendor_cat);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(0, 61);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(547, 58);
            this.panel2.TabIndex = 6;
            // 
            // cmb_Purchase
            // 
            this.cmb_Purchase.AllowTypedIn = false;
            this.cmb_Purchase.CurrentItem = null;
            this.cmb_Purchase.Location = new System.Drawing.Point(234, 1);
            this.cmb_Purchase.Name = "cmb_Purchase";
            this.cmb_Purchase.PopupBorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cmb_Purchase.PopupOffset = new System.Drawing.Point(12, 0);
            this.cmb_Purchase.PopupSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.cmb_Purchase.PopupSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmb_Purchase.PopupWidth = 300;
            this.cmb_Purchase.Size = new System.Drawing.Size(197, 20);
            this.cmb_Purchase.TabIndex = 21;
            // 
            // eb_comment
            // 
            this.eb_comment.Location = new System.Drawing.Point(228, 36);
            this.eb_comment.Name = "eb_comment";
            this.eb_comment.Size = new System.Drawing.Size(100, 20);
            this.eb_comment.TabIndex = 18;
            // 
            // eb_cost
            // 
            this.eb_cost.Location = new System.Drawing.Point(53, 38);
            this.eb_cost.Name = "eb_cost";
            this.eb_cost.Size = new System.Drawing.Size(100, 20);
            this.eb_cost.TabIndex = 17;
            // 
            // eb_conversion
            // 
            this.eb_conversion.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Conversion", true));
            this.eb_conversion.Location = new System.Drawing.Point(490, 1);
            this.eb_conversion.Name = "eb_conversion";
            this.eb_conversion.Size = new System.Drawing.Size(59, 20);
            this.eb_conversion.TabIndex = 16;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Ehs.Models.ItemVend);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(428, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Conversion";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(178, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Memo";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Po Cost";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(205, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "UOP";
            // 
            // eb_mfg_code
            // 
            this.eb_mfg_code.Location = new System.Drawing.Point(480, 23);
            this.eb_mfg_code.Name = "eb_mfg_code";
            this.eb_mfg_code.Size = new System.Drawing.Size(69, 20);
            this.eb_mfg_code.TabIndex = 9;
            // 
            // eb_mfg_name
            // 
            this.eb_mfg_name.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "MFGName", true));
            this.eb_mfg_name.Location = new System.Drawing.Point(295, 20);
            this.eb_mfg_name.Name = "eb_mfg_name";
            this.eb_mfg_name.Size = new System.Drawing.Size(130, 20);
            this.eb_mfg_name.TabIndex = 8;
            // 
            // eb_mfg_cat
            // 
            this.eb_mfg_cat.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "MFGCatalog", true));
            this.eb_mfg_cat.Location = new System.Drawing.Point(105, 19);
            this.eb_mfg_cat.Name = "eb_mfg_cat";
            this.eb_mfg_cat.Size = new System.Drawing.Size(130, 20);
            this.eb_mfg_cat.TabIndex = 7;
            // 
            // eb_vendor_cat
            // 
            this.eb_vendor_cat.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "VendorCatalog", true));
            this.eb_vendor_cat.Location = new System.Drawing.Point(105, 1);
            this.eb_vendor_cat.Name = "eb_vendor_cat";
            this.eb_vendor_cat.Size = new System.Drawing.Size(100, 20);
            this.eb_vendor_cat.TabIndex = 6;
            this.eb_vendor_cat.Leave += new System.EventHandler(this.eb_vendor_cat_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(421, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "MFG Code";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(237, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "MFG Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "MFG catalogue No";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Vend catalogue No";
            // 
            // q_Command
            // 
            this.q_Command.CommandText = "      ";
            this.q_Command.Connection = this.sqlConnection7;
            // 
            // sqlConnection7
            // 
            this.sqlConnection7.FireInfoMessageEventOnUserErrors = false;
            // 
            // AddItemVend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 144);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddItemVend";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Item to Vendor";
            this.Load += new System.EventHandler(this.AddItemVend_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox eb_mfg_code;
        private System.Windows.Forms.TextBox eb_mfg_name;
        private System.Windows.Forms.TextBox eb_mfg_cat;
        private System.Windows.Forms.TextBox eb_vendor_cat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox eb_comment;
        private System.Windows.Forms.TextBox eb_cost;
        private System.Windows.Forms.TextBox eb_conversion;
        private System.Data.SqlClient.SqlCommand q_Command;
        private System.Data.SqlClient.SqlConnection sqlConnection7;
        private Ehs.Controls.AutoCompleteTextBox cmb_Vendor;
        private Ehs.Controls.AutoCompleteTextBox cmb_Mat;
        private Ehs.Controls.AutoCompleteTextBox cmb_Purchase;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}