namespace PoEntry
{
    partial class FrmVendor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVendor));
            this.txt_vender_phone = new System.Windows.Forms.MaskedTextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.txt_vendor_city = new Ehs.Controls.EhsTextBox();
            this.cueExtender1 = new Ehs.Controls.CueExtender();
            this.txt_vendor_email = new Ehs.Controls.EhsTextBox();
            this.txt_vendor_fax = new System.Windows.Forms.MaskedTextBox();
            this.txt_vendor_zip = new Ehs.Controls.EhsTextBox();
            this.txt_vendor_State = new Ehs.Controls.EhsTextBox();
            this.txt_vendor_address3 = new Ehs.Controls.EhsTextBox();
            this.txt_vendor_address2 = new Ehs.Controls.EhsTextBox();
            this.txt_vendor_address1 = new Ehs.Controls.EhsTextBox();
            this.txt_vendor_attention = new Ehs.Controls.EhsTextBox();
            this.cmb_vendor = new Ehs.Controls.AutoCompleteTextBox();
            this.Pnl_Vendor = new Ehs.Controls.EhsPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.eb_V_Memo = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.cmb_Terms_Code = new Ehs.Controls.AutoCompleteTextBox();
            this.eb_FOB = new Ehs.Controls.EhsTextBox();
            this.eb_VMShip_Account = new Ehs.Controls.EhsTextBox();
            this.eb_Vendor_Account = new Ehs.Controls.EhsTextBox();
            this.eb_Order_Days = new Ehs.Controls.EhsTextBox();
            this.lbl_Ship_Account = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.Pnl_Vendor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_vender_phone
            // 
            this.txt_vender_phone.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "PhoneNo", true));
            this.txt_vender_phone.Location = new System.Drawing.Point(0, 120);
            this.txt_vender_phone.Mask = "(999) 000-0000";
            this.txt_vender_phone.Name = "txt_vender_phone";
            this.txt_vender_phone.Size = new System.Drawing.Size(79, 20);
            this.txt_vender_phone.TabIndex = 112;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Ehs.Models.Vendor);
            // 
            // txt_vendor_city
            // 
            this.cueExtender1.SetCueText(this.txt_vendor_city, "City");
            this.txt_vendor_city.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "City", true));
            this.txt_vendor_city.Location = new System.Drawing.Point(0, 100);
            this.txt_vendor_city.Name = "txt_vendor_city";
            this.txt_vendor_city.Size = new System.Drawing.Size(109, 20);
            this.txt_vendor_city.TabIndex = 109;
            this.txt_vendor_city.Validated += new System.EventHandler(this.txt_vendor_city_Validated);
            // 
            // txt_vendor_email
            // 
            this.cueExtender1.SetCueText(this.txt_vendor_email, "Email");
            this.txt_vendor_email.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "VendorEmail", true));
            this.txt_vendor_email.Location = new System.Drawing.Point(0, 140);
            this.txt_vendor_email.Name = "txt_vendor_email";
            this.txt_vendor_email.Size = new System.Drawing.Size(301, 20);
            this.txt_vendor_email.TabIndex = 30;
            // 
            // txt_vendor_fax
            // 
            this.cueExtender1.SetCueText(this.txt_vendor_fax, "Phone Number");
            this.txt_vendor_fax.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Fax", true));
            this.txt_vendor_fax.Location = new System.Drawing.Point(222, 120);
            this.txt_vendor_fax.Mask = "(999) 000-0000";
            this.txt_vendor_fax.Name = "txt_vendor_fax";
            this.txt_vendor_fax.Size = new System.Drawing.Size(79, 20);
            this.txt_vendor_fax.TabIndex = 29;
            // 
            // txt_vendor_zip
            // 
            this.cueExtender1.SetCueText(this.txt_vendor_zip, "Zip");
            this.txt_vendor_zip.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Zip", true));
            this.txt_vendor_zip.Location = new System.Drawing.Point(236, 100);
            this.txt_vendor_zip.Name = "txt_vendor_zip";
            this.txt_vendor_zip.Size = new System.Drawing.Size(65, 20);
            this.txt_vendor_zip.TabIndex = 27;
            this.txt_vendor_zip.Validated += new System.EventHandler(this.txt_vendor_zip_Validated);
            // 
            // txt_vendor_State
            // 
            this.cueExtender1.SetCueText(this.txt_vendor_State, "State");
            this.txt_vendor_State.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "State", true));
            this.txt_vendor_State.Location = new System.Drawing.Point(109, 100);
            this.txt_vendor_State.Name = "txt_vendor_State";
            this.txt_vendor_State.Size = new System.Drawing.Size(127, 20);
            this.txt_vendor_State.TabIndex = 26;
            this.txt_vendor_State.Validated += new System.EventHandler(this.txt_vendor_State_Validated);
            // 
            // txt_vendor_address3
            // 
            this.cueExtender1.SetCueText(this.txt_vendor_address3, "Address3");
            this.txt_vendor_address3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Address3", true));
            this.txt_vendor_address3.Location = new System.Drawing.Point(0, 80);
            this.txt_vendor_address3.Name = "txt_vendor_address3";
            this.txt_vendor_address3.Size = new System.Drawing.Size(301, 20);
            this.txt_vendor_address3.TabIndex = 24;
            this.txt_vendor_address3.Validated += new System.EventHandler(this.txt_vendor_address3_Validated);
            // 
            // txt_vendor_address2
            // 
            this.cueExtender1.SetCueText(this.txt_vendor_address2, "Address2");
            this.txt_vendor_address2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Address2", true));
            this.txt_vendor_address2.Location = new System.Drawing.Point(0, 60);
            this.txt_vendor_address2.Name = "txt_vendor_address2";
            this.txt_vendor_address2.Size = new System.Drawing.Size(301, 20);
            this.txt_vendor_address2.TabIndex = 23;
            this.txt_vendor_address2.Validated += new System.EventHandler(this.txt_vendor_address2_Validated);
            // 
            // txt_vendor_address1
            // 
            this.cueExtender1.SetCueText(this.txt_vendor_address1, "Address1");
            this.txt_vendor_address1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Address1", true));
            this.txt_vendor_address1.Location = new System.Drawing.Point(0, 40);
            this.txt_vendor_address1.Name = "txt_vendor_address1";
            this.txt_vendor_address1.Size = new System.Drawing.Size(301, 20);
            this.txt_vendor_address1.TabIndex = 22;
            this.txt_vendor_address1.Validated += new System.EventHandler(this.txt_vendor_address1_Validated);
            // 
            // txt_vendor_attention
            // 
            this.cueExtender1.SetCueText(this.txt_vendor_attention, "Attention");
            this.txt_vendor_attention.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "VendorAccount", true));
            this.txt_vendor_attention.Location = new System.Drawing.Point(0, 20);
            this.txt_vendor_attention.Name = "txt_vendor_attention";
            this.txt_vendor_attention.Size = new System.Drawing.Size(301, 20);
            this.txt_vendor_attention.TabIndex = 21;
            // 
            // cmb_vendor
            // 
            this.cmb_vendor.AllowTypedIn = false;
            this.cmb_vendor.BackColor = System.Drawing.SystemColors.Window;
            this.cueExtender1.SetCueText(this.cmb_vendor, "Vendor");
            this.cmb_vendor.CurrentItem = null;
            this.cmb_vendor.Location = new System.Drawing.Point(0, 0);
            this.cmb_vendor.Name = "cmb_vendor";
            this.cmb_vendor.PopupBorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cmb_vendor.PopupOffset = new System.Drawing.Point(12, 0);
            this.cmb_vendor.PopupSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.cmb_vendor.PopupSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmb_vendor.PopupWidth = 300;
            this.cmb_vendor.Size = new System.Drawing.Size(301, 20);
            this.cmb_vendor.TabIndex = 20;
            this.cmb_vendor.Validated += new System.EventHandler(this.cmb_vendor_Validated);
            // 
            // Pnl_Vendor
            // 
            this.Pnl_Vendor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Vendor.Controls.Add(this.button1);
            this.Pnl_Vendor.Controls.Add(this.txt_vendor_email);
            this.Pnl_Vendor.Controls.Add(this.txt_vendor_city);
            this.Pnl_Vendor.Controls.Add(this.txt_vender_phone);
            this.Pnl_Vendor.Controls.Add(this.txt_vendor_fax);
            this.Pnl_Vendor.Controls.Add(this.txt_vendor_zip);
            this.Pnl_Vendor.Controls.Add(this.txt_vendor_State);
            this.Pnl_Vendor.Controls.Add(this.txt_vendor_address3);
            this.Pnl_Vendor.Controls.Add(this.txt_vendor_address2);
            this.Pnl_Vendor.Controls.Add(this.txt_vendor_address1);
            this.Pnl_Vendor.Controls.Add(this.txt_vendor_attention);
            this.Pnl_Vendor.Controls.Add(this.cmb_vendor);
            this.Pnl_Vendor.Controls.Add(this.eb_V_Memo);
            this.Pnl_Vendor.Controls.Add(this.label25);
            this.Pnl_Vendor.Controls.Add(this.cmb_Terms_Code);
            this.Pnl_Vendor.Controls.Add(this.eb_FOB);
            this.Pnl_Vendor.Controls.Add(this.eb_VMShip_Account);
            this.Pnl_Vendor.Controls.Add(this.eb_Vendor_Account);
            this.Pnl_Vendor.Controls.Add(this.eb_Order_Days);
            this.Pnl_Vendor.Controls.Add(this.lbl_Ship_Account);
            this.Pnl_Vendor.Controls.Add(this.label22);
            this.Pnl_Vendor.Controls.Add(this.label21);
            this.Pnl_Vendor.Controls.Add(this.label20);
            this.Pnl_Vendor.Location = new System.Drawing.Point(0, 0);
            this.Pnl_Vendor.Name = "Pnl_Vendor";
            this.Pnl_Vendor.ReadOnly = false;
            this.Pnl_Vendor.Size = new System.Drawing.Size(795, 164);
            this.Pnl_Vendor.TabIndex = 113;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(489, 137);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 113;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // eb_V_Memo
            // 
            this.eb_V_Memo.Location = new System.Drawing.Point(548, 100);
            this.eb_V_Memo.Name = "eb_V_Memo";
            this.eb_V_Memo.Size = new System.Drawing.Size(109, 23);
            this.eb_V_Memo.TabIndex = 36;
            this.eb_V_Memo.Text = "Vendor Memo";
            this.eb_V_Memo.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(545, 83);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(37, 13);
            this.label25.TabIndex = 103;
            this.label25.Text = "F.O.B.";
            // 
            // cmb_Terms_Code
            // 
            this.cmb_Terms_Code.AllowTypedIn = false;
            this.cmb_Terms_Code.CurrentItem = null;
            this.cmb_Terms_Code.Location = new System.Drawing.Point(582, 20);
            this.cmb_Terms_Code.Name = "cmb_Terms_Code";
            this.cmb_Terms_Code.PopupBorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cmb_Terms_Code.PopupOffset = new System.Drawing.Point(12, 0);
            this.cmb_Terms_Code.PopupSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.cmb_Terms_Code.PopupSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmb_Terms_Code.PopupWidth = 300;
            this.cmb_Terms_Code.Size = new System.Drawing.Size(200, 20);
            this.cmb_Terms_Code.TabIndex = 32;
            this.cmb_Terms_Code.Validating += new System.ComponentModel.CancelEventHandler(this.cmb_Terms_Code_Validating);
            this.cmb_Terms_Code.Validated += new System.EventHandler(this.cmb_Terms_Code_Validated);
            // 
            // eb_FOB
            // 
            this.eb_FOB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "FOB", true));
            this.eb_FOB.Location = new System.Drawing.Point(582, 80);
            this.eb_FOB.Name = "eb_FOB";
            this.eb_FOB.Size = new System.Drawing.Size(150, 20);
            this.eb_FOB.TabIndex = 35;
            // 
            // eb_VMShip_Account
            // 
            this.eb_VMShip_Account.Location = new System.Drawing.Point(582, 60);
            this.eb_VMShip_Account.Name = "eb_VMShip_Account";
            this.eb_VMShip_Account.Size = new System.Drawing.Size(200, 20);
            this.eb_VMShip_Account.TabIndex = 34;
            // 
            // eb_Vendor_Account
            // 
            this.eb_Vendor_Account.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "VendorAccount", true));
            this.eb_Vendor_Account.Location = new System.Drawing.Point(582, 40);
            this.eb_Vendor_Account.Name = "eb_Vendor_Account";
            this.eb_Vendor_Account.Size = new System.Drawing.Size(200, 20);
            this.eb_Vendor_Account.TabIndex = 33;
            // 
            // eb_Order_Days
            // 
            this.eb_Order_Days.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "OrderDays", true));
            this.eb_Order_Days.Location = new System.Drawing.Point(582, 0);
            this.eb_Order_Days.Name = "eb_Order_Days";
            this.eb_Order_Days.Size = new System.Drawing.Size(150, 20);
            this.eb_Order_Days.TabIndex = 31;
            // 
            // lbl_Ship_Account
            // 
            this.lbl_Ship_Account.AutoSize = true;
            this.lbl_Ship_Account.Location = new System.Drawing.Point(511, 63);
            this.lbl_Ship_Account.Name = "lbl_Ship_Account";
            this.lbl_Ship_Account.Size = new System.Drawing.Size(71, 13);
            this.lbl_Ship_Account.TabIndex = 95;
            this.lbl_Ship_Account.Text = "Ship Account";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(535, 43);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(47, 13);
            this.label22.TabIndex = 94;
            this.label22.Text = "Account";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(546, 23);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(36, 13);
            this.label21.TabIndex = 93;
            this.label21.Text = "Terms";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(522, 3);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(60, 13);
            this.label20.TabIndex = 92;
            this.label20.Text = "Order Days";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FrmVendor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 165);
            this.Controls.Add(this.Pnl_Vendor);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmVendor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vendor Selection";
            this.Load += new System.EventHandler(this.FrmVendor_Load);
            this.Shown += new System.EventHandler(this.FrmVendor_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.Pnl_Vendor.ResumeLayout(false);
            this.Pnl_Vendor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.MaskedTextBox txt_vender_phone;
        private Ehs.Controls.EhsTextBox txt_vendor_city;
        private Ehs.Controls.CueExtender cueExtender1;
        private Ehs.Controls.EhsPanel Pnl_Vendor;
        private Ehs.Controls.EhsTextBox txt_vendor_email;
        private System.Windows.Forms.MaskedTextBox txt_vendor_fax;
        private Ehs.Controls.EhsTextBox txt_vendor_zip;
        private Ehs.Controls.EhsTextBox txt_vendor_State;
        private Ehs.Controls.EhsTextBox txt_vendor_address3;
        private Ehs.Controls.EhsTextBox txt_vendor_address2;
        private Ehs.Controls.EhsTextBox txt_vendor_address1;
        private Ehs.Controls.EhsTextBox txt_vendor_attention;
        private System.Windows.Forms.Button eb_V_Memo;
        private System.Windows.Forms.Label label25;
        private Ehs.Controls.AutoCompleteTextBox cmb_Terms_Code;
        private Ehs.Controls.EhsTextBox eb_FOB;
        private Ehs.Controls.EhsTextBox eb_Vendor_Account;
        private Ehs.Controls.EhsTextBox eb_Order_Days;
        private System.Windows.Forms.Label lbl_Ship_Account;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        public Ehs.Controls.AutoCompleteTextBox cmb_vendor;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        public Ehs.Controls.EhsTextBox eb_VMShip_Account;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}