namespace Ehs.Controls
{
    partial class VendorWidget
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Ehs.Models.Common.Numbers numbers1 = new Ehs.Models.Common.Numbers();
            Ehs.Models.Common.Address address1 = new Ehs.Models.Common.Address();
            this.cmb_vendor = new Ehs.Controls.EhsComboBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.numbersWidget1 = new Ehs.Controls.NumbersWidget();
            this.addressWidget1 = new Ehs.Controls.AddressWidget();
            this.email = new Ehs.Controls.EhsLookupTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmb_vendor
            // 
            this.cmb_vendor.DataSource = this.bindingSource1;
            this.cmb_vendor.DisplayMember = "Display";
            this.cmb_vendor.FormattingEnabled = true;
            this.cmb_vendor.Location = new System.Drawing.Point(3, 1);
            this.cmb_vendor.Name = "cmb_vendor";
            this.cmb_vendor.ReadOnly = false;
            this.cmb_vendor.Size = new System.Drawing.Size(337, 21);
            this.cmb_vendor.TabIndex = 2;
            this.cmb_vendor.ValueMember = "VendorID";
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Ehs.Models.Vendor);
            // 
            // numbersWidget1
            // 
            this.numbersWidget1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numbersWidget1.Location = new System.Drawing.Point(0, 129);
            this.numbersWidget1.MinimumSize = new System.Drawing.Size(340, 20);
            this.numbersWidget1.Name = "numbersWidget1";
            numbers1.FaxNumber = "";
            numbers1.PhoneNumber = "";
            this.numbersWidget1.NumberValue = numbers1;
            this.numbersWidget1.ReadOnly = false;
            this.numbersWidget1.Size = new System.Drawing.Size(340, 20);
            this.numbersWidget1.TabIndex = 3;
            this.numbersWidget1.DoubleClick += new System.EventHandler(this.numbersWidget1_DoubleClick);
            this.numbersWidget1.Validated += new System.EventHandler(this.numbersWidget1_Validated);
            // 
            // addressWidget1
            // 
            address1.Address1 = "";
            address1.Address2 = "";
            address1.Address3 = "";
            address1.Attention = "";
            address1.City = "";
            address1.State = "";
            address1.Zip = "";
            this.addressWidget1.AddressValue = address1;
            this.addressWidget1.Location = new System.Drawing.Point(0, 23);
            this.addressWidget1.MinimumSize = new System.Drawing.Size(315, 105);
            this.addressWidget1.Name = "addressWidget1";
            this.addressWidget1.ReadOnly = false;
            this.addressWidget1.Size = new System.Drawing.Size(315, 105);
            this.addressWidget1.TabIndex = 4;
            this.addressWidget1.Validated += new System.EventHandler(this.addressWidget1_Validated);
            // 
            // email
            // 
            this.email.IconColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(69)))), ((int)(((byte)(114)))));
            this.email.IconColorHighlight = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(137)))), ((int)(((byte)(193)))));
            this.email.Location = new System.Drawing.Point(3, 150);
            this.email.Mode = Ehs.Controls.SearchTextBoxMode.None;
            this.email.Name = "email";
            this.email.PromptText = "Email";
            this.email.Size = new System.Drawing.Size(337, 20);
            this.email.TabIndex = 5;
            // 
            // VendorWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmb_vendor);
            this.Controls.Add(this.email);
            this.Controls.Add(this.addressWidget1);
            this.Controls.Add(this.numbersWidget1);
            this.MinimumSize = new System.Drawing.Size(340, 170);
            this.Name = "VendorWidget";
            this.Size = new System.Drawing.Size(345, 170);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumbersWidget numbersWidget1;
        private EhsComboBox cmb_vendor;
        private System.Windows.Forms.BindingSource bindingSource1;
        private AddressWidget addressWidget1;
        private EhsLookupTextBox email;

    }
}
