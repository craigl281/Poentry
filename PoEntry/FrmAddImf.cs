using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Ehs.Lookup;
using Ehs.Util;
using EHS.Forms.Orders;
using Ehs.Models;

namespace Purchase_Order_Entry
{
    public partial class FrmAddImf : Form
    {
        public EhsSysFile sys;
        bool Changing, AutoStockMatCode, AutoNonStockMatCode, rbcheck;
        string LocType, CurrentUOP, CurrentUOI, LocEntity, SQLusername, temp_prod, temp_sub, temp_po;
        //string Unit_Issue, LUOI;
        int PoClassFieldSize, SubClassFieldSize, Product_ClassFieldSize, UOISize, UOPSize;
        SqlTransaction transaction;
        /*
        private Objects.IMF _ImfData;

        public Objects.IMF ImfData
        {
            get { return _ImfData; }
            set { _ImfData = value; }
        }
        */

        public Objects.IMF ImfData = new Objects.IMF();

        public FrmAddImf()
        {
            InitializeComponent();


            SQLusername = variables.SQLusername;
            EhsLogin login9 = new EhsLogin(this.sqlConnection8);
            login9.Login();
            q_Command.Connection = sqlConnection8;
            sys = new EhsSysFile(sqlConnection8);
            rbcheck = false;

            eb_Mfg_Catalog_AddImf.Text = variables.MFG_Cat;
            eb_Account_No_AddImf.Text = variables.Account_No;
            CurrentUOP = variables.CurrentUOPPrime;
            try { if (CurrentUOP.Trim() == "") { CurrentUOP = ""; } }
            catch { CurrentUOP = ""; }
            rb_Stock.Checked = variables.stock;
            /* if (rb_Stock.Checked) { stock_click(); }
             else { nonStockClick(); }
             */
        }
        private void FrmAddImf_Load(object sender, EventArgs e)
        {
            cmb_Vendor.Text = ImfData.VendorId + new string(' ', 5) + ImfData.VendorName;

            Changing = false;
            q_Command.Parameters.Clear();
            cmb_Sub_Class_AddImf.DataSource = Util.getSubAccountList(q_Command);
            cmb_Po_Class_AddImf.DataSource = Util.getComboBoxPoClass(q_Command);
            cmb_Product_Class_AddImf.DataSource = Util.getComboBoxProductClassList(q_Command);
            cbx_UOP_AddImf.DataSource = Util.getComboBoxUOMList(q_Command);
            //FillUOP(CurrentUOP);  //////////////////from form 1
            FillUOI("");

            rb_NonStock.Checked = true;
            AutoNonStockMatCode = false;
            AutoStockMatCode = false;

            sys.Read("USE_AUTO_MAT_CODE");
            if (sys.SysFileValue.ToUpper() == "Y")
            {
                AutoStockMatCode = true;
                AutoNonStockMatCode = true;
            }
            else if (sys.SysFileValue.ToUpper() == "S") { AutoStockMatCode = true; }
            else if (sys.SysFileValue.ToUpper() == "A") { AutoNonStockMatCode = true; }
            LocEntity = " ";
        }
        private void rb_Stock_Click(object sender, EventArgs e)
        {
            stock_click();
        }

        public void stock_click()
        {
            rbcheck = true;
            if ((rb_Stock.Checked == true) && (AutoStockMatCode == false))
            { eb_Mat_Code_AddImf.Enabled = true; }
            else { eb_Mat_Code_AddImf.Enabled = false; }
            eb_Location_AddImf.Text = variables.Default_Location;
            eb_Sub_Account_AddImf.Enabled = true;
            cbx_UOI_AddImf.Enabled = true;
            nf_UOI_Conversion_AddImf.Enabled = true;
            rbcheck = false;
        }

        private void rb_NonStock_Click(object sender, EventArgs e)
        {
            nonStockClick();
        }

