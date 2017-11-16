using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using Ehs.Forms;

namespace Ehs.Controls
{
    [DesignerCategory("Data")]
    public partial class VendorWidget : UserControl
    {
        SqlCommand _Com = new SqlCommand();
        public SqlConnection _Con;
        public Ehs.Models.Vendor Vendor = new Ehs.Models.Vendor();
        public Ehs.Models.Common.Address AddressValue
        {
            get { return addressWidget1.AddressValue; }
            set { addressWidget1.AddressValue = value; }
        }
        public Ehs.Models.Common.Numbers NumbersValues
        {
            get { return numbersWidget1.NumberValue; }
            set { numbersWidget1.NumberValue = value; }
        }

        [Category("Behavior"), Description("Controls whether the text in the edit portion can be changed or not")]
        public bool ReadOnly
        {
            get { return cmb_vendor.ReadOnly; }
            set
            {
                this.cmb_vendor.ReadOnly = value;
                this.addressWidget1.ReadOnly = value;
                this.numbersWidget1.ReadOnly = value;
                this.email.ReadOnly = value;
            }
        }

        public VendorWidget()
        {
            InitializeComponent();
            bindingSource1.DataSource = new Ehs.Models.ComboBoxVendor("Vendor", "");
            cmb_vendor.DoubleClick += new EventHandler(cmb_vendor_DoubleClick);
        }

        void Setup()
        {
            if (_Con.State == ConnectionState.Closed)
                _Con.Open();
            _Com.Connection = _Con;
        }

        void cmb_vendor_DoubleClick(object sender, EventArgs e)
        {
            List<string> STC = new List<string>();
            STC.Add("Vendor_Id");
            STC.Add("Vendor_Name");
            //STC.Add("Address1");
            STC.Add("City");
            STC.Add("State");
            STC.Add("Zip");
            STC.Add("Product");

            Setup();

            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT TOP 20 Vendor_Id, Vendor_Name, Address1, City, State, Zip, user2, product FROM Vendor WHERE ((non_material_vendor = 0) or (non_material_vendor is null)) "
            				 + "AND Vendor_Id like @Vendor_Id AND Vendor_Name like @Vendor_Name AND City like @City AND State like @State AND Zip like @Zip AND Product like @Product AND Active = 1 "
            				 + "ORDER BY Vendor_Id";
            /*_Com.CommandText = "SELECT Vendor_Id, Vendor_Name, Address1, City, State, Zip, user2, product FROM "
                                      + "Vendor   WHERE (Vendor_Id in (select vendor_id from "
                                      + "vendortoentityrestricted where entity = @entity) or vendor_id not in (select "
                                      + "distinct vendor_id from vendortoentityrestricted)) and ((non_material_vendor = 0)"
                                      + " or (non_material_vendor is null)) and Active = 1 Order By Vendor_Id";*/
            SqlParameter Vendor_Id = new SqlParameter("Vendor_Id", '%');
            _Com.Parameters.Add(Vendor_Id);
                        SqlParameter Vendor_Name = new SqlParameter("Vendor_Name", '%');
            _Com.Parameters.Add(Vendor_Name);
                        SqlParameter City = new SqlParameter("City", '%');
            _Com.Parameters.Add(City);
                        SqlParameter State = new SqlParameter("State", '%');
            _Com.Parameters.Add(State);
                        SqlParameter Zip = new SqlParameter("Zip", '%');
            _Com.Parameters.Add(Zip);
                        SqlParameter Product = new SqlParameter("Product", '%');
            _Com.Parameters.Add(Product);
            using (Ehs.Forms.Lookup Lookup = new Lookup(STC, _Com, 850))
            {
                if (Lookup.ShowDialog() == DialogResult.OK)
                    Vendor.VendorID = Lookup.FieldByName("Vendor_Id");
            }
            GetVendor();
        }

        public void GetVendor()
        {
            Setup();
            _Com.Parameters.Clear();
            Vendor = Ehs.Models.Util.getVendor(_Com, Vendor.VendorID);

            bindingSource1.DataSource = Vendor;

            AddressValue = new Ehs.Models.Common.Address(Vendor.Attention, Vendor.Address1, Vendor.Address2,
                                Vendor.Address3, Vendor.City, Vendor.State, Vendor.Zip);
            NumbersValues = new Ehs.Models.Common.Numbers(Vendor.PhoneNo, Vendor.Fax);

            email.DataBindings.Clear();
            email.DataBindings.Add("text", bindingSource1, "vendorEmail");
        }

        public void Clear()
        {
            Vendor = new Ehs.Models.Vendor();
            Vendor.VendorID = "Vendor";
            Vendor.VendorName = "";
            Refresh();
        }

        public void Refresh()
        {
            bindingSource1.DataSource = Vendor;

            AddressValue = new Ehs.Models.Common.Address(Vendor.Attention, Vendor.Address1, Vendor.Address2,
                                Vendor.Address3, Vendor.City, Vendor.State, Vendor.Zip);
            NumbersValues = new Ehs.Models.Common.Numbers(Vendor.PhoneNo, Vendor.Fax);
        }

        private void numbersWidget1_DoubleClick(object sender, EventArgs e)
        {
            List<string> STC = new List<string>(); 
            STC.Add("Phone_No");
            STC.Add("Vendor_Id");
            STC.Add("Vendor_Name");
            STC.Add("Address1");
            STC.Add("City");
            STC.Add("State");
            STC.Add("Zip");

            Setup();

            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT Phone_No, Vendor_Id, Vendor_Name, Address1, City, State, Zip FROM Vendor with "
                                      + " (nolock) WHERE Phone_No <> '' AND Phone_no is not null Order By Phone_No";
            using (Ehs.Forms.Lookup Lookup = new Lookup(STC, _Com, 850))
            {
                Lookup.Width = 820;
                if (Lookup.ShowDialog() == DialogResult.OK)
                    Vendor.VendorID = Lookup.FieldByName("Vendor_Id");
            }
            GetVendor();
        }

        private void addressWidget1_Validated(object sender, EventArgs e)
        {
            Vendor.Attention = addressWidget1.Attention;
            Vendor.Address1 = addressWidget1.Address1;
            Vendor.Address2 = addressWidget1.Address2;
            Vendor.Address3 = addressWidget1.Address3;
            Vendor.City = addressWidget1.City;
            Vendor.State = addressWidget1.State;
            Vendor.Zip = addressWidget1.Zip;
        }

        private void numbersWidget1_Validated(object sender, EventArgs e)
        {
            Vendor.PhoneNo = numbersWidget1.PhoneNumber;
            Vendor.Fax = numbersWidget1.FaxNumber;
        }


    }
}
