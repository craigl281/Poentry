using Ehs.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoEntry
{
    public partial class FrmVendor : Form
    {
        SqlCommand _Com;
        public Data data;
        public Form1 mainfrm;
        public Ehs.Models.Vendor Vendor { get => (Ehs.Models.Vendor)bindingSource1.Current; }
        public string CurrTermsCode
        {
            get 
            {
                return (cmb_Terms_Code.CurrentItem == null) ? null : cmb_Terms_Code.CurrentItem.Key;
            }
            set
            {
                cmb_Terms_Code.CurrentItem = (cmb_Terms_Code.Items.Exists(r => r.Key == value)) ? cmb_Terms_Code.Items.Find(r => r.Key == value) :
                                             (cmb_Terms_Code.Items.Count > 0) ? cmb_Terms_Code.Items[0] : null;
            }
        }
        bool HeaderVMemoColor
        {
            set
            {
                if (value)
                {
                    eb_V_Memo.Text.ToUpper();
                    eb_V_Memo.Font = new Font(Font, FontStyle.Bold);
                    eb_V_Memo.ForeColor = Color.Red;
                    eb_V_Memo.Visible = true;
                }
                else
                {
                    eb_V_Memo.Text.ToLower();
                    eb_V_Memo.Font = new Font(Font, FontStyle.Regular);
                    eb_V_Memo.ForeColor = SystemColors.ControlText;
                    eb_V_Memo.Visible = false;
                }
            }
        }
        public bool ReadOnly, Updated;

        public FrmVendor()
        {
            InitializeComponent();
        }

        private void FrmVendor_Shown(object sender, EventArgs e)
        {
            data.Open();
            cmb_Terms_Code.Items = EHS.Orders.prefillcombo(data._Com, "Terms", "");
            data.Close();
            CurrTermsCode = mainfrm.Header.TermsCode;
        }

        public void GetVendor()
        {
            if (mainfrm == null)
                return;
            if (mainfrm.viewMode1.Mode == ViewingMode.Adding)
            {
                if (mainfrm.CurVendor == null)
                {
                    mainfrm.Header.VendorID = "";
                    mainfrm.Header.VendorName = "";
                    txt_vendor_address1.Text = "";
                    txt_vendor_address2.Text = "";
                    txt_vendor_address3.Text = "";
                    txt_vendor_city.Text = "";
                    txt_vendor_State.Text = "";
                    txt_vendor_zip.Text = "";
                    txt_vendor_email.Text = "";
                    txt_vender_phone.Text = "";
                    txt_vendor_fax.Text = "";
                    CurrTermsCode = "";
                    mainfrm.Header.TermsCode = "";
                    eb_Vendor_Account.Text = "";
                    eb_FOB.Text = "";
                    HeaderVMemoColor = false;
                    eb_VMShip_Account.Text = "";
                    return;
                }
                else
                {
                    data.Open();
                    bindingSource1.DataSource = Ehs.Models.Util.getVendor(data._Com, cmb_vendor.CurrentItem.Key);
                    data.Close();
                }

            }
            else
            {
                bindingSource1.DataSource = new Ehs.Models.Vendor(mainfrm.Header.VendorID, mainfrm.Header.VendorName, mainfrm.Header.VendorAddress1, mainfrm.Header.VendorAddress2,
                                                mainfrm.Header.VendorAddress3, mainfrm.Header.VendorCity, mainfrm.Header.VendorState, mainfrm.Header.VendorZip,
                                                mainfrm.Header.VendorPhone, mainfrm.Header.VendorFax, mainfrm.Header.VendorAccount, "", "", mainfrm.Header.FOB, "", "", "", "",
                                                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 0m, mainfrm.Header.TermsCode, "", "", false, "", "",
                                                false, false, false, 0m, "", "", new DateTime(), "", "", "", "", "", "", "", "", "", "", 0m, false, "", new DateTime(), 
                                                new DateTime(), "", false, false, false, false, new DateTime(), 0m, false, "", false, "", "", false, false, "", 0m, false,
                                                "", false, false, "", false, "");
            }

            CurrTermsCode = ((Ehs.Models.Vendor)bindingSource1.DataSource).TermsCode;
            HeaderVMemoColor = ((Ehs.Models.Vendor)bindingSource1.DataSource).Memo.Length > 0;
            eb_VMShip_Account.Text = data.GetVendorAccount(mainfrm.Header.VendorID, mainfrm.Header.ShipTo);
            if (eb_VMShip_Account.Text.Length > 0)
            {
                lbl_Ship_Account.Visible = true;
                eb_VMShip_Account.Visible = true;
            }
            else
            {
                lbl_Ship_Account.Visible = false;
                eb_VMShip_Account.Visible = false;
            }
        }

        private void cmb_vendor_Validated(object sender, EventArgs e)
        {
            mainfrm.Header.VendorID = cmb_vendor.CurrentItem.Key;
            mainfrm.Header.VendorName = cmb_vendor.CurrentItem.ValueString;
            GetVendor();
            cmb_Terms_Code.Focus();
        }

        private void cmb_Terms_Code_Validating(object sender, CancelEventArgs e)
        {
            if (mainfrm.CurVendor.ToUpper() != data.SystemOptionsDictionary["GENERAL_VENDOR"].ToNonNullString().ToUpper())
            {
                if (CurrTermsCode == "<NONE>" || CurrTermsCode == "")
                {
                    errorProvider1.SetError(cmb_Terms_Code, "You must choose a Terms Code");
                    e.Cancel = true;
                }
            }
            errorProvider1.Clear();
        }

        private void FrmVendor_Load(object sender, EventArgs e)
        {
            if (mainfrm.viewMode1.Mode == ViewingMode.Editing && ReadOnly)
                cmb_vendor.ReadOnly = true;
            else
                Pnl_Vendor.ReadOnly = ReadOnly;
            if (ReadOnly == false)
                eb_Vendor_Account.ReadOnly = !data.SystemOptionsDictionary["CAN_EDIT_VENDOR_ACCOUNT"].ToBoolean();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (data.SystemOptionsDictionary["ALLOW_UPDATE_VENDOR_ADDRESS_POENTRY"].ToBoolean())
            {
                if (UpdateCheck())
                {
                    data.Open();
                    data._Com.Parameters.Clear();
                    data._Com.CommandText = "UPDATE Vendor SET Address1 = @Add1, Address2 = @Add2, Address3 = @Add3, City = @City, State = @State, Zip = @Zip WHERE vendor_Id = @id AND active = 1";
                    data._Com.Parameters.AddWithValue("id", Vendor.VendorID);
                    data._Com.Parameters.AddWithValue("Add1", Vendor.Address1);
                    data._Com.Parameters.AddWithValue("Add2", Vendor.Address2);
                    data._Com.Parameters.AddWithValue("Add3", Vendor.Address3);
                    data._Com.Parameters.AddWithValue("City", Vendor.City);
                    data._Com.Parameters.AddWithValue("State", Vendor.State);
                    data._Com.Parameters.AddWithValue("Zip", Vendor.Zip);
                    data._Com.ExecuteNonQuery();
                    data.Close();
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        bool UpdateCheck()
        {
            if (Updated)
            {
                if (MessageBox.Show("The Vendor's Address has been changed. Would you like to update the Vendor Master with this change?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    return true;
                }
            }
            return false;
        }
        private void txt_vendor_address1_Validated(object sender, EventArgs e)
        {
            Updated = true;
        }
        private void txt_vendor_address2_Validated(object sender, EventArgs e)
        {
            Updated = true;
        }

        private void txt_vendor_address3_Validated(object sender, EventArgs e)
        {
            Updated = true;
        }

        private void txt_vendor_city_Validated(object sender, EventArgs e)
        {
            Updated = true;
        }

        private void txt_vendor_State_Validated(object sender, EventArgs e)
        {
            Updated = true;
        }

        private void cmb_Terms_Code_Validated(object sender, EventArgs e)
        {
            button1.Focus();
        }

        private void txt_vendor_zip_Validated(object sender, EventArgs e)
        {
            Updated = true;
        }


    }
}
