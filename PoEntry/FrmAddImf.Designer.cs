namespace Purchase_Order_Entry
{
    partial class FrmAddImf
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddImf));
            this.panel1 = new System.Windows.Forms.Panel();
            this.rb_Stock = new System.Windows.Forms.RadioButton();
            this.rb_NonStock = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmb_Vendor = new System.Windows.Forms.ComboBox();
            this.cbx_UOI_AddImf = new System.Windows.Forms.ComboBox();
            this.cbx_UOP_AddImf = new System.Windows.Forms.ComboBox();
            this.eb_Reorder_Location = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.eb_Bin = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.nf_Po_Cost_AddImf = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.eb_Sub_Account_AddImf = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.eb_Mfg_Catalog_AddImf = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.eb_Mfg_Code_AddImf = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.eb_Mfg_Name_AddImf = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.eb_Mat_Code_AddImf = new System.Windows.Forms.TextBox();
            this.nf_UOI_Conversion_AddImf = new System.Windows.Forms.TextBox();
            this.nf_UOP_Conversion_AddImf = new System.Windows.Forms.TextBox();
            this.eb_Buyer_AddImf = new System.Windows.Forms.TextBox();
            this.eb_Account_No_AddImf = new System.Windows.Forms.TextBox();
            this.cmb_Po_Class_AddImf = new System.Windows.Forms.ComboBox();
            this.cmb_Product_Class_AddImf = new System.Windows.Forms.ComboBox();
            this.cmb_Sub_Class_AddImf = new System.Windows.Forms.ComboBox();
            this.eb_Location_AddImf = new System.Windows.Forms.TextBox();
            this.eb_Description2_AddImf = new System.Windows.Forms.TextBox();
            this.eb_Description1_AddImf = new System.Windows.Forms.TextBox();
            this.eb_Vendor_Catalog_AddImf = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.q_Command = new System.Data.SqlClient.SqlCommand();
            this.sqlConnection8 = new System.Data.SqlClient.SqlConnection();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rb_Stock);
            this.panel1.Controls.Add(this.rb_NonStock);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 40);
            this.panel1.TabIndex = 0;
            // 
            // rb_Stock
            // 
            this.rb_Stock.AutoSize = true;
            this.rb_Stock.Location = new System.Drawing.Point(310, 12);
            this.rb_Stock.Name = "rb_Stock";
            this.rb_Stock.Size = new System.Drawing.Size(103, 17);
            this.rb_Stock.TabIndex = 25;
            this.rb_Stock.TabStop = true;
            this.rb_Stock.Text = "Add Stock Items";
            this.rb_Stock.UseVisualStyleBackColor = true;
            this.rb_Stock.Click += new System.EventHandler(this.rb_Stock_Click);
            // 
            // rb_NonStock
            // 
            this.rb_NonStock.AutoSize = true;
            this.rb_NonStock.Location = new System.Drawing.Point(159, 12);
            this.rb_NonStock.Name = "rb_NonStock";
            this.rb_NonStock.Size = new System.Drawing.Size(98, 17);
            this.rb_NonStock.TabIndex = 24;
            this.rb_NonStock.TabStop = true;
            this.rb_NonStock.Text = "Add Non Stock";
            this.rb_NonStock.UseVisualStyleBackColor = true;
            this.rb_NonStock.Click += new System.EventHandler(this.rb_NonStock_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Vendor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Vendor Catalog No";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Description 1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Description 2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Sub Class";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Product Class";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "PO Class";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Location";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 191);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Unit of Purchase";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 215);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Unit of Issue";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 238);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Account No";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 261);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "Buyer";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmb_Vendor);
            this.panel2.Controls.Add(this.cbx_UOI_AddImf);
            this.panel2.Controls.Add(this.cbx_UOP_AddImf);
            this.panel2.Controls.Add(this.eb_Reorder_Location);
            this.panel2.Controls.Add(this.label22);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Controls.Add(this.eb_Bin);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.nf_Po_Cost_AddImf);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.eb_Sub_Account_AddImf);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.eb_Mfg_Catalog_AddImf);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.eb_Mfg_Code_AddImf);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.eb_Mfg_Name_AddImf);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.eb_Mat_Code_AddImf);
            this.panel2.Controls.Add(this.nf_UOI_Conversion_AddImf);
            this.panel2.Controls.Add(this.nf_UOP_Conversion_AddImf);
            this.panel2.Controls.Add(this.eb_Buyer_AddImf);
            this.panel2.Controls.Add(this.eb_Account_No_AddImf);
            this.panel2.Controls.Add(this.cmb_Po_Class_AddImf);
            this.panel2.Controls.Add(this.cmb_Product_Class_AddImf);
            this.panel2.Controls.Add(this.cmb_Sub_Class_AddImf);
            this.panel2.Controls.Add(this.eb_Location_AddImf);
            this.panel2.Controls.Add(this.eb_Description2_AddImf);
            this.panel2.Controls.Add(this.eb_Description1_AddImf);
            this.panel2.Controls.Add(this.eb_Vendor_Catalog_AddImf);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Location = new System.Drawing.Point(0, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(640, 280);
            this.panel2.TabIndex = 13;
            // 
            // cmb_Vendor
            // 
            this.cmb_Vendor.FormattingEnabled = true;
            this.cmb_Vendor.Location = new System.Drawing.Point(123, 2);
            this.cmb_Vendor.Name = "cmb_Vendor";
            this.cmb_Vendor.Size = new System.Drawing.Size(350, 21);
            this.cmb_Vendor.TabIndex = 44;
            // 
            // cbx_UOI_AddImf
            // 
            this.cbx_UOI_AddImf.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingSource1, "Uoi", true));
            this.cbx_UOI_AddImf.FormattingEnabled = true;
            this.cbx_UOI_AddImf.Location = new System.Drawing.Point(123, 211);
            this.cbx_UOI_AddImf.Name = "cbx_UOI_AddImf";
            this.cbx_UOI_AddImf.Size = new System.Drawing.Size(170, 21);
            this.cbx_UOI_AddImf.TabIndex = 43;
            this.cbx_UOI_AddImf.SelectedIndexChanged += new System.EventHandler(this.cbx_UOI_AddImf_SelectedIndexChanged);
            // 
            // cbx_UOP_AddImf
            // 
            this.cbx_UOP_AddImf.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingSource1, "Unit_Purchase", true));
            this.cbx_UOP_AddImf.FormattingEnabled = true;
            this.cbx_UOP_AddImf.Location = new System.Drawing.Point(123, 187);
            this.cbx_UOP_AddImf.Name = "cbx_UOP_AddImf";
            this.cbx_UOP_AddImf.Size = new System.Drawing.Size(170, 21);
            this.cbx_UOP_AddImf.TabIndex = 42;
            this.cbx_UOP_AddImf.SelectedIndexChanged += new System.EventHandler(this.cbx_UOP_AddImf_SelectedIndexChanged);
            // 
            // eb_Reorder_Location
            // 
            this.eb_Reorder_Location.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ReorderLocation", true));
            this.eb_Reorder_Location.Location = new System.Drawing.Point(324, 257);
            this.eb_Reorder_Location.Name = "eb_Reorder_Location";
            this.eb_Reorder_Location.Size = new System.Drawing.Size(100, 20);
            this.eb_Reorder_Location.TabIndex = 41;
            this.eb_Reorder_Location.DoubleClick += new System.EventHandler(this.eb_Reorder_Location_DoubleClick);
            this.eb_Reorder_Location.KeyDown += new System.Windows.Forms.KeyEventHandler(this.eb_Reorder_Location_KeyDown);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(230, 261);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(89, 13);
            this.label22.TabIndex = 40;
            this.label22.Text = "Reorder Location";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(430, 261);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(22, 13);
            this.label21.TabIndex = 39;
            this.label21.Text = "Bin";
            // 
            // eb_Bin
            // 
            this.eb_Bin.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Bin", true));
            this.eb_Bin.Location = new System.Drawing.Point(458, 257);
            this.eb_Bin.Name = "eb_Bin";
            this.eb_Bin.Size = new System.Drawing.Size(121, 20);
            this.eb_Bin.TabIndex = 38;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(300, 168);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(49, 13);
            this.label20.TabIndex = 37;
            this.label20.Text = "P O Cost";
            // 
            // nf_Po_Cost_AddImf
            // 
            this.nf_Po_Cost_AddImf.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "PO_Class", true));
            this.nf_Po_Cost_AddImf.Location = new System.Drawing.Point(410, 164);
            this.nf_Po_Cost_AddImf.Name = "nf_Po_Cost_AddImf";
            this.nf_Po_Cost_AddImf.Size = new System.Drawing.Size(169, 20);
            this.nf_Po_Cost_AddImf.TabIndex = 36;
            this.nf_Po_Cost_AddImf.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(300, 215);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(82, 13);
            this.label19.TabIndex = 35;
            this.label19.Text = "UOI Conversion";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(300, 191);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(86, 13);
            this.label18.TabIndex = 34;
            this.label18.Text = "UOP Conversion";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(330, 238);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(69, 13);
            this.label17.TabIndex = 33;
            this.label17.Text = "Sub Account";
            // 
            // eb_Sub_Account_AddImf
            // 
            this.eb_Sub_Account_AddImf.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "SubAccount", true));
            this.eb_Sub_Account_AddImf.Location = new System.Drawing.Point(410, 234);
            this.eb_Sub_Account_AddImf.Name = "eb_Sub_Account_AddImf";
            this.eb_Sub_Account_AddImf.Size = new System.Drawing.Size(169, 20);
            this.eb_Sub_Account_AddImf.TabIndex = 32;
            this.eb_Sub_Account_AddImf.DoubleClick += new System.EventHandler(this.eb_Sub_Account_AddImf_DoubleClick);
            this.eb_Sub_Account_AddImf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.eb_Sub_Account_AddImf_KeyDown);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(300, 145);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(104, 13);
            this.label16.TabIndex = 31;
            this.label16.Text = "Mfg Catalog Number";
            // 
            // eb_Mfg_Catalog_AddImf
            // 
            this.eb_Mfg_Catalog_AddImf.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Mfg_Catalog", true));
            this.eb_Mfg_Catalog_AddImf.Location = new System.Drawing.Point(410, 141);
            this.eb_Mfg_Catalog_AddImf.Name = "eb_Mfg_Catalog_AddImf";
            this.eb_Mfg_Catalog_AddImf.Size = new System.Drawing.Size(169, 20);
            this.eb_Mfg_Catalog_AddImf.TabIndex = 30;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(300, 121);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 13);
            this.label15.TabIndex = 29;
            this.label15.Text = "Mfg Code";
            // 
            // eb_Mfg_Code_AddImf
            // 
            this.eb_Mfg_Code_AddImf.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "MfgCode", true));
            this.eb_Mfg_Code_AddImf.Location = new System.Drawing.Point(410, 117);
            this.eb_Mfg_Code_AddImf.Name = "eb_Mfg_Code_AddImf";
            this.eb_Mfg_Code_AddImf.Size = new System.Drawing.Size(169, 20);
            this.eb_Mfg_Code_AddImf.TabIndex = 28;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(300, 97);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 13);
            this.label14.TabIndex = 27;
            this.label14.Text = "Mfg Name";
            // 
            // eb_Mfg_Name_AddImf
            // 
            this.eb_Mfg_Name_AddImf.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Mfg_Name", true));
            this.eb_Mfg_Name_AddImf.Location = new System.Drawing.Point(410, 93);
            this.eb_Mfg_Name_AddImf.Name = "eb_Mfg_Name_AddImf";
            this.eb_Mfg_Name_AddImf.Size = new System.Drawing.Size(169, 20);
            this.eb_Mfg_Name_AddImf.TabIndex = 26;
            this.eb_Mfg_Name_AddImf.DoubleClick += new System.EventHandler(this.eb_Mfg_Name_AddImf_DoubleClick);
            this.eb_Mfg_Name_AddImf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.eb_Mfg_Name_AddImf_KeyDown);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(401, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 13);
            this.label13.TabIndex = 25;
            this.label13.Text = "Material Code";
            // 
            // eb_Mat_Code_AddImf
            // 
            this.eb_Mat_Code_AddImf.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Mat", true));
            this.eb_Mat_Code_AddImf.Location = new System.Drawing.Point(479, 24);
            this.eb_Mat_Code_AddImf.Name = "eb_Mat_Code_AddImf";
            this.eb_Mat_Code_AddImf.Size = new System.Drawing.Size(100, 20);
            this.eb_Mat_Code_AddImf.TabIndex = 24;
            // 
            // nf_UOI_Conversion_AddImf
            // 
            this.nf_UOI_Conversion_AddImf.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "UOIConversion", true));
            this.nf_UOI_Conversion_AddImf.Location = new System.Drawing.Point(410, 211);
            this.nf_UOI_Conversion_AddImf.Name = "nf_UOI_Conversion_AddImf";
            this.nf_UOI_Conversion_AddImf.Size = new System.Drawing.Size(169, 20);
            this.nf_UOI_Conversion_AddImf.TabIndex = 23;
            this.nf_UOI_Conversion_AddImf.Text = "0";
            // 
            // nf_UOP_Conversion_AddImf
            // 
            this.nf_UOP_Conversion_AddImf.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "UOPConversion", true));
            this.nf_UOP_Conversion_AddImf.Location = new System.Drawing.Point(410, 187);
            this.nf_UOP_Conversion_AddImf.Name = "nf_UOP_Conversion_AddImf";
            this.nf_UOP_Conversion_AddImf.Size = new System.Drawing.Size(169, 20);
            this.nf_UOP_Conversion_AddImf.TabIndex = 22;
            this.nf_UOP_Conversion_AddImf.Text = "0";
            this.nf_UOP_Conversion_AddImf.TextChanged += new System.EventHandler(this.nf_UOP_Conversion_AddImf_TextChanged);
            this.nf_UOP_Conversion_AddImf.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nf_UOP_Conversion_AddImf_KeyPress);
            // 
            // eb_Buyer_AddImf
            // 
            this.eb_Buyer_AddImf.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Buyer", true));
            this.eb_Buyer_AddImf.Location = new System.Drawing.Point(123, 257);
            this.eb_Buyer_AddImf.Name = "eb_Buyer_AddImf";
            this.eb_Buyer_AddImf.Size = new System.Drawing.Size(100, 20);
            this.eb_Buyer_AddImf.TabIndex = 21;
            // 
            // eb_Account_No_AddImf
            // 
            this.eb_Account_No_AddImf.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Account", true));
            this.eb_Account_No_AddImf.Location = new System.Drawing.Point(123, 234);
            this.eb_Account_No_AddImf.Name = "eb_Account_No_AddImf";
            this.eb_Account_No_AddImf.Size = new System.Drawing.Size(200, 20);
            this.eb_Account_No_AddImf.TabIndex = 20;
            this.eb_Account_No_AddImf.TextChanged += new System.EventHandler(this.eb_Account_No_AddImf_TextChanged);
            this.eb_Account_No_AddImf.DoubleClick += new System.EventHandler(this.eb_Account_No_AddImf_DoubleClick);
            this.eb_Account_No_AddImf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.eb_Account_No_AddImf_KeyDown);
            this.eb_Account_No_AddImf.Leave += new System.EventHandler(this.eb_Account_No_AddImf_Leave);
            // 
            // cmb_Po_Class_AddImf
            // 
            this.cmb_Po_Class_AddImf.FormattingEnabled = true;
            this.cmb_Po_Class_AddImf.Location = new System.Drawing.Point(123, 141);
            this.cmb_Po_Class_AddImf.Name = "cmb_Po_Class_AddImf";
            this.cmb_Po_Class_AddImf.Size = new System.Drawing.Size(170, 21);
            this.cmb_Po_Class_AddImf.TabIndex = 19;
            // 
            // cmb_Product_Class_AddImf
            // 
            this.cmb_Product_Class_AddImf.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingSource1, "ProductClass", true));
            this.cmb_Product_Class_AddImf.FormattingEnabled = true;
            this.cmb_Product_Class_AddImf.Location = new System.Drawing.Point(123, 117);
            this.cmb_Product_Class_AddImf.Name = "cmb_Product_Class_AddImf";
            this.cmb_Product_Class_AddImf.Size = new System.Drawing.Size(170, 21);
            this.cmb_Product_Class_AddImf.TabIndex = 18;
            // 
            // cmb_Sub_Class_AddImf
            // 
            this.cmb_Sub_Class_AddImf.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingSource1, "SubClass", true));
            this.cmb_Sub_Class_AddImf.FormattingEnabled = true;
            this.cmb_Sub_Class_AddImf.Location = new System.Drawing.Point(123, 93);
            this.cmb_Sub_Class_AddImf.Name = "cmb_Sub_Class_AddImf";
            this.cmb_Sub_Class_AddImf.Size = new System.Drawing.Size(170, 21);
            this.cmb_Sub_Class_AddImf.TabIndex = 17;
            // 
            // eb_Location_AddImf
            // 
            this.eb_Location_AddImf.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Loc", true));
            this.eb_Location_AddImf.Location = new System.Drawing.Point(123, 164);
            this.eb_Location_AddImf.Name = "eb_Location_AddImf";
            this.eb_Location_AddImf.Size = new System.Drawing.Size(170, 20);
            this.eb_Location_AddImf.TabIndex = 16;
            this.eb_Location_AddImf.TextChanged += new System.EventHandler(this.eb_Location_AddImf_TextChanged);
            this.eb_Location_AddImf.DoubleClick += new System.EventHandler(this.eb_Location_AddImf_DoubleClick);
            this.eb_Location_AddImf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.eb_Location_AddImf_KeyDown);
            this.eb_Location_AddImf.Leave += new System.EventHandler(this.eb_Location_AddImf_Leave);
            // 
            // eb_Description2_AddImf
            // 
            this.eb_Description2_AddImf.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Description2", true));
            this.eb_Description2_AddImf.Location = new System.Drawing.Point(123, 70);
            this.eb_Description2_AddImf.Name = "eb_Description2_AddImf";
            this.eb_Description2_AddImf.Size = new System.Drawing.Size(456, 20);
            this.eb_Description2_AddImf.TabIndex = 15;
            // 
            // eb_Description1_AddImf
            // 
            this.eb_Description1_AddImf.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Description1", true));
            this.eb_Description1_AddImf.Location = new System.Drawing.Point(123, 47);
            this.eb_Description1_AddImf.Name = "eb_Description1_AddImf";
            this.eb_Description1_AddImf.Size = new System.Drawing.Size(456, 20);
            this.eb_Description1_AddImf.TabIndex = 14;
            // 
            // eb_Vendor_Catalog_AddImf
            // 
            this.eb_Vendor_Catalog_AddImf.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Vendor_Catalog", true));
            this.eb_Vendor_Catalog_AddImf.Location = new System.Drawing.Point(123, 24);
            this.eb_Vendor_Catalog_AddImf.Name = "eb_Vendor_Catalog_AddImf";
            this.eb_Vendor_Catalog_AddImf.Size = new System.Drawing.Size(170, 20);
            this.eb_Vendor_Catalog_AddImf.TabIndex = 14;
            this.eb_Vendor_Catalog_AddImf.DoubleClick += new System.EventHandler(this.eb_Vendor_Catalog_AddImf_DoubleClick);
            this.eb_Vendor_Catalog_AddImf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.eb_Vendor_Catalog_AddImf_KeyDown);
            this.eb_Vendor_Catalog_AddImf.Leave += new System.EventHandler(this.eb_Vendor_Catalog_AddImf_Leave);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(159, 334);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(338, 334);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // q_Command
            // 
            this.q_Command.CommandText = "      ";
            // 
            // sqlConnection8
            // 
            this.sqlConnection8.FireInfoMessageEventOnUserErrors = false;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(EHS.Forms.Orders.Objects.IMF);
            // 
            // FrmAddImf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 366);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAddImf";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAddImf";
            this.Load += new System.EventHandler(this.FrmAddImf_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox eb_Description2_AddImf;
        private System.Windows.Forms.TextBox eb_Description1_AddImf;
        private System.Windows.Forms.TextBox eb_Vendor_Catalog_AddImf;
        private System.Windows.Forms.ComboBox cmb_Po_Class_AddImf;
        private System.Windows.Forms.ComboBox cmb_Product_Class_AddImf;
        private System.Windows.Forms.ComboBox cmb_Sub_Class_AddImf;
        private System.Windows.Forms.TextBox eb_Location_AddImf;
        private System.Windows.Forms.TextBox eb_Buyer_AddImf;
        private System.Windows.Forms.TextBox eb_Account_No_AddImf;
        private System.Windows.Forms.TextBox nf_UOI_Conversion_AddImf;
        private System.Windows.Forms.TextBox nf_UOP_Conversion_AddImf;
        private System.Windows.Forms.RadioButton rb_Stock;
        private System.Windows.Forms.RadioButton rb_NonStock;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox eb_Mat_Code_AddImf;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox eb_Mfg_Code_AddImf;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox eb_Mfg_Name_AddImf;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox eb_Mfg_Catalog_AddImf;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox eb_Sub_Account_AddImf;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox nf_Po_Cost_AddImf;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox eb_Bin;
        private System.Windows.Forms.ComboBox cbx_UOI_AddImf;
        private System.Windows.Forms.ComboBox cbx_UOP_AddImf;
        private System.Windows.Forms.TextBox eb_Reorder_Location;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Data.SqlClient.SqlCommand q_Command;
        private System.Data.SqlClient.SqlConnection sqlConnection8;
        private System.Windows.Forms.ComboBox cmb_Vendor;
        private System.Windows.Forms.BindingSource bindingSource1;

    }
}