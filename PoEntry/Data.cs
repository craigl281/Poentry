using Ehs.Controls;
using Ehs.Forms;
using Ehs.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PoEntry
{
    public class Data
    {
        //public EhsUtil EhsUtil;
        Ehs.Forms.Util EhsForms;
        public SqlCommand _Com = new SqlCommand();
        SqlConnection _Con = new SqlConnection();
        public string SqlUsername;
        public decimal Po_No;
        public bool SameEntityInLoc;
        List<string> STC = new List<string>();

        #region Data connections
        public void Connect(string ConnectionString)
        {
            _Con.ConnectionString = ConnectionString;
            _Com.Connection = _Con;
            EhsForms = new Ehs.Forms.Util(_Con);
        }

        public void Open()
        {
            if (_Con.State == ConnectionState.Closed)
                _Con.Open();
        }

        public void Close()
        {
            if (_Con.State == ConnectionState.Open)
                _Con.Close();
        }
        #endregion

        #region Menu
        public void SaveSettings(Form _frm)
        {
            Setting(_frm, 0);
        }

        public void RestoreSettings(Form _frm)
        {
            Setting(_frm, 1);
        }

        public void RestoreDefault(Form _frm)
        {
            Setting(_frm, 2);
        }

        private void Setting(Form _frm, Int16 _hold)
        {
            Open();
            switch (_hold)
            {
                case 0:
                    EhsForms.SaveFormSettings(_frm, SqlUsername);
                    break;
                case 1:
                    EhsForms.RestoreFormSettings(_frm, SqlUsername);
                    break;
                case 2:
                    EhsForms.RestoreDefaultFormSettings(_frm);
                    break;
            }
            Close();
        }
        #endregion

        #region Combobox stuff
        public string Lookup(string index, bool Use_Po_Groups, string param)
        {
            string values = "";

            Open();

            _Com.Parameters.Clear();

            switch (index)
            {
                #region Header Stuff
                case "Po No":
                    #region Po Number
                    {
                        STC.Clear();
                        STC.Add("Po_No");

                        _Com.CommandText = "SELECT p.Po_No, p.Po_Date, p.Vendor_Name FROM PoHeader as P JOIN UserToEntity AS U ON p.Entity = u.Entity WHERE u.Username = "
                                         + "@Username AND cast(p.Po_No as varchar) LIKE @po_no ";
                        if (Use_Po_Groups)
                            _Com.CommandText += "AND PoGroup_Id in (SELECT PoGroup_Id FROM usertopogroups WHERE username = @username) ";
                        _Com.CommandText += "ORDER BY p.Po_date DESC, p.po_no DESC";
                        _Com.Parameters.AddWithValue("username", this.SqlUsername);
                        SqlParameter po_no = new SqlParameter("po_no", '%');
                        _Com.Parameters.Add(po_no);
                        using (Ehs.Forms.Lookup Lookup = new Ehs.Forms.Lookup(STC, _Com))
                        {
                            Lookup.Width = 400;
                            if (Lookup.ShowDialog() == DialogResult.OK)
                                values = Lookup.FieldByName("Po_No").ToNonNullString();
                        }
                        break;
                    }
                #endregion
                case "Vendor":
                    #region Vendor
                    {
                        STC.Clear();
                        STC.Add("Vendor_Id");
                        STC.Add("Vendor_Name");

                        _Com.CommandText = "SELECT Vendor_ID, Vendor_Name, Address1 FROM Vendor ORDER BY Vendor_Name";
                        using (Ehs.Forms.Lookup Lookup = new Ehs.Forms.Lookup(STC, _Com))
                        {
                            Lookup.Width = 500;
                            if (Lookup.ShowDialog() == DialogResult.OK)
                                values = Lookup.FieldByName("Vendor_Id").ToString() + "   " + Lookup.FieldByName("Vendor_Name").ToNonNullString();
                        }
                        break;
                    }
                #endregion
                case "Req":
                    #region Req
                    {
                        STC.Clear();
                        STC.Add("Req_No");
                        STC.Add("Vendor_Id");
                        STC.Add("Po_No");

                        _Com.CommandText = "SELECT p.Req_No, p.Po_No, p.Vendor_Id, p.Vendor_Name FROM PoHeader as p " +
                                           "  JOIN UserToEntity as u   on p.Entity = u.Entity " +
                                           "WHERE u.Username = @Username AND NOT p.Req_no = '0' AND NOT p.Req_no = ' ' " +
                                           "ORDER BY p.Req_No DESC";
                        _Com.Parameters.AddWithValue("username", this.SqlUsername);
                        using (Ehs.Forms.Lookup Lookup = new Ehs.Forms.Lookup(STC, _Com))
                        {
                            Lookup.Width = 600;
                            if (Lookup.ShowDialog() == DialogResult.OK)
                                values = Lookup.FieldByName("Req_No").ToString();
                        }
                        break;
                    }
                #endregion
                case "Project":
                    #region Project
                    {
                        STC.Clear();
                        STC.Add("Project_No");
                        STC.Add("Description");

                        _Com.CommandText = "SELECT Project_No, Description, Budget_Amount, Project_Spend_Amount FROM " +
                                           "Project ORDER BY Project_No";
                        using (Ehs.Forms.Lookup Lookup = new Ehs.Forms.Lookup(STC, _Com))
                        {
                            Lookup.Width = 450;
                            if (Lookup.ShowDialog() == DialogResult.OK)
                                values = Lookup.FieldByName("Project_No").ToString();
                        }
                        break;
                    }
                #endregion
                case "Buyer":
                    #region Buyer
                    {
                        STC.Clear();
                        STC.Add("Buyer_Username");
                        STC.Add("Last_Name");

                        _Com.CommandText = "SELECT DISTINCT Buyer_Username, First_Name, Last_Name FROM Poheader LEFT JOIN " +
                                           "Users on Buyer_Username = Username WHERE Buyer_Username <> '' " +
                                           "ORDER BY Buyer_Username";
                        using (Ehs.Forms.Lookup Lookup = new Ehs.Forms.Lookup(STC, _Com))
                        {
                            Lookup.Width = 350;
                            if (Lookup.ShowDialog() == DialogResult.OK)
                                values = Lookup.FieldByName("Buyer_Username").ToString();
                        }
                        break;
                    }
                #endregion
                case "C/R":
                    #region Confirmation
                    {
                        STC.Clear();
                        STC.Add("Confirmation_No");

                        _Com.CommandText = "SELECT DISTINCT Confirmation_No FROM PoHeader WHERE Confirmation_No <> '' " +
                                           "ORDER BY Confirmation_No";
                        using (Ehs.Forms.Lookup Lookup = new Ehs.Forms.Lookup(STC, _Com))
                        {
                            Lookup.Width = 200;
                            if (Lookup.ShowDialog() == DialogResult.OK)
                                values = Lookup.FieldByName("Confirmation_No").ToString();
                        }
                        break;
                    }
                #endregion
                #endregion

                #region Detail Stuff
                case "MatCode":
                    #region Mat Code
                    {
                        STC.Clear();
                        STC.Add("Mat_Code");
                        STC.Add("Description1");

                        _Com.CommandText = "SELECT DISTINCT Mat_Code, Description1 FROM PoDetail WHERE Mat_Code <> '' AND charindex('*', mat_code)= 0 ORDER BY Mat_Code";
                        using (Ehs.Forms.Lookup Lookup = new Ehs.Forms.Lookup(STC, _Com, 450))
                        {
                            if (Lookup.ShowDialog() == DialogResult.OK)
                                values = Lookup.FieldByName("Mat_Code").ToString();
                        }
                        break;
                    }
                #endregion
                case "Loc":
                    #region Location
                    {
                        STC.Clear();
                        STC.Add("Location");
                        STC.Add("Description");

                        _Com.CommandText = "SELECT DISTINCT Loc.Location, L.Description FROM Location L JOIN Loc Loc ON " +
                                           "L.Location= Loc.location JOIN Usertolocation ul ON ul.Location = L.location " +
                                           "WHERE Loc.Location <> '' AND ul.username = @user AND Loc.Active = 1 " +
                                           "ORDER BY Loc.Location";
                        _Com.Parameters.AddWithValue("user", this.SqlUsername);
                        using (Ehs.Forms.Lookup Lookup = new Ehs.Forms.Lookup(STC, _Com))
                        {
                            Lookup.Width = 350;
                            if (Lookup.ShowDialog() == DialogResult.OK)
                                values = Lookup.FieldByName("Location").ToString();
                        }
                        break;
                    }
                #endregion
                case "Dept":
                    #region Department
                    {
                        STC.Clear();
                        STC.Add("Department");
                        STC.Add("Department_Name");

                        _Com.CommandText = "SELECT DISTINCT Department, Department_Name FROM Department D JOIN UserToEntity U ON D.Entity = U.Entity WHERE U.username = @user AND "
                                         + "D.Entity = @Entity AND D.Department <> '' AND D.Department LIKE @Department AND Department_Name LIKE @Department_Name ORDER BY Department";
                        _Com.Parameters.AddWithValue("username", this.SqlUsername);
                        SqlParameter Department = new SqlParameter("Department", '%');
                        _Com.Parameters.Add(Department);
                        SqlParameter Department_Name = new SqlParameter("Department_Name", '%');
                        _Com.Parameters.Add(Department_Name);
                        _Com.Parameters.AddWithValue("user", this.SqlUsername);
                        _Com.Parameters.AddWithValue("Entity", param);
                        using (Ehs.Forms.Lookup Lookup = new Ehs.Forms.Lookup(STC, _Com))
                        {
                            Lookup.Width = 350;
                            if (Lookup.ShowDialog() == DialogResult.OK)
                            {
                                values = Lookup.FieldByName("Department").ToString();
                                //values = Lookup.FieldByName("Department").ToString() + "   " +
                                //         Lookup.FieldByName("Department_Name").ToNonNullString();
                            }
                        }
                        break;
                    }
                #endregion
                case "DR":
                    #region Doctor
                    {
                        STC.Clear();
                        STC.Add("Doctor_Id");
                        STC.Add("Last_Name");
                        _Com.CommandText = "SELECT Doctor_Id, Last_Name, First_Name FROM Doctor ORDER BY Doctor_Id";
                        using (Ehs.Forms.Lookup Lookup = new Ehs.Forms.Lookup(STC, _Com))
                        {
                            Lookup.Width = 350;
                            if (Lookup.ShowDialog() == DialogResult.OK)
                                values = Lookup.FieldByName("Doctor_Id").ToString();
                        }
                        break;
                    }
                #endregion
                case "Account":
                    #region Account Number
                    {
                        STC.Clear();
                        STC.Add("Account_No");
                        STC.Add("Sub_Account_Name");
                        STC.Add("Department");
                        STC.Add("Entity");
                        _Com.CommandText = "SELECT AN.Account_No,SA.Sub_Account_Name,AN.Department,AN.Sub_Account," +
                                           "AN.Entity FROM AccountNo AN JOIN SubAccount SA ON AN.Sub_Account = " +
                                           "SA.Sub_Account JOIN UserToEntity U ON U.Entity = AN.Entity WHERE " +
                                           "U.username = @User";
                        _Com.Parameters.AddWithValue("User", this.SqlUsername);
                        using (Ehs.Forms.Lookup Lookup = new Ehs.Forms.Lookup(STC, _Com))
                        {
                            Lookup.Width = 550;
                            if (Lookup.ShowDialog() == DialogResult.OK)
                                values = Lookup.FieldByName("Account_No").ToString();
                        }
                        break;
                    }
                #endregion
                case "MFGCAT":
                    #region Mfg Catalog
                    {
                        STC.Clear();
                        STC.Add("Mfg_Catalog");
                        STC.Add("Description1");
                        _Com.CommandText = "SELECT DISTINCT iv.Mfg_Catalog, imf.Description1 FROM IMF  ," +
                                           "ItemVend as iv   WHERE iv.Mat_Code = imf.Mat_Code AND iv.Mfg_Catalog <> '' " +
                                           "AND iv.active = 1 ORDER BY Mfg_Catalog";
                        using (Ehs.Forms.Lookup Lookup = new Ehs.Forms.Lookup(STC, _Com))
                        {
                            Lookup.Width = 550;
                            if (Lookup.ShowDialog() == DialogResult.OK)
                                values = Lookup.FieldByName("Mfg_Catalog").ToString();
                        }
                        break;
                    }
                #endregion
                case "Profile":
                    #region Profile Id
                    {
                        STC.Clear();
                        STC.Add("Profile_id");
                        STC.Add("Description");
                        _Com.CommandText = "SELECT Profile_id, Description FROM ProfileId ORDER BY Profile_Id ";
                        using (Ehs.Forms.Lookup Lookup = new Ehs.Forms.Lookup(STC, _Com))
                        {
                            Lookup.Width = 550;
                            if (Lookup.ShowDialog() == DialogResult.OK)
                                values = Lookup.FieldByName("Profile_id").ToString();
                        }
                        break;
                    }
                #endregion
                case "DeliverTo":
                    #region Deliver To
                    {
                        STC.Clear();
                        STC.Add("Deliver_To");
                        STC.Add("Description");
                        _Com.CommandText = "SELECT DISTINCT Deliver_To, Description FROM DeliverTo ORDER BY Deliver_To ";
                        using (Ehs.Forms.Lookup Lookup = new Ehs.Forms.Lookup(STC, _Com))
                        {
                            Lookup.Width = 550;
                            if (Lookup.ShowDialog() == DialogResult.OK)
                                values = Lookup.FieldByName("Deliver_To").ToString();
                        }
                        break;
                    }
                #endregion

                #endregion

                #region IMF
                case "UNSPSC":
                    {
                        using (Ehs.Forms.UNSPSClookup Look = new UNSPSClookup(_Con))
                        {
                            if (Look.ShowDialog() == DialogResult.OK)
                                values = Look.Segment + "-" + Look.Family + "-" + Look.Class + "-" + Look.Commodity + new string(' ', 5) + Look.Title;
                        }
                        break;
                    }
                    #endregion
            }
            Close();
            return values;
        }

        public List<string> Lookup2(string index, bool Full)
        {
            List<string> values = new List<string>();

            Open();

            _Com.Parameters.Clear();
            if (Full)
                _Com.CommandText = "SELECT DISTINCT ";
            else
                _Com.CommandText = "SELECT TOP 10 ";

            switch (index)
            {
                #region Header Stuff
                case "Vendor":
                    #region Vendor
                    {
                        _Com.CommandText += "Vendor_ID,Vendor_Name,Address1 FROM Vendor ORDER By Vendor_Name";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "Req":
                    #region Req
                    {
                        _Com.CommandText += "p.Req_No, p.Po_No, p.Vendor_Id, p.Vendor_Name FROM PoHeader as p " +
                                           "  JOIN UserToEntity as u   on p.Entity = u.Entity " +
                                           "WHERE u.Username = @Username AND NOT p.Req_no = '0' AND NOT p.Req_no = ' ' " +
                                           "ORDER BY p.Req_No DESC";
                        _Com.Parameters.AddWithValue("username", this.SqlUsername);
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "Project":
                    #region Project
                    {
                        _Com.CommandText += "Project_No, Description, Budget_Amount, Project_Spend_Amount FROM " +
                                           "Project ORDER BY Project_No";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "Buyer":
                    #region Buyer
                    {
                        _Com.CommandText += "Buyer_Username FROM Poheader LEFT JOIN Users on Buyer_Username = Username WHERE " +
                                           "Buyer_Username <> '' ORDER BY Buyer_Username";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "C/R":
                    #region Confirmation
                    {
                        _Com.CommandText += "Confirmation_No FROM PoHeader WHERE Confirmation_No <> '' " +
                                           "ORDER BY Confirmation_No";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                #endregion

                #region Detail Stuff
                case "MatCode":
                    #region Mat Code
                    {
                        _Com.CommandText += "Mat_Code, Description1 FROM IMF ORDER BY Mat_Code";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "MatCodePO":
                    #region Mat Code
                    {
                        _Com.CommandText += "Mat_Code,Description1 FROM PoDetail WHERE Po_No =@Po_No ORDER BY Mat_Code";
                        _Com.Parameters.AddWithValue("Po_No", Po_No);
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "Loc":
                    #region Location
                    {
                        _Com.CommandText += "Loc.Location, L.Description FROM Location L JOIN Loc Loc ON L.Location = " +
                                           "Loc.location JOIN Usertolocation ul ON ul.Location = L.location WHERE Mat_Code " +
                                           "= @Mat_Code AND Loc.Location <> '' AND ul.username = @user AND Loc.Active = 1 ";
                        if (SameEntityInLoc)
                            _Com.CommandText += "AND Loc.Entity = @Entity ";
                        else
                        {
                            _Com.CommandText += "AND Loc.Entity in (SELECT DISTINCT Entity FROM Loc WHERE Mat_Code = @Mat_Code " +
                                "AND ((entity = @Entity) OR (Type = 'N'))) ";
                        }
                        _Com.CommandText += "ORDER BY Loc.Location";
                        _Com.Parameters.AddWithValue("user", this.SqlUsername);
                        //_Com.Parameters.AddWithValue("Entity", Entity);
                        //_Com.Parameters.AddWithValue("Mat_Code", this.Mat_Code);
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "Dept":
                    #region Department
                    {
                        _Com.CommandText += "Department, Department_Name FROM Department D JOIN " +
                                           "UserToEntity U ON D.Entity = U.Entity WHERE U.username = @user AND " +
                                           "D.Department <> '' ORDER BY Department";
                        _Com.Parameters.AddWithValue("user", this.SqlUsername);
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "DR":
                    #region Doctor
                    {
                        _Com.CommandText += "Doctor_Id, Last_Name, First_Name FROM Doctor ORDER BY Doctor_Id";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "Account":
                    #region Account Number
                    {
                        if (SystemOptionsDictionary["USE_USERTODEPT"].ToBoolean())
                        {
                            _Com.CommandText += "AN.Account_No,SA.Sub_Account_Name, DEP.Department_Name, AN.Department, " +
                                                "AN.Sub_Account, AN.Entity FROM AccountNo AN JOIN SubAccount SA ON " +
                                                "AN.Sub_Account = SA.Sub_Account JOIN Department DEP ON DEP.Department = " +
                                                "AN.Department AND DEP.Entity = AN.Entity JOIN UserToDepartment U ON U.Entity = " +
                                                "AN.Entity AND U.Department = DEP.Department AND U.username = @User WHERE " +
                                                "AN.Entity = @entity AND DEP.Entity = @entity AND DEP.active = 1 AND " +
                                                "AN.Active = 1 ORDER BY AN.Account_No";
                            _Com.Parameters.AddWithValue("User", this.SqlUsername);
                        }
                        else
                        {
                            _Com.CommandText += "AN.Account_No,SA.Sub_Account_Name, DEP.Department_Name, AN.Department, " +
                                                "AN.Sub_Account, AN.Entity FROM AccountNo AN JOIN SubAccount SA ON " +
                                                "AN.Sub_Account = SA.Sub_Account JOIN Department DEP ON DEP.Department = " +
                                                "AN.Department AND DEP.Entity = AN.Entity WHERE AN.Entity = @entity AND " +
                                                "DEP.Entity = @entity AND DEP.active = 1 AND AN.Active = 1 ORDER BY AN.Account_No";
                        }
                        //_Com.Parameters.AddWithValue("Entity", Entity);
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "Profile":
                    #region Profile Id
                    {
                        _Com.CommandText += "Profile_id, Description FROM ProfileId ORDER BY Profile_Id ";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "DeliverTo":
                    #region Deliver To
                    {
                        _Com.CommandText += "Deliver_To, Description FROM DeliverTo WHERE Active = 1 AND Entity = @entity " +
                                            "ORDER BY Deliver_To ";
                        //_Com.Parameters.AddWithValue("Entity", Entity);
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "PoClass":
                    #region PoClass
                    {
                        _Com.CommandText += "Po_Class, Description FROM PoClass   ORDER BY PO_Class";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "MFGCAT":
                    #region Mfg Catalog
                    {
                        _Com.CommandText += "iv.Mfg_Catalog, imf.Description1 FROM IMF  ," +
                                           "ItemVend as iv   WHERE iv.Mat_Code = imf.Mat_Code AND iv.Mfg_Catalog <> '' " +
                                           "AND iv.active = 1 ORDER BY Mfg_Catalog";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "MfgName":
                    #region Mfg Name
                    {
                        _Com.CommandText += "Mfg_Name FROM Manufacturer   ORDER BY Mfg_Name";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "VendCat":
                    #region Vendor Catalog
                    {
                        _Com.CommandText += " iv.Vendor_Catalog, imf.Description1 FROM IMF  ," +
                                           "ItemVend as iv   WHERE iv.Mat_Code = imf.Mat_Code AND iv.Vendor_Catalog " +
                                           "<> '' AND iv.active = 1 ORDER BY Vendor_Catalog";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                #endregion

                #region IMF
                case "SubClass":
                    #region Sub Class
                    {
                        _Com.CommandText += "Sub_Class FROM IMF WHERE Sub_Class <> 'NULL' ORDER BY Sub_Class";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString());
                        }
                        break;
                    }
                    #endregion
                    #endregion
            }
            Close();
            return values;
        }

        public AutoCompleteStringCollection Lookup3(string index)
        {
            AutoCompleteStringCollection values = new AutoCompleteStringCollection();

            Open();

            _Com.Parameters.Clear();

            switch (index)
            {
                #region Header Stuff
                case "Vendor":
                    #region Vendor
                    {
                        _Com.CommandText = "SELECT DISTINCT Vendor_ID,Vendor_Name,Address1 FROM Vendor ORDER By Vendor_Name";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "Req":
                    #region Req
                    {
                        _Com.CommandText = "SELECT DISTINCT p.Req_No, p.Po_No, p.Vendor_Id, p.Vendor_Name FROM PoHeader as p " +
                                           "  JOIN UserToEntity as u   on p.Entity = u.Entity " +
                                           "WHERE u.Username = @Username AND NOT p.Req_no = '0' AND NOT p.Req_no = ' ' " +
                                           "ORDER BY p.Req_No DESC";
                        _Com.Parameters.AddWithValue("username", this.SqlUsername);
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "Project":
                    #region Project
                    {
                        _Com.CommandText = "SELECT DISTINCT Project_No, Description, Budget_Amount, Project_Spend_Amount FROM " +
                                           "Project ORDER BY Project_No";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "Buyer":
                    #region Buyer
                    {
                        _Com.CommandText = "SELECT DISTINCT Buyer_Username, First_Name, Last_Name FROM Poheader LEFT JOIN " +
                                           "Users on Buyer_Username = Username WHERE Buyer_Username <> '' " +
                                           "ORDER BY Buyer_Username";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "C/R":
                    #region Confirmation
                    {
                        _Com.CommandText = "SELECT DISTINCT Confirmation_No FROM PoHeader WHERE Confirmation_No <> '' " +
                                           "ORDER BY Confirmation_No";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                #endregion
                #region Detail Stuff
                case "MatCode":
                    #region Mat Code
                    {
                        _Com.CommandText = "SELECT Mat_Code, Description1 FROM IMF ORDER BY Mat_Code";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "Loc":
                    #region Location
                    {
                        _Com.CommandText += "SELECT DISTINCT Loc.Location, L.Description FROM Location L JOIN Loc Loc ON " +
                                            "L.Location = Loc.location JOIN Usertolocation ul ON ul.Location = L.location WHERE " +
                                            "Mat_Code = @Mat_Code AND Loc.Location <> '' AND ul.username = @user AND Loc.Active = 1 ";
                        if (SameEntityInLoc)
                            _Com.CommandText += "AND Loc.Entity = @Entity ";
                        else
                        {
                            _Com.CommandText += "AND Loc.Entity in (SELECT DISTINCT Entity FROM Loc WHERE Mat_Code = @Mat_Code " +
                                "AND ((entity = @Entity) OR (Type = 'N'))) ";
                        }
                        _Com.CommandText += "ORDER BY Loc.Location";
                        _Com.Parameters.AddWithValue("user", this.SqlUsername);
                        //_Com.Parameters.AddWithValue("Entity", Entity);
                        //_Com.Parameters.AddWithValue("Mat_Code", this.Mat_Code);
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "Dept":
                    #region Department
                    {
                        _Com.CommandText = "SELECT DISTINCT Department, Department_Name FROM Department D JOIN " +
                                           "UserToEntity U ON D.Entity = U.Entity WHERE U.username = @user AND " +
                                           "D.Department <> '' ORDER BY Department";
                        _Com.Parameters.AddWithValue("user", this.SqlUsername);
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "DR":
                    #region Doctor
                    {
                        _Com.CommandText = "SELECT DISTINCT Doctor_Id, Last_Name, First_Name FROM Doctor ORDER BY Doctor_Id";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "Account":
                    #region Account Number
                    {
                        if (SystemOptionsDictionary["USE_USERTODEPT"].ToBoolean())
                        {
                            _Com.CommandText = "SELECT DISTINCT AN.Account_No,SA.Sub_Account_Name, DEP.Department_Name, AN.Department, " +
                                               "AN.Sub_Account, AN.Entity FROM AccountNo AN JOIN SubAccount SA ON " +
                                               "AN.Sub_Account = SA.Sub_Account JOIN Department DEP ON DEP.Department = " +
                                               "AN.Department AND DEP.Entity = AN.Entity JOIN UserToDepartment U ON U.Entity = " +
                                               "AN.Entity AND U.Department = DEP.Department AND U.username = @User WHERE " +
                                               "AN.Entity = @entity AND DEP.Entity = @entity AND DEP.active = 1 AND " +
                                               "AN.Active = 1 ORDER BY AN.Account_No";
                            _Com.Parameters.AddWithValue("User", this.SqlUsername);
                        }
                        else
                        {
                            _Com.CommandText = "SELECT DISTINCT AN.Account_No,SA.Sub_Account_Name, DEP.Department_Name, AN.Department, " +
                                               "AN.Sub_Account, AN.Entity FROM AccountNo AN JOIN SubAccount SA ON " +
                                               "AN.Sub_Account = SA.Sub_Account JOIN Department DEP ON DEP.Department = " +
                                               "AN.Department AND DEP.Entity = AN.Entity WHERE AN.Entity = @entity AND " +
                                               "DEP.Entity = @entity AND DEP.active = 1 AND AN.Active = 1 ORDER BY AN.Account_No";
                        }
                        //_Com.Parameters.AddWithValue("Entity", Entity);
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "Profile":
                    #region Profile Id
                    {
                        _Com.CommandText = "SELECT DISTINCT Profile_id, Description FROM ProfileId ORDER BY Profile_Id ";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "DeliverTo":
                    #region Deliver To
                    {
                        _Com.CommandText = "SELECT DISTINCT Deliver_To, Description FROM DeliverTo WHERE Active = 1 AND " +
                                           "Entity = @entity ORDER BY Deliver_To ";
                        //_Com.Parameters.AddWithValue("Entity", Entity);
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "PoClass":
                    #region PoClass
                    {
                        _Com.CommandText = "SELECT DISTINCT Po_Class, Description FROM PoClass   ORDER BY PO_Class";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "MfgName":
                    #region Mfg Name
                    {
                        _Com.CommandText = "SELECT DISTINCT Mfg_Name FROM Manufacturer   ORDER BY Mfg_Name";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "VendCat":
                    #region Vendor Catalog
                    {
                        _Com.CommandText = "SELECT DISTINCT iv.Vendor_Catalog, imf.Description1 FROM IMF  ," +
                                           "ItemVend as iv   WHERE iv.Mat_Code = imf.Mat_Code AND iv.Vendor_Catalog " +
                                           "<> '' AND iv.active = 1 ORDER BY Vendor_Catalog";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion
                case "MFGCAT":
                    #region Mfg Catalog
                    {
                        _Com.CommandText = "SELECT DISTINCT iv.Mfg_Catalog, imf.Description1 FROM IMF  ," +
                                           "ItemVend as iv   WHERE iv.Mat_Code = imf.Mat_Code AND iv.Mfg_Catalog <> '' " +
                                           "AND iv.active = 1 ORDER BY Mfg_Catalog";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString() + new string(' ', 5) + read[1].ToNonNullString());
                        }
                        break;
                    }
                #endregion

                #endregion
                #region IMF
                case "SubClass":
                    #region Sub Class
                    {
                        _Com.CommandText = "SELECT DISTINCT Sub_Class FROM IMF WHERE Sub_Class <> 'NULL' ORDER BY Sub_Class";
                        using (SqlDataReader read = _Com.ExecuteReader())
                        {
                            while (read.Read())
                                values.Add(read[0].ToNonNullString());
                        }
                        break;
                    }
                    #endregion

                    #endregion
            }
            Close();
            return values;
        }

        public int Lookup4(string index)
        {
            int value = 0;

            Open();

            _Com.Parameters.Clear();

            switch (index)
            {
                #region Header Stuff
                case "Vendor":
                    #region Vendor
                    {
                        _Com.CommandText = "SELECT count(Vendor_ID) FROM Vendor";
                        value = _Com.ExecuteScalar().ToInt32();
                        break;
                    }
                #endregion
                case "Req":
                    #region Req
                    {
                        _Com.CommandText = "SELECT count(p.Req_No) FROM PoHeader as p " +
                                           "  JOIN UserToEntity as u   on p.Entity = u.Entity " +
                                           "WHERE u.Username = @Username AND NOT p.Req_no = '0' AND NOT p.Req_no = ' ' ";
                        _Com.Parameters.AddWithValue("username", this.SqlUsername);
                        value = _Com.ExecuteScalar().ToInt32();
                        break;
                    }
                #endregion
                case "Project":
                    #region Project
                    {
                        _Com.CommandText = "SELECT count(Project_No) FROM Project";
                        value = _Com.ExecuteScalar().ToInt32();
                        break;
                    }
                #endregion
                case "Buyer":
                    #region Buyer
                    {
                        _Com.CommandText = "SELECT count(Buyer_Username) FROM Poheader LEFT JOIN " +
                                           "Users on Buyer_Username = Username WHERE Buyer_Username <> '' ";
                        value = _Com.ExecuteScalar().ToInt32();
                        break;
                    }
                #endregion
                case "C/R":
                    #region Confirmation
                    {
                        _Com.CommandText = "SELECT count(Confirmation_No) FROM PoHeader WHERE Confirmation_No <> '' ";
                        value = _Com.ExecuteScalar().ToInt32();
                        break;
                    }
                #endregion
                #endregion
                #region Detail Stuff
                case "MatCode":
                    #region Mat Code
                    {
                        _Com.CommandText = "SELECT count(Mat_Code) FROM ItemVend WHERE Mat_Code <> '' AND "
                                         + "charindex('*', mat_code)= 0";
                        break;
                    }
                #endregion
                case "Loc":
                    #region Location
                    {
                        _Com.CommandText = "SELECT count(Loc.Location) FROM Location L JOIN Loc Loc ON L.Location = " +
                                            "Loc.location JOIN Usertolocation ul ON ul.Location = L.location WHERE Mat_Code = " +
                                            "@Mat_Code AND Loc.Location <> '' AND ul.username = @user AND Loc.Active = 1 ";
                        if (SameEntityInLoc)
                            _Com.CommandText += "AND Loc.Entity = @Entity ";
                        else
                        {
                            _Com.CommandText += "AND Loc.Entity in (SELECT DISTINCT Entity FROM Loc WHERE Mat_Code = @Mat_Code " +
                                "AND ((entity = @Entity) OR (Type = 'N'))) ";
                        }
                        _Com.Parameters.AddWithValue("user", this.SqlUsername);
                        // _Com.Parameters.AddWithValue("Entity", Entity);
                        // _Com.Parameters.AddWithValue("Mat_Code", this.Mat_Code);
                        break;
                    }
                #endregion
                case "Dept":
                    #region Department
                    {
                        _Com.CommandText = "SELECT count(Department) FROM Department D JOIN " +
                                           "UserToEntity U ON D.Entity = U.Entity WHERE U.username = @user AND " +
                                           "D.Department <> ''";
                        _Com.Parameters.AddWithValue("user", this.SqlUsername);
                        break;
                    }
                #endregion
                case "DR":
                    #region Doctor
                    {
                        _Com.CommandText = "SELECT count(Doctor_Id) FROM Doctor";
                        break;
                    }
                #endregion
                case "Account":
                    #region Account Number
                    {
                        if (SystemOptionsDictionary["USE_USERTODEPT"].ToBoolean())
                        {
                            _Com.CommandText = "SELECT count(AN.Account_No) FROM AccountNo AN JOIN SubAccount SA ON " +
                                               "AN.Sub_Account = SA.Sub_Account JOIN Department DEP ON DEP.Department = " +
                                               "AN.Department AND DEP.Entity = AN.Entity JOIN UserToDepartment U ON U.Entity = " +
                                               "AN.Entity AND U.Department = DEP.Department AND U.username = @User WHERE " +
                                               "AN.Entity = @entity AND DEP.Entity = @entity AND DEP.active = 1 AND " +
                                               "AN.Active = 1";
                            _Com.Parameters.AddWithValue("User", this.SqlUsername);
                        }
                        else
                        {
                            _Com.CommandText = "SELECT count(AN.Account_No) FROM AccountNo AN JOIN SubAccount SA ON " +
                                               "AN.Sub_Account = SA.Sub_Account JOIN Department DEP ON DEP.Department = " +
                                               "AN.Department AND DEP.Entity = AN.Entity WHERE AN.Entity = @entity AND " +
                                               "DEP.Entity = @entity AND DEP.active = 1 AND AN.Active = 1";
                        }
                        //_Com.Parameters.AddWithValue("Entity", Entity);
                        break;
                    }
                #endregion
                case "Profile":
                    #region Profile Id
                    {
                        _Com.CommandText = "SELECT count(Profile_id) FROM ProfileId";
                        break;
                    }
                #endregion
                case "DeliverTo":
                    #region Deliver To
                    {
                        _Com.CommandText = "SELECT count(Deliver_To) FROM DeliverTo WHERE Active = 1 AND Entity = @entity";
                        //_Com.Parameters.AddWithValue("Entity", Entity);
                        break;
                    }
                #endregion
                case "PoClass":
                    #region PoClass
                    {
                        _Com.CommandText = "SELECT count(Po_Class) FROM PoClass";
                        break;
                    }
                #endregion
                case "MfgName":
                    #region Mfg Name
                    {
                        _Com.CommandText = "SELECT count(Mfg_Name) FROM Manufacturer";
                        break;
                    }
                #endregion
                case "VendCat":
                    #region Vendor Catalog
                    {
                        _Com.CommandText = "SELECT count(iv.Vendor_Catalog) FROM IMF  ," +
                                           "ItemVend as iv   WHERE iv.Mat_Code = imf.Mat_Code AND iv.Vendor_Catalog " +
                                           "<> '' AND iv.active = 1";
                        break;
                    }
                #endregion
                case "MFGCAT":
                    #region Mfg Catalog
                    {
                        _Com.CommandText = "SELECT count(iv.Mfg_Catalog) FROM IMF  ," +
                                           "ItemVend as iv   WHERE iv.Mat_Code = imf.Mat_Code AND iv.Mfg_Catalog <> '' " +
                                           "AND iv.active = 1";
                        break;
                    }
                #endregion

                #endregion
                #region IMF
                case "SubClass":
                    #region Sub Class
                    {
                        _Com.CommandText = "SELECT count(Sub_Class) FROM IMF WHERE Sub_Class <> 'NULL'";
                        value = _Com.ExecuteScalar().ToInt32();
                        break;
                    }
                    #endregion

                    #endregion
            }
            value = _Com.ExecuteScalar().ToInt32();
            Close();
            return value;
        }
        #endregion

        #region ComboboxPreFills
        public List<ComboBoxString> getEntity()
        {
            List<ComboBoxString> ret = new List<ComboBoxString>();
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT e.Entity, e.Name, e.Address1, e.Address2, e.Address3, e.City, e.State, e.Zip, e.Phone_No, e.Federal_ID, e.Adjustment_Account, e.Active, e.Default_Location, e.Default_Ship_To, "
                             + "e.Default_NonStock_Location, e.Tax_Exempt_Id, e.Fax_Number, e.Minimum_Order, e.Next_Po_Number, e.Patient_Charge_Department, e.Adjustment_ProfileID, e.Entry_Date, e.Notify_AP FROM Entity E "
                             + "JOIN UserToEntity U ON E.Entity = U.Entity WHERE U.Username = @user AND E.Active = 1 ORDER BY E.Entity";
            _Com.Parameters.AddWithValue("user", SqlUsername);
            using (SqlDataReader reader = _Com.ExecuteReader())
            {
                while (reader.Read())
                {
                    ret.Add(new ComboBoxString(reader[0].ToNonNullString(), new ComboBoxEntity("", reader[1].ToNonNullString(), reader[2].ToNonNullString(), reader[3].ToNonNullString(), reader[4].ToNonNullString(),
                            reader[5].ToNonNullString(), reader[6].ToNonNullString(), reader[7].ToNonNullString(), reader[8].ToNonNullString(), reader[9].ToNonNullString(), reader[10].ToNonNullString(),
                            reader[11].ToBoolean(), reader[12].ToNonNullString(), reader[13].ToNonNullString(), reader[14].ToNonNullString(), reader[15].ToNonNullString(), reader[16].ToNonNullString(),
                            reader[17].ToDecimal(), reader[18].ToDecimal(), reader[19].ToNonNullString(), reader[20].ToNonNullString(), reader[21].ToDateTime(), reader[22].ToBoolean())));
                }
            }
            Close();
            return ret;
        }

        public List<ComboBoxString> getPoType()
        {
            List<ComboBoxString> ret = new List<ComboBoxString>();
            Open();
            _Com.Parameters.Clear();
            ret.Add(new ComboBoxString("<NONE>"));

            _Com.CommandText = "SELECT Po_Type, Description, Prepay, Frequency, Not_Exceed, Dont_Accrual, Fill_And_Kill, Sub_Ledger, Auto_Receive, Active, Notify_AP, Return_Repair, PO_Live_Months, PO_History_Months, "
                             + "No_Voucher, Profile_Id, Not_Exceed_Header, one_req_per_po, Service_Contract, Auto_Line_Rec FROM PoType ORDER BY Po_Type";
            using (SqlDataReader reader = _Com.ExecuteReader())
            {
                while (reader.Read())
                {
                    ret.Add(new ComboBoxString(reader[0].ToString(), new ComboBoxPoType("", reader[1].ToNonNullString(), reader[2].ToBoolean(), reader[3].ToBoolean(), reader[4].ToBoolean(), reader[5].ToBoolean(),
                                               reader[6].ToBoolean(), reader[7].ToBoolean(), reader[8].ToBoolean(), reader[9].ToBoolean(), reader[10].ToBoolean(), reader[11].ToBoolean(), reader[12].ToInt32(),
                                               reader[13].ToInt32(), reader[14].ToBoolean(), reader[15].ToBoolean(), reader[16].ToBoolean(), reader[17].ToBoolean(), reader[18].ToBoolean(), reader[19].ToBoolean())));
                }
            }
            Close();
            return ret;
        }

        public List<ComboBoxString> getShipTo()
        {
            List<ComboBoxString> ret = new List<ComboBoxString>();
            Open();
            _Com.Parameters.Clear();
            ret.Add(new ComboBoxString("<NONE>"));

            _Com.CommandText = "SELECT Ship_To, Name, Address1, Address2, Address3, City, State, Zip, Phone_No, Fax_No, Entity, GLN_Number FROM ShipTo WHERE Active = 1 ORDER BY Ship_To";
            using (SqlDataReader reader = _Com.ExecuteReader())
            {
                while (reader.Read())
                {
                    ret.Add(new ComboBoxString(reader[0].ToString(), new ComboBoxShipTo("", reader[1].ToNonNullString(), reader[2].ToNonNullString(), reader[3].ToNonNullString(), reader[4].ToNonNullString(),
                                               reader[5].ToNonNullString(), reader[6].ToNonNullString(), reader[7].ToNonNullString(), reader[8].ToNonNullString(), reader[9].ToNonNullString(), true,
                                               reader[10].ToNonNullString(), reader[11].ToNonNullString())));
                }
            }
            Close();
            return ret;
        }

        public List<ComboBoxString> prefillCombos(string Choice, string parameter)
        {
            List<ComboBoxString> ret = new List<ComboBoxString>();
            Open();
            _Com.Parameters.Clear();
            switch (Choice)
            {
                case "Vat":
                    {
                        _Com.CommandText = "SELECT VAT_Code, Percentage FROM Vat ORDER BY Vat_Code";
                        ret.Add(new ComboBoxString("None"));
                        break;
                    }
                case "Mat":
                    {
                        // _Com.CommandText = "SELECT DISTINCT IV.Mat_Code, IMF.Description1 FROM IMF JOIN ItemVend IV ON IV.Mat_Code = IMF.Mat_Code WHERE IMF.Active = 1 AND "
                        //                  + "IV.Active = 1 AND IV.Vendor_Id = @Vendor ORDER BY IV.Mat_Code";
                        _Com.CommandText = "SELECT DISTINCT IV.Mat_Code, IMF.Description1 FROM IMF JOIN ItemVend IV ON IV.Mat_Code = IMF.Mat_Code LEFT JOIN LOC ON LOC.Mat_Code = IMF.Mat_Code WHERE IMF.Active = 1 AND IV.Active = 1 AND LOC.Active = 1 AND "
                                         + "IV.Vendor_Id = @Vendor ORDER BY IV.Mat_Code";
                        _Com.Parameters.AddWithValue("Vendor", parameter);
                        break;
                    }
                case "MatDetail":
                    {
                        _Com.CommandText = "SELECT Mat_Code, Description1 FROM PoDetail WHERE Po_No = @PoNo ORDER BY Item_Count";
                        _Com.Parameters.AddWithValue("PoNo", parameter);
                        break;
                    }
                case "MatNotVendor":
                    {
                        _Com.CommandText = "SELECT DISTINCT IV.Mat_Code, IMF.Description1 FROM IMF JOIN ItemVend IV ON IV.Mat_Code = IMF.Mat_Code LEFT JOIN LOC ON LOC.Mat_Code = IMF.Mat_Code WHERE IMF.Active = 1 AND IV.Active = 1 AND LOC.Active = 1 AND "
                                         + "IV.Vendor_Id <> @Vendor ORDER BY IV.Mat_Code";
                        _Com.Parameters.AddWithValue("Vendor", parameter);
                        break;
                    }
                case "Deliver":
                    {
                        _Com.CommandText = "SELECT DISTINCT Deliver_To, Description FROM DeliverTo WHERE Active = 1 AND Entity = @entity ORDER BY Deliver_To ";
                        _Com.Parameters.AddWithValue("entity", parameter);
                        break;
                    }
                case "VendorCat":
                    {
                        _Com.CommandText = "SELECT DISTINCT IV.Vendor_Catalog, IMF.Description1 FROM IMF JOIN ItemVend IV ON IV.Mat_Code = IMF.Mat_Code WHERE IV.Active = 1 AND IV.Active = 1 AND IV.Vendor_Id = @Vendor ORDER BY Vendor_Catalog";
                        _Com.Parameters.AddWithValue("Vendor", parameter);
                        break;
                    }
                case "VendorCatNotVendor":
                    {
                        _Com.CommandText = "SELECT DISTINCT IV.Vendor_Catalog, IMF.Description1 FROM IMF JOIN ItemVend IV ON IV.Mat_Code = IMF.Mat_Code WHERE IV.Active = 1 AND IV.Active = 1 AND IV.Vendor_Id <> @Vendor ORDER BY Vendor_Catalog";
                        _Com.Parameters.AddWithValue("Vendor", parameter);
                        break;
                    }
                case "MfgCat":
                    {
                        _Com.CommandText = "SELECT DISTINCT IV.Mfg_Catalog, IMF.Description1 FROM IMF JOIN ItemVend IV ON IV.Mat_Code = IMF.Mat_Code WHERE IV.Active = 1 AND IV.Active = 1 AND IV.Vendor_Id = @Vendor ORDER BY Mfg_Catalog";
                        _Com.Parameters.AddWithValue("Vendor", parameter);
                        break;
                    }
                case "MfgCatNotVendor":
                    {
                        _Com.CommandText = "SELECT DISTINCT IV.Mfg_Catalog, IMF.Description1 FROM IMF JOIN ItemVend IV ON IV.Mat_Code = IMF.Mat_Code WHERE IV.Active = 1 AND IV.Active = 1 AND IV.Vendor_Id <> @Vendor ORDER BY Mfg_Catalog";
                        _Com.Parameters.AddWithValue("Vendor", parameter);
                        break;
                    }
                case "Mfg_Name":
                    {
                        _Com.CommandText = "SELECT DISTINCT Mfg_Name, Mfg_Id FROM Manufacturer ORDER BY Mfg_Id";
                        break;
                    }
                case "Catalog":
                    {
                        _Com.CommandText = "SELECT Catalog, Description FROM CatalogHeader WHERE active = 1 ORDER BY Catalog";
                        break;
                    }
            }
            using (SqlDataReader reader = _Com.ExecuteReader())
            {
                while (reader.Read())
                    ret.Add(new ComboBoxString(reader[0].ToString(), reader[1].ToString()));
            }
            return ret;
        }

        public List<ComboBoxString> GetLocation(string Entity, string Mat_Code)
        {
            List<ComboBoxString> ret = new List<ComboBoxString>();
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT Loc.Location, L.Description FROM Location L JOIN Loc ON L.Location = Loc.location JOIN Usertolocation ul ON ul.Location = L.location WHERE Mat_Code = "
                             + "@Mat_Code AND Loc.Location <> '' AND ul.username = @user AND Loc.Active = 1 ";
            if (SameEntityInLoc)
                _Com.CommandText += "AND Loc.Entity = @Entity ";
            else
            {
                _Com.CommandText += "AND Loc.Entity in (SELECT DISTINCT Entity FROM Loc WHERE Mat_Code = @Mat_Code AND ((entity = @Entity) OR (Type = 'N'))) ";
            }
            _Com.CommandText += "ORDER BY Loc.Location";
            _Com.Parameters.AddWithValue("user", this.SqlUsername);
            _Com.Parameters.AddWithValue("Entity", Entity);
            _Com.Parameters.AddWithValue("Mat_Code", Mat_Code);
            using (SqlDataReader reader = _Com.ExecuteReader())
            {
                while (reader.Read())
                    ret.Add(new ComboBoxString(reader[0].ToString(), reader[1].ToString()));
            }
            return ret;
        }

        public List<ComboBoxString> GetAct(string Entity)
        {
            List<ComboBoxString> temp = new List<ComboBoxString>();
            Open();
            _Com.Parameters.Clear();
            if (SystemOptionsDictionary["USE_USERTODEPT"].ToBoolean())
            {
                _Com.CommandText = "SELECT DISTINCT AN.Account_No, SA.Sub_Account_Name, DEP.Department_Name, AN.Department, AN.Sub_Account, AN.Entity, AN.Bank_Account, AN.Accrual, AN.Active, "
                                 + "AN.Default_Deliver_To, AN.Balance_Sheet_Account FROM AccountNo AN JOIN SubAccount SA ON AN.Sub_Account = SA.Sub_Account JOIN Department DEP ON DEP.Department = "
                                 + "AN.Department AND DEP.Entity = AN.Entity JOIN UserToDepartment U ON U.Entity = AN.Entity AND U.Department = DEP.Department AND U.username = @User WHERE AN.Entity = @entity "
                                 + "AND DEP.Entity = @entity AND DEP.active = 1 AND AN.Active = 1 ORDER BY AN.Account_No";
                _Com.Parameters.AddWithValue("User", this.SqlUsername);
            }
            else
            {
                _Com.CommandText = "SELECT DISTINCT AN.Account_No, SA.Sub_Account_Name, DEP.Department_Name, AN.Department, AN.Sub_Account, AN.Entity, AN.Bank_Account, AN.Accrual, AN.Active, "
                                 + "AN.Default_Deliver_To, AN.Balance_Sheet_Account FROM AccountNo AN JOIN SubAccount SA ON AN.Sub_Account = SA.Sub_Account JOIN Department DEP ON DEP.Department = AN.Department "
                                 + "AND DEP.Entity = AN.Entity WHERE AN.Entity = @entity AND DEP.Entity = @entity AND DEP.active = 1 AND AN.Active = 1 ORDER BY AN.Account_No";
            }
            _Com.Parameters.AddWithValue("Entity", Entity);
            using (SqlDataReader reader = _Com.ExecuteReader())
            {
                while (reader.Read())
                {
                    temp.Add(new ComboBoxString(reader[0].ToNonNullString(), new AccountNo(Entity, reader[3].ToNonNullString(), reader[4].ToNonNullString(), "", reader[6].ToNonNullString(),
                                                reader[7].ToBoolean(), reader[8].ToBoolean(), reader[9].ToNonNullString(), reader[10].ToBoolean())));
                }
            }
            Close();
            return temp;
        }
        public List<ComboBoxString> GetPoClass()
        {
            List<ComboBoxString> ret = new List<ComboBoxString>();
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT Po_Class, Description, Capitation FROM PoClass ORDER BY Po_Class";
            using (SqlDataReader reader = _Com.ExecuteReader())
            {
                while (reader.Read())
                    ret.Add(new ComboBoxString(reader[0].ToString(), new ComboBoxPoClass("", reader[1].ToString(), reader[2].ToBoolean())));
            }
            return ret;
        }
        public List<ComboBoxString> GetProfileId(char Type, string Entity, string FieldTwo)
        {
            List<ComboBoxString> ret = new List<ComboBoxString>();
            Open();
            _Com.Parameters.Clear();

            switch (Type)
            {
                case 'A':
                    {
                        _Com.CommandText = "SELECT p.Profile_Id, p.Description FROM ProfileId p JOIN accountnotoprofileid atp ON atp.Profile_Id = p.Profile_Id WHERE atp.account_no = @account_no AND "
                                         + "atp.entity = @entity AND p.active = 1 ORDER BY p.Profile_Id";
                        _Com.Parameters.AddWithValue("entity", Entity);
                        _Com.Parameters.AddWithValue("Account_No", FieldTwo);
                        break;
                    }
                case 'G':
                    {
                        _Com.CommandText = "SELECT Profile_Id, Description FROM ProfileId WHERE department = @dept and entity = @entity AND active = 1 ORDER BY Profile_Id";
                        _Com.Parameters.AddWithValue("entity", Entity);
                        _Com.Parameters.AddWithValue("dept", FieldTwo);
                        break;
                    }
                default:
                    {
                        _Com.CommandText = "SELECT Profile_Id, Description FROM ProfileId WHERE active = 1 ORDER BY Profile_Id";
                        break;
                    }
            }
            using (SqlDataReader reader = _Com.ExecuteReader())
            {
                while (reader.Read())
                    ret.Add(new ComboBoxString(reader[0].ToString(), reader[1].ToString()));
            }
            return ret;
        }

        /*
        public string VendorCombos()
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
                                      + " or (non_material_vendor is null)) and Active = 1 Order By Vendor_Id";
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
        }*/

        #endregion

        public Dictionary<string, string> SystemOptionsDictionary = new Dictionary<string, string>();
        public void GetSystemOptions()
        {
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT Name, Value FROM sysfile ";
            using (SqlDataReader reader = _Com.ExecuteReader())
            {
                while (reader.Read())
                    SystemOptionsDictionary[(string)reader[0]] = (string)reader[1];
            }
            Close();
        }

        public void GetUserOptions(Form1 MainForm)
        {
            MainForm.ENABLE_ADD_ITEMS = SystemOptionsDictionary["ENABLE_ADD_ITEMS"] == "Y";

            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT Can_View_Patient_Memo, PoEntry_Allow_Update_Contract, Can_Insert_MFG, poentry_dollar_limit, PoEntry_Can_Add_Items FROM Users WHERE Username = @Username";
            _Com.Parameters.AddWithValue("Username", SqlUsername);
            using (SqlDataReader q_user = _Com.ExecuteReader())
            {
                if (q_user.HasRows)
                {
                    q_user.Read();
                    MainForm.CanViewPatientMemo = q_user[0].ToBoolean();
                    MainForm.ALLOW_UPDATE_CONTRACT = q_user[1].ToBoolean();
                    MainForm.Can_Insert_MFG = q_user[2].ToBoolean();
                    MainForm.poentry_dollar_limit = q_user[3].ToDecimal();
                    if (MainForm.ENABLE_ADD_ITEMS == false)
                        MainForm.ENABLE_ADD_ITEMS = q_user[4].ToBoolean();
                }
            }
            Close();
        }

        public Int32 GetPoGroup()
        {
            Int32 temp = 0;
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT poGroup_Id FROM usertopogroups WHERE username = @username ";
            _Com.Parameters.AddWithValue("@username", SqlUsername);
            if (SystemOptionsDictionary["MUST_USE_DEFAULT_PO_GROUP"].ToBoolean())
                _Com.CommandText += "AND default_group = 1";
            temp = _Com.ExecuteScalar().ToInt32();
            Close();
            return temp;
        }

        public void Fillcmb_POGroups()
        {

        }

        public string GetSplit(string Entity)
        {
            string ret = "";
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT Split_Account FROM ApSystem WHERE Entity = @Entity";
            _Com.Parameters.AddWithValue("Entity", Entity);
            using (SqlDataReader Reader = _Com.ExecuteReader())
            {
                Reader.Read();
                if (Reader.HasRows)
                    ret = Reader[0].ToNonNullString();

            }
            Close();
            return ret;
        }

        #region Procedures
        public Int32 GetQuantityReceived(decimal po)
        {
            Int32 temp = 0;
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT qty_received FROM podetail WHERE qty_received > 0 AND po_no = @po_no";
            _Com.Parameters.AddWithValue("po_no", po);
            temp = _Com.ExecuteScalar().ToInt32();
            Close();
            return temp;
        }
        #region Invoice
        public Int32 GetInvoiceReceived(decimal po)
        {
            Int32 temp = 0;
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT qty_on_invoice FROM podetail WHERE qty_received > 0 AND po_no = @po_no";
            _Com.Parameters.AddWithValue("po_no", po);
            temp = _Com.ExecuteScalar().ToInt32();
            Close();
            return temp;
        }
        public Int32 GetInvoiced(decimal po)
        {
            Int32 temp = 0;
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT COUNT(po_no) FROM apheader WHERE cancelled = 0 AND posted_void = 0 AND po_no = @po_no";
            _Com.Parameters.AddWithValue("po_no", po);
            temp = _Com.ExecuteScalar().ToInt32();
            Close();
            return temp;
        }
        #endregion
        public bool GetMinOrder(out decimal Min_Order, out decimal Min_Order_Fee, string Vendor_Id, string Entity, string table)
        {
            bool ret = false;
            Int32 temp = 0;
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT Min_Order, Min_Order_Fee FROM " + table + " WHERE Vendor_Id = @Vendor_Id ";
            _Com.Parameters.AddWithValue("Vendor_Id", Vendor_Id);
            if (table == "VendorMinOrder")
            {
                _Com.CommandText += "AND Entity = @Entity";
                _Com.Parameters.AddWithValue("Entity", Entity);
            }

            using (SqlDataReader reader = _Com.ExecuteReader())
            {
                reader.Read();
                Min_Order = (reader.HasRows) ? reader[0].ToDecimal() : 0m;
                Min_Order_Fee = (reader.HasRows) ? reader[1].ToDecimal() : 0m;
                ret = !reader.HasRows;
            }
            Close();
            return ret;
        }

        public bool GetClosed(decimal Po)
        {
            bool temp = false;
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT closed FROM poheader WHERE po_no = @po_no";
            _Com.Parameters.AddWithValue("po_no", Po);
            temp = _Com.ExecuteScalar().ToBoolean();
            Close();
            return temp;
        }

        public bool HasImage(decimal Req)
        {
            bool temp = false;
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT Has_Image FROM ReqHeader WHERE Req_No = @Req_No";
            _Com.Parameters.AddWithValue("Req_No", Req);
            temp = _Com.ExecuteScalar().ToBoolean();
            Close();
            return temp;
        }

        public string GetDefaultDeliverTo(string Entity, string Account_No)
        {
            string temp = "";
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT A.Default_Deliver_To, D.Default_Deliver_To FROM AccountNo A JOIN Department D ON D.Entity = A.Entity and D.department = A.Department WHERE A.Entity = @Entity AND "
                             + "A.Account_No = @Account_No";
            _Com.Parameters.AddWithValue("Entity", Entity);
            _Com.Parameters.AddWithValue("Account_No", Account_No);
            using (SqlDataReader reader = _Com.ExecuteReader())
            {
                reader.Read();
                if (reader.HasRows)
                {
                    temp = reader[0].ToNonNullString();
                    if (temp.Trim() == "")
                        temp = reader[1].ToNonNullString();
                }
            }
            return temp;
        }

        public int IncSys(string SystemOptionValue)
        {
            int hold = 0;
            Open();
            hold = EHS.Orders.IncNumber(ref _Com, SystemOptionValue);
            Close();
            return hold;
        }

        public bool GetNonFile(decimal Po_No)
        {
            bool temp = false;
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT Nonfile FROM PoDetail WHERE Nonfile = 0 AND Po_No = @Po_No";
            _Com.Parameters.AddWithValue("po_no", Po_No);
            using (SqlDataReader reader = _Com.ExecuteReader())
                temp = reader.HasRows;
            Close();
            return temp;
        }

        public bool GetVoucher(decimal Po_No)
        {
            bool temp = false;
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT voucher FROM apheader WHERE Po_No = @Po_No AND Cancelled = 0";
            _Com.Parameters.AddWithValue("po_no", Po_No);
            using (SqlDataReader reader = _Com.ExecuteReader())
                temp = reader.HasRows;
            Close();
            return temp;
        }

        public decimal GetNextPoNo(string table, string parameter)
        {
            decimal temp = 0m;
            Open();
            _Com.Parameters.Clear();
            switch (table)
            {
                case "Entity":
                    {
                        _Com.CommandText = "SELECT MIN(Next_Po_Number) FROM ENTITY E LEFT OUTER JOIN PoHeader P ON E.Next_Po_Number = P.Po_No LEFT OUTER JOIN PoHeaderHistory PH ON E.Next_Po_Number = PH.Po_No "
                                         + "WHERE P.Po_No is null AND PH.Po_No is null AND E.Entity = @Entity";
                        _Com.Parameters.AddWithValue("Entity", parameter);
                        break;
                    }
                case "Pogroups":
                    {
                        _Com.CommandText = "SELECT MIN(Next_Po_No) FROM pogroups E LEFT OUTER JOIN PoHeader P ON E.Next_Po_No = P.Po_No LEFT OUTER JOIN PoHeaderHistory PH ON E.Next_Po_No = PH.Po_No "
                                         + "WHERE P.Po_No is null AND PH.Po_No is null AND pogroup_id = @group_id";
                        _Com.Parameters.AddWithValue("group_id", parameter);
                        break;
                    }
                case "SysFile":
                    {
                        _Com.CommandText = "SELECT MIN(Value) FROM sysfile E LEFT OUTER JOIN PoHeader P ON E.Value = P.Po_No LEFT OUTER JOIN PoHeaderHistory PH ON E.Value = PH.Po_No WHERE P.Po_No is null AND "
                                         + "PH.Po_No is null AND Name = 'NEXT_PO_NUMBER'";
                        break;
                    }
            }
            temp = _Com.ExecuteScalar().ToDecimal();
            temp = (temp == 0) ? 1 : temp;

            switch (table)
            {
                case "Entity":
                    {
                        _Com.CommandText = "UPDATE ENTITY SET Next_Po_Number = @Next WHERE Entity = @Entity";
                        break;
                    }
                case "Pogroups":
                    {
                        _Com.CommandText = "UPDATE pogroups SET Next_Po_No = @Next WHERE pogroup_id = @group_id";
                        break;
                    }
                case "SysFile":
                    {
                        _Com.CommandText = "UPDATE sysfile SET Value = @Next WHERE Name = 'NEXT_PO_NUMBER'";
                        break;
                    }
            }
            _Com.Parameters.AddWithValue("Next", temp + 1);
            _Com.ExecuteNonQuery();
            Close();
            return temp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projno"></param>
        /// <returns>CerId</returns>
        public int GetCerId(string projno)
        {
            int temp = 0;
            if (projno.Trim() == "")
                return 0;
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT cer_id FROM cer WHERE cer_no = @cer_no AND cer_no <> '' ";
            _Com.Parameters.AddWithValue("cer_no", projno);
            temp = _Com.ExecuteScalar().ToInt32();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT cer_id FROM capreq WHERE grant_no = @grant_no AND grant_no <> '' ";
            _Com.Parameters.AddWithValue("cer_no", projno);
            temp = (temp > 0) ? temp : _Com.ExecuteScalar().ToInt32();
            Close();
            return temp;
        }

        public string GetReqNo(string req_no)
        {
            string temp = "";
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT TOP 1 po_no FROM poheader WHERE req_no = @req_no UNION SELECT TOP 1 po_no FROM poheaderhistory WHERE req_no = @req_no";
            _Com.Parameters.AddWithValue("req_no", req_no);
            temp = _Com.ExecuteScalar().ToNonNullString();
            Close();
            return temp;
        }

        public string GetVendorAccount(string VendorId, string ShipTo)
        {
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT Vendor_Account FROM VmShip WHERE Vendor_Id = @Vendor_Id AND Ship_To = @Ship_To AND Vendor_Account <> '' AND Vendor_Account is not null";
            _Com.Parameters.AddWithValue("Vendor_Id", VendorId);
            _Com.Parameters.AddWithValue("Ship_To", ShipTo);
            var temp = _Com.ExecuteScalar().ToNonNullString();
            Close();
            return temp;
        }

        public bool GetActive(string Mat, string Vendor)
        {
            bool temp = false;
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT Active FROM ItemVend WHERE Mat_Code = @Mat_Code AND Vendor_Id = @Vendor_Id";
            _Com.Parameters.AddWithValue("Mat_Code", Mat);
            _Com.Parameters.AddWithValue("Vendor_Id", Vendor);
            temp = _Com.ExecuteScalar().ToBoolean();
            Close();
            return temp;
        }

        public IMF FillImf(string MatCode, string Vendor, string UOP)
        {
            IMF ret = null;

            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT TOP 1 UOP.Vendor_Catalog, UOP.Mfg_Catalog, UOP.Mfg_Name, IMF.Description1, UOP.Unit_Purchase, UOP.Conversion, UOP.Po_Cost, IMF.Buyer, IMF.Po_Class, UOP.Average_Lead_Time, "
                             + "IV.Contract, IMF.Description2 FROM IMF, itemvend iv, UOP WHERE IMF.Mat_Code = @Mat_Code AND IV.Mat_Code = IMF.Mat_Code AND IV.Vendor_Id = @Vendor_Id AND UOP.Mat_Code = "
                             + "IMF.Mat_Code AND UOP.vendor_Id = IV.Vendor_Id ";
            if (UOP.Trim() == "")
                _Com.CommandText += "AND uop.Default_UOP = 1 ";
            else
            {
                _Com.CommandText += "AND uop.Unit_Purchase = @uop";
                _Com.Parameters.AddWithValue("uop", UOP);
            }
            _Com.Parameters.AddWithValue("Mat_Code", MatCode);
            _Com.Parameters.AddWithValue("Vendor_Id", Vendor);
            using (SqlDataReader read = _Com.ExecuteReader())
            {
                if (read.HasRows)
                {
                    read.Read();
                    ret = new IMF(read[0].ToNonNullString(), read[1].ToNonNullString(), read[2].ToNonNullString(), read[3].ToNonNullString(), read[4].ToNonNullString(), 
                                  read[5].ToNonNullString(), read[6].ToNonNullString(), read[7].ToNonNullString(), read[8].ToNonNullString(), read[9].ToInt64(), 
                                  read[10].ToNonNullString(), read[11].ToNonNullString());
                }
            }
            if (ret == null)
            {
                _Com.Parameters.Clear();
                _Com.CommandText = "SELECT TOP 1 UOP.Vendor_Catalog, UOP.Mfg_Catalog, UOP.Mfg_Name, IMF.Description1, UOP.Unit_Purchase, UOP.Conversion, UOP.Po_Cost, IMF.Buyer, IMF.Po_Class, "
                                 + "UOP.Average_Lead_Time, IV.Contract, IMF.Description2 FROM IMF, itemvend IV, UOP WHERE IMF.Mat_Code = @Mat_Code AND IV.Mat_Code = IMF.Mat_Code AND IV.Main_Vendor = 1 AND "
                                 + "UOP.Mat_Code = IMF.Mat_Code AND UOP.vendor_Id = IV.Vendor_Id AND uop.Default_UOP = 1";
                _Com.Parameters.AddWithValue("Mat_Code", MatCode);
                using (SqlDataReader read2 = _Com.ExecuteReader())
                {
                    if (read2.HasRows)
                    {
                        read2.Read();
                        ret = new IMF(read2[0].ToString(), read2[1].ToString(), read2[2].ToString(), read2[3].ToString(), read2[4].ToString(), read2[5].ToString(), read2[6].ToString(), read2[7].ToString(),
                                      read2[8].ToString(), read2[9].ToInt64(), read2[10].ToString(), read2[11].ToNonNullString());
                    }
                    else
                        ret = null;
                }
            }
            Close();
            return ret;
        }

        public string GetVatPercentage(string VatCode)
        {
            string temp = "";
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT Percentage FROM Vat WHERE Vat_Code = @VatCode";
            _Com.Parameters.AddWithValue("VatCode", VatCode);
            temp = _Com.ExecuteScalar().ToNonNullString();
            Close();
            return temp;
        }

        public List<ComboBoxString> GetLoc(string Mat)
        {
            List<ComboBoxString> temp = new List<ComboBoxString>();
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT LOC.Location, LOC.Vat_Code FROM Loc JOIN UserToLocation UL ON UL.Location = LOC.Location WHERE Mat_Code = @Mat_Code and Active = 1 AND username = @username";
            _Com.Parameters.AddWithValue("Mat_Code", Mat);
            _Com.Parameters.AddWithValue("username", SqlUsername);
            using (SqlDataReader Read = _Com.ExecuteReader())
            {
                while (Read.Read())
                {
                    temp.Add(new ComboBoxString(Read[0].ToNonNullString(), Read[1].ToNonNullString()));
                }
            }
            Close();
            return temp;
        }

        #endregion Procedures

        public LocationDetail getLoc(string Mat_Code, string Location, string Entity)
        {
            LocationDetail ret = new LocationDetail();

            Open();
            try
            {
                _Com.Parameters.Clear();
                _Com.CommandText = "SELECT Mat_Code, Loc.Location, Patient_Charge_Number, On_Hand, Minimum, Maximum, Reorder_Point, Entity, Account_No, Type, Patient_Cost, Reorder_Location, Bin, Issue_Cost, "
                                 + "On_Order, Sub_Account, Average_Cost, ABC, Reorder_Quantity, Stockout_Quantity, Last_Activity_Date, Active, Memo, Entry_Date, Reorder_Override, Stockless, Fill_and_Kill, "
                                 + "Patient_Charge, Overnight, Vat_Code, Exclude_ROQ, Exclude_ABC, Substitute_Item, Count_Code, Interface_Flag, Bin2, Bin3, Average_Daily_Usage, DOQ, Floor_Stock, Exclude_OLR, "
                                 + "Alias_Item, Alias_Description, Last_Ordered_Date, Phase_Out, Additional_Qty_To_Order, Critical_Item, Original_Consignment_Quantity, Issue_On_Order_Req, "
                                 + "Additional_Qty_To_Order_Memo, Dont_Update_Issue_Cost, Entered_By, Interface_Previous_On_Hand, Deactivated_Date, Last_Counted_Date, Print_Barcode_On_Receipt, "
                                 + "Print_Barcode_On_Transfer, Implant, Max_Counts FROM Loc JOIN usertolocation ul ON ul.Location = LOC.Location WHERE Mat_Code = @Mat_Code AND Username = @username AND "
                                 + "LOC.Location = @Location AND LOC.Active = 1 ";
                if (SameEntityInLoc)
                    _Com.CommandText += "AND Entity = @Entity ";
                else
                    _Com.CommandText += "AND LOC.Entity in (SELECT DISTINCT Entity FROM loc WHERE mat_code = @mat_code AND ((entity = @entity) or (type = 'N'))) ";
                _Com.Parameters.AddWithValue("Mat_Code", Mat_Code);
                _Com.Parameters.AddWithValue("username", SqlUsername);
                _Com.Parameters.AddWithValue("Location", Location);
                _Com.Parameters.AddWithValue("Entity", Entity);
                using (SqlDataReader reader = _Com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ret = new LocationDetail(reader[0].ToNonNullString(), reader[1].ToNonNullString(), reader[2].ToNonNullString(), reader[3].ToDecimal(), reader[4].ToDecimal(), reader[5].ToDecimal(),
                                                 reader[6].ToDecimal(), reader[7].ToNonNullString(), reader[8].ToNonNullString(), reader[9].ToNonNullString(), reader[10].ToDecimal(), reader[11].ToNonNullString(),
                                                 reader[12].ToNonNullString(), reader[13].ToDecimal(), reader[14].ToDecimal(), reader[15].ToNonNullString(), reader[16].ToDecimal(), reader[17].ToNonNullString(),
                                                 reader[18].ToDecimal(), reader[19].ToDecimal(), reader[20].ToDateTime(), reader[21].ToBoolean(), reader[22].ToNonNullString(), reader[23].ToDateTime(),
                                                 reader[24].ToBoolean(), reader[25].ToBoolean(), reader[26].ToBoolean(), reader[27].ToBoolean(), reader[28].ToBoolean(), reader[29].ToNonNullString(),
                                                 reader[30].ToBoolean(), reader[31].ToBoolean(), reader[32].ToNonNullString(), reader[33].ToNonNullString(), reader[34].ToNonNullString(),
                                                 reader[35].ToNonNullString(), reader[36].ToNonNullString(), reader[37].ToDecimal(), reader[38].ToBoolean(), reader[39].ToBoolean(), reader[40].ToBoolean(),
                                                 reader[41].ToNonNullString(), reader[42].ToNonNullString(), reader[43].ToDateTime(), reader[44].ToBoolean(), reader[45].ToDecimal(), reader[46].ToBoolean(),
                                                 reader[47].ToDecimal(), reader[48].ToBoolean(), reader[49].ToNonNullString(), reader[50].ToBoolean(), reader[51].ToNonNullString(), reader[52].ToDecimal(),
                                                 reader[53].ToDateTime(), reader[54].ToDateTime(), reader[55].ToBoolean(), reader[56].ToBoolean(), reader[57].ToBoolean(), reader[58].ToInt32(), "");
                    }
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            Close();
            return ret;
            /*  _Com.CommandText = "SELECT Loc.Mat_Code, Loc.Location, Loc.Patient_Charge_Number, Loc.On_Hand, Loc.Minimum, Loc.Maximum, Loc.Reorder_Point, Loc.Entity, Loc.Account_No, Loc.Type, "
                   + "Loc.Patient_Cost, Loc.Reorder_Location, Loc.Bin, Loc.Issue_Cost, Loc.On_Order, Loc.Sub_Account, Loc.Average_Cost, Loc.ABC, Loc.Reorder_Quantity, Loc.Stockout_Quantity, "
                   +"Loc.Last_Activity_Date, Loc.Active, Loc.Memo, Loc.Entry_Date, Loc.Reorder_Override, Loc.Stockless, Loc.Fill_and_Kill, Loc.Patient_Charge, Loc.Overnight, Loc.Vat_Code, "
                   +"Loc.Exclude_ROQ, Loc.Exclude_ABC, Loc.Substitute_Item, Loc.Count_Code, Loc.Interface_Flag, Loc.Bin2, Loc.Bin3, Loc.Average_Daily_Usage, Loc.DOQ, Loc.Floor_Stock, "
                   +"Loc.Exclude_OLR, Loc.Alias_Item, Loc.Alias_Description, Loc.Last_Ordered_Date, Loc.Phase_Out, Loc.Additional_Qty_To_Order, Loc.Critical_Item, "
                   +"Loc.Original_Consignment_Quantity, Loc.Issue_On_Order_Req, Loc.Additional_Qty_To_Order_Memo, Loc.Dont_Update_Issue_Cost, Loc.Entered_By, Loc.Interface_Previous_On_Hand, "
                   +"Loc.Deactivated_Date, Loc.Last_Counted_Date, Loc.Print_Barcode_On_Receipt, Loc.Print_Barcode_On_Transfer, Loc.Implant, Loc.Max_Counts FROM Loc JOIN "
                   +"location ON Loc.Location = Location.Location WHERE Loc.mat_code = @mat ORDER BY Loc.location ASC";*/
        }

        public string GetDeliverTo(string Department)
        {
            string temp = "";
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT Default_Deliver_To FROM Department WHERE Department = @Department";
            _Com.Parameters.AddWithValue("Department", Department);
            temp = _Com.ExecuteScalar().ToNonNullString();
            Close();
            return temp;
        }

        public bool GetRsl(string Entity, string DeliverTo)
        {
            bool temp = false;
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT rsl FROM deliverto WHERE Entity = @Entity AND Deliver_To = @Deliver_To";
            _Com.Parameters.AddWithValue("Deliver_To", DeliverTo);
            _Com.Parameters.AddWithValue("Entity", Entity);
            temp = _Com.ExecuteScalar().ToBoolean();
            Close();
            return temp;
        }

        public ContractDetail GetContract(string Contract, string Mat_Code)
        {
            ContractDetail ret = null;
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT Contract, Vendor_Catalog, Mfg_Name, Unit_Purchase, Conversion, Purchase_Cost, Mfg_Catalog FROM ContractDetail WHERE Contract = @Contract AND Mat_Code = @Mat_Code";
            _Com.Parameters.AddWithValue("Mat_Code", Mat_Code);
            _Com.Parameters.AddWithValue("Contract", Contract);
            using (SqlDataReader reader = _Com.ExecuteReader())
            {
                while (reader.Read())
                {
                    ret = new ContractDetail(reader[0].ToNonNullString(), Mat_Code, reader[1].ToNonNullString(), reader[2].ToNonNullString(), reader[3].ToNonNullString(), reader[4].ToDecimal(),
                                             reader[5].ToDecimal(), reader[6].ToNonNullString());
                }
            }
            Close();
            return ret;
        }

        public List<UOP> GetUop(string Mat, string Vendor)
        {
            List<UOP> ret = null;
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT Unit_Purchase, Vendor_Catalog, Conversion, PO_Cost, Default_UOP, Stockless_UOP, Purchase_In_Multiples_Of FROM UOP WHERE Mat_Code = @Mat_Code AND Vendor_Id = @Vendor_Id";
            _Com.Parameters.AddWithValue("Mat_Code", Mat);
            _Com.Parameters.AddWithValue("Vendor_Id", Vendor);
            using (SqlDataReader reader = _Com.ExecuteReader())
            {
                if (reader.HasRows)
                    ret = new List<UOP>();
                while (reader.Read())
                {
                    ret.Add(new UOP(reader[0].ToNonNullString(), Mat, Vendor, reader[1].ToNonNullString(), reader[2].ToDecimal(), reader[3].ToDecimal(), reader[4].ToBoolean(), reader[5].ToBoolean(),
                                    reader[6].ToInt32()));
                }
            }
            Close();
            return ret;
        }

        public string MatchNonFile(string Vendor, string param, string Name)
        {
            //ComboBoxString ret = null;
            string ret = "";
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT Mat_Code, Unit_Purchase FROM UOP WHERE Vendor_Id = @Vendor_Id AND ";
            if (param == "Vendor_Catalog")
                _Com.CommandText += "Vendor_Catalog ";
            else
                _Com.CommandText += "Mfg_Catalog ";
            _Com.CommandText += "= @param";//            sql.add('and Mat_Code <> :Mat_Code ');order by active desc ');
            _Com.Parameters.AddWithValue("param", param);
            _Com.Parameters.AddWithValue("Vendor_Id", Vendor);
            ret = _Com.ExecuteScalar().ToNonNullString();
            Close();
            return ret;
        }

        public void UpdateItemVend()
        {
            _Com.CommandText = "UPDATE ItemVend SET ";

            _Com.CommandText += "WHERE Mat_Code = @Mat_Code AND Vendor_Id = @Vendor_Id";
            /*             
                  sql.add('update ItemVend ');
                  sql.add('set MFG_Catalog = :MFG_Catalog ');
                      sql.add('update ItemVend ');
                      sql.add('set PO_Cost = :PO_Cost ');
                  sql.add('update ItemVend ');
                  sql.add('set Conversion = :Conversion ');
         Sql.Add('Update Itemvend Set ');
          Sql.Add('Contract = :Contract ');

                        Sql.Add('Update ItemVend');
                        sql.add('set Vendor_Catalog = :Vendor_Catalog,');
                        Sql.Add(' Mfg_Catalog = :Mfg_Catalog,');
                        Sql.Add(' Unit_Purchase = :Unit_Purchase,');
                        Sql.Add(' Mfg_Name = :Mfg_Name,');
                        Sql.Add(' Conversion = :Conversion,');
                        Sql.Add(' Po_Cost = :Po_Cost ');
                        Sql.Add('where Mat_Code = :Mat_Code ');
                        Sql.Add('and Vendor_Id = :Vendor_Id');
                        
                  sql.add('update itemvend ');
                  sql.add('set Unit_Purchase = cd.unit_purchase, ');
                  sql.add('    Vendor_Catalog = cd.Vendor_Catalog, ');
                  sql.add('    Conversion = cd.Conversion, ');
                  sql.add('    MFG_Name = cd.Mfg_Name, ');
                  sql.add('    MFG_Catalog = cd.Mfg_Catalog, ');
                  sql.add('    PO_Cost = cd.Purchase_Cost ');
                  sql.add('from contractdetail cd join itemvend iv ');
                  sql.add('on cd.mat_code = iv.mat_code and cd.contract = iv.contract ');
                  sql.add('and cd.mat_code = :mat_code ');

*/
        }

        public decimal GetItemCount(decimal po_no)
        {
            decimal ret = 0m;
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT MAX(item_Count) AS Item_Count FROM podetail WHERE PO_No = @po_no";
            _Com.Parameters.AddWithValue("po_no", po_no);
            ret = _Com.ExecuteScalar().ToDecimal() + 1;
            Close();
            return ret;
        }

        public decimal GetPoTotal(decimal po_no)
        {
            decimal ret = 0m;
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT SUM(qty_order * (Unit_cost + Vat_Amount)) as Total FROM podetail WHERE Po_No = @Po_No";
            _Com.Parameters.AddWithValue("po_no", po_no);
            ret = _Com.ExecuteScalar().ToDecimal();
            Close();
            return ret;
        }

        public void UpdateHeaderFromDetail(decimal po_no, decimal item_count, decimal total, string bank_account)
        {
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "UPDATE PoHeader SET Item_Count = @Item_Count, Total = @Total, Bank_Account = @Bank WHERE Po_No = @Po_No";
            _Com.Parameters.AddWithValue("Item_Count", item_count);
            _Com.Parameters.AddWithValue("Po_No", po_no);
            _Com.Parameters.AddWithValue("Total", total);
            _Com.Parameters.AddWithValue("Bank", bank_account);
            _Com.ExecuteNonQuery();
            Close();
        }

        public void UpdateHeaderTotal(decimal po_no, decimal total)
        {
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "UPDATE PoHeader SET Total = @Total, Item_Count = (SELECT COUNT(*) FROM PoDetail WHERE Po_No = @Po_No) WHERE Po_No = @Po_No";
            _Com.Parameters.AddWithValue("Po_No", po_no);
            _Com.Parameters.AddWithValue("Total", total);
            _Com.ExecuteNonQuery();
            Close();
        }

        public void UpdateReqDetail(PoDetail pd)
        {
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "UPDATE ReqDetail SET unit_measure= @uop, conversion = @conversion, unit_cost = @unit_cost, quantity_ordered = @quantity WHERE req_no = @req_no AND mat_code = @mat_code AND "
                             + "location = @location";
            _Com.Parameters.AddWithValue("Location", pd.Location);
            _Com.Parameters.AddWithValue("Mat_Code", pd.MatCode);
            _Com.Parameters.AddWithValue("Req_No", pd.ReqNo);
            _Com.Parameters.AddWithValue("Uop", pd.UnitPurchase);
            _Com.Parameters.AddWithValue("Conversion", pd.Conversion);
            _Com.Parameters.AddWithValue("Unit_Cost", pd.UnitCost);
            _Com.Parameters.AddWithValue("Quantity", pd.QtyOrder);
            _Com.ExecuteNonQuery();
            Close();
        }

        public bool ValidateFrequency(decimal po_no, decimal Freq_Period, int Freq_Batch)
        {
            bool ret = false;
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT Count(*) FROM PODETAIL WHERE Po_No = @Po_No AND Frequency_Batch = @FBatch AND Frequency_Period >= @FPeriod AND ((Qty_On_Invoice <> 0) OR (Qty_Matched <> 0))";// order by Frequency_Period";
            _Com.Parameters.AddWithValue("po_no", po_no);
            _Com.Parameters.AddWithValue("FPeriod", Freq_Period);
            _Com.Parameters.AddWithValue("FBatch", Freq_Batch);
            ret = _Com.ExecuteScalar().ToDecimal() > 0m;
            Close();
            return ret;
        }

        public List<Frequency> GetFrequency(decimal po_no, decimal Freq_Period, int Freq_Batch)
        {
            List<Frequency> ret = null;
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT mat_code, location, item_count, qty_on_invoice, nonfile, qty_received, conversion FROM PoDetail WHERE Po_No = @Po_No AND Frequency_Batch = @Frequency_Batch AND "
                             + "Frequency_Period >= @Frequency_Period AND ((Qty_On_Invoice = 0) AND (Qty_Matched = 0)) ORDER BY Frequency_Period";
            _Com.Parameters.AddWithValue("po_no", po_no);
            _Com.Parameters.AddWithValue("FPeriod", Freq_Period);
            _Com.Parameters.AddWithValue("FBatch", Freq_Batch);
            using (SqlDataReader Reader = _Com.ExecuteReader())
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                        ret.Add(new Frequency(Reader[2].ToInt32(), Reader[3].ToInt32(), Reader[5].ToInt32(), Reader[6].ToInt32(), Reader[4].ToBoolean(), Reader[0].ToString(), Reader[1].ToString()));
                }
                else
                    ret = null;
            }
            Close();
            return ret;
        }

        public bool CanDelete(decimal po_no, decimal item_count)
        {
            bool ret = false;

            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT sum(Quantity_Received) as Amount_Received FROM Receiving WHERE Po_No = @Po_No ";
            if (item_count > 0)
            {
                _Com.CommandText += "AND Item_Count = @Item_Count";
                _Com.Parameters.AddWithValue("Item_Count", item_count);
            }
            _Com.Parameters.AddWithValue("Po_No", po_no);
            ret = _Com.ExecuteScalar().ToInt32() > 0;

            if (ret == false)
            {
                _Com.CommandText = "SELECT Qty_On_Invoice FROM podetail WHERE Po_No = @Po_NO ";
                if (item_count > 0)
                    _Com.CommandText += "AND Item_Count = @Item_Count";
                ret = _Com.ExecuteScalar().ToInt32() > 0;
            }
            return ret;
        }

        public void Delete(decimal po_no, decimal item_count)
        {
            Open();
            using (SqlTransaction trans = _Com.Connection.BeginTransaction())
            {
                _Com.Transaction = trans;
                try
                {
                    _Com.Parameters.Clear();
                    _Com.CommandText = "DELETE FROM Receiving WHERE Po_No = @Po_No ";
                    if (item_count > 0)
                    {
                        _Com.CommandText += "AND Item_Count = @Item_Count";
                        _Com.Parameters.AddWithValue("Item_Count", item_count);
                    }
                    _Com.ExecuteNonQuery();

                    _Com.CommandText = "DELETE FROM PatientPODetail WHERE po_no = @po_no ";
                    if (item_count > 0)
                        _Com.CommandText += "AND Item_Count = @Item_Count";
                    _Com.ExecuteNonQuery();

                    _Com.CommandText = "DELETE FROM PoDetailSplit WHERE Po_No = @Po_No ";
                    if (item_count > 0)
                        _Com.CommandText += "AND Item_Count = @Item_Count";
                    _Com.ExecuteNonQuery();

                    if (SystemOptionsDictionary["POENTRY_MAKE_QTY_ZERO_ON_DELETE"].ToBoolean())
                    {
                        _Com.CommandText = "UPDATE PoDetail SET qty_order = 0 WHERE Po_No = @Po_No ";
                        if (item_count > 0)
                            _Com.CommandText += "AND Item_Count = @Item_Count";
                    }
                    else
                    {
                        _Com.CommandText = "DELETE FROM PoDetail WHERE Po_No = @po_no ";
                        if (item_count > 0)
                            _Com.CommandText += "AND Item_Count = @Item_Count";
                    }
                    _Com.ExecuteNonQuery();
                    if (item_count == 0)
                        _Com.CommandText = "UPDATE PoHeader SET Cancelled = 1, Total = 0 WHERE Po_No= @Po_No";
                    /*
                    try
                    {
                        if (Detail.ReqNo != 0)
                        {
                            this.q_Command.Parameters.Clear();
                            this.q_Command.CommandText = "UPDATE ReqDetail SET PO_Cancelled = 1 WHERE Req_No= @Req_No " +
                                                         "AND Mat_Code= @Mat_Code AND Location = @Location";
                            this.q_Command.Parameters.Add("Req_No", SqlDbType.Int).Value = this.Detail.ReqNo;
                            this.q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = this.Detail.MatCode;
                            this.q_Command.Parameters.Add("Location", SqlDbType.VarChar).Value = this.Detail.Location;
                            this.q_Command.ExecuteNonQuery();
                        }
                    }

                    _Com.CommandText = "INSERT INTO PatientMemoAudit (Po_No, Item_Count, Username, Event_Type) SELECT po_no, Item_Count, @Username, @Event_Type from PatientPoDetail where Po_No = @Po_No";
                    this.q_Command.Parameters.AddWithValue("Po_No", CurrPo);
                    this.q_Command.Parameters.AddWithValue("Username", SqlUsername);
                    this.q_Command.Parameters.Add("Event_Type", SqlDbType.VarChar).Value = "D";
                    */
                    trans.Commit();
                }
                catch (Exception acte)
                {
                    trans.Rollback();
                }
            }
            Close();
        }

        public void WriteToPoDetailChange(PoDetail Detail, string RecType)
        {
            int RevisionNo = 0;
            string hBMemo = "", hVMemo = "", hRMemo = "";

            //this procedure will take a podetail record and move it to podetailchange
            /*
            if (Detail.PODate == null)
                Detail.PODate = PoDate;
            */
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT Vendor_Memo, Buyer_Memo, Receiver_Memo FROM PoDetail WHERE Po_No = @Po_No and Item_Count = @Item_Count";
            _Com.Parameters.AddWithValue("po_no", Detail.PONo);
            _Com.Parameters.AddWithValue("Item_Count", Detail.ItemCount);
            using (SqlDataReader Read = _Com.ExecuteReader())
            {
                Read.Read();
                if (Read.HasRows)
                {
                    hBMemo = Read["Buyer_memo"].ToNonNullString();
                    hVMemo = Read["Vendor_memo"].ToNonNullString();
                    hRMemo = Read["Receiver_memo"].ToNonNullString();
                }
            }

            _Com.CommandText = "SELECT max(Revision_No) AS Revision_No FROM PoDetailChange WHERE Type = @Type AND Po_No = @Po_No AND Item_Count = @Item_Count";
            _Com.Parameters.AddWithValue("Type", RecType);
            RevisionNo = _Com.ExecuteScalar().ToInt32() + 1;

            _Com.Parameters.Clear();
            _Com.CommandText = "INSERT INTO PoDetailChange (Type, Revision_No, PO_No, Item_Count, Program, Username, Entity, Account_No, Mat_Code, Location, Department, Sub_Account, Deliver_To, "
                             + "PO_Date, Vendor_ID, Qty_Order, Unit_Cost, Unit_Purchase, Conversion, Qty_Received, Qty_On_Invoice, Qty_Matched, Cost_On_Invoice, Cost_Matched, Last_Qty_Received, "
                             + "Receipt_Date, Vendor_Catalog, MFG_Catalog, MFG_Name, Description1, Description2, Po_Class, Deliver_Date, Buyer_Username, Receiver_Username, Contract, Return_No, Req_No, "
                             + "Limit_Amount, Cancelled, NonFile, Buyer_Memo, Receiver_Memo, Vendor_Memo, Frequency, Begin_Date, End_Date, Frequency_Period, Frequency_Batch, Total_Return, "
                             + "Total_Replaced, Short_Pay, Profile_ID, Vat_Amount, Vat_Percentage, Vat_Code, Substitute_Item, Date_Edited, Program_Edited, Last_Username, do_not_accrue) VALUES (@Type, "
                             + "@Revision_No, @PO_No, @Item_Count, @Program, @Username, @Entity, @Account_No, @Mat_Code, @Location, @Department, @Sub_Account, @Deliver_To, @PO_Date, @Vendor_ID, "
                             + "@Qty_Order, @Unit_Cost, @Unit_Purchase, @Conversion, @Qty_Received, @Qty_On_Invoice, @Qty_Matched, @Cost_On_Invoice, @Cost_Matched, @Last_Qty_Received, @Receipt_Date, "
                             + "@Vendor_Catalog, @MFG_Catalog, @MFG_Name, @Description1, @Description2, @Po_Class, @Deliver_Date, @Buyer_Username, @Receiver_Username, @Contract, @Return_No, @Req_No, "
                             + "@Limit_Amount, @Cancelled, @NonFile, @Buyer_Memo, @Receiver_Memo, @Vendor_Memo, @Frequency, @Begin_Date, @End_Date, @Frequency_Period, @Frequency_Batch, @Total_Return, "
                             + "@Total_Replaced, @Short_Pay, @Profile_ID, @Vat_Amount, @Vat_Percentage, @Vat_Code, @Substitute_Item, @Date_Edited, @Program_Edited, @Last_Username, @do_not_accrue)";

            _Com.Parameters.AddWithValue("Type", RecType);
            _Com.Parameters.AddWithValue("Revision_No", RevisionNo);
            _Com.Parameters.AddWithValue("po_no", Detail.PONo);
            _Com.Parameters.AddWithValue("Item_Count", Detail.ItemCount);
            _Com.Parameters.AddWithValue("Program", "PoEntry");
            _Com.Parameters.AddWithValue("Username", SqlUsername);
            _Com.Parameters.AddWithValue("Entity", Detail.Entity);
            _Com.Parameters.AddWithValue("Account_No", Detail.AccountNo);
            _Com.Parameters.AddWithValue("Mat_Code", Detail.MatCode);
            _Com.Parameters.AddWithValue("Location", Detail.Location);
            _Com.Parameters.AddWithValue("Department", Detail.Department);
            _Com.Parameters.AddWithValue("Sub_Account", Detail.SubAccount);
            _Com.Parameters.AddWithValue("Deliver_To", Detail.DeliverTo);
            _Com.Parameters.AddWithValue("Po_Date", Detail.PODate);
            _Com.Parameters.AddWithValue("Vendor_Id", Detail.VendorID);
            _Com.Parameters.AddWithValue("Qty_Order", Detail.QtyOrder);
            _Com.Parameters.AddWithValue("Unit_Cost", Detail.UnitCost);
            _Com.Parameters.AddWithValue("Unit_Purchase", Detail.UnitPurchase);
            _Com.Parameters.AddWithValue("Conversion", Detail.Conversion);
            _Com.Parameters.AddWithValue("Qty_Received", Detail.QtyReceived);
            _Com.Parameters.AddWithValue("Qty_On_Invoice", Detail.QtyOnInvoice);
            _Com.Parameters.AddWithValue("Qty_Matched", Detail.QtyMatched);
            _Com.Parameters.AddWithValue("Cost_On_Invoice", Detail.CostOnInvoice);
            _Com.Parameters.AddWithValue("Cost_Matched", Detail.CostMatched);
            _Com.Parameters.AddWithValue("Last_Qty_Received", Detail.LastQtyReceived);
            _Com.Parameters.AddWithValue("Receipt_Date", Detail.ReceiptDate.AsDbDateTime());
            _Com.Parameters.AddWithValue("Vendor_Catalog", Detail.VendorCatalog);
            _Com.Parameters.AddWithValue("MFG_Catalog", Detail.MFGCatalog);
            _Com.Parameters.AddWithValue("MFG_Name", Detail.MFGName);
            _Com.Parameters.AddWithValue("Description1", Detail.Description1);
            _Com.Parameters.AddWithValue("Description2", Detail.Description2);
            _Com.Parameters.AddWithValue("Po_Class", Detail.PoClass);
            _Com.Parameters.AddWithValue("Deliver_Date", Detail.DeliverDate.AsDbDateTime());
            _Com.Parameters.AddWithValue("Buyer_Username", Detail.BuyerUsername);
            _Com.Parameters.AddWithValue("Receiver_Username", Detail.ReceiverUsername);
            _Com.Parameters.AddWithValue("Contract", Detail.Contract);
            _Com.Parameters.AddWithValue("Return_No", Detail.ReturnNo);
            _Com.Parameters.AddWithValue("Req_No", Detail.ReqNo);
            _Com.Parameters.AddWithValue("Limit_Amount", Detail.LimitAmount);
            _Com.Parameters.AddWithValue("Cancelled", Detail.Cancelled);
            _Com.Parameters.AddWithValue("NonFile", Detail.NonFile);
            _Com.Parameters.AddWithValue("Buyer_Memo", hBMemo);
            _Com.Parameters.AddWithValue("Receiver_Memo", hRMemo);
            _Com.Parameters.AddWithValue("Vendor_Memo", hVMemo);
            _Com.Parameters.AddWithValue("Frequency", Detail.Frequency);
            _Com.Parameters.AddWithValue("Begin_Date", Detail.BeginDate.AsDbDateTime());
            _Com.Parameters.AddWithValue("End_Date", Detail.EndDate.AsDbDateTime());
            _Com.Parameters.AddWithValue("Frequency_Period", Detail.FrequencyPeriod);
            _Com.Parameters.AddWithValue("Frequency_Batch", Detail.FrequencyBatch);
            _Com.Parameters.AddWithValue("Total_Return", Detail.TotalReturn);
            _Com.Parameters.AddWithValue("Total_Replaced", Detail.TotalReplaced);
            _Com.Parameters.AddWithValue("Short_Pay", Detail.ShortPay);
            _Com.Parameters.AddWithValue("Profile_Id", Detail.ProfileID);
            _Com.Parameters.AddWithValue("Vat_Amount", Detail.VATAmount);
            _Com.Parameters.AddWithValue("Vat_Percentage", Detail.VatPercentage);
            _Com.Parameters.AddWithValue("Vat_Code", Detail.VatCode);
            _Com.Parameters.AddWithValue("Substitute_Item", Detail.SubstituteItem);
            _Com.Parameters.AddWithValue("Date_Edited", Detail.DateEdited);
            _Com.Parameters.AddWithValue("Program_Edited", Detail.ProgramEdited);
            _Com.Parameters.AddWithValue("Last_Username", Detail.LastUsername);
            _Com.Parameters.AddWithValue("do_not_accrue", Detail.DoNotAccrue);
            _Com.ExecuteNonQuery();
        }

        #region Catalog
        public bool InsertItemsToCatalog(string Catalog, string Mat_Code, string Location)
        {
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT COUNT(*) FROM CatalogDetail WHERE Catalog = @Catalog AND Mat_Code = @Mat_Code";
            _Com.Parameters.AddWithValue("Catalog", Catalog);
            _Com.Parameters.AddWithValue("Mat_Code", Mat_Code);
            if (_Com.ExecuteScalar().ToInt32() > 0)
            {
                Close();
                return false;
            }
            _Com.CommandText = "INSERT catalogdetail (catalog, location, mat_code) VALUES (@catalog, @location, @mat_code)";
            _Com.Parameters.AddWithValue("location", Location);
            _Com.ExecuteNonQuery();
            Close();
            return true;
        }
        #endregion

        public void UpdateDeliveryDate(DateTime date, decimal po_no)
        {
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "UPDATE PoDetail SET Deliver_Date = @Deliver_Date WHERE PO_NO = @po_no";
            _Com.Parameters.AddWithValue("Deliver_Date", date);
            _Com.Parameters.AddWithValue("Po_No", po_no);
            _Com.ExecuteNonQuery();
            Close();
        }

        public DataTable getDetailBreakdown(decimal po)
        {
            DataTable dt = new DataTable();
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT Item_Count, Mat_Code, Qty_Order, Unit_Cost, Qty_Order * Unit_Cost AS Sub_Total, Vat_Percentage, Unit_Cost * Qty_Order * (Vat_Percentage / 100) AS Tax_Total, "
                             +"Unit_Cost * Qty_Order * (1 + Vat_Percentage / 100) AS Total FROM podetail WHERE po_no = @po_no";
            _Com.Parameters.AddWithValue("po_no", po);
            using (SqlDataAdapter sa = new SqlDataAdapter(_Com))
            {
                sa.Fill(dt);
            }
            return dt;
        }

        public DateTime getLastEdited(decimal po_no)
        {
            DateTime ret;
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT MAX(date_edited) AS maxdate FROM podetail WHERE po_no = @po_no";
            _Com.Parameters.AddWithValue("po_no", po_no);
            ret = _Com.ExecuteScalar().ToDateTime();
            Close();
            return ret;
        }

        public string getEdiNote(decimal po_no)
        {
            string ret;
            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT edi855_NOTE FROM poheader WHERE po_no = @po_no";
            _Com.Parameters.AddWithValue("po_no", po_no);
            ret = _Com.ExecuteScalar().ToNonNullString();
            Close();
            return ret;
        }

        public string resequenceDetails(decimal po_no)
        {
            Open();

            _Com.Parameters.Clear();
            _Com.Parameters.AddWithValue("po_no", po_no);
            _Com.CommandText = "SELECT count(po_no) FROM patientpodetail WHERE po_no = @po_no";
            if (_Com.ExecuteScalar().ToInt32() > 0)
                return "Can't resequence this po. There are patient memos associated with it.";
            _Com.CommandText = "SELECT count(po_no) FROM podetailsplit WHERE po_no = @po_no";
            if (_Com.ExecuteScalar().ToInt32() > 0)
                return "Can't resequence this po. There are split lines associated with it.";
            _Com.CommandText = "SELECT count(po_no) FROM podetailchange WHERE po_no = @po_no";
            if (_Com.ExecuteScalar().ToInt32() > 0)
                return "Can't resequence this po. There are change records associated with it.";
            _Com.CommandText = "SELECT count(po_no) FROM returndetail WHERE po_no = @po_no";
            if (_Com.ExecuteScalar().ToInt32() > 0)
                return "Can't resequence this po. There are returns associated with it.";
            _Com.CommandText = "SELECT count(po_no) FROM receiving WHERE po_no = @po_no";
            if (_Com.ExecuteScalar().ToInt32() > 0)
                return "Can't resequence this po. There are receipts associated with it.";
            /*
            if (InsertDetailLineMode)
            {
                q_Command.Parameters.Clear();
                q_Command.CommandText = "SELECT * FROM podetail WHERE po_no = @po_no AND item_count >= @item_count ORDER BY item_count desc";
                q_Command.Parameters.AddWithValue("po_no", CurrPo);
                q_Command.Parameters.AddWithValue("item_count", lineno);
                using (SqlDataReader read8 = q_Command.ExecuteReader())
                {
                    while (read8.Read())
                    {
                        q_Command2.Parameters.Clear();
                        q_Command2.CommandText = "UPDATE podetail SET item_count = item_count + 1 WHERE po_no = @po_no "
                            + "AND item_count = @old_line";
                        q_Command2.Parameters.AddWithValue("po_no", CurrPo);
                        q_Command2.Parameters.Add("old_line", SqlDbType.Int).Value = read8["item_count"].ToInt32();
                        q_Command2.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                q_Command.Parameters.Clear();
                q_Command.CommandText = "SELECT * FROM podetail WHERE po_no = @po_no ORDER BY item_count";
                q_Command.Parameters.AddWithValue("po_no", CurrPo);
                using (SqlDataReader read9 = q_Command.ExecuteReader())
                {
                    i = 1;
                    while (read9.Read())
                    {
                        temp_i = read9["item_count"].ToInt32();
                        if (temp_i != i)
                        {
                            q_Command2.Parameters.Clear();
                            q_Command2.CommandText = "UPDATE podetail SET item_count = @i WHERE po_no = @po_no "
                                + "AND item_count = @old_line";
                            q_Command2.Parameters.AddWithValue("po_no", CurrPo);
                            q_Command2.Parameters.Add("old_line", SqlDbType.Int).Value = temp_i;
                            q_Command2.Parameters.Add("i", SqlDbType.Int).Value = i;
                            q_Command2.ExecuteNonQuery();
                        }
                        i++;
                    }
                }
            }
            sqlConnection6.Close();
            sqlConnection6.Dispose();
            q_Command2.Dispose();
            return true;
            */
            return "Complete";
        }

        public void UpdateFilesForSingleItem(int posneg, PoDetail Detail, PoHeader Header, bool USE_SUBLEDGER_AMOUNT)
        {/*
            decimal QuantityTimesCost, Quantity;
            DataTable Hold = new DataTable();
            int i = 0;

            QuantityTimesCost = Detail.QtyOrder * Detail.UnitCost * (decimal)posneg;
            QuantityTimesCost += (QuantityTimesCost * Detail.VatPercentage / 100m);
            Quantity = Detail.QtyOrder * Detail.Conversion * (decimal)posneg;

            Open();
            _Com.Parameters.Clear();
            _Com.CommandText = "SELECT entity, account_no, profile_id, extended_amount, vat_amount FROM PoDetailSplit WHERE po_no = @po_no AND item_count = @item_count";
            _Com.Parameters.AddWithValue("po_no", Detail.PONo);
            _Com.Parameters.AddWithValue("item_count", Detail.ItemCount);
            using (SqlDataAdapter da = new SqlDataAdapter(_Com))
            {
                da.Fill(Hold);
            }

            if (USE_SUBLEDGER_AMOUNT)
            {
                if (Header.ProjectNo != "")
                {
                    EhsUtil.Change_ProjectBudget(ref _Com, Header.ProjectNo, SystemOptionsDictionary["MM_YEAR"].ToDecimal(), 
                                                 SystemOptionsDictionary["MM_PERIOD"].ToDecimal(), QuantityTimesCost, 'E');
                }
            }
            #region//is frequency is false
            if (Is_Frequency == false)
            {
                EhsUtil.Change_VendorPurchase(ref _Com, Header.Entity, Header.VendorID, SystemOptionsDictionary["MM_YEAR"].ToDecimal(),
                                              SystemOptionsDictionary["MM_PERIOD"].ToDecimal(), QuantityTimesCost, 'D', 'P');

                if (Detail.NonFile == false)
                {
                    EhsUtil.Change_ItemUsage(ref _Com, Detail.Location, Detail.MatCode, SystemOptionsDictionary["MM_YEAR"].ToDecimal(),
                                             SystemOptionsDictionary["MM_PERIOD"].ToDecimal(), QuantityTimesCost, Quantity, 'D', 'P');
                    if (Hold.Rows.Count == 0)
                    {
                        EhsUtil.Change_ItemBudget(ref _Com, Detail.Entity, Detail.AccountNo, Detail.ProfileID, Detail.Location, Detail.MatCode, 
                                                  SystemOptionsDictionary["MM_YEAR"].ToDecimal(), SystemOptionsDictionary["MM_PERIOD"].ToDecimal(), 
                                                  QuantityTimesCost, Quantity, 'D', 'P', Cross_Account_No);
                    }
                    else
                    {
                        i = 0;
                        while (i < Hold.Rows.Count)
                        {
                            EhsUtil.Change_ItemBudget(ref _Com, Hold.Rows[i]["entity"].ToString(), Hold.Rows[i]["account_No"].ToString(), 
                                Hold.Rows[i]["Profile_Id"].ToString(),
                                Detail.Location, Detail.MatCode, SystemOptionsDictionary["MM_YEAR"].ToDecimal(), SystemOptionsDictionary["MM_PERIOD"].ToDecimal(),
                                (decimal)posneg * (Hold.Rows[i]["Extended_Amount"].ToDecimal() +
                                Hold.Rows[i]["Vat_Amount"].ToDecimal()), Quantity * (Hold.Rows[i]["percentage"].ToDecimal()
                                / 100m), 'D', 'P', Cross_Account_No);
                            i++;
                        }
                    }
                }
                if (Hold.Rows.Count == 0)
                {
                    EhsUtil.Change_Budget(ref _Com, CurrEntity, Detail.AccountNo,
                        Detail.ProfileID, SystemOptionsDictionary["MM_YEAR"].ToDecimal(), SystemOptionsDictionary["MM_PERIOD"].ToDecimal(), QuantityTimesCost, 'D', 'P',
                        Detail.MatCode, Cross_Account_No);
                }
                else
                {
                    i = 0;
                    while (i < Hold.Rows.Count)
                    {
                        EhsUtil.Change_Budget(ref _Com, Hold.Rows[i]["entity"].ToString(),
                               Hold.Rows[i]["account_No"].ToString(), Detail.ProfileID, SystemOptionsDictionary["MM_YEAR"].ToDecimal(), SystemOptionsDictionary["MM_PERIOD"].ToDecimal(), (decimal)posneg * Hold.Rows[i]["Extended_Amount"].ToDecimal() +
                               Hold.Rows[i]["Vat_Amount"].ToDecimal(), 'D', 'P', Detail.MatCode,
                               Cross_Account_No);
                        i++;
                    }
                }
            }
            #endregion
            if (Detail.NonFile == false)
            {
                Change_Inventory(Detail.QtyOrder - Detail.QtyReceived
                                    * Detail.Conversion, posneg);
                if (rsl)
                {
                    if (Hold.Rows.Count == 0)
                    {
                        EhsUtil.Change_RSLUsage(ref _Com, Detail.DeliverTo,
                            Detail.MatCode, Detail.Location, Detail.Entity,
                            Detail.AccountNo, SystemOptionsDictionary["MM_YEAR"].ToDecimal(), SystemOptionsDictionary["MM_PERIOD"].ToDecimal(), QuantityTimesCost, Quantity, 'D',
                            'P');
                    }
                    else
                    {
                        i = 0;
                        while (i < Hold.Rows.Count)
                        {
                            EhsUtil.Change_RSLUsage(ref _Com, Detail.DeliverTo,
                                Detail.MatCode, Detail.Location,
                                Hold.Rows[i]["entity"].ToString(), Hold.Rows[i]["account_No"].ToString(), SystemOptionsDictionary["MM_YEAR"].ToDecimal(), SystemOptionsDictionary["MM_PERIOD"].ToDecimal(), (decimal)posneg * (Hold.Rows[i]["Extended_Amount"].ToDecimal() +
                                Hold.Rows[i]["Vat_Amount"].ToDecimal()), Quantity * (Hold.Rows[i]["percentage"].ToDecimal()
                                / 100m), 'D', 'P');
                            i++;
                        }
                    }
                }
            }
            if (Detail.Contract != "")
            {
                if (!Return_Repair)
                {
                    EhsUtil.Change_ContractUsage(ref _Com, Detail.Contract, Detail.Location,
                           Detail.MatCode, SystemOptionsDictionary["MM_YEAR"].ToDecimal(), SystemOptionsDictionary["MM_PERIOD"].ToDecimal(), QuantityTimesCost, Quantity, 'D', 'P');
                }
            }*/
        }
    }


    public class ContractDetail
    {
        public string Mat_Code;
        public string Contract;
        public string Vendor_Catalog;
        public string Mfg_Name;
        public string Unit_Purchase;
        public decimal Conversion;
        public decimal Purchase_Cost;
        public string MFG_Catalog;

        public ContractDetail(string Contract, string Mat_Code, string Vendor_Catalog, string Mfg_Name, string Unit_Purchase, decimal Conversion, decimal Purchase_Cost, string MFG_Catalog)
        {
            this.Contract = Contract;
            this.Mat_Code = Mat_Code;
            this.Unit_Purchase = Unit_Purchase;
            this.Conversion = Conversion;
            this.Purchase_Cost = Purchase_Cost;
            this.Vendor_Catalog = Vendor_Catalog;
            this.Mfg_Name = Mfg_Name;
            this.MFG_Catalog = MFG_Catalog;
        }
    }

    public class Frequency
    {
        public int Item_Count;
        public int Qty_on_invoice;
        public int Qty_received;
        public int Conversion;
        public bool NonFile;
        public string Mat_Code;
        public string Location;

        public Frequency(int Item_Count, int Qty_on_invoice, int Qty_received, int Conversion, bool NonFile, string Mat_Code, string Location)
        {
            this.Item_Count = Item_Count;
            this.Qty_on_invoice = Qty_on_invoice;
            this.Qty_received = Qty_received;
            this.Conversion = Conversion;
            this.NonFile = NonFile;
            this.Mat_Code = Mat_Code;
            this.Location = Location;
        }
    }

    public class IMF
    {
        public string Vendor_Catalog
        {
            get;
            set;
        }
        public string Mfg_Catalog
        {
            get;
            set;
        }
        public string Mfg_Name
        {
            get;
            set;
        }
        public string Description1
        {
            get;
            set;
        }
        public string Unit_Purchase
        {
            get;
            set;
        }
        public string Conversion
        {
            get;
            set;
        }
        public string PO_Cost
        {
            get;
            set;
        }
        public string Buyer
        {
            get;
            set;
        }
        public string PO_Class
        {
            get;
            set;
        }
        public double Average_Lead_Time
        {
            get;
            set;
        }
        public string UseContract
        {
            get;
            set;
        }

        public string Description2 { get; set; }

        public IMF(string Vendor_Catalog, string Mfg_Catalog, string Mfg_Name, string Description1, string Unit_Purchase, string Conversion, string PO_Cost, string Buyer, string PO_Class, 
                   double Average_Lead_Time, string UseContract, string Description2)
        {
            this.Vendor_Catalog = Vendor_Catalog;
            this.Mfg_Catalog = Mfg_Catalog;
            this.Mfg_Name = Mfg_Name;
            this.Description1 = Description1;
            this.Unit_Purchase = Unit_Purchase;
            this.Conversion = Conversion;
            this.PO_Cost = PO_Cost;
            this.Buyer = Buyer;
            this.PO_Class = PO_Class;
            this.Average_Lead_Time = Average_Lead_Time;
            this.UseContract = UseContract;
            this.Description2 = Description2;
        }
    }

    public class UOP
    {
        public string Unit_Purchase;
        public string Mat_Code;
        public string Vendor_Id;
        public string Vendor_Catalog;
        public decimal Conversion;
        public decimal PO_Cost;
        public bool Default_UOP;
        public bool Stockless_UOP;
        public int Purchase_In_Multiples_Of;

        public UOP(string Unit_Purchase, string Mat_Code, string Vendor_Id, string Vendor_Catalog, decimal Conversion, decimal PO_Cost, bool Default_UOP, bool Stockless_UOP, int Purchase_In_Multiples_Of)
        {
            this.Unit_Purchase = Unit_Purchase;
            this.Mat_Code = Mat_Code;
            this.Vendor_Id = Vendor_Id;
            this.Vendor_Catalog = Vendor_Catalog;
            this.Conversion = Conversion;
            this.PO_Cost = PO_Cost;
            this.Default_UOP = Default_UOP;
            this.Stockless_UOP = Stockless_UOP;
            this.Purchase_In_Multiples_Of = Purchase_In_Multiples_Of;
        }
    }

    public class LocationDetail
    {
        #region Private Members
        private string m_MatCode; private string m_Location; private string m_PatientChargeNumber; private decimal m_OnHand; private decimal m_Minimum; private decimal m_Maximum; private decimal m_ReorderPoint; private string m_Entity; private string m_AccountNo; private string m_Type; private decimal m_PatientCost; private string m_ReorderLocation; private string m_Bin; private decimal m_IssueCost; private decimal m_OnOrder; private string m_SubAccount; private decimal m_AverageCost; private string m_ABC; private decimal m_ReorderQuantity; private decimal m_StockoutQuantity; private DateTime m_LastActivityDate; private bool m_Active; private string m_Memo; private DateTime m_EntryDate; private bool m_ReorderOverride; private bool m_Stockless; private bool m_FillandKill; private bool m_PatientCharge; private bool m_Overnight; private string m_VatCode; private bool m_ExcludeROQ; private bool m_ExcludeABC; private string m_SubstituteItem; private string m_CountCode; private string m_InterfaceFlag; private string m_Bin2; private string m_Bin3; private decimal m_AverageDailyUsage; private bool m_DOQ; private bool m_FloorStock; private bool m_ExcludeOLR; private string m_AliasItem; private string m_AliasDescription; private DateTime m_LastOrderedDate; private bool m_PhaseOut; private decimal m_AdditionalQtyToOrder; private bool m_CriticalItem; private decimal m_OriginalConsignmentQuantity; private bool m_IssueOnOrderReq; private string m_AdditionalQtyToOrderMemo; private bool m_DontUpdateIssueCost; private string m_EnteredBy; private decimal m_InterfacePreviousOnHand; private DateTime m_DeactivatedDate; private DateTime m_LastCountedDate; private bool m_PrintBarcodeOnReceipt; private bool m_PrintBarcodeOnTransfer; private bool m_Implant; private int m_MaxCounts; private string m_locdesc;
        #endregion
        #region Public Properties
        public string Display { get { return this.ToString(); } }
        public string MatCode { get { return m_MatCode; } set { if (value.Length > 10) throw new ArgumentException("Mat Code must be 10 characters or fewer", "value"); else m_MatCode = value; } }
        public string Location { get { return m_Location; } set { if (value.Length > 7) throw new ArgumentException("Location must be 7 characters or fewer", "value"); else m_Location = value; } }
        public string PatientChargeNumber { get { return m_PatientChargeNumber; } set { if (value.Length > 20) throw new ArgumentException("Patient Charge Number must be 20 characters or fewer", "value"); else m_PatientChargeNumber = value; } }
        public decimal OnHand { get { return m_OnHand; } set { m_OnHand = value; } }
        public decimal Minimum { get { return m_Minimum; } set { m_Minimum = value; } }
        public decimal Maximum { get { return m_Maximum; } set { m_Maximum = value; } }
        public decimal ReorderPoint { get { return m_ReorderPoint; } set { m_ReorderPoint = value; } }

        public string Entity { get { return m_Entity; } set { m_Entity = value; } }

        public string AccountNo { get { return m_AccountNo; } set { if (value.Length > 31) throw new ArgumentException("Account No must be 31 characters or fewer", "value"); else m_AccountNo = value; } }
        public string Type { get { return m_Type; } set { if (value.Length > 1) throw new ArgumentException("Type must be 1 characters or fewer", "value"); else m_Type = value; } }
        public decimal PatientCost { get { return m_PatientCost; } set { m_PatientCost = value; } }
        public string ReorderLocation { get { return m_ReorderLocation; } set { if (value.Length > 7) throw new ArgumentException("Reorder Location must be 7 characters or fewer", "value"); else m_ReorderLocation = value; } }
        public string Bin { get { return m_Bin; } set { if (value.Length > 25) throw new ArgumentException("Bin must be 25 characters or fewer", "value"); else m_Bin = value; } }
        public decimal IssueCost { get { return m_IssueCost; } set { m_IssueCost = value; } }
        public decimal OnOrder { get { return m_OnOrder; } set { m_OnOrder = value; } }
        public string SubAccount { get { return m_SubAccount; } set { if (value.Length > 15) throw new ArgumentException("Sub Account must be 15 characters or fewer", "value"); else m_SubAccount = value; } }
        public decimal AverageCost { get { return m_AverageCost; } set { m_AverageCost = value; } }
        public string ABC { get { return m_ABC; } set { if (value.Length > 1) throw new ArgumentException("ABC must be 1 characters or fewer", "value"); else m_ABC = value; } }
        public decimal ReorderQuantity { get { return m_ReorderQuantity; } set { m_ReorderQuantity = value; } }
        public decimal StockoutQuantity { get { return m_StockoutQuantity; } set { m_StockoutQuantity = value; } }
        public DateTime LastActivityDate { get { return m_LastActivityDate; } set { m_LastActivityDate = value; } }
        public bool Active { get { return m_Active; } set { m_Active = value; } }
        public string Memo { get { return m_Memo; } set { m_Memo = value; } }
        public DateTime EntryDate { get { return m_EntryDate; } set { m_EntryDate = value; } }
        public bool ReorderOverride { get { return m_ReorderOverride; } set { m_ReorderOverride = value; } }
        public bool Stockless { get { return m_Stockless; } set { m_Stockless = value; } }
        public bool FillandKill { get { return m_FillandKill; } set { m_FillandKill = value; } }
        public bool PatientCharge { get { return m_PatientCharge; } set { m_PatientCharge = value; } }
        public bool Overnight { get { return m_Overnight; } set { m_Overnight = value; } }
        public string VatCode { get { return m_VatCode; } set { if (value.Length > 3) throw new ArgumentException("Vat Code must be 3 characters or fewer", "value"); else m_VatCode = value; } }
        public bool ExcludeROQ { get { return m_ExcludeROQ; } set { m_ExcludeROQ = value; } }
        public bool ExcludeABC { get { return m_ExcludeABC; } set { m_ExcludeABC = value; } }
        public string SubstituteItem { get { return m_SubstituteItem; } set { if (value.Length > 10) throw new ArgumentException("Substitute Item must be 10 characters or fewer", "value"); else m_SubstituteItem = value; } }
        public string CountCode { get { return m_CountCode; } set { if (value.Length > 7) throw new ArgumentException("Count Code must be 7 characters or fewer", "value"); else m_CountCode = value; } }
        public string InterfaceFlag { get { return m_InterfaceFlag; } set { if (value.Length > 5) throw new ArgumentException("Interface Flag must be 5 characters or fewer", "value"); else m_InterfaceFlag = value; } }
        public string Bin2 { get { return m_Bin2; } set { if (value.Length > 25) throw new ArgumentException("Bin2 must be 25 characters or fewer", "value"); else m_Bin2 = value; } }
        public string Bin3 { get { return m_Bin3; } set { if (value.Length > 25) throw new ArgumentException("Bin3 must be 25 characters or fewer", "value"); else m_Bin3 = value; } }
        public decimal AverageDailyUsage { get { return m_AverageDailyUsage; } set { m_AverageDailyUsage = value; } }
        public bool DOQ { get { return m_DOQ; } set { m_DOQ = value; } }
        public bool FloorStock { get { return m_FloorStock; } set { m_FloorStock = value; } }
        public bool ExcludeOLR { get { return m_ExcludeOLR; } set { m_ExcludeOLR = value; } }
        public string AliasItem { get { return m_AliasItem; } set { if (value.Length > 25) throw new ArgumentException("Alias Item must be 25 characters or fewer", "value"); else m_AliasItem = value; } }
        public string AliasDescription { get { return m_AliasDescription; } set { if (value.Length > 75) throw new ArgumentException("Alias Description must be 75 characters or fewer", "value"); else m_AliasDescription = value; } }
        public DateTime LastOrderedDate { get { return m_LastOrderedDate; } set { m_LastOrderedDate = value; } }
        public bool PhaseOut { get { return m_PhaseOut; } set { m_PhaseOut = value; } }
        public decimal AdditionalQtyToOrder { get { return m_AdditionalQtyToOrder; } set { m_AdditionalQtyToOrder = value; } }
        public bool CriticalItem { get { return m_CriticalItem; } set { m_CriticalItem = value; } }
        public decimal OriginalConsignmentQuantity { get { return m_OriginalConsignmentQuantity; } set { m_OriginalConsignmentQuantity = value; } }
        public bool IssueOnOrderReq { get { return m_IssueOnOrderReq; } set { m_IssueOnOrderReq = value; } }
        public string AdditionalQtyToOrderMemo { get { return m_AdditionalQtyToOrderMemo; } set { m_AdditionalQtyToOrderMemo = value; } }
        public bool DontUpdateIssueCost { get { return m_DontUpdateIssueCost; } set { m_DontUpdateIssueCost = value; } }
        public string EnteredBy { get { return m_EnteredBy; } set { if (value.Length > 10) throw new ArgumentException("Entered By must be 10 characters or fewer", "value"); else m_EnteredBy = value; } }
        public decimal InterfacePreviousOnHand { get { return m_InterfacePreviousOnHand; } set { m_InterfacePreviousOnHand = value; } }
        public DateTime DeactivatedDate { get { return m_DeactivatedDate; } set { m_DeactivatedDate = value; } }
        public DateTime LastCountedDate { get { return m_LastCountedDate; } set { m_LastCountedDate = value; } }
        public bool PrintBarcodeOnReceipt { get { return m_PrintBarcodeOnReceipt; } set { m_PrintBarcodeOnReceipt = value; } }
        public bool PrintBarcodeOnTransfer { get { return m_PrintBarcodeOnTransfer; } set { m_PrintBarcodeOnTransfer = value; } }
        public bool Implant { get { return m_Implant; } set { m_Implant = value; } }
        public int MaxCounts { get { return m_MaxCounts; } set { m_MaxCounts = value; } }
        public string Description { get { return m_locdesc; } set { m_locdesc = value; } }
        #endregion
        #region Constructor
        public LocationDetail() { }
        public LocationDetail
        (string MatCode, string Location, string PatientChargeNumber, decimal OnHand, decimal Minimum, decimal Maximum, decimal ReorderPoint, string Entity, string AccountNo, string Type, decimal PatientCost, string ReorderLocation, string Bin, decimal IssueCost, decimal OnOrder, string SubAccount, decimal AverageCost, string ABC, decimal ReorderQuantity, decimal StockoutQuantity, DateTime LastActivityDate, bool Active, string Memo, DateTime EntryDate, bool ReorderOverride, bool Stockless, bool FillandKill, bool PatientCharge, bool Overnight, string VatCode, bool ExcludeROQ, bool ExcludeABC, string SubstituteItem, string CountCode, string InterfaceFlag, string Bin2, string Bin3, decimal AverageDailyUsage, bool DOQ, bool FloorStock, bool ExcludeOLR, string AliasItem, string AliasDescription, DateTime LastOrderedDate, bool PhaseOut, decimal AdditionalQtyToOrder, bool CriticalItem, decimal OriginalConsignmentQuantity, bool IssueOnOrderReq, string AdditionalQtyToOrderMemo, bool DontUpdateIssueCost, string EnteredBy, decimal InterfacePreviousOnHand, DateTime DeactivatedDate, DateTime LastCountedDate, bool PrintBarcodeOnReceipt, bool PrintBarcodeOnTransfer, bool Implant, int MaxCounts, string description)
        {
            this.MatCode = MatCode;
            this.Location = Location;
            this.PatientChargeNumber = PatientChargeNumber;
            this.OnHand = OnHand;
            this.Minimum = Minimum;
            this.Maximum = Maximum;
            this.ReorderPoint = ReorderPoint;
            this.Entity = Entity;
            this.AccountNo = AccountNo;
            this.Type = Type;
            this.PatientCost = PatientCost;
            this.ReorderLocation = ReorderLocation;
            this.Bin = Bin;
            this.IssueCost = IssueCost;
            this.OnOrder = OnOrder;
            this.SubAccount = SubAccount;
            this.AverageCost = AverageCost;
            this.ABC = ABC;
            this.ReorderQuantity = ReorderQuantity;
            this.StockoutQuantity = StockoutQuantity;
            this.LastActivityDate = LastActivityDate;
            this.Active = Active;
            this.Memo = Memo;
            this.EntryDate = EntryDate;
            this.ReorderOverride = ReorderOverride;
            this.Stockless = Stockless;
            this.FillandKill = FillandKill;
            this.PatientCharge = PatientCharge;
            this.Overnight = Overnight;
            this.VatCode = VatCode;
            this.ExcludeROQ = ExcludeROQ;
            this.ExcludeABC = ExcludeABC;
            this.SubstituteItem = SubstituteItem;
            this.CountCode = CountCode;
            this.InterfaceFlag = InterfaceFlag;
            this.Bin2 = Bin2;
            this.Bin3 = Bin3;
            this.AverageDailyUsage = AverageDailyUsage;
            this.DOQ = DOQ;
            this.FloorStock = FloorStock;
            this.ExcludeOLR = ExcludeOLR;
            this.AliasItem = AliasItem;
            this.AliasDescription = AliasDescription;
            this.LastOrderedDate = LastOrderedDate;
            this.PhaseOut = PhaseOut;
            this.AdditionalQtyToOrder = AdditionalQtyToOrder;
            this.CriticalItem = CriticalItem;
            this.OriginalConsignmentQuantity = OriginalConsignmentQuantity;
            this.IssueOnOrderReq = IssueOnOrderReq;
            this.AdditionalQtyToOrderMemo = AdditionalQtyToOrderMemo;
            this.DontUpdateIssueCost = DontUpdateIssueCost;
            this.EnteredBy = EnteredBy;
            this.InterfacePreviousOnHand = InterfacePreviousOnHand;
            this.DeactivatedDate = DeactivatedDate;
            this.LastCountedDate = LastCountedDate;
            this.PrintBarcodeOnReceipt = PrintBarcodeOnReceipt;
            this.PrintBarcodeOnTransfer = PrintBarcodeOnTransfer;
            this.Implant = Implant;
            this.MaxCounts = MaxCounts;
            this.m_locdesc = description;
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return Location + " - " + Description + " - " + MatCode;
        }
        #endregion
    }

}
