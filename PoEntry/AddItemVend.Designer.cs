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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.eb_comment = new System.Windows.Forms.TextBox();
            this.eb_cost = new System.Windows.Forms.TextBox();
            this.eb_conversion = new System.Windows.Forms.TextBox();
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
            this.cmb_Vendor = new Ehs.Controls.AutoCompleteTextBox();
            this.autoCompleteTextBox1 = new Ehs.Controls.AutoCompleteTextBox();
            this.autoCompleteTextBox2 = new Ehs.Controls.AutoCompleteTextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.autoCompleteTextBox2);
            this.panel1.Controls.Add(this.cmb_Vendor);
            this.panel1.Location = new System.Drawing.Point(0, 20);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(812, 72);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(253, 185);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 35);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(447, 185);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 35);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.autoCompleteTextBox1);
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
            this.panel2.Location = new System.Drawing.Point(0, 94);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(821, 90);
            this.panel2.TabIndex = 6;
            // 
            // eb_comment
            // 
            this.eb_comment.Location = new System.Drawing.Point(342, 55);
            this.eb_comment.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eb_comment.Name = "eb_comment";
            this.eb_comment.Size = new System.Drawing.Size(148, 26);
            this.eb_comment.TabIndex = 18;
            // 
            // eb_cost
            // 
            this.eb_cost.Location = new System.Drawing.Point(80, 58);
            this.eb_cost.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eb_cost.Name = "eb_cost";
            this.eb_cost.Size = new System.Drawing.Size(148, 26);
            this.eb_cost.TabIndex = 17;
            // 
            // eb_conversion
            // 
            this.eb_conversion.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Conversion", true));
            this.eb_conversion.Location = new System.Drawing.Point(735, 2);
            this.eb_conversion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eb_conversion.Name = "eb_conversion";
            this.eb_conversion.Size = new System.Drawing.Size(86, 26);
            this.eb_conversion.TabIndex = 16;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(642, 6);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 20);
            this.label11.TabIndex = 13;
            this.label11.Text = "Conversion";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(267, 59);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 20);
            this.label10.TabIndex = 12;
            this.label10.Text = "Memo";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 61);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 20);
            this.label9.TabIndex = 11;
            this.label9.Text = "Po Cost";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(308, 7);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 20);
            this.label8.TabIndex = 10;
            this.label8.Text = "UOP";
            // 
            // eb_mfg_code
            // 
            this.eb_mfg_code.Location = new System.Drawing.Point(720, 35);
            this.eb_mfg_code.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eb_mfg_code.Name = "eb_mfg_code";
            this.eb_mfg_code.Size = new System.Drawing.Size(101, 26);
            this.eb_mfg_code.TabIndex = 9;
            // 
            // eb_mfg_name
            // 
            this.eb_mfg_name.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "MFGName", true));
            this.eb_mfg_name.Location = new System.Drawing.Point(442, 31);
            this.eb_mfg_name.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eb_mfg_name.Name = "eb_mfg_name";
            this.eb_mfg_name.Size = new System.Drawing.Size(193, 26);
            this.eb_mfg_name.TabIndex = 8;
            // 
            // eb_mfg_cat
            // 
            this.eb_mfg_cat.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "MFGCatalog", true));
            this.eb_mfg_cat.Location = new System.Drawing.Point(157, 29);
            this.eb_mfg_cat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eb_mfg_cat.Name = "eb_mfg_cat";
            this.eb_mfg_cat.Size = new System.Drawing.Size(193, 26);
            this.eb_mfg_cat.TabIndex = 7;
            // 
            // eb_vendor_cat
            // 
            this.eb_vendor_cat.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "VendorCatalog", true));
            this.eb_vendor_cat.Location = new System.Drawing.Point(157, 2);
            this.eb_vendor_cat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eb_vendor_cat.Name = "eb_vendor_cat";
            this.eb_vendor_cat.Size = new System.Drawing.Size(148, 26);
            this.eb_vendor_cat.TabIndex = 6;
            this.eb_vendor_cat.Leave += new System.EventHandler(this.eb_vendor_cat_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(632, 36);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 20);
            this.label7.TabIndex = 4;
            this.label7.Text = "MFG Code";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(355, 35);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 20);
            this.label6.TabIndex = 3;
            this.label6.Text = "MFG Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 28);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "MFG catalogue No";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 20);
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
            // cmb_Vendor
            // 
            this.cmb_Vendor.AllowTypedIn = false;
            this.cmb_Vendor.CurrentItem = null;
            this.cmb_Vendor.Location = new System.Drawing.Point(13, 35);
            this.cmb_Vendor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmb_Vendor.Name = "cmb_Vendor";
            this.cmb_Vendor.PopupBorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cmb_Vendor.PopupOffset = new System.Drawing.Point(12, 0);
            this.cmb_Vendor.PopupSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.cmb_Vendor.PopupSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmb_Vendor.PopupWidth = 300;
            this.cmb_Vendor.Size = new System.Drawing.Size(795, 29);
            this.cmb_Vendor.TabIndex = 20;
            // 
            // autoCompleteTextBox1
            // 
            this.autoCompleteTextBox1.AllowTypedIn = false;
            this.autoCompleteTextBox1.CurrentItem = null;
            this.autoCompleteTextBox1.Location = new System.Drawing.Point(351, 1);
            this.autoCompleteTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.autoCompleteTextBox1.Name = "autoCompleteTextBox1";
            this.autoCompleteTextBox1.PopupBorderStyle = System.Windows.Forms.BorderStyle.None;
            this.autoCompleteTextBox1.PopupOffset = new System.Drawing.Point(12, 0);
            this.autoCompleteTextBox1.PopupSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.autoCompleteTextBox1.PopupSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.autoCompleteTextBox1.PopupWidth = 300;
            this.autoCompleteTextBox1.Size = new System.Drawing.Size(294, 29);
            this.autoCompleteTextBox1.TabIndex = 21;
            // 
            // autoCompleteTextBox2
            // 
            this.autoCompleteTextBox2.AllowTypedIn = false;
            this.autoCompleteTextBox2.CurrentItem = null;
            this.autoCompleteTextBox2.Location = new System.Drawing.Point(13, 5);
            this.autoCompleteTextBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.autoCompleteTextBox2.Name = "autoCompleteTextBox2";
            this.autoCompleteTextBox2.PopupBorderStyle = System.Windows.Forms.BorderStyle.None;
            this.autoCompleteTextBox2.PopupOffset = new System.Drawing.Point(12, 0);
            this.autoCompleteTextBox2.PopupSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.autoCompleteTextBox2.PopupSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.autoCompleteTextBox2.PopupWidth = 300;
            this.autoCompleteTextBox2.Size = new System.Drawing.Size(795, 29);
            this.autoCompleteTextBox2.TabIndex = 21;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Ehs.Models.ItemVend);
            // 
            // AddItemVend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 221);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
        private Ehs.Controls.AutoCompleteTextBox autoCompleteTextBox2;
        private Ehs.Controls.AutoCompleteTextBox autoCompleteTextBox1;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}