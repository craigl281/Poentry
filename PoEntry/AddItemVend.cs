using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Ehs.Controls;
using Ehs.Util;


namespace PoEntry
{
    public partial class AddItemVend : Form
    {
        Form1 Form;
        public Ehs.Models.ItemVend item;
        string Comment = "", CurrentUOPIV;
        int IVUOPSize;
        //bool Changing;

        public AddItemVend(Form1 frm)
        {
            InitializeComponent();
            Form = frm;
            Form.data.Open();
            cmb_Purchase.Items = EHS.Orders.prefillcombo(Form.data._Com, "UOM", "");
            Form.data.Close();
            cmb_Mat.Items = new System.Collections.Generic.List<ComboBoxString>(1) { new ComboBoxString(frm.CurMat) };
            //var temp = new 
            //cmb_Mat.Items= new System.Collections.Generic.List<ComboBoxString> = new ComboBoxString(Form.CurMat);
            /*
            EhsLogin login8 = new EhsLogin(this.sqlConnection7);
            login8.Login();
            q_Command.Connection = sqlConnection7;
            FillUOPCombo(variables.CurrentUOPPrime);
            eb_mat_code.Text = variables.Mat_Code;
            eb_description.Text = variables.Description;
            eb_vendor_id.Text = variables.Vendor_Id;
            eb_vendor_name.Text = variables.Vendor_Name;
            eb_vendor_cat.Text = variables.Vendor_Cat;
            eb_mfg_cat.Text = variables.MFG_Cat;
            eb_mfg_name.Text = variables.MFG_Name;
            eb_mfg_code.Text = variables.Mat_Code;
            eb_conversion.Text = variables.Conversion;
            eb_cost.Text = variables.Unit_Cost;
            */
        }

        private void AddItemVend_Load(object sender, EventArgs e)
        {
            eb_comment.Text = "memo";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateRecord())
            {
                q_Command.Parameters.Clear();
                q_Command.CommandText = "INSERT INTO ItemVend (Mat_Code, Vendor_Id, Vendor_Catalog, Mfg_Name, Mfg_Catalog, Mfg_Code, Unit_Purchase, PO_Cost, Comment, Active, Conversion ) VALUES (@Mat_Code, @Vendor_Id, @Vendor_Catalog, @Mfg_Name, @Mfg_Catalog, @Mfg_Code, @Unit_Purchase, @PO_Cost, @Comment, 1, @Conversion )";
                q_Command.Parameters.AddWithValue("Mat_Code", item.MatCode);
                q_Command.Parameters.AddWithValue("Vendor_Id", item.VendorId);
                q_Command.Parameters.AddWithValue("Vendor_Catalog", item.VendorCatalog);
                q_Command.Parameters.AddWithValue("Mfg_Name", item.MFGName);
                q_Command.Parameters.AddWithValue("Mfg_Catalog", item.MFGCatalog);
                q_Command.Parameters.AddWithValue("Mfg_Code", item.MFGCode);
                q_Command.Parameters.AddWithValue("Unit_Purchase", item.UnitPurchase);
                q_Command.Parameters.AddWithValue("PO_Cost", item.POCost);
                q_Command.Parameters.AddWithValue("Comment", SqlDbType.Text).Value = Comment;
                q_Command.Parameters.AddWithValue("Conversion", item.Conversion);
                q_Command.ExecuteNonQuery();

                q_Command.Parameters.Clear();
                q_Command.CommandText = "INSERT INTO UOP (Mat_Code, Vendor_Id, Vendor_Catalog, Mfg_Name, Mfg_Catalog, Mfg_Code, Unit_Purchase, PO_Cost, Conversion, Default_UOP, Active) VALUES (@Mat_Code, @Vendor_Id, @Vendor_Catalog, @Mfg_Name, @Mfg_Catalog, @Mfg_Code, @Unit_Purchase, @PO_Cost, @Conversion, 1, 1 )";
                q_Command.Parameters.AddWithValue("Mat_Code", item.MatCode);
                q_Command.Parameters.AddWithValue("Vendor_Id", item.VendorId);
                q_Command.Parameters.AddWithValue("Vendor_Catalog", item.VendorCatalog);
                q_Command.Parameters.AddWithValue("Mfg_Name", item.MFGName);
                q_Command.Parameters.AddWithValue("Mfg_Catalog", item.MFGCatalog);
                q_Command.Parameters.AddWithValue("Mfg_Code", item.MFGCode);
                q_Command.Parameters.AddWithValue("Unit_Purchase", item.UnitPurchase);
                q_Command.Parameters.AddWithValue("PO_Cost", item.POCost);
                q_Command.Parameters.AddWithValue("Comment", SqlDbType.Text).Value = Comment;
                q_Command.Parameters.AddWithValue("Conversion", item.Conversion);
                q_Command.ExecuteNonQuery();
                this.Close();
            }
        }

        public bool ValidateRecord()
        {
            if (eb_vendor_cat.Text.Trim() == "")
            {
                MessageBox.Show("You must enter a Vendor Catalogue Number", "information", MessageBoxButtons.OK);
                if (eb_vendor_cat.Enabled) { eb_vendor_cat.Focus(); }
                return false;
            }
            /*
            if (variables.MUST_ENTER_MFG_NAME)
            {
                if (eb_mfg_name.Text.Trim() == "")
                {
                    MessageBox.Show("You must enter a MFG Name.", "Warning", MessageBoxButtons.OK);
                    eb_mfg_name.Focus();
                    return false;
                }
            }*/
            if (Convert.ToSingle(eb_conversion.Text) <= 0f)
            {
                MessageBox.Show("Conversion must be a positive number", "information", MessageBoxButtons.OK);
                if (eb_conversion.Enabled) { eb_conversion.Focus(); }
                return false;
            }
            return true;
        }

        private void eb_vendor_cat_Leave(object sender, EventArgs e)
        {
            /*
            if (variables.COPY_VC_TO_MFG && variables.AddingRecord)
            {
                if (eb_mfg_cat.Text.Trim() == "") { eb_mfg_cat.Text = eb_vendor_cat.Text; }
                if (eb_mfg_name.Text.Trim() == "") { eb_mfg_name.Text = eb_vendor_name.Text; }
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}