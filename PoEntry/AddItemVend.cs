﻿using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Ehs.Util;


namespace PoEntry
{
    public partial class AddItemVend : Form
    {
        public Ehs.Models.ItemVend item;
        string Comment = "", CurrentUOPIV;
        int IVUOPSize;
        //bool Changing;

        public AddItemVend(Form1 frm)
        {
            InitializeComponent();
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

        public void FillUOPCombo(string PassUnit)
        {
            int i = 0;

            q_Command.Parameters.Clear();
            q_Command.CommandText = "SELECT * FROM UnitOfMeasure WHERE Active = 1 ORDER BY UOM ";
            cmb_uop.Items.Clear();
            IVUOPSize = 9;
            using (SqlDataReader read = q_Command.ExecuteReader())
            {
                while (read.Read()) { cmb_uop.Items.Add(((string)read["UOM"]).PadRight(IVUOPSize, ' ') + (string)read["Description"]); }
                try
                {
                    while (cmb_uop.Items.Count > i && cmb_uop.Items[i].ToString().Substring(0, IVUOPSize).Trim() != PassUnit)
                    {
                        i++;
                    }
                }
                catch { }
                if (i >= cmb_uop.Items.Count) { cmb_uop.SelectedIndex = 0; }
                else { cmb_uop.SelectedIndex = i; }
                CurrentUOPIV = cmb_uop.SelectedItem.ToString().Substring(0, IVUOPSize).Trim();
            }
        }

        private void cmb_uop_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentUOPIV = cmb_uop.SelectedItem.ToString().Substring(0, IVUOPSize).Trim();
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