        public void nonStockClick()
        {
            rbcheck = true;
            if ((rb_NonStock.Checked == true) && (AutoNonStockMatCode == false))
            { eb_Mat_Code_AddImf.Enabled = true; }
            else { eb_Mat_Code_AddImf.Enabled = false; }
            eb_Location_AddImf.Text = variables.Default_NonStock_Location;
            cbx_UOI_AddImf.Enabled = false;
            nf_UOI_Conversion_AddImf.Enabled = false;
            FillUOI(CurrentUOP);
            nf_UOI_Conversion_AddImf.Text = nf_UOP_Conversion_AddImf.Text;
            rbcheck = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool MatCodeFound;

            if (ValidateRecord())
            {
                if (rb_Stock.Checked)
                {
                    if (AutoStockMatCode)
                    {
                        try
                        {
                            if (sys.Inc("NEXT_MAT_CODE") == false)
                            {
                                MessageBox.Show("Can't increment Next Mat Code in Sysfile", "error", MessageBoxButtons.OK);
                                return;
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Can't increment Next Mat Code in Sysfile", "error", MessageBoxButtons.OK);
                            return;
                        }
                        MatCodeFound = true;
                        while (MatCodeFound)
                        {
                            q_Command.Parameters.Clear();
                            q_Command.CommandText = "SELECT Mat_Code FROM Imf WHERE Mat_Code = @Mat_Code";
                            q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = variables.Mat_Code_Prefix + sys.SysFileValue;
                            using (SqlDataReader read11 = q_Command.ExecuteReader())
                            {
                                read11.Read();
                                if (read11.HasRows) { sys.Inc("NEXT_MAT_CODE"); }
                                else
                                {
                                    MatCodeFound = false;
                                    eb_Mat_Code_AddImf.Text = variables.Mat_Code_Prefix + sys.SysFileValue;
                                }
                            }
                        }
                    }
                }
                if (rb_NonStock.Checked)
                {
                    if (AutoNonStockMatCode)
                    {
                        try
                        {
                            if (sys.Inc("NEXT_NON_STOCK_MAT_CODE") == false)
                            {
                                MessageBox.Show("Can't increment Next Mat Code in Sysfile", "error", MessageBoxButtons.OK);
                                return;
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Can't increment Next Mat Code in Sysfile", "error", MessageBoxButtons.OK);
                            return;
                        }
                        MatCodeFound = true;
                        while (MatCodeFound)
                        {
                            q_Command.Parameters.Clear();
                            q_Command.CommandText = "SELECT Mat_Code FROM Imf WHERE Mat_Code = @Mat_Code";
                            q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = variables.NMat_Code_Prefix + sys.SysFileValue;
                            using (SqlDataReader read12 = q_Command.ExecuteReader())
                            {
                                read12.Read();
                                if (read12.HasRows) { sys.Inc("NEXT_NON_STOCK_MAT_CODE"); }
                                else
                                {
                                    MatCodeFound = false;
                                    eb_Mat_Code_AddImf.Text = variables.NMat_Code_Prefix + sys.SysFileValue;
                                }
                            }
                        }
                    }
                }
                if (rb_NonStock.Checked) { variables.add_items = false; }
                else { variables.add_items = true; }
                if (SaveFiles())
                {
                    variables.result = true;
                    variables.Mat_Code = eb_Mat_Code_AddImf.Text;
                    this.Close();
                }
                else
                {

                }
            }
        }

        private void GetSubAccount()
        {
            q_Command.Parameters.Clear();
            q_Command.CommandText = "SELECT Sub_Account FROM AccountNo ";
            q_Command.CommandText += "WHERE Account_No = @Account_No AND Entity = @Entity";
            q_Command.Parameters.Add("Account_No", SqlDbType.VarChar).Value = eb_Account_No_AddImf.Text;
            q_Command.Parameters.Add("Entity", SqlDbType.VarChar).Value = LocEntity;
            using (SqlDataReader readSub = q_Command.ExecuteReader())
            {
                readSub.Read();
                if (readSub.HasRows) { eb_Sub_Account_AddImf.Text = readSub["Sub_Account"].ToString(); }
                else { eb_Sub_Account_AddImf.Text = ""; }
            }
        }

        private void FillUOI(string PassUnit)
        {
            int i = 0;
            cbx_UOI_AddImf.Items.Clear();
            q_Command.Parameters.Clear();
            q_Command.CommandText = "SELECT * FROM UnitOfMeasure WHERE Active = 1 ORDER BY UOM ";
            UOISize = 7;
            using (SqlDataReader q_Query = q_Command.ExecuteReader())
            {
                while (q_Query.Read())
                { cbx_UOI_AddImf.Items.Add(((string)q_Query["UOM"]).PadRight(UOISize, ' ') + (string)q_Query["Description"]); }
                do
                {
                    i++;
                } while ((i < cbx_UOI_AddImf.Items.Count) && (cbx_UOI_AddImf.Items[i].ToString().Substring(0, UOISize).Trim() != PassUnit.Trim()));
                if (i >= cbx_UOI_AddImf.Items.Count)
                {
                    cbx_UOI_AddImf.SelectedIndex = 0;
                    CurrentUOP = "";
                }
                else
                {
                    cbx_UOI_AddImf.SelectedIndex = i;
                    CurrentUOI = cbx_UOI_AddImf.Items[i].ToString().Substring(0, UOISize).Trim();
                }
            }
        }

        public bool ValidateRecord()
        {
            bool found1, blah = false;

            if (eb_Vendor_Catalog_AddImf.Text.Trim() == "")
            {
                MessageBox.Show("You must enter a Vendor Catalogue Number.", "Information", MessageBoxButtons.OK);
                if (eb_Vendor_Catalog_AddImf.Enabled) { eb_Vendor_Catalog_AddImf.Focus(); }
                return false;
            }
            q_Command.Parameters.Clear();
            q_Command.CommandText = "SELECT Mat_Code FROM ItemVend   WHERE Vendor_Id = @Vendor_Id AND Vendor_Catalog = @Vendor_Catalog ";
            //q_Command.Parameters.Add("Vendor_Id", SqlDbType.VarChar).Value = eb_Vendor_Id_AddImf.Text;
            q_Command.Parameters.Add("Vendor_Catalog", SqlDbType.VarChar).Value = eb_Vendor_Catalog_AddImf.Text;
            using (SqlDataReader read1 = q_Command.ExecuteReader())
            {
                read1.Read();
                if (read1.HasRows)
                {
                    MessageBox.Show("This Catalogue number is already on file for this vendor. "
                        + "\nIf you wish to use this number, you must make it unique."
                        + "\nEx. For catalogue number 'SA30AL', enter 'SA30AL-22'"
                        + "\nThe Material Code is " + read1["Mat_Code"].ToString(), "Warning", MessageBoxButtons.OK);
                    if (eb_Vendor_Catalog_AddImf.Enabled) { eb_Vendor_Catalog_AddImf.Focus(); }
                    return false;
                }
            }
            if (eb_Mat_Code_AddImf.Enabled)
            {
                q_Command.Parameters.Clear();
                q_Command.CommandText = "SELECT Mat_Code FROM IMF WHERE Mat_Code = @Mat_Code ";
                q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = eb_Mat_Code_AddImf.Text;
                using (SqlDataReader read2 = q_Command.ExecuteReader())
                {
                    read2.Read();
                    if (read2.HasRows)
                    {
                        MessageBox.Show("This Material Code already exists in the IMF.", "Information", MessageBoxButtons.OK);
                        eb_Mat_Code_AddImf.Focus();
                        return false;
                    }
                }
            }
            if (eb_Description1_AddImf.Text.Trim() == "")
            {
                MessageBox.Show("Description can't be blank.", "info", MessageBoxButtons.OK);
                if (eb_Description1_AddImf.Enabled) { eb_Description1_AddImf.Focus(); }
                return false;
            }
            q_Command.Parameters.Clear();
            q_Command.CommandText = "SELECT Vendor_Id FROM ItemVend";
            q_Command.CommandText += " WHERE Mat_Code = @Mat_Code AND Vendor_Id = @Vendor_Id";
            q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = eb_Mat_Code_AddImf.Text;
            //q_Command.Parameters.Add("Vendor_Id", SqlDbType.VarChar).Value = eb_Vendor_Id_AddImf.Text;
            using (SqlDataReader read3 = q_Command.ExecuteReader())
            {
                read3.Read();
                if (read3.HasRows)
                {
                    MessageBox.Show("This Vendor Already Exists in The Item Vendor Master File, for this item.", "Information", MessageBoxButtons.OK);
                    return false;
                }
            }
            if (variables.MUST_ENTER_MFG_NAME)
            {
                if (eb_Mfg_Name_AddImf.Text.Trim() == "")
                {
                    MessageBox.Show("You must enter a mfg name.", "warning", MessageBoxButtons.OK);
                    eb_Mfg_Name_AddImf.Focus();
                    return false;
                }
            }
            if (eb_Mfg_Name_AddImf.Text.Trim() != "")
            {
                q_Command.Parameters.Clear();
                q_Command.CommandText = "SELECT Mfg_Name FROM Manufacturer ";
                q_Command.CommandText += "WHERE Mfg_Name = @Mfg_Name ";
                q_Command.Parameters.Add("Mfg_Name", SqlDbType.VarChar).Value = eb_Mfg_Name_AddImf.Text;
                using (SqlDataReader read4 = q_Command.ExecuteReader())
                {
                    read4.Read();
                    if (!read4.HasRows)
                    {
                        if (variables.Can_Insert_MFG)
                        {
                            if (MessageBox.Show("The Manufacturer Does Not Exist in the Manufacturer Master File."
                                + "\nDo you want to continue?", "Information", MessageBoxButtons.YesNo) == DialogResult.No)
                                eb_Mfg_Name_AddImf.Focus();
                            return false;
                        }
                        else { blah = true; }
                    }
                }
                if (blah)
                {
                    if (MessageBox.Show("The Manufacturer Does Not Exist in the Manufacturer Master File."
    + "\nClick Yes if you would like to automatically add this Manufacturer"
    + "\nto the Master File?"
    + "\nYou can also click No and use the lookup to see if it was entered"
    + "\nwith similar spelling. ", "Information", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        found1 = true;
                        while (found1)
                        {
                            sys.Inc("NEXT_MFG_ID");
                            q_Command.Parameters.Clear();
                            q_Command.CommandText = "SELECT Mfg_Id FROM Manufacturer WHERE Mfg_Id = @Mfg_Id";
                            q_Command.Parameters.Add("Mfg_Id", SqlDbType.VarChar).Value = sys.SysFileValue;
                            using (SqlDataReader read5 = q_Command.ExecuteReader())
                            {
                                read5.Read();
                                if (!read5.HasRows) { found1 = false; }
                            }
                        }
                        q_Command.Parameters.Clear();
                        q_Command.CommandText = "INSERT INTO Manufacturer (Mfg_Id, Mfg_Name) VALUES (@Mfg_Id, @Mfg_Name)";
                        q_Command.Parameters.Add("Mfg_Id", SqlDbType.VarChar).Value = sys.SysFileValue;
                        q_Command.Parameters.Add("Mfg_Name", SqlDbType.VarChar).Value = eb_Mfg_Name_AddImf.Text;
                        q_Command.ExecuteNonQuery();
                    }
                    else { return false; }
                }
            }
            if (eb_Location_AddImf.Text.Trim() == "")
            {
                MessageBox.Show("You must choose a Location", "information", MessageBoxButtons.OK);
                if (eb_Location_AddImf.Enabled) { eb_Location_AddImf.Focus(); }
                return false;
            }
            q_Command.Parameters.Clear();
            q_Command.CommandText = "SELECT l.Location, Entity, l.Stock_Type from Location l join UserToLocation ul ";
            q_Command.CommandText += "on ul.Location = l.Location ";
            q_Command.CommandText += "where l.Location = @Location AND username = @username ";
            q_Command.Parameters.Add("Location", SqlDbType.VarChar).Value = eb_Location_AddImf.Text;
            q_Command.Parameters.Add("username", SqlDbType.VarChar).Value = SQLusername;
            using (SqlDataReader read6 = q_Command.ExecuteReader())
            {
                read6.Read();
                if (!read6.HasRows)
                {
                    MessageBox.Show("This Location does not exist or you do not have rights to this Location.", "information", MessageBoxButtons.OK);
                    if (eb_Location_AddImf.Enabled) { eb_Location_AddImf.Focus(); }
                    return false;
                }
                else
                {
                    string temp_stock_type;
                    temp_stock_type = read6["Stock_Type"].ToString();
                    if ((temp_stock_type == "S") && rb_NonStock.Checked)
                    {
                        MessageBox.Show("This Location is set to only use Stock Types", "Error", MessageBoxButtons.OK);
                        return false;
                    }
                    if ((temp_stock_type == "N") && rb_Stock.Checked)
                    {
                        MessageBox.Show("This Location is set to only use NonStock Types", "Error", MessageBoxButtons.OK);
                        return false;
                    }
                    LocEntity = read6["Entity"].ToString();
                }
            }
            q_Command.Parameters.Clear();
            q_Command.CommandText = "SELECT Mat_Code FROM Loc WHERE Mat_Code = @Mat_Code AND Location = @Location";
            q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = eb_Mat_Code_AddImf.Text;
            q_Command.Parameters.Add("Location", SqlDbType.VarChar).Value = eb_Location_AddImf.Text;
            using (SqlDataReader read7 = q_Command.ExecuteReader())
            {
                read7.Read();
                if (read7.HasRows)
                {
                    MessageBox.Show("The Location for this Material Code already exists in the Loc table.", "information", MessageBoxButtons.OK);
                    if (eb_Location_AddImf.Enabled) { eb_Location_AddImf.Focus(); }
                    return false;
                }
            }
            try
            {
                if (Convert.ToSingle(nf_UOP_Conversion_AddImf.Text) <= 0)
                {
                    MessageBox.Show("You must enter a positive value for the Unit Of Purchase Conversion", "Information", MessageBoxButtons.OK);
                    if (nf_UOP_Conversion_AddImf.Enabled) { nf_UOP_Conversion_AddImf.Focus(); }
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("You must enter a positive value for the Unit Of Purchase Conversion", "Information", MessageBoxButtons.OK);
                if (nf_UOP_Conversion_AddImf.Enabled) { nf_UOP_Conversion_AddImf.Focus(); }
                return false;
            }
            if (rb_Stock.Checked)
            {
                if ((AutoStockMatCode == false) && (eb_Mat_Code_AddImf.Text.Trim() == ""))
                {
                    MessageBox.Show("You must enter a material code", "info", MessageBoxButtons.OK);
                    if (eb_Mat_Code_AddImf.Enabled) { eb_Mat_Code_AddImf.Focus(); }
                    return false;
                }
                if (Convert.ToSingle(nf_UOI_Conversion_AddImf.Text) <= 0)
                {
                    MessageBox.Show("UoI Conversion must be positive.", "info", MessageBoxButtons.OK);
                    if (nf_UOI_Conversion_AddImf.Enabled) { nf_UOI_Conversion_AddImf.Focus(); }
                    return false;
                }
                if (eb_Account_No_AddImf.Text.Trim() == "")
                {
                    MessageBox.Show("You must enter an Account Number.", "Info", MessageBoxButtons.OK);
                    if (eb_Account_No_AddImf.Enabled) { eb_Account_No_AddImf.Focus(); }
                    return false;
                }
            }
            else
            {
                if ((AutoStockMatCode == false) && (eb_Mat_Code_AddImf.Text.Trim() == ""))
                {
                    MessageBox.Show("You must enter a Material Code.", "info", MessageBoxButtons.OK);
                    if (eb_Mat_Code_AddImf.Enabled) { eb_Mat_Code_AddImf.Focus(); }
                    return false;
                }
            }
            if (eb_Account_No_AddImf.Text.Trim() == "")
            {
                MessageBox.Show("You must enter an Account Number.", "Info", MessageBoxButtons.OK);
                if (eb_Account_No_AddImf.Enabled) { eb_Account_No_AddImf.Focus(); }
                return false;
            }
            else
            {
                q_Command.Parameters.Clear();
                q_Command.CommandText = "SELECT TOP 1 Active FROM AccountNo WHERE Account_No = Account_No AND Entity = @Entity";
                q_Command.Parameters.Add("Account_No", SqlDbType.VarChar).Value = eb_Account_No_AddImf.Text;
                q_Command.Parameters.Add("Entity", SqlDbType.VarChar).Value = LocEntity;
                using (SqlDataReader read8 = q_Command.ExecuteReader())
                {
                    read8.Read();
                    if (read8.HasRows)
                    {
                        /* if (Convert.ToBoolean(read8["Active"]) == false)
                         {
                             MessageBox.Show("This Account isn't active", "info", MessageBoxButtons.OK);
                             if (eb_Account_No_AddImf.Enabled) { eb_Account_No_AddImf.Focus(); }
                             return false;
                         }*/
                    }
                    else
                    {
                        MessageBox.Show("This Account doesn't exist", "info", MessageBoxButtons.OK);
                        if (eb_Account_No_AddImf.Enabled) { eb_Account_No_AddImf.Focus(); }
                        return false;
                    }
                }
            }
            if (eb_Sub_Account_AddImf.Text.Trim() != "")
            {
                q_Command.Parameters.Clear();
                q_Command.CommandText = "SELECT Active FROM SubAccount WHERE Sub_Account = @Sub_Account";
                q_Command.Parameters.Add("Sub_Account", SqlDbType.VarChar).Value = eb_Sub_Account_AddImf.Text;
                using (SqlDataReader read9 = q_Command.ExecuteReader())
                {
                    read9.Read();
                    if (read9.HasRows)
                    {
                        if (Convert.ToBoolean(read9["Active"]) == false)
                        {
                            MessageBox.Show("This Sub Account isn't active", "info", MessageBoxButtons.OK);
                            if (eb_Sub_Account_AddImf.Enabled) { eb_Sub_Account_AddImf.Focus(); }
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("This Sub Account doesn't exist", "info", MessageBoxButtons.OK);
                        if (eb_Sub_Account_AddImf.Enabled) { eb_Sub_Account_AddImf.Focus(); }
                        return false;
                    }
                }
            }
            else
            {
                MessageBox.Show("You must enter a Sub Account.", "Info", MessageBoxButtons.OK);
                if (eb_Sub_Account_AddImf.Enabled) { eb_Sub_Account_AddImf.Focus(); }
                return false;
            }
            if (eb_Reorder_Location.Text.Trim() != "")
            {
                q_Command.Parameters.Clear();
                q_Command.CommandText = "SELECT location FROM location WHERE location = @loc";
                q_Command.Parameters.Add("loc", SqlDbType.VarChar).Value = eb_Reorder_Location.Text;
                using (SqlDataReader read10 = q_Command.ExecuteReader())
                {
                    read10.Read();
                    if (!read10.HasRows)
                    {
                        MessageBox.Show("This Reorder Location Doesn't Exist.", "Info", MessageBoxButtons.OK);
                        if (eb_Reorder_Location.Enabled) { eb_Reorder_Location.Focus(); }
                        return false;
                    }
                }
            }
            return true;
        }

        private void nf_UOP_Conversion_AddImf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void eb_Sub_Account_AddImf_DoubleClick(object sender, EventArgs e)
        {
            Sub_AccountLookup("");
        }

        public bool SaveFiles()
        {

            /*
              If Append_Conversion = 'Y' then
                Luoi := 'EA/1'
              Else
                Luoi := 'EA';
            */
            q_Command.Parameters.Clear();
            transaction = this.sqlConnection8.BeginTransaction("Begin_Save");
            q_Command.Transaction = transaction;

            temp_prod = cmb_Product_Class_AddImf.SelectedItem.ToString().Substring(0, Product_ClassFieldSize).Trim();
            if (temp_prod == "<NONE>" || temp_prod.Trim() == "") { temp_prod = ""; }
            temp_sub = cmb_Sub_Class_AddImf.SelectedItem.ToString().Substring(0, SubClassFieldSize).Trim();
            if (temp_sub == "<NONE>" || temp_sub.Trim() == "") { temp_sub = ""; }
            temp_po = cmb_Po_Class_AddImf.SelectedItem.ToString().Substring(0, PoClassFieldSize).Trim();
            if (temp_po == "<NONE>" || temp_po.Trim() == "") { temp_po = ""; }

            try { InsertIMF(); }
            catch { RollBack("IMF"); return false; }

            try { InsertItemVend(); }
            catch { RollBack("ItemVend"); return false; }

            if (rb_Stock.Checked) { LocType = "S"; }
            else { LocType = "N"; }

            try { InsertLOC(); }
            catch { RollBack("Loc"); return false; }

            try { InsertUoi(); }
            catch { RollBack("UOI"); return false; }

            transaction.Commit();
            q_Command.Transaction = null;

            return true;
        }

        public void InsertIMF()
        {
            q_Command.Parameters.Clear();
            q_Command.CommandText = "INSERT INTO IMF";
            q_Command.CommandText += "(Mat_Code,Description1,Description2,Product_Class,Sub_Class,PO_Class,Buyer,Active) VALUES";
            q_Command.CommandText += " (@Mat_Code,@Description1,@Description2,@Product_Class,@Sub_Class,@PO_Class,@Buyer,1)";
            q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = eb_Mat_Code_AddImf.Text;
            q_Command.Parameters.Add("Description1", SqlDbType.VarChar).Value = eb_Description1_AddImf.Text;
            q_Command.Parameters.Add("Description2", SqlDbType.VarChar).Value = eb_Description2_AddImf.Text;
            if (temp_prod.Trim() == "") { q_Command.Parameters.Add("Product_Class", SqlDbType.VarChar).Value = DBNull.Value; }
            else { q_Command.Parameters.Add("Product_Class", SqlDbType.VarChar).Value = temp_prod; }
            if (temp_sub.Trim() == "") { q_Command.Parameters.Add("Sub_Class", SqlDbType.VarChar).Value = DBNull.Value; }
            else { q_Command.Parameters.Add("Sub_Class", SqlDbType.VarChar).Value = temp_sub; }
            if (temp_po.Trim() == "") { q_Command.Parameters.Add("PO_Class", SqlDbType.VarChar).Value = DBNull.Value; }
            else { q_Command.Parameters.Add("PO_Class", SqlDbType.VarChar).Value = temp_po; }
            q_Command.Parameters.Add("Buyer", SqlDbType.VarChar).Value = eb_Buyer_AddImf.Text;
            q_Command.ExecuteNonQuery();
        }

        public void RollBack(string text)
        {
            transaction.Rollback();
            MessageBox.Show("An Error Occurred which Rolled Back the Insertion of.\n" + text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            return;
        }

        public void InsertItemVend()
        {
            q_Command.Parameters.Clear();
            q_Command.CommandText = "INSERT INTO ItemVend ";
            q_Command.CommandText += "(Mat_Code,Vendor_Id,Vendor_Catalog,Mfg_Catalog,Unit_Purchase,Purchase_Cost,Mfg_Name,Mfg_Code,Conversion,Po_Cost,Main_Vendor,Active) VALUES ";
            q_Command.CommandText += "(@Mat_Code,@Vendor_Id,@Vendor_Catalog,@Mfg_Catalog,@Unit_Purchase,@Purchase_Cost,@Mfg_Name,@Mfg_Code,@Conversion,@Po_Cost,1,1)";
            q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = eb_Mat_Code_AddImf.Text;
            // q_Command.Parameters.Add("Vendor_Id", SqlDbType.VarChar).Value = eb_Vendor_Id_AddImf.Text;
            q_Command.Parameters.Add("Vendor_Catalog", SqlDbType.VarChar).Value = eb_Vendor_Catalog_AddImf.Text;
            q_Command.Parameters.Add("Mfg_Catalog", SqlDbType.VarChar).Value = eb_Mfg_Catalog_AddImf.Text;
            q_Command.Parameters.Add("Unit_Purchase", SqlDbType.VarChar).Value = CurrentUOP;
            if (nf_Po_Cost_AddImf.Text.Trim() == "") { q_Command.Parameters.Add("PO_Cost", SqlDbType.Float).Value = 0f; }
            else { q_Command.Parameters.Add("PO_Cost", SqlDbType.Float).Value = Convert.ToSingle(nf_Po_Cost_AddImf.Text); }
            q_Command.Parameters.Add("Mfg_Name", SqlDbType.VarChar).Value = eb_Mfg_Name_AddImf.Text;
            q_Command.Parameters.Add("Mfg_Code", SqlDbType.VarChar).Value = eb_Mfg_Code_AddImf.Text;
            if (nf_UOP_Conversion_AddImf.Text.Trim() == "") { q_Command.Parameters.Add("Conversion", SqlDbType.Float).Value = 0f; }
            else { q_Command.Parameters.Add("Conversion", SqlDbType.Float).Value = Convert.ToSingle(nf_UOP_Conversion_AddImf.Text); }
            q_Command.Parameters.Add("Purchase_Cost", SqlDbType.Float).Value = 0f;
            q_Command.ExecuteNonQuery();

            q_Command.Parameters.Clear();
            q_Command.CommandText = "INSERT INTO UOP ";
            q_Command.CommandText += "(Mat_Code,Vendor_Id,Unit_Purchase,Conversion,Vendor_Catalog,MFG_Name,MFG_Catalog,PO_Cost,Default_UOP,Active) VALUES ";
            q_Command.CommandText += "(@Mat_Code,@Vendor_Id,@Unit_Purchase,@Conversion,@Vendor_Catalog,@MFG_Name,@MFG_Catalog,@PO_Cost,1,1) ";
            q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = eb_Mat_Code_AddImf.Text;
            //q_Command.Parameters.Add("Vendor_Id", SqlDbType.VarChar).Value = eb_Vendor_Id_AddImf.Text;
            q_Command.Parameters.Add("Vendor_Catalog", SqlDbType.VarChar).Value = eb_Vendor_Catalog_AddImf.Text;
            q_Command.Parameters.Add("Mfg_Catalog", SqlDbType.VarChar).Value = eb_Mfg_Catalog_AddImf.Text;
            q_Command.Parameters.Add("Unit_Purchase", SqlDbType.VarChar).Value = CurrentUOP;
            if (nf_Po_Cost_AddImf.Text.Trim() == "") { q_Command.Parameters.Add("PO_Cost", SqlDbType.Float).Value = 0f; }
            else { q_Command.Parameters.Add("PO_Cost", SqlDbType.Float).Value = Convert.ToSingle(nf_Po_Cost_AddImf.Text); }
            q_Command.Parameters.Add("Mfg_Name", SqlDbType.VarChar).Value = eb_Mfg_Name_AddImf.Text;
            if (nf_UOP_Conversion_AddImf.Text.Trim() == "") { q_Command.Parameters.Add("Conversion", SqlDbType.Float).Value = 0f; }
            else { q_Command.Parameters.Add("Conversion", SqlDbType.Float).Value = Convert.ToSingle(nf_UOP_Conversion_AddImf.Text); }
            q_Command.ExecuteNonQuery();
        }

        public void InsertLOC()
        {
            q_Command.Parameters.Clear();
            q_Command.CommandText = "INSERT INTO Loc ";
            q_Command.CommandText += "(Mat_Code,Location,Type,Issue_Cost,Sub_Account,Entity,Account_No,Average_Cost,Active,reorder_location, bin) Values ";
            q_Command.CommandText += "(@Mat_Code,@Location,@Type,@Issue_Cost,@Sub_Account,@Entity,@Account_No,@Average_Cost,1,@reorder_location, @bin)";
            q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = eb_Mat_Code_AddImf.Text;
            q_Command.Parameters.Add("Location", SqlDbType.VarChar).Value = eb_Location_AddImf.Text;
            q_Command.Parameters.Add("Type", SqlDbType.VarChar).Value = LocType;
            float temp_loc_cost;
            try { temp_loc_cost = Convert.ToSingle(nf_Po_Cost_AddImf.Text) / Convert.ToSingle(nf_UOP_Conversion_AddImf.Text); }
            catch { temp_loc_cost = 0f; }
            q_Command.Parameters.Add("Issue_Cost", SqlDbType.Float).Value = temp_loc_cost;
            q_Command.Parameters.Add("Average_Cost", SqlDbType.Float).Value = temp_loc_cost;
            q_Command.Parameters.Add("Sub_Account", SqlDbType.VarChar).Value = eb_Sub_Account_AddImf.Text;
            q_Command.Parameters.Add("Entity", SqlDbType.VarChar).Value = LocEntity;
            q_Command.Parameters.Add("Account_No", SqlDbType.VarChar).Value = eb_Account_No_AddImf.Text;
            q_Command.Parameters.Add("reorder_location", SqlDbType.VarChar).Value = eb_Reorder_Location.Text;
            q_Command.Parameters.Add("bin", SqlDbType.VarChar).Value = eb_Bin.Text;
            q_Command.ExecuteNonQuery();
        }

        public void InsertUoi()
        {
            if (nf_UOI_Conversion_AddImf.Text == "1")
            {
                q_Command.Parameters.Clear();
                q_Command.CommandText = "INSERT INTO UOI ";
                q_Command.CommandText += "(Mat_Code,Location,Unit_Issue,Conversion,Is_Default,Active,LUOI,Allow_Issue) VALUES ";
                q_Command.CommandText += "(@Mat_Code,@Location,@Unit_Issue,@Conversion,1,1,1,1)";
                q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = eb_Mat_Code_AddImf.Text;
                q_Command.Parameters.Add("Location", SqlDbType.VarChar).Value = eb_Location_AddImf.Text;
                q_Command.Parameters.Add("Unit_Issue", SqlDbType.VarChar).Value = CurrentUOI;
                q_Command.Parameters.Add("Conversion", SqlDbType.Float).Value = Convert.ToSingle(nf_UOI_Conversion_AddImf.Text);
                q_Command.ExecuteNonQuery();
            }
            else
            {
                q_Command.Parameters.Clear();
                q_Command.CommandText = "INSERT INTO UOI ";
                q_Command.CommandText += "(Mat_Code,Location,Unit_Issue,Conversion,Is_Default,Active,LUOI,Allow_Issue) VALUES ";
                q_Command.CommandText += "(@Mat_Code,@Location,@Unit_Issue,@Conversion,0,1,1,@Allow_Issue)";
                q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = eb_Mat_Code_AddImf.Text;
                q_Command.Parameters.Add("Location", SqlDbType.VarChar).Value = eb_Location_AddImf.Text;
                if (CurrentUOI.ToUpper() == "EA") { q_Command.Parameters.Add("Unit_Issue", SqlDbType.VarChar).Value = "EACH"; }
                else { q_Command.Parameters.Add("Unit_Issue", SqlDbType.VarChar).Value = "EA"; }

                q_Command.Parameters.Add("Conversion", SqlDbType.Float).Value = 1f;
                if (LocType.ToUpper() == "N") { q_Command.Parameters.Add("Allow_Issue", SqlDbType.Bit).Value = false; }
                else { q_Command.Parameters.Add("Allow_Issue", SqlDbType.Bit).Value = true; }
                q_Command.ExecuteNonQuery();
            }
        }

        public void Sub_AccountLookup(string Search)
        {
            LookupForm lookup = new LookupForm(this.sqlConnection8);
            try
            {
                lookup.Width = 500;
                lookup.LookupName = "Lookup By Sub Account";
                lookup.Text = "Lookup By Sub Account";
                lookup.SearchSql.Add("SELECT * FROM SubAccount   ");
                lookup.SearchSql.Add("WHERE Sub_Account LIKE ~*~ ");
                lookup.SearchSql.Add("ORDER BY Sub_Account");
                lookup.AddColumn("Sub_Account", "Sub Account", 220);
                lookup.AddColumn("Sub_Account_Name", "Name", 260);
                lookup.SearchText = Search;
                if (lookup.ShowDialog() == DialogResult.OK) { eb_Sub_Account_AddImf.Text = lookup.FieldByName("Sub_Account"); }
            }
            finally { lookup.Dispose(); }
        }

        private void eb_Account_No_AddImf_DoubleClick(object sender, EventArgs e)
        {
            Account_NoLookup("");
        }

        public void Account_NoLookup(string Search)
        {
            if (LocEntity.Trim() == "") { MessageBox.Show("You must choose a location before choosing an account", "warning", MessageBoxButtons.OK); return; }

            LookupForm lookup = new LookupForm(this.sqlConnection8);
            try
            {
                lookup.Width = 500;
                lookup.LookupName = "Lookup By Account";
                lookup.Text = "Lookup By Account";
                if (variables.UseUsertoDept)
                {
                    lookup.SearchSql.Add("SELECT a.Account_No, s.Sub_Account_Name, d.Department_Name, a.Department, a.Sub_Account FROM ");
                    lookup.SearchSql.Add("AccountNo as a   join SubAccount as s   ");
                    lookup.SearchSql.Add("ON a.Sub_Account = s.Sub_Account ");
                    lookup.SearchSql.Add("JOIN Department as d   on d.Department = a.Department ");
                    lookup.SearchSql.Add("AND d.entity = a.entity ");
                    lookup.SearchSql.Add("JOIN UserToDepartment ud on d.entity = ud.entity ");
                    lookup.SearchSql.Add("AND d.Department = ud.Department ");
                    lookup.SearchSql.Add("AND ud.username = @username ");
                    lookup.SearchParams.Add(new SqlParameter("Entity", LocEntity));
                    lookup.SearchParams.Add(new SqlParameter("Username", SQLusername));
                }
                else
                {
                    lookup.SearchSql.Add("SELECT a.Account_No, s.Sub_Account_Name, d.Department_Name, a.Department, a.Sub_Account FROM ");
                    lookup.SearchSql.Add("AccountNo as a   join SubAccount as s   ");
                    lookup.SearchSql.Add("ON a.Sub_Account = s.Sub_Account ");
                    lookup.SearchSql.Add("JOIN Department as d   on d.Department = a.Department ");
                    lookup.SearchSql.Add("AND d.entity = a.entity ");
                    lookup.SearchParams.Add(new SqlParameter("Entity", LocEntity));
                }
                lookup.SearchSql.Add("WHERE a.Entity = @Entity ");
                lookup.SearchSql.Add("AND d.Entity = @Entity ");
                lookup.SearchSql.Add("AND a.Account_No Like ~*~ ");
                lookup.SearchSql.Add("Order By a.Account_No ");
                lookup.SearchSql.Add("Option (Fast 250) ");
                lookup.AddColumn("Account_No", "Account Number", 100);
                lookup.AddColumn("Sub_Account_Name", "Sub Account", 220);
                lookup.AddColumn("Department_Name", "Department", 100);

                lookup.SearchText = Search;
                if (lookup.ShowDialog() == DialogResult.OK)
                {
                    Changing = true;
                    eb_Account_No_AddImf.Text = lookup.FieldByName("Account_No");
                    Changing = false;
                }
            }
            finally { lookup.Dispose(); }
        }

        public void VendorCatLookup(string Search)
        {
            LookupForm lookup = new LookupForm(this.sqlConnection8);
            try
            {
                lookup.Width = 600;
                lookup.LookupName = "Lookup By Vendor Catalog";
                lookup.Text = "Lookup By Vendor Catalog";
                lookup.SearchSql.Add("SELECT iv.Vendor_Catalog as VendCat, imf.Mat_Code, imf.Description1 FROM IMF   join ItemVend as iv   ");
                lookup.SearchSql.Add("ON iv.Mat_Code = imf.Mat_Code ");
                lookup.SearchSql.Add("WHERE iv.Vendor_Id = @Vendor_Id AND iv.Vendor_Catalog Like ~*~ ");
                lookup.SearchSql.Add("ORDER BY Vendcat");
                //lookup.SearchParams.Add(new SqlParameter("Vendor_Id", eb_Vendor_Id_AddImf.Text));
                lookup.AddColumn("VendCat", "Vendor Catalogue", 170);
                lookup.AddColumn("Mat_Code", "Material Code", 90);
                lookup.AddColumn("Description1", "Description", 230);
                lookup.SearchText = Search;
                if (lookup.ShowDialog() == DialogResult.OK)
                {
                    Changing = true;
                    eb_Vendor_Catalog_AddImf.Text = lookup.FieldByName("VendCat");
                    // if (eb_Vendor_Catalog_AddImf.Text.Trim() == "") {eb_Vendor_Catalog_AddImf.Text = eb_Search.text;}
                    Changing = false;
                }
            }
            finally { lookup.Dispose(); }
        }

        public void LocationLookup(string Search)
        {
            LookupForm lookup = new LookupForm(this.sqlConnection8);
            try
            {
                lookup.Width = 460;
                lookup.LookupName = "Lookup By Location";
                lookup.Text = "Lookup By Location";
                lookup.SearchSql.Add("SELECT l.Location, Description FROM Location l   JOIN UserToLocation ul   ");
                lookup.SearchSql.Add("ON l.Location = ul.Location ");
                lookup.SearchSql.Add("WHERE Active = 1 AND username = @username AND l.Location Like ~*~ ");
                lookup.SearchSql.Add("ORDER BY l.Location");
                lookup.SearchParams.Add(new SqlParameter("username", SQLusername));
                lookup.AddColumn("Location", "Location", 120);
                lookup.AddColumn("Description", "Description", 220);
                lookup.SearchText = Search;
                if (lookup.ShowDialog() == DialogResult.OK) { eb_Location_AddImf.Text = lookup.FieldByName("Location"); }
            }
            finally { lookup.Dispose(); }
        }

        public void MFGLookup2(string Search)
        {
            LookupForm lookup = new LookupForm(this.sqlConnection8);
            try
            {
                lookup.Width = 500;
                lookup.LookupName = "MfgLookup";
                lookup.Text = "Manufacturer Code Lookup";
                lookup.SearchSql.Add("SELECT Mfg_Name, Mfg_Id from Manufacturer   ");
                lookup.SearchSql.Add("WHERE Mfg_Name Like ~*~ ");
                lookup.SearchSql.Add("ORDER BY Mfg_Name");
                lookup.AddColumn("Mfg_Name", "Name", 230);
                lookup.AddColumn("Mfg_Id", "Id", 150);
                lookup.SearchText = Search;
                if (lookup.ShowDialog() == DialogResult.OK) { Changing = true; eb_Mfg_Name_AddImf.Text = lookup.FieldByName("Mfg_Name"); Changing = false; }
            }
            finally { lookup.Dispose(); }
        }

        public void ReOrderLocationLookup(string Search)
        {
            LookupForm lookup = new LookupForm(this.sqlConnection8);
            try
            {
                lookup.Width = 500;
                lookup.LookupName = "Lookup By Location";
                lookup.Text = "Lookup By Location";
                lookup.SearchSql.Add("SELECT l.Location, Description FROM Location l   JOIN usertolocation ul   ");
                lookup.SearchSql.Add("ON l.Location = ul.Location ");
                lookup.SearchSql.Add("WHERE Active = 1 AND username = @username AND l.Location Like ~*~ ");
                lookup.SearchSql.Add("ORDER BY l.Location");
                lookup.SearchParams.Add(new SqlParameter("username", SQLusername));
                lookup.AddColumn("Location", "Location", 170);
                lookup.AddColumn("Description", "Description", 250);
                lookup.SearchText = Search;
                if (lookup.ShowDialog() == DialogResult.OK) { eb_Reorder_Location.Text = lookup.FieldByName("Location"); }
            }
            finally { lookup.Dispose(); }
        }

        private void eb_Location_AddImf_Leave(object sender, EventArgs e)
        {
            q_Command.Parameters.Clear();
            q_Command.CommandText = "SELECT l.Location, Entity from Location l join UserToLocation ul ON ul.Location = l.Location ";
            q_Command.CommandText += "WHERE l.Location = @Location AND username = @username ";
            q_Command.Parameters.Add("Location", SqlDbType.VarChar).Value = eb_Location_AddImf.Text;
            q_Command.Parameters.Add("username", SqlDbType.VarChar).Value = SQLusername;
            using (SqlDataReader LocRead = q_Command.ExecuteReader())
            {
                LocRead.Read();
                if (LocRead.HasRows) { LocEntity = LocRead["Entity"].ToString(); }
                else
                {
                    MessageBox.Show("This Location doesn't exist or you do not have rights to this Location.", "Info", MessageBoxButtons.OK);
                    if (eb_Location_AddImf.Enabled) { eb_Location_AddImf.Focus(); }
                    return;
                }
            }
        }

        private void eb_Location_AddImf_TextChanged(object sender, EventArgs e)
        {
            if (rbcheck)
            {
                q_Command.Parameters.Clear();
                q_Command.CommandText = "SELECT l.Location, Entity FROM Location l join UserToLocation ul ON ul.Location = l.Location ";
                q_Command.CommandText += "WHERE l.Location = @Location AND username = @username";
                q_Command.Parameters.Add("Location", SqlDbType.VarChar).Value = eb_Location_AddImf.Text;
                q_Command.Parameters.Add("username", SqlDbType.VarChar).Value = SQLusername;
                using (SqlDataReader LocChange = q_Command.ExecuteReader())
                {
                    LocChange.Read();
                    LocEntity = LocChange["Entity"].ToString();
                }
            }
        }

        private void eb_Sub_Account_AddImf_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8) { Sub_AccountLookup(eb_Sub_Account_AddImf.Text); }
        }

        private void eb_Account_No_AddImf_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8) { Account_NoLookup(eb_Account_No_AddImf.Text); }
            else if (e.KeyCode == Keys.Enter) { GetSubAccount(); }
        }

        private void eb_Vendor_Catalog_AddImf_DoubleClick(object sender, EventArgs e)
        {
            VendorCatLookup("");
        }

        private void eb_Vendor_Catalog_AddImf_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8) { VendorCatLookup(eb_Vendor_Catalog_AddImf.Text); }
        }

