using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ehs.Controls;
using Ehs.Models;

namespace PoEntry
{
    public partial class ItemSelection : Form
    {
        Form1 orig;

        public string CurMat
        {
            get => cmb_Mat.CurrentItem.Key;
            set => cmb_Mat.CurrentItem = (cmb_Mat.Items.Exists(r => r.Key == value)) ? cmb_Mat.Items.Find(r => r.Key == value) : null;
        }
        public string CurVendorCat
        {
            get => cmb_VendorCatalog.CurrentItem.Key;
            set => cmb_VendorCatalog.CurrentItem = (cmb_VendorCatalog.Items.Exists(r => r.Key == value)) ? cmb_VendorCatalog.Items.Find(r => r.Key == value) : null;
        }
        public string CurMfgCat
        {
            get => cmb_MfgCatalog.CurrentItem.Key;
            set => cmb_MfgCatalog.CurrentItem = (cmb_MfgCatalog.Items.Exists(r => r.Key == value)) ? cmb_MfgCatalog.Items.Find(r => r.Key == value) : null;
        }
        public string CurMfg
        {
            get => (cmb_Mfg_Name.CurrentItem == null) ? null : cmb_Mfg_Name.CurrentItem.Key;
            set => cmb_Mfg_Name.CurrentItem = (cmb_Mfg_Name.Items.Exists(r => r.Key == value)) ? cmb_Mfg_Name.Items.Find(r => r.Key == value) : null;
        }


        public ItemSelection(Form1 frm)
        {
            InitializeComponent();
            orig = frm;
            addItemFromOtherVendorToolStripMenuItem.Enabled = orig.data.SystemOptionsDictionary["ENABLE_ADD_ITEMVEND"].ToBoolean();
            cmb_Mat.Items = orig.data.prefillCombos("Mat", orig.Header.VendorID);
            if (cmb_Mat.Items.Count < 1)
            {
                cmb_Mat.AllowTypedIn = true;
                var mat = "No File Items Available for this Entity/Vendor, must select Nonfile";
                cmb_Mat.Items.Add(new ComboBoxString(mat));
                CurMat = mat;
                toolsToolStripMenuItem.ForeColor = Color.Red;
            }
            cmb_VendorCatalog.Items = orig.data.prefillCombos("VendorCat", orig.Header.VendorID);
            cmb_MfgCatalog.Items = orig.data.prefillCombos("MfgCat", orig.Header.VendorID);
            cmb_Mfg_Name.Items = orig.data.prefillCombos("Mfg_Name", "");

            cmb_Mfg_Name.AllowTypedIn = orig.data.SystemOptionsDictionary["POENTRY_CAN_FREEFORM_MFG"].ToBoolean();//Allows Freeform
        }

        private void ItemSelection_Load(object sender, EventArgs e)
        {
            eb_Description.Visible = false;
        }

        #region Validating 
        private void eb_Description_Validating(object sender, CancelEventArgs e)
        {
            if (eb_Description.Text.Trim() == "")
            {
                errorProvider1.SetError(eb_Description, "You must enter a Description");
                e.Cancel = true;
                return;
            }
            errorProvider1.Clear();
        }
        private void cmb_VendorCatalog_Validating(object sender, CancelEventArgs e)
        {
            if (cmb_VendorCatalog.AllowTypedIn && cmb_VendorCatalog.CurrentItem == null)
            {
                cmb_VendorCatalog.CurrentItem = new ComboBoxString(cmb_VendorCatalog.Text);
            }
            if (orig.Detail.NonFile && orig.viewMode1.Mode == ViewingMode.Adding)
            {
                var temp = orig.data.MatchNonFile(orig.Detail.VendorID, "Vendor_Catalog", CurVendorCat);//data.MatchNonFile(orig.Detail.VendorID, CurVendorCat);
                if (temp == null || temp.Trim().Length == 0)
                    return;
                if (MessageBox.Show("This Item exits for this vendor with this Vendor Catalog, the current Material code is " + temp + " Would you like to use this Material Code instead?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CurMat = temp;
                    cmb_Mat.Focus();
                }
            }
        }
        private void cmb_MfgCatalog_Validating(object sender, CancelEventArgs e)
        {
            if (cmb_MfgCatalog.AllowTypedIn && cmb_MfgCatalog.CurrentItem == null)
            {
                cmb_MfgCatalog.CurrentItem = new ComboBoxString(cmb_MfgCatalog.Text);
            }
            if (orig.Detail.NonFile && orig.viewMode1.Mode == ViewingMode.Adding)
            {
                var temp = orig.data.MatchNonFile(orig.Detail.VendorID, "Mfg_Catalog", CurMfgCat);//data.MatchNonFile(orig.Detail.VendorID, CurVendorCat);
                if (temp == null || temp.Trim().Length == 0)
                    return;
                if (MessageBox.Show("This Item exits for this vendor with this Manufacturer Catalog, the current Material code is " + temp + " Would you like to use this Material Code instead?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CurMat = temp;
                    cmb_Mat.Focus();
                }
            }
        }
        private void cmb_Mfg_Name_Validating(object sender, CancelEventArgs e)
        {
            if (orig.Detail.NonFile == false && orig.data.SystemOptionsDictionary["MUST_ENTER_MFG_NAME"].ToBoolean() && CurMfg == null)
            {
                errorProvider1.SetError(cmb_Mfg_Name, "MFG name can't be blank");
                e.Cancel = true;
                return;
            }

            if (cmb_Mfg_Name.FreeFormText != null)
            {
                DialogResult dialogResult = MessageBox.Show("The Manufacturer Does Not Exist in the Manufacturer Master \nYes:     Add this Manufacturer to the Master File\nNo:      To Enter another Manufacturer\nCancel: Do not enter into Mfg Table but keep on PO", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {/*
                    bool found = true;
                    while (found)
                    {
                        this.sys.Inc("NEXT_MFG_ID");
                        this.q_Command.Parameters.Clear();
                        this.q_Command.CommandText = "SELECT Mfg-Id FROM Manufacturer WHERE Mfg_Id = @Mfg_Id ";
                        this.q_Command.Parameters.Add("Mfg_Id", SqlDbType.VarChar).Value = this.sys.SysFileValue;
                        using (SqlDataReader r = this.q_Command.ExecuteReader())
                        {
                            r.Read();
                            if (!r.HasRows)
                            { found = false; }
                        }
                    }
                    this.q_Command.Parameters.Clear();
                    this.q_Command.CommandText = "INSERT INTO Manufacturer (Mfg_Id, Mfg_Name) VALUES (@Mfg_Id, @Mfg_Name) ";
                    this.q_Command.Parameters.Add("Mfg_Id", SqlDbType.VarChar).Value = this.sys.SysFileValue;
                    this.q_Command.Parameters.Add("Mfg_Name", SqlDbType.VarChar).Value = this.eb_Mfg_Name.Text;
                    this.q_Command.ExecuteNonQuery();*/
                }
                if (dialogResult == DialogResult.No)
                {
                    errorProvider1.SetError(cmb_Mfg_Name, "The Manufacturer Does Not Exist in the Manufacturer Master");
                    e.Cancel = true;
                }
            }

            errorProvider1.Clear();
        }

        #endregion

        #region Validated
        private void cmb_Mat_Validated(object sender, EventArgs e)
        {
            if (cmb_Mat.Text == "")
                return;
            orig.CurMat = CurMat;
            if (orig.Detail.NonFile)// || orig.AddItemFromVendor)
                return;
            orig.GetMatDetails("");

            if (orig._IMF.UseContract.Trim().Length > 0)
            {
                CurVendorCat = orig.CD.Vendor_Catalog;
                CurMfgCat = orig.CD.MFG_Catalog;
                cmb_Mfg_Name.ReadOnly = true;
                CurMfg = orig.CD.Mfg_Name;
            }
            else
            {
                CurVendorCat = orig._IMF.Vendor_Catalog;
                CurMfgCat = orig._IMF.Mfg_Catalog;
                cmb_Mfg_Name.ReadOnly = false;
            }
            CurMfg = orig._IMF.Mfg_Name;
            eb_Description.Text = orig._IMF.Description1;
            button1.Focus();
            if (orig.cb_Substitute_Item.Checked || orig.Detail.NonFile)
                return;

        }

        private void cmb_VendorCatalog_Validated(object sender, EventArgs e)
        {

        }

        private void cmb_MfgCatalog_Validated(object sender, EventArgs e)
        {

        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        private void cmb_Mat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("TAB");
                this.DialogResult = DialogResult.OK;
            }
        }
        private void ItemSelection_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                orig.gettingitem = true;
                var tempreturn = this.DialogResult;
                if (tempreturn == null)
                {
                    orig.gettingitem = false;
                    this.DialogResult = DialogResult.Abort;
                }
                if (tempreturn == DialogResult.OK)
                {
                    if (orig.Detail.NonFile || orig.AddItemFromVendor)
                        orig.Detail.MatCode = CurMat;
                    else
                        orig.CurMat = CurMat;
                    orig.Detail.VendorCatalog = CurVendorCat;
                    orig.Detail.MFGCatalog = CurMfgCat ?? "";
                    orig.Detail.MFGName = CurMfg ?? "";
                }
            }
            catch
            {
                orig.gettingitem = false;
                this.DialogResult = DialogResult.Abort;
            }
        }
        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eb_Description.Visible = true;
            var mat = orig.data.SystemOptionsDictionary["NONFILE_PREFIX"] + orig.data.IncSys("NEXT_NONFILE_NUMBER").ToString();
            cmb_Mat.Items.Add(new ComboBoxString(mat));
            CurMat = mat;
            orig.Detail.NonFile = true;
            eb_Description.Text = "";
            cmb_Mfg_Name.ReadOnly = false;
            eb_Description.ReadOnly = false;
            if (orig.data.SystemOptionsDictionary["USE_LAST_MFG_NAME"].ToBoolean() && CurMfg.Trim() == "")
            {
                if (orig.bs2.Count > 1)
                {
                    CurMfg = ((PoDetail)orig.bs2[orig.bs2.Position - 1]).MFGName;
                }
            }
            cmb_VendorCatalog.AllowTypedIn = true;
            cmb_MfgCatalog.AllowTypedIn = true;


            //cmb_MfgCatalog.EditMode = true;
            cmb_MfgCatalog.KeyLabel = "ID";
            cmb_MfgCatalog.ValueLabel = "Name";

            //cmb_VendorCatalog.EditMode = true;
            cmb_VendorCatalog.KeyLabel = "Id";
            cmb_VendorCatalog.ValueLabel = "Name";


            SendKeys.Send("{TAB}");
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void addItemFromOtherVendorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmb_Mat.Items = orig.data.prefillCombos("MatNotVendor", orig.Header.VendorID);
            cmb_VendorCatalog.Items = orig.data.prefillCombos("VendorCatNotVendor", orig.Header.VendorID);
            cmb_MfgCatalog.Items = orig.data.prefillCombos("MfgCatNotVendor", orig.Header.VendorID);
            CurMat = null;
            orig.AddItemFromVendor = true;
            cmb_VendorCatalog.AllowTypedIn = true;
            cmb_MfgCatalog.AllowTypedIn = true;
        }

        private void cmb_Mat_Validating(object sender, CancelEventArgs e)
        {
            if (orig.Detail.NonFile)
                return;
            if (orig.data.SystemOptionsDictionary["POENTRY_NOT_ORDERED_MONTHS"].ToInt32() > 0)
            {
                if (orig.data.OrderDays(CurMat, orig.Header.VendorID))
                {
                    if (MessageBox.Show("This item hasn't been ordered in the past\n" + orig.data.SystemOptionsDictionary["POENTRY_NOT_ORDERED_MONTHS"] + " month.\nDo you want to continue?", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                        e.Cancel = true;
                }
            }
        }

    }
}