        private void eb_Location_AddImf_DoubleClick(object sender, EventArgs e)
        {
            LocationLookup("");
        }

        private void eb_Location_AddImf_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8) { LocationLookup(eb_Location_AddImf.Text); }
        }

        private void eb_Account_No_AddImf_TextChanged(object sender, EventArgs e)
        {
            if (!Changing) { eb_Sub_Account_AddImf.Text = ""; }
        }

        private void nf_UOP_Conversion_AddImf_TextChanged(object sender, EventArgs e)
        {
            if (rb_NonStock.Checked) { nf_UOI_Conversion_AddImf.Text = nf_UOP_Conversion_AddImf.Text; }
        }

        private void eb_Vendor_Catalog_AddImf_Leave(object sender, EventArgs e)
        {
            if (variables.COPY_VC_TO_MFG && variables.AddingRecord)
            {
                if (eb_Mfg_Catalog_AddImf.Text.Trim() == "") { eb_Mfg_Catalog_AddImf.Text = eb_Vendor_Catalog_AddImf.Text; }
                //  if (eb_Mfg_Name_AddImf.Text.Trim() == "") { eb_Mfg_Name_AddImf.Text = eb_Vendor_Name_AddImf.Text; }
            }
        }

        private void eb_Account_No_AddImf_Leave(object sender, EventArgs e)
        {
            if (eb_Sub_Account_AddImf.Text.Trim() == "") { GetSubAccount(); }
        }

        private void cbx_UOP_AddImf_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentUOP = cbx_UOP_AddImf.SelectedItem.ToString().Substring(0, UOPSize);
            if (rb_NonStock.Checked) { FillUOI(CurrentUOP); }
        }

        private void cbx_UOI_AddImf_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentUOI = cbx_UOI_AddImf.SelectedItem.ToString().Substring(0, UOISize);
        }

        private void eb_Mfg_Name_AddImf_DoubleClick(object sender, EventArgs e)
        {
            MFGLookup2("");
        }

        private void eb_Mfg_Name_AddImf_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8) { MFGLookup2(eb_Mfg_Name_AddImf.Text); }
        }

        private void eb_Reorder_Location_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8) { ReOrderLocationLookup(eb_Reorder_Location.Text); }
        }

        private void eb_Reorder_Location_DoubleClick(object sender, EventArgs e)
        {
            ReOrderLocationLookup("");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            variables.result = false;
            variables.add_items = false;
            this.Close();
        }
    }


}
