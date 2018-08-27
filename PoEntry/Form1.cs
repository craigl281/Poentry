using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ehs.Util;
using System.Data.SqlClient;
using Ehs.Models;
using Ehs.Controls;
using System.Linq;
using System.Threading;
using System.ComponentModel;

namespace PoEntry
{
    public partial class Form1 : Form
    {
        #region Variables
        public Data data = new Data();
        public EhsUtil EhsUtil;
        private Ehs.Forms.Util EhsForms;
        static EHS.POControl.PoStatus.FormPoStatus PoStatus;
        //private static EHS.Report.Util RptUtil;
        //private readonly ReportDocument cryRpt = new ReportDocument();
        List<string> STC = new List<string>();
        public LocationDetail _CurLocationDetail;
        public List<UnitOfPurchase> ListUop;
        
        #region Objects
        PoHeader List_Header;
        FilteredBindingList<PoDetail> List_Detail = new FilteredBindingList<PoDetail>();
        public Ehs.Models.PoHeader Header
        {
            get
            {
                return (Ehs.Models.PoHeader)bs1.Current;
            }
        }
        public Ehs.Models.PoDetail Detail
        {
            get
            {
                return (Ehs.Models.PoDetail)bs2.Current;
            }
        }
        Ehs.Models.PoDetailController _PDC;
        Ehs.Models.PoHeaderController _PHC;
        public IMF _IMF;
        List<ItemMemo> _ItemMemo = new List<ItemMemo>();
        #endregion

        #region Datetime
        public DateTime? Surgery_Date;

        #endregion

        bool Already_Save_Header;
        #region//bool
        /// <summary>
        /// bool
        /// </summary>
        public bool UseVatDetail, FormOpen, GoodPass, EOMWarned, CanViewPatientMemo, PrimeChange, ALLOW_POENTRY_DETAIL_SPLIT, SHOW_CHANGE_DELIVER, USE_CANCEL_MEMO, Updated_Master, Price_Changed, Landscape, Print_Ok, POENTRY_SHOW_MULTIMEDIA_PATH;
        public bool InsertDetailLineMode, POENTRY_MAKE_QTY_ZERO_ON_DELETE, ALLOW_UPDATE_VENDOR_ACCT_POENTRY, SHOW_PROJECT_WHEN_NOT_PROJECT_POTYPE;
        public bool Use_Project_Spend_Amount, ENABLE_ADD_ITEMS, Enable_Add_ItemVend, Allow_Update_Master, ALLOW_UPDATE_CONTRACT, rsl, SameEntityInLoc;
        public bool USER_MUST_CHOOSE_LOC, GOTO_NEXT_LINE_EDIT, Can_Insert_MFG, USE_SUBLEDGER_AMOUNT;
        public bool Changing, CanSwitch, AddItemsStockChecked;
        bool closing = false;
        bool skipme = false;

        bool _UsePoGroups;
        bool USE_PO_GROUPS
        {
            get { return _UsePoGroups; }
            set
            {
                lbl_PONo.Visible = !value;
                lbl_Status.Visible = !value;
                eb_Po_No.Visible = !value;
                eb_Status.Visible = !value;
                lbl_PoGroup.Visible = value;
                cmb_POGroup.Visible = value;
                _UsePoGroups = value;
            }
        }
        bool AutoReceive
        {
            get { return ((ComboBoxPoType)cmb_Po_Type.CurrentItem.Value).AutoReceive; }
        }

        bool Not_Exceed
        {
            get { return ((ComboBoxPoType)cmb_Po_Type.CurrentItem.Value).NotExceed; }
        }
        bool Require_Profile_Id
        {
            get { return ((ComboBoxPoType)cmb_Po_Type.CurrentItem.Value).ProfileId; }
        }
        bool Not_Exceed_Header
        {
            get { return ((ComboBoxPoType)cmb_Po_Type.CurrentItem.Value).NotExceedHeader; }
        }

        bool HeaderBuyerMemoColor
        {
            set
            {
                if (value)
                {
                    b_buyer_memo.Text.ToUpper();
                    b_buyer_memo.Font = new Font(Font, FontStyle.Bold);
                    b_buyer_memo.ForeColor = Color.Red;
                }
                else
                {
                    b_buyer_memo.Text.ToLower();
                    b_buyer_memo.Font = new Font(Font, FontStyle.Regular);
                    b_buyer_memo.ForeColor = SystemColors.ControlText;
                }
            }
        }
        bool HeaderVendorMemoColor
        {
            set
            {
                if (value)
                {
                    b_vendor_memo.Text.ToUpper();
                    b_vendor_memo.Font = new Font(Font, FontStyle.Bold);
                    b_vendor_memo.ForeColor = Color.Red;
                }
                else
                {
                    b_vendor_memo.Text.ToLower();
                    b_vendor_memo.Font = new Font(Font, FontStyle.Regular);
                    b_vendor_memo.ForeColor = SystemColors.ControlText;
                }
            }
        }
        bool HeaderReceiverMemoColor
        {
            set
            {
                if (value)
                {
                    b_rec_memo.Text.ToUpper();
                    b_rec_memo.Font = new Font(Font, FontStyle.Bold);
                    b_rec_memo.ForeColor = Color.Red;
                }
                else
                {
                    b_rec_memo.Text.ToLower();
                    b_rec_memo.Font = new Font(Font, FontStyle.Regular);
                    b_rec_memo.ForeColor = SystemColors.ControlText;
                }
            }
        }

        bool HeaderPatientMemoColor
        {
            set
            {
                if (value)
                {
                    b_pat_memo.Text.ToUpper();
                    b_pat_memo.Font = new Font(Font, FontStyle.Bold);
                    b_pat_memo.ForeColor = Color.Red;
                }
                else
                {
                    b_pat_memo.Text.ToLower();
                    b_pat_memo.Font = new Font(Font, FontStyle.Regular);
                    b_pat_memo.ForeColor = SystemColors.ControlText;
                }
            }
        }

        bool DetailPatientMemoColor
        {
            set
            {
                if (value)
                {
                    b_Detail_Patient_Memo.Text = "PATIENT";
                    b_Detail_Patient_Memo.Font = new Font(Font, FontStyle.Bold);
                    b_Detail_Patient_Memo.ForeColor = Color.Red;
                }
                else
                {
                    b_Detail_Patient_Memo.Text = "patient";
                    b_Detail_Patient_Memo.Font = new Font(Font, FontStyle.Regular);
                    b_Detail_Patient_Memo.ForeColor = SystemColors.ControlText;
                }
            }
        }

        bool HeaderNotifyMemoColor
        {
            set
            {
                if (value)
                {
                    btn_Notify.Text.ToUpper();
                    btn_Notify.Font = new Font(Font, FontStyle.Bold);
                    btn_Notify.ForeColor = Color.Red;
                }
                else
                {
                    btn_Notify.Text.ToLower();
                    btn_Notify.Font = new Font(Font, FontStyle.Regular);
                    btn_Notify.ForeColor = SystemColors.ControlText;
                }
            }
        }
        bool DetailBuyerMemoColor
        {
            set
            {
                if (value)
                {
                    b_D_buyer_memo.Text.ToUpper();
                    b_D_buyer_memo.Font = new Font(Font, FontStyle.Bold);
                    b_D_buyer_memo.ForeColor = Color.Red;
                }
                else
                {
                    b_D_buyer_memo.Text.ToLower();
                    b_D_buyer_memo.Font = new Font(Font, FontStyle.Regular);
                    b_D_buyer_memo.ForeColor = SystemColors.ControlText;
                }
            }
        }
        bool DetailVendorMemoColor
        {
            set
            {
                if (value)
                {

                    b_D_Vendor_memo.Text.ToUpper();
                    b_D_Vendor_memo.Font = new Font(Font, FontStyle.Bold);
                    b_D_Vendor_memo.ForeColor = Color.Red;
                }
                else
                {
                    b_D_Vendor_memo.Text.ToLower();
                    b_D_Vendor_memo.Font = new Font(Font, FontStyle.Regular);
                    b_D_Vendor_memo.ForeColor = SystemColors.ControlText;
                }
            }
        }
        bool InDetail;
        #endregion
        public bool Append_Conversion;
        #region//numbers
        public int POENTRY_NOT_ORDERED_MONTHS;
        public decimal holdItem_Count, POENTRY_PRICE_CHECK_AMOUNT, POENTRY_PRICE_CHECK_PERCENT, poentry_dollar_limit, holdPO_Cost;
        #endregion

        #region//words
        public char Cross_Account_No;

        /// <summary>
        /// strings
        /// </summary>
        public string SqlUsername, CommentToAdd, CommentToAdd2nd, LastLocUsed;
        //Probably get rid of soon
        public string Report_Name, Default_Po_Class, Default_NonStock_Location, Buyer_Username, NMat_Code_Prefix, Mat_Code_Prefix;

        public string splitacct, StorePO, Patient_Id, DetailPatientMemo, Patient_Name, Serial_No;



        private string _IM;



        public string Item_Memo
        {
            get
            {
                return _IM;
            }
            set
            {
                _IM = value;
                if (_IM.Trim() != "")
                {
                    b_d_ItemMemo.Text.ToUpper();
                    b_d_ItemMemo.Font = new Font(Font, FontStyle.Bold);
                    b_d_ItemMemo.ForeColor = Color.Red;
                }
                else
                {
                    b_d_ItemMemo.Text.ToLower();
                    b_d_ItemMemo.Font = new Font(Font, FontStyle.Regular);
                    b_d_ItemMemo.ForeColor = SystemColors.ControlText;
                }
            }
        }

        public StringBuilder string_Build = new StringBuilder();
        #endregion

        #region//Lists
        public List<ComboBoxString> list_Mat;
        List<Ehs.Models.ComboBoxUOM> list_Uom;
        List<UOP> List_Uop;
        #endregion

        #region//Current Values Helpers
        decimal CurrPo
        {
            get { return eb_Po_No.Text.ToDecimal(); }
            set
            {
                eb_Po_No.Text = value.ToString();
                eb_PO_Number.Text = value.ToString();
                Header.PONo = value;
            }
        }

        string CurrEntity
        {
            get=> (cmb_Entity.CurrentItem == null) ? null : cmb_Entity.CurrentItem.Key;
            set
            {
                cmb_Entity.CurrentItem = (cmb_Entity.Items.Exists(r => r.Key == value)) ? cmb_Entity.Items.Find(r => r.Key == value) : null;
                //cmb_Entity.CurrentItem = (cmb_Entity.Items.Exists(r => r.Key == value)) ? cmb_Entity.Items.Find(r => r.Key == value) :
                                         //(cmb_Entity.Items.Count > 0) ? cmb_Entity.Items[0] : null;
            }
        }

        string CurrPoType
        {
            get => (cmb_Po_Type.CurrentItem == null) ? null : cmb_Po_Type.CurrentItem.Key;
            set
            {
                cmb_Po_Type.CurrentItem = (cmb_Po_Type.Items.Exists(r => r.Key == value)) ? cmb_Po_Type.Items.Find(r => r.Key == value) :
                                          (cmb_Po_Type.Items.Count > 0) ? cmb_Po_Type.Items[0] : null;
            }
        }

        string CurrShipTo
        {
            get
            {
                return (cmb_Ship_To.CurrentItem == null) ? null : cmb_Ship_To.CurrentItem.Key;
            }
            set
            {
                cmb_Ship_To.CurrentItem = (value == null || value == "") ? cmb_Ship_To.Items[0] :
                                          (cmb_Ship_To.Items.Exists(r => r.Key == value)) ? cmb_Ship_To.Items.Find(r => r.Key == value) :
                                          cmb_Ship_To.Items[0];
            }
        }


        string CurHeaderVat
        {
            get
            {
                return (cmb_Vat_Code.CurrentItem == null) ? null : cmb_Vat_Code.CurrentItem.Key;
            }
            set
            {
                cmb_Vat_Code.CurrentItem = (cmb_Vat_Code.Items.Exists(r => r.Key == value)) ? cmb_Vat_Code.Items.Find(r => r.Key == value) : (cmb_Vat_Code.Items.Count > 0) ? cmb_Vat_Code.Items[0] : null;
            }
        }
        public string CurVendor
        {
            get => (vendorFrm.cmb_vendor?.CurrentItem == null) ? null : vendorFrm.cmb_vendor.CurrentItem.Key ?? null;
            set
            {
                if (vendorFrm.cmb_vendor.Items != null)
                    vendorFrm.cmb_vendor.CurrentItem = (vendorFrm.cmb_vendor.Items.Exists(r => r.Key == value)) ? vendorFrm.cmb_vendor.Items.Find(r => r.Key == value) : null;
                if (cmb_vendor.Items == null)
                    cmb_vendor.Text = "";
                else
                    cmb_vendor.CurrentItem = (cmb_vendor.Items.Exists(r => r.Key == value)) ? cmb_vendor.Items.Find(r => r.Key == value) : null;
            }
        }

        public string CurMat
        {
            get => cmb_Mat.CurrentItem?.Key ?? null;
            set => cmb_Mat.CurrentItem = (cmb_Mat.Items.Exists(r => r.Key == value)) ? cmb_Mat.Items.Find(r => r.Key == value) : null;
        }

        string CurLoc
        {
            get => (cmb_Loc.CurrentItem == null) ? null : cmb_Loc.CurrentItem.Key;
            set => cmb_Loc.CurrentItem = (cmb_Loc.Items.Exists(r => r.Key == value)) ? cmb_Loc.Items.Find(r => r.Key == value) : null;
        }

        string CurAct
        {
            get => (cmb_Act.CurrentItem == null) ? null : cmb_Act.CurrentItem.Key;
            set => cmb_Act.CurrentItem = (cmb_Act.Items.Exists(r => r.Key == value)) ? cmb_Act.Items.Find(r => r.Key == value) : null;
        }

        string CurDeliver
        {
            get => (cmb_Deliver.CurrentItem == null) ? null : cmb_Deliver.CurrentItem.Key;
            set => cmb_Deliver.CurrentItem = (cmb_Deliver.Items.Exists(r => r.Key == value)) ? cmb_Deliver.Items.Find(r => r.Key == value) : null;
        }
        string CurPoClass
        {
            get => (cmb_PoClass.CurrentItem == null) ? null : cmb_PoClass.CurrentItem.Key;
            set => cmb_PoClass.CurrentItem = (cmb_PoClass.Items.Exists(r => r.Key == value)) ? cmb_PoClass.Items.Find(r => r.Key == value) : null;
        }
        string CurProfile
        {
            get => (cmb_ProfileId.CurrentItem == null) ? null : cmb_ProfileId.CurrentItem.Key;
            set => cmb_ProfileId.CurrentItem = (cmb_ProfileId.Items.Exists(r => r.Key == value)) ? cmb_ProfileId.Items.Find(r => r.Key == value) : null;
        }
        string CurDetailVat
        {
            get => cmb_DetailVatCode.CurrentItem?.Key ?? "";
            set => cmb_DetailVatCode.CurrentItem = (cmb_DetailVatCode.Items.Exists(r => r.Key == value)) ? cmb_DetailVatCode.Items.Find(r => r.Key == value) : null;
        }
        public string CurrentUOPPrime
        {
            get
            {
                return (cmb_Purchase.CurrentItem == null) ? null : cmb_Purchase.CurrentItem.Key;
            }
            set
            {
                cmb_Purchase.CurrentItem = (value == null || value == "None") ? null :
                                           (cmb_Purchase.Items.Exists(r => r.Key == value)) ? cmb_Purchase.Items.Find(r => r.Key == value) :
                                           null;
            }
        }

        DateTime? DeliverDate
        {
            get
            {
                if (od_DeliverDate.Checked)
                    return od_DeliverDate.Value;
                else
                    return null;
            }
            set
            {
                if (value == DateTime.MinValue)
                {
                    od_DeliverDate.Checked = false;
                    od_DeliverDate.CustomFormat = " ";
                    od_DeliverDate.Format = DateTimePickerFormat.Custom;
                }
                else
                {
                    od_DeliverDate.CustomFormat = null;
                    od_DeliverDate.Format = DateTimePickerFormat.Short;
                    od_DeliverDate.Value = (DateTime)value;
                    od_DeliverDate.Checked = true;
                }
            }
        }

        bool NewEnabled
        {
            get
            {
                return TS_New.Enabled;
            }
            set
            {
                MS_New.Enabled = value;
                TS_New.Enabled = value;
            }
        }

        bool EditEnabled
        {
            get
            {
                return MS_Edit.Enabled;
            }
            set
            {
                MS_Edit.Enabled = value;
                TS_Edit.Enabled = value;
            }
        }

        bool DeleteEnabled
        {
            set
            {
                MS_Delete.Enabled = value;
                TS_Delete.Enabled = value;
            }
        }
        bool SaveEnabled
        {
            set
            {
                MS_Save.Enabled = value;
                TS_Save.Enabled = value;
            }
        }


        #endregion

        #region//Dialog

        DialogResult MainVendor;

        #endregion
        public static string DataPath;

        #endregion

        public bool gettingitem, AddItemFromVendor;
        ControlController FormController;

        public Form1(string[] parameters)
        {
            InitializeComponent();
            Application.AddMessageFilter(m_filter);
            p_Header.Validated += new EventHandler(p_Header_Validated);
            p_Header.Validating += new CancelEventHandler(p_Header_Validating);
            p_Header.Enter += new EventHandler(p_Header_Enter);
            p_Detail.Validated += new EventHandler(p_Detail_Validated);
            p_Detail.Validating += new CancelEventHandler(p_Detail_Validating);
            p_Detail.Enter += new EventHandler(p_Detail_Enter);

            viewMode1.Mode = ViewingMode.Inquiry;
            ///Pulls In Parameters 
            ///
            string ConnectionString = "Data Source='" + parameters[0].ToString() + "';Initial Catalog='" + parameters[1].ToString() + "';Persist Security Info=True;User ID='"
                                    + parameters[4].ToString() + "';Password='" + parameters[2].ToString() + "';Connect Timeout=600;;Application Name='" + parameters[3].ToString() + "'";
            Text = parameters[5].ToString();
            SqlUsername = parameters[4].ToString();
            data.SqlUsername = SqlUsername;
            data.Connect(ConnectionString);
            data.RestoreSettings(this);
            SetupConnections(ConnectionString);
            FormController = new ControlController(this);
            cmb_Mat.DoubleClick += Cmb_Mat_DoubleClick;
        }

        private void Cmb_Mat_DoubleClick(object sender, EventArgs e)
        {
            gettingitem = false;
            Mat_Enter();
        }

        #region Status Text
        private void status2_TextChanged(object sender, EventArgs e)
        {
            if (Changing)
                return;
            Changing = true;
            status2.Text = (status2.Text == "") ? "" : "Account: " + status2.Text;
            Changing = false;
        }
        private void lbl49_TextChanged(object sender, EventArgs e)
        {
            if (Changing)
                return;
            Changing = true;
            lbl49.Text = (lbl49.Text == "") ? "" : "Contract: " + lbl49.Text;
            Changing = false;
        }
        #endregion
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x10)// Attempting to close Form
                AutoValidate = AutoValidate.Disable;//this stops (all) validations
            base.WndProc(ref m);
        }

        public void SetupConnections(string ConnectionString)
        {
            EhsUtil = new EhsUtil();
            EhsForms = new Ehs.Forms.Util(data._Com.Connection);
            _PHC = new PoHeaderController(data._Com.Connection);
            _PDC = new PoDetailController(data._Com.Connection);

            b_buyer_memo.Connection = data._Com.Connection;
            b_pat_memo.Connection = data._Com.Connection;
            b_vendor_memo.Connection = data._Com.Connection;
            b_rec_memo.Connection = data._Com.Connection;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ///Setup as Viewing
            viewMode1.Mode = ViewingMode.Viewing;

            //initialize stuff
            //POEINT();
            try
            {
                GetSystemOptions();
            }
            catch (Exception SystemOptionsError)
            {
                MessageBox.Show("Error in System Options: " + SystemOptionsError.ToString());
            }
            GetUserOptions();

            DataPath = data.SystemOptionsDictionary["DATA_PATH"];
            data.Open();
            EhsForms.BuildQuickLinks(Quicklinks_TS, "POentry.exe"); //Application.ExecutablePath);

            data.Close();
            //EhsForms.RestoreFormSettings(this, SqlUsername);

            b_pat_memo.Visible = CanViewPatientMemo;
            b_Detail_Patient_Memo.Visible = CanViewPatientMemo;

            List_Header = _PHC.New();
            bs1.DataSource = List_Header;

            PrefillHeaderComboBoxes();
            //firstload = false;
            CheckPoGroup();

            //FillUserDefineCaptions();
            SetViewing();
            if (!POENTRY_SHOW_MULTIMEDIA_PATH)
            {
                lbl_MM_Path.Visible = false;
                eb_MM_Path.Visible = false;
                speedbutton1.Visible = false;
            }
            Changing = true;
            eb_Po_No.Text = "0";
            Changing = false;
            EditEnabled = false;
            dt_PO_Date.Value = DateTime.Now;
        }

        #region Get Options for form
        public void GetSystemOptions()
        {
            data.GetSystemOptions();

            POENTRY_NOT_ORDERED_MONTHS = data.SystemOptionsDictionary["POENTRY_NOT_ORDERED_MONTHS"].ToInt32();
            USE_PO_GROUPS = data.SystemOptionsDictionary["USE_PO_GROUPS"].ToBoolean();
            cmb_Vat_Code.Visible = data.SystemOptionsDictionary["USE_VAT_CODE_POHEADER"].ToBoolean();
            lbl_Vat.Visible = data.SystemOptionsDictionary["USE_VAT_CODE_POHEADER"].ToBoolean();

            UseVatDetail = data.SystemOptionsDictionary["USE_VAT_CODE_PODETAIL"].ToBoolean();
            lbl_VAT2.Visible = UseVatDetail;
            lbl_Percent.Visible = UseVatDetail;
            cmb_DetailVatCode.Visible = UseVatDetail;
            eb_Vat2.Visible = UseVatDetail;
            POENTRY_SHOW_MULTIMEDIA_PATH = data.SystemOptionsDictionary["POENTRY_SHOW_MULTIMEDIA_PATH"].ToBoolean();
            if (data.SystemOptionsDictionary["VAT_HEADER_MATCH_DETAIL"].ToBoolean())
            {
                if (data.SystemOptionsDictionary["USE_VAT_CODE_POHEADER"].ToBoolean() == false)
                {
                    MessageBox.Show("Your sysfile options must be changed.\nYou can't have VAT_HEADER_MATCH_DETAIL "
                                   + "set when \nUSE_VAT_HEADER is not set.\n", "Warning", MessageBoxButtons.OK);
                }
                if (UseVatDetail == false)
                {
                    MessageBox.Show("Your sysfile options must be changed.\nYou can't have VAT_HEADER_MATCH_DETAIL "
                                    + "set when \nUSE_VAT_DETAIL is not set.\n", "Warning", MessageBoxButtons.OK);
                }
            }
            if (data.SystemOptionsDictionary["SHOW_PROJECT_WHEN_NOT_PROJECT_POTYPE"].ToBoolean())
            {
                SHOW_PROJECT_WHEN_NOT_PROJECT_POTYPE = true;
            }
            else
            {
                SHOW_PROJECT_WHEN_NOT_PROJECT_POTYPE = false;
                lbl_Project_No.Visible = false;
                cmb_Project.Visible = false;
            }
            Default_Po_Class = data.SystemOptionsDictionary["DEFAULT_PO_CLASS"].ToNonNullString();
            Cross_Account_No = data.SystemOptionsDictionary["ACCOUNT_NO_XREF_TYPE"].ToChar();
            Mat_Code_Prefix = data.SystemOptionsDictionary["MAT_CODE_PREFIX"];
            if (Mat_Code_Prefix == "*")
                Mat_Code_Prefix = "";

            NMat_Code_Prefix = data.SystemOptionsDictionary["NMAT_CODE_PREFIX"];
            if (NMat_Code_Prefix == "*")
                NMat_Code_Prefix = "";
            Append_Conversion = data.SystemOptionsDictionary["APPEND_CONVERSION"].ToBoolean();
            Allow_Update_Master = data.SystemOptionsDictionary["ALLOW_UPDATE_MASTER"].ToBoolean();
            ALLOW_UPDATE_VENDOR_ACCT_POENTRY = data.SystemOptionsDictionary["ALLOW_UPDATE_VENDOR_ACCT_POENTRY"].ToBoolean();
            Enable_Add_ItemVend = data.SystemOptionsDictionary["ENABLE_ADD_ITEMVEND"].ToBoolean();
            SameEntityInLoc = data.SystemOptionsDictionary["CHECK_PO_ENTITY_IN_LOC"].ToBoolean();
            USE_SUBLEDGER_AMOUNT = data.SystemOptionsDictionary["USE_SUBLEDGER_AMOUNT"].ToBoolean();
            USER_MUST_CHOOSE_LOC = data.SystemOptionsDictionary["USER_MUST_CHOOSE_LOC"].ToBoolean();
            GOTO_NEXT_LINE_EDIT = data.SystemOptionsDictionary["GOTO_NEXT_LINE_EDIT"].ToBoolean();
            POENTRY_PRICE_CHECK_AMOUNT = data.SystemOptionsDictionary["POENTRY_PRICE_CHECK_AMOUNT"].ToDecimal();
            POENTRY_PRICE_CHECK_PERCENT = data.SystemOptionsDictionary["POENTRY_PRICE_CHECK_PERCENT"].ToDecimal();
            Use_Project_Spend_Amount = data.SystemOptionsDictionary["POENTRY_USE_PROJECT_SPEND_AMOUNT"].ToBoolean();
            addToCatalogToolStripMenuItem.Visible = data.SystemOptionsDictionary["ALLOW_ADD_CATALOG"].ToBoolean();
            SHOW_CHANGE_DELIVER = data.SystemOptionsDictionary["SHOW_CHANGE_DELIVER"].ToBoolean();
            ALLOW_POENTRY_DETAIL_SPLIT = data.SystemOptionsDictionary["ALLOW_POENTRY_DETAIL_SPLIT"].ToBoolean();

            USE_CANCEL_MEMO = data.SystemOptionsDictionary["USE_CANCEL_MEMO"].ToBoolean();
            Report_Name = data.SystemOptionsDictionary["POPRINT_REPORT_NAME"].ToNonNullString();
            Landscape = data.SystemOptionsDictionary["POPRINT_LAND_PORT"] == "L";
            POENTRY_MAKE_QTY_ZERO_ON_DELETE = data.SystemOptionsDictionary["POENTRY_MAKE_QTY_ZERO_ON_DELETE"].ToBoolean();

            EOMWarned = false;

            data.Open();
            data._Com.Parameters.Clear();

            if (data.SystemOptionsDictionary["USE_VAT_CODE_POHEADER"].ToBoolean())
            {
                cmb_Vat_Code.Items = EHS.Orders.prefillcombo(data._Com, "Vat", "");
                CurHeaderVat = data.SystemOptionsDictionary["DEFAULT_VAT"];
            }
            data._Com.Parameters.Clear();
            data._Com.CommandText = "SELECT Comment FROM Comments WHERE Comment_Id = @Comment_Id";
            data._Com.Parameters.AddWithValue("@Comment_Id", data.SystemOptionsDictionary["REQUISITION_STORED_COMMENT"].ToNonNullString());
            using (SqlDataReader Comment = data._Com.ExecuteReader())
            {
                Comment.Read();
                CommentToAdd = (Comment.HasRows) ? Comment["Comment"].ToNonNullString() : "";
            }
            data._Com.Parameters.Clear();
            data._Com.CommandText = "SELECT Comment FROM Comments WHERE Comment_Id = @Comment_Id";
            data._Com.Parameters.AddWithValue("@Comment_Id", data.SystemOptionsDictionary["REQUISITION_STORED_COMMENT_2ND"].ToNonNullString());
            using (SqlDataReader Comment2 = data._Com.ExecuteReader())
            {
                Comment2.Read();
                CommentToAdd = (Comment2.HasRows) ? Comment2["Comment"].ToNonNullString() : "";
            }
            data.Close();
        }
        public void GetUserOptions()
        {
            try
            {
                data.GetUserOptions(this);
            }
            catch (Exception ee)
            {
                MessageBox.Show("An Error Occurred in UserOptions\n" + ee.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
                return;
            }
        }
        #endregion

        #region What Mode you are in
        private void viewMode1_ModeChangedEvent(object source, ViewModeEventArgs e)
        {
            switch (e.Mode)
            {
                case ViewingMode.Adding:
                    {
                        status.Text = "Adding Record";
                        NewEnabled = false;
                        EditEnabled = false;
                        DeleteEnabled = false;
                        SaveEnabled = true;
                        TS_Cancel.Image = global::PoEntry.Properties.Resources.edit_delete1;
                        TS_Cancel.ToolTipText = "Cancel";
                        ts_nextpo.Enabled = false;
                        ts_previouspo.Enabled = false;
                        resequenceLineNumbersToolStripMenuItem.Enabled = false;
                        break;
                    }
                case ViewingMode.Editing:
                    {
                        status.Text = "Editing Record";
                        NewEnabled = false;
                        EditEnabled = false;
                        DeleteEnabled = false;
                        SaveEnabled = true;
                        TS_Cancel.Image = global::PoEntry.Properties.Resources.edit_delete1;
                        TS_Cancel.ToolTipText = "Cancel";
                        ts_nextpo.Enabled = false;
                        ts_previouspo.Enabled = false;
                        resequenceLineNumbersToolStripMenuItem.Enabled = false;
                        break;
                    }
                case ViewingMode.Inquiry:
                    {
                        status.Text = "Inquiry Mode";
                        NewEnabled = false;
                        EditEnabled = false;
                        SaveEnabled = false;
                        DeleteEnabled = false;
                        ts_nextpo.Enabled = true;
                        ts_previouspo.Enabled = true;
                        resequenceLineNumbersToolStripMenuItem.Enabled = true;
                        break;
                    }
                case ViewingMode.Viewing:
                    {
                        status.Text = "Viewing Record";
                        NewEnabled = true;
                        EditEnabled = true;
                        DeleteEnabled = true;
                        SaveEnabled = false;
                        TS_Cancel.Image = global::PoEntry.Properties.Resources.view_refresh_7;
                        TS_Cancel.ToolTipText = "Refresh";
                        ts_nextpo.Enabled = true;
                        ts_previouspo.Enabled = true;
                        resequenceLineNumbersToolStripMenuItem.Enabled = true;
                        errorProvider1.Clear();
                        break;
                    }
            }
        }


        #endregion

        #region Header
        void PrefillHeaderComboBoxes()
        {
            cmb_Entity.Items = data.getEntity();//fill
            CurrEntity = data.SystemOptionsDictionary["DEFAULT_ENTITY"].ToNonNullString();//set
            cmb_Po_Type.Items = data.getPoType();//fill
            CurrPoType = data.SystemOptionsDictionary["DEFAULT_PO_TYPE"].ToNonNullString();//set
            cmb_Ship_To.Items = data.getShipTo();//fill
            
            data.Open();

            cmb_Purchase.Items = EHS.Orders.prefillcombo(data._Com, "UOM", "");
            data.Close();
            cmb_Purchase.Items.Insert(0, new ComboBoxString("None", "Please Set UOM"));
            vendorFrm = new FrmVendor();
            //FillVendor();
            //            list_PoType = Ehs.Models.Util.getPoType(data._Com, "None");
            //            cmb_Po_Type.Items = new List<ComboBoxString>(list_PoType.Select(p => new ComboBoxString()).ToList());
        }

        public void CheckPoGroup()
        {
            if (USE_PO_GROUPS)
            {
                Header.PoGroupId = data.GetPoGroup();
                if (Header.PoGroupId == 0)
                {
                    if (data.SystemOptionsDictionary["MUST_USE_DEFAULT_PO_GROUP"].ToBoolean())
                    {
                        MessageBox.Show("Your system is set to use PO groups and you do not have a\n Default Group assigned.  You must set a Default Group before\n"
                                       + "you can open this program.", "Warning", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Your system is set to use PO Groups but you do not have any\nPO Groups established.  You must create PO Groups using the \n"
                                       + "PO Groups master before you can continue.", "Warning", MessageBoxButtons.OK);
                    }
                }
            }
        }

        #endregion

        #region Fill Combos

        public void FillPrimeCombo(string PassUnit)
        {
            if (PassUnit.Trim().Length == 0)
                CurrentUOPPrime = "None";
            else
                CurrentUOPPrime = PassUnit;
        }

        #endregion

        #region Header

        private void eb_Po_No_DoubleClick(object sender, EventArgs e)
        {
            CurrPo = data.Lookup("Po No", _UsePoGroups, "").ToDecimal();
            GetPo();
        }
        private void eb_Po_No_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        this.GetPo(); break;
                    }
            }
        }
        public void GetPo()
        {
            data.Open();
            data._Com.Parameters.Clear();
            data._Com.CommandText = "SELECT po_no FROM poheader h JOIN usertoentity u on h.entity = u.entity WHERE po_no = @po_no AND u.username = @username ";
            if (USE_PO_GROUPS)
                data._Com.CommandText += "AND POGroup_Id in (SELECT POGroup_Id FROM usertopogroups WHERE username = @username ";
            if (tabControl1.SelectedTab == p_Detail)
                CurrPo = eb_PO_Number.Text.ToDecimal();
            data._Com.Parameters.AddWithValue("po_no", CurrPo);
            data._Com.Parameters.AddWithValue("username", SqlUsername);
            using (SqlDataReader po = data._Com.ExecuteReader())
            {
                try
                {
                    if (po.HasRows)
                    {
                        po.Read();
                        viewMode1.Mode = ViewingMode.Viewing;
                        Changing = true;
                        po.Close();
                        CanSwitch = true;
                        Changing = false;
                    }
                    else
                    {
                        po.Close();
                        MessageBox.Show("PO not found, or user does not have\nrights to the PO's Entity.", "Information", MessageBoxButtons.OK);
                        ClearHeader();
                        ClearDetails();
                        viewMode1.Mode = ViewingMode.Viewing;
                        return;
                    }
                }
                finally
                {
                    po.Close();
                }
            }
            data.Close();
            FillHeaderQuery();
            FillHeader();


            if (tabControl1.SelectedTab == p_Detail)
            {
                FillDetailQuery();
                FillDetails();
            }
            else
                bs2.Position = -1;
            SetViewing();
        }

        #region Fill Header
        public void FillHeaderQuery()
        {
            if (CurrPo == 0)
                return;
            List_Header = _PHC.Get(eb_Po_No.Text);//                Ehs.Models.Util.getPoHeader(_Com, CurrPo);
            bs1.DataSource = List_Header;
        }

        public void FillHeader()
        {
            string hold;
            int i;

            if (bs1.Count == 0)
            {
                ClearHeader();
                return;
            }
            Changing = true;

            //Item_Count = C_Header["Item_Count"].ToInt32();
            //CurrPo = C_Header["Po_No"].ToInt32();
            //SetPOTypes(Header.POType);
            Changing = true;
            eb_Date.Text = dt_PO_Date.Value.ToString();
            eb_Total.Text = Header.Total.ToString();
            eb_Total2.Text = Header.Total.ToString();
            eb_Not_Total.Text = Header.NotExceedTotal.ToString();
            try
            {
                FillStatus();
            }
            catch
            {

            }
            CurrEntity = Header.Entity;
            CurrPoType = Header.POType;
            //CurrTermsCode = Header.TermsCode;
            try
            {
                CurrShipTo = Header.ShipTo;
            }
            catch
            {
                MessageBox.Show(Header.ShipTo + " is now an invalid ship to");
            }
            CurHeaderVat = Header.VatCode;
            FillVendor();
            cmb_vendor.Items = new List<ComboBoxString>(1) { new ComboBoxString(Header.VendorID, Header.VendorName) };
            CurVendor = Header.VendorID;
            cmb_vendor.Text = vendorFrm.cmb_vendor.Text;
            vendorFrm.GetVendor();
            //eb_Order_Days.Text = Vendor.OrderDays;
            status2.Text = vendorFrm.Vendor.VendorAccount;
            cb_Has_Image.Checked = Header.HasScannedImage;
            btn_rcv.Enabled = Header.HasScannedImageRCV;
        }

        public void FillStatus()
        {
            Changing = true;
            eb_Status.Text = "";
            string temp = "";

            if (Header.Cancelled)
            {
                eb_Status.Text = " CANCELLED";
                return;
            }
            else if (Header.Closed)
            {
                eb_Status.Text = " CLOSED";
                return;
            }

            temp = GetStatus(Header.EDIStatus);
            if (temp != "")
                eb_Status.Text += temp == "NEVER" ? "NEVER EDI: " : "EDI " + temp + ": ";

            temp = GetStatus(Header.FaxStatus);
            if (temp != "")
                eb_Status.Text += temp == "NEVER" ? "NEVER FAX: " : "FAX " + temp + ": ";

            temp = GetStatus(Header.PrintStatus);
            if (temp != "")
                eb_Status.Text += temp == "NEVER" ? "NEVER PRINT: " : "PRINT " + temp + ": ";

            temp = GetStatus(Header.EmailStatus);
            if (temp != "")
                eb_Status.Text += temp == "NEVER" ? "NEVER EMAIL: " : "EMAIL " + temp + ": ";


            if (eb_Status.Text.Trim() == "")
            {
                resequenceLineNumbersToolStripMenuItem.Enabled = true;
                eb_Status.Text = " OPEN";

                if (Header.Sent)
                    eb_Status.Text = " REOPENED";
            }
            else
                resequenceLineNumbersToolStripMenuItem.Enabled = false;
            eb_Status2.Text = eb_Status.Text;
            Changing = false;
        }
        public string GetStatus(char temp)
        {
            switch (temp)
            {
                case 'A':
                    {
                        return "Active";
                    }
                case 'S':
                    {
                        return "Sent";
                    }
                case 'C':
                    {
                        return "Released";
                    }
                case 'X':
                    {
                        return "NEVER";
                    }
                default:
                    {
                        return "";
                    }
            }
        }
        #endregion

        public void ClearHeader()
        {
            Changing = true;

            eb_Total.Text = "0";
            eb_Total2.Text = "0";
            eb_Total3.Text = "0";
            eb_Not_Total.Text = "0";

            CurrEntity = data.SystemOptionsDictionary["DEFAULT_ENTITY"].ToNonNullString();
            CurrPoType = data.SystemOptionsDictionary["DEFAULT_PO_TYPE"].ToNonNullString();
            CurrShipTo = null;
            try
            {
                cmb_Vat_Code.CurrentItem = null;
            }
            catch
            {
                cmb_Vat_Code.Items = null;
            }
            Changing = false;
            //eb_Entity.Text = "";
            //eb_Project_No.Text = "";
            CurVendor = null;
            vendorFrm.GetVendor();
            eb_Vendor.Text = "";
            eb_Status.Text = "";
            eb_Status2.Text = "";
            lbl_Not_Total.ForeColor = SystemColors.ControlText;
            eb_Not_Total.ForeColor = SystemColors.ControlText;
            label27.Visible = false;
            btn_Notify.Visible = false;
            cmb_Po_Type.Width = cmb_Entity.Width;//
            status2.Text = "";
            Status3.Text = "";

            btn_scan.Font = new Font(Font, FontStyle.Regular);
            btn_scan.ForeColor = SystemColors.ControlText;

            Changing = false;
        }
        private void bs1_CurrentItemChanged(object sender, EventArgs e)
        {


            cmb_Project.Text = Header.ProjectNo;
            HeaderBuyerMemoColor = Header.BuyerMemo.Length > 0;
            HeaderNotifyMemoColor = Header.NotifyAPMemo.Length > 0;
            HeaderPatientMemoColor = Header.PatientMemo.Length > 0;
            HeaderReceiverMemoColor = Header.ReceiverMemo.Length > 0;
            HeaderVendorMemoColor = Header.VendorMemo.Length > 0;
        }

        #region Vendor
        private void cmb_vendor_DoubleClick(object sender, EventArgs e)
        {

        }

        FrmVendor vendorFrm;

        void FillVendor()
        {
            vendorFrm.data = data;
            vendorFrm.mainfrm = this;
            data.Open();
            vendorFrm.cmb_vendor.Items = EHS.Orders.prefillcombo(data._Com, "Vendor", CurrEntity);
            data.Close();
        }

        #endregion


        #endregion

        #region Detail
        public ContractDetail CD;
        public void FillDetailQuery()
        {
            Changing = true;
            List_Detail = _PDC.GetList(CurrPo);
            bs2.DataSource = List_Detail;
            if (Detail != null && List_Detail.Count == 0)
                Detail.ItemCount = 0;
            Changing = false;
        }

        public void FillDetails()
        {
            eb_Entity.Text = CurrEntity;

            if (Detail == null || Detail.ItemCount == 0)
            {
                ClearDetails();
                return;
            }
            if (cmb_Mat.Items.Count != bs2.Count)
            {
                cmb_Mat.Items = data.prefillCombos("MatDetail", CurrPo.ToString());
                CurMat = Detail.MatCode;
                prefillDetailCombos();
                CurLoc = Detail.Location;
            }
            this.Changing = true;
            Detail.PONo = CurrPo;
            CurMat = Detail.MatCode;
            CurLoc = Detail.Location;
            CurAct = Detail.AccountNo;
            CurDeliver = Detail.DeliverTo;

            if (Detail.SplitDetail)
                this.cmb_Act.ReadOnly = true;

            this.od_DeliverDate.Visible = true;
            DeliverDate = Detail.DeliverDate;
            CurPoClass = Detail.PoClass;
            CurProfile = Detail.ProfileID;
            this.FillPrimeCombo(Detail.UnitPurchase);
            LastLocUsed = Detail.Location;
            CurDetailVat = Detail.VatCode;

            if (this.CanViewPatientMemo)
            {/* Fix
                    this.q_Command.Parameters.Clear();
                    this.q_Command.CommandText = "SELECT Patient_memo FROM PatientPoDetail WHERE po_no = @po_no AND item_count = @Item_count";
                    this.q_Command.Parameters.AddWithValue("@po_no", CurrPo);
                    this.q_Command.Parameters.AddWithValue("@Item_count", Detail.ItemCount);
                    using (SqlDataReader DPmemo = this.q_Command.ExecuteReader())
                    {
                        DetailPatientMemoColor = DPmemo.HasRows;
                    }*/
            }

            if ((Detail.Contract != "") && (tabControl1.SelectedTab == p_Detail))
                lbl49.Text = Detail.Contract;

            this.eb_Total3.Text = (Detail.UnitCost * Detail.QtyOrder * (1m + Detail.VatPercentage / 100m)).ToString("0.00"); ;
            if (this.cb_Substitute_Item.Checked)
                this.cb_Substitute_Item.Enabled = false;

            try
            {
                if (Detail.ReqNo != 0m)
                {
                    b_req_info.Text.ToUpper();
                    b_req_info.Font = new Font(b_req_info.Font, FontStyle.Bold);
                    b_req_info.ForeColor = Color.Red;

                    btn_olr.Enabled = data.HasImage(Detail.ReqNo);
                }
                else
                {
                    b_req_info.Text.ToLower();
                    b_req_info.Font = new Font(b_req_info.Font, FontStyle.Regular);
                    b_req_info.ForeColor = SystemColors.ControlText;
                }
            }
            catch { btn_olr.Enabled = false; }
            /*      cb_DNAccrue.Checked := FieldByName('Do_Not_Accrue').AsBoolean;

  if fieldbyname('item_backordered').asstring = 'B' then
    begin
      pop_Backorder.caption := 'Unset as Backorder';
    end
  else
    begin
      pop_Backorder.caption := 'Set as Backorder';
    end;

 end;



Changing := false;
            */
            Changing = false;
        }

        public void ClearDetails()
        {
            CurMat = null;
            if (bs2.Count < 1)
                return;
            this.Changing = true;
            /*try
            {
                Detail.PONo = CurrPo;
            }
            catch { }*/
            eb_PO_Number.Text = CurrPo.ToString();
            eb_Vendor.Text = CurVendor;
            this.Buyer_Username = "";
            this.cmb_Loc.Text = "";
            this.cmb_Act.Text = "";
            CurPoClass = "";
            this.eb_Quantity2.Text = "0";

            try
            {
                DeliverDate = DateTime.Today.AddDays((double)vendorFrm.Vendor.LeadDays);
            }
            catch
            {
                DeliverDate = DateTime.Today;
            }
            if (cb_overnight.Checked) { DeliverDate = DateTime.Today.AddDays(1); }
            else if (cb_2ndDay.Checked) { DeliverDate = DateTime.Today.AddDays(2); }

            this.cmb_Deliver.Text = "";
            CurProfile = "";
            this.eb_Doctor_Id.Text = "";
            /*
            for (int i = 0; i < this.cmb_Purchase.Items.Count; i++)
            {
                this.cmb_Purchase.Items.RemoveAt(i);
                i--;
            }
            this.cmb_Purchase.Items.Clear();
            */
            this.eb_Conversion.Text = "1";
            this.cb_Substitute_Item.Checked = false;
            if (viewMode1.Mode == ViewingMode.Adding || viewMode1.Mode == ViewingMode.Editing)
                this.cb_Substitute_Item.Enabled = true;
            CurDetailVat = "";
            this.eb_Vat2.Text = "0";

            lbl49.Text = "";
            this.eb_Total3.Text = "0";
            DetailPatientMemo = "";
            DetailPatientMemoColor = false;
            Patient_Id = "";
            Patient_Name = "";
            Serial_No = "";
            Surgery_Date = DateTime.Today;
            //cb_Accrue.Checked = (cmb_Po_Type.CurrentItem.Value as ComboBoxPoType).DontAccrual; check on this.
            btn_olr.Enabled = false;
        }
        private void bs2_CurrentItemChanged(object sender, EventArgs e)
        {
            DetailBuyerMemoColor = Detail?.BuyerMemo.Length > 0;
            //DetailPatientMemoColor = 
            DetailVendorMemoColor = Detail?.VendorMemo.Length > 0;
        }
        void prefillDetailCombos()
        {
            cmb_Loc.Items = data.GetLocation(CurrEntity, CurMat);
            if (cmb_Loc.Items.Count == 0)
                cmb_Loc.Text = "No Location Available";
        }
        private void GetImf()
        {
            if (_IMF == null)
                return;
            if (_IMF.UseContract != "" && (cmb_Po_Type.CurrentItem.Value as ComboBoxPoType).ReturnRepair == false)
            {
                CD = data.GetContract(_IMF.UseContract, CurMat);
                if (CD == null)
                {
                    MessageBox.Show("This item is set to be on a contract, but the contract doesn't exist", "error", MessageBoxButtons.OK);
                    return;
                }
            }
        }
        private void FillDetailsImf()
        {
            this.Changing = true;
            //getmuop();

            FillPrimeCombo(_IMF.Unit_Purchase);
            eb_Conversion.Text = _IMF.Conversion;
            eb_Unit_Cost2.Text = _IMF.PO_Cost;
            Buyer_Username = _IMF.Buyer;
            CurPoClass = _IMF.PO_Class;

            if (Header.Overnight == false && Header.SecondDayDelivery == false)
            {
                if (data.SystemOptionsDictionary["USE_AVERAGE_LEADTIME"].ToBoolean() || vendorFrm.Vendor.UseAverageLeadtime)
                {
                    try
                    {
                        if (_IMF.Average_Lead_Time != 0)
                        {
                            DeliverDate = DateTime.Today;
                            DeliverDate.Value.AddDays(_IMF.Average_Lead_Time);
                        }
                    }
                    catch { }
                }
            }
            if ((cmb_Po_Type.CurrentItem?.Value as ComboBoxPoType)?.ReturnRepair??false)
                eb_Unit_Cost2.Text = "0";
            else
            {
                if (_IMF.UseContract.Trim().Length > 0)
                {
                    if (data.SystemOptionsDictionary["NOT_ALLOW_LOWER_CONTRACT_COST"].ToBoolean())
                        eb_Unit_Cost2.ReadOnly = true;
                    else
                        eb_Unit_Cost2.ReadOnly = false;
                    cmb_Purchase.ReadOnly = true;
                    eb_Conversion.ReadOnly = true;
                    lbl49.Text = CD.Contract;
                    FillPrimeCombo(CD.Unit_Purchase);
                    eb_Conversion.Text = CD.Conversion.ToString();
                    eb_Unit_Cost2.Text = CD.Purchase_Cost.ToString();
                }
                else
                {
                    eb_Unit_Cost2.Enabled = true;
                    cmb_Purchase.ReadOnly = false;
                    eb_Conversion.Enabled = true;
                }
            }
            Changing = false;
        }
        private void GetLoc()
        {
            bool value = false;
            string holdLoc = "", holdVat = "";
            if (Detail.MatCode == "")
            {
                this.cmb_Loc.Text = "";
                return;
            }
            else
            {
                var temp = data.GetLoc(CurMat);
                if (temp != null)
                {
                    this.Changing = true;
                    if (LastLocUsed != null && LastLocUsed.Trim().Length > 0)
                    {
                        var row = (temp.Exists(r => r.Key == LastLocUsed)) ? temp.Find(r => r.Key == LastLocUsed) : null;
                        if (row == null)
                        {
                            cmb_Loc.Focus();
                            return;
                        }
                        CurLoc = row.Key;
                        if (data.SystemOptionsDictionary["VAT_HEADER_MATCH_DETAIL"].ToBoolean() == false)
                            cmb_DetailVatCode.Text = row.Value.ToString();
                    }
                    else if (USER_MUST_CHOOSE_LOC && !(m_addItems.Checked))
                    {
                        cmb_Loc.Text = "";
                        cmb_Loc.Focus();
                        cmb_Loc.SelectAll();
                    }
                    else
                    {
                        var row2 = (temp.Exists(r => r.Key == ((ComboBoxEntity)cmb_Entity.CurrentItem.Value).DefaultLocation)) ? temp.Find(r => r.Key == ((ComboBoxEntity)cmb_Entity.CurrentItem.Value).DefaultLocation) : temp[0];
                        CurLoc = row2.Key;
                        if (data.SystemOptionsDictionary["VAT_HEADER_MATCH_DETAIL"].ToBoolean() == false)
                            CurDetailVat = row2.Value.ToString();
                    }
                    if (data.SystemOptionsDictionary["VAT_HEADER_MATCH_DETAIL"].ToBoolean())
                    {
                        //eb_VatCode.Text = CurVat;
                        eb_Vat2.Text = data.GetVatPercentage(CurDetailVat);
                    }
                    Changing = false;
                }
            }
        }



        #endregion Detail

        #region Set Visibility
        public void SetHeaderFields(bool state)
        {
            eb_Po_No.ReadOnly = state;
            cmb_Entity.ReadOnly = !state;
            cmb_Po_Type.ReadOnly = !state;
            cmb_Vat_Code.ReadOnly = !state;
            cmb_Project.ReadOnly = !state;
            cb_overnight.Enabled = state;
            cb_2ndDay.Enabled = state;
            cmb_Ship_To.ReadOnly = !state;
            eb_Placed_With.ReadOnly = !state;
            eb_Not_Total.ReadOnly = !state;
            eb_Req_No.ReadOnly = !state;
            eb_Special_Instructions.ReadOnly = !state;
            eb_Confirmation.ReadOnly = !state;
            eb_UD1.Enabled = state;
            eb_UD2.Enabled = state;
            eb_UD3.Enabled = state;
            eb_MM_Path.Enabled = state;
            eb_Man_Req_User.Enabled = state;
            eb_Nonfile_Contract.Enabled = state;
            vendorFrm.ReadOnly = !state;
        }

        public void SetDetailFields(bool state)
        {
            this.Changing = true;
            this.eb_PO_Number.ReadOnly = state;
            this.cmb_Mat.ReadOnly = false;
            this.cmb_Loc.ReadOnly = !state;

            this.cmb_Act.ReadOnly = !state;

            od_DeliverDate.Enabled = state;
            cmb_PoClass.ReadOnly = !state;
            this.cmb_Deliver.ReadOnly = !state;
            cmb_ProfileId.ReadOnly = !state;
            this.eb_Doctor_Id.Enabled = true;
            this.cmb_Purchase.ReadOnly = !state;
            this.cmb_Loc.ReadOnly = !state;
            this.cb_Accrue.Enabled = state;
            cmb_DetailVatCode.ReadOnly = true;
            this.eb_Vat2.ReadOnly = true;
            b_Dept.Visible = false;

            if (state)
            {
                if (tabControl1.SelectedTab == p_Detail)
                {
                    if (bs2.Count > 0)
                    {
                        if (viewMode1.Mode == ViewingMode.Editing)
                        {
                            if (Detail.SplitDetail)
                            {
                                this.eb_Unit_Cost2.ReadOnly = true;
                                this.eb_Quantity2.ReadOnly = true;
                                cmb_DetailVatCode.ReadOnly = true;
                                this.cmb_Act.ReadOnly = true;
                            }
                            if (data.SystemOptionsDictionary["ENTER_DEPT_USE_LOC_SUB"].ToBoolean() && Detail.NonFile == false)
                            {
                                /*Fix
                               // if ((cmb_Loc.CurrentItem.Value as LocationDetail).
                                this.q_Command.Parameters.Clear();
                                this.q_Command.CommandText = "SELECT type FROM loc WHERE mat_code = @mat_code AND location = @location";
                                this.q_Command.Parameters.Add("mat_code", SqlDbType.VarChar).Value = Detail.MatCode;
                                this.q_Command.Parameters.Add("location", SqlDbType.VarChar).Value = Detail.Location;
                                using (SqlDataReader q_query = this.q_Command.ExecuteReader())
                                {
                                    q_query.Read();
                                    if (q_query.HasRows)
                                    {
                                        if (q_query["type"].ToString().ToUpper() != "S")
                                        {
                                            if (Detail.SplitDetail == false)
                                                b_Dept.Visible = true;
                                        }
                                    }
                                }*/
                            }
                            if (data.SystemOptionsDictionary["VAT_HEADER_MATCH_DETAIL"].ToBoolean())
                            {
                                if (UseVatDetail)
                                    cmb_DetailVatCode.ReadOnly = false;
                            }
                            else
                                cmb_DetailVatCode.ReadOnly = false;
                        }
                    }
                }
            }

            this.eb_Unit_Cost2.ReadOnly = !state;
            this.eb_Quantity2.ReadOnly = !state;
            this.eb_Conversion.ReadOnly = !state;
            this.cb_Substitute_Item.Enabled = state;
            if (this.cb_Substitute_Item.Checked)
                this.cb_Substitute_Item.Enabled = false;

            this.Changing = false;
        }

        public void SetViewing()
        {
            int i = this.dbgrid1.CurrentCellAddress.Y;
            togglePagesToolStripMenuItem.Enabled = true;
            m_addItems.Enabled = false;
            m_addFreight.Enabled = false;
            reopenPurchaseOrderToolStripMenuItem.Enabled = false;
            /*  SetSplit;
  m_General_Vendor.enabled := false;            */
            this.InsertDetailLineMode = false;
            this.eb_Po_No.Visible = true;
            this.lbl_PONo.Visible = true;
            this.lbl_Status.Visible = true;
            this.eb_Status.Visible = true;
            this.lbl_PoGroup.Visible = false;
            this.cmb_POGroup.Visible = false;
            this.Updated_Master = false;
            this.Price_Changed = false;
            viewMode1.Mode = ViewingMode.Viewing;
            dbgrid1.Enabled = true;
            dbgrid1.ReadOnly = true;
            cmb_Mat.ReadOnly = false;
            this.SetHeaderFields(false);
            this.SetDetailFields(false);
            if (Header == null || Header.PONo == 0)
                ClearHeader();
            else
            {
                FillHeaderQuery();
                //this.fillcmbTermsCode();
                FillHeader();
            }
            if (tabControl1.SelectedTab == p_Detail)
            {
                if (bs2.Count == 0)
                {
                    this.Changing = true;

                    List_Detail.Add(_PDC.New());
                    bs2.MoveLast();

                    cmb_Mat.CurrentItem = null;
                    cmb_Act.CurrentItem = null;
                    this.ClearDetails();
                }
                else
                {
                    this.holdItem_Count = this.Detail.ItemCount;
                    this.Changing = true;
                    FillDetailQuery();
                    //dbgrid1.Rows[0].Selected = false;
                    //dbgrid1.Rows[i-1].Selected = true;
                    //dbgrid1.FirstDisplayedScrollingRowIndex = i-1;///makes sure you scroll down so first row is visible
                    //dbgrid1.Refresh();///refresh
                    bs2.Position = bs2.Find("ItemCount", holdItem_Count);
                    this.Changing = false;
                }
            }
            else
            {
                List_Detail = _PDC.GetList(CurrPo);
                bs2.DataSource = List_Detail;
            }
            if (this.viewMode1.Mode != ViewingMode.Inquiry)
            {
                NewEnabled = true;
                if (eb_Po_No.Text.Trim() == "" || eb_Po_No.Text == "0")
                    EditEnabled = false;
                else
                    EditEnabled = true;
                DeleteEnabled = true;
            }

            if (bs1.Count == 0)
            {
                EditEnabled = false;
                SetHeaderFields(false);
                SaveEnabled = false;
                DeleteEnabled = false;
                releasePurchaseOrderToolStripMenuItem.Enabled = false;
                eb_Po_No.Focus();
                return;
            }
            else
            {
                if (Header.Closed)
                {
                    EditEnabled = false;
                    DeleteEnabled = false;
                    if (tabControl1.SelectedTab == p_Detail)
                        NewEnabled = false;
                }
                if (Header.EDIStatus == 'A' || Header.EDIStatus == 'S' ||
                    (Header.FaxStatus == 'A') || (Header.FaxStatus == 'S') ||
                    (Header.EmailStatus == 'A') || (Header.EmailStatus == 'S') ||
                    (Header.PrintStatus == 'A') || (Header.PrintStatus == 'S') ||
                    (Header.Closed))
                {
                    releasePurchaseOrderToolStripMenuItem.Enabled = false;
                }
                else
                    releasePurchaseOrderToolStripMenuItem.Enabled = true;

                if ((Header.EDIStatus != '\0') || (Header.FaxStatus != '\0') || (Header.PrintStatus != '\0') || (Header.EmailStatus != '\0') || (Header.Closed))
                {
                    EditEnabled = false;
                    DeleteEnabled = false;
                    if (tabControl1.SelectedTab == p_Detail)
                        NewEnabled = false;
                }
                if (Header.Sent)
                {
                    if (Header.Cancelled)
                        reopenPurchaseOrderToolStripMenuItem.Enabled = false;
                    else
                        reopenPurchaseOrderToolStripMenuItem.Enabled = true;
                }
            }


            /*
              if autoreceive then
                begin
                  m_ReleasePurchaseOrder1.enabled := true;
                end;

              if not q_PoDetail.active or (q_PoDetail.recordcount = 0) then
            //  if q_Podetail.eof then
                begin
                  changing := true;
                  eb_Mat_Code.enabled := false;
                  eb_Vendor_Catalog.enabled := false;
                  eb_Mfg_Catalog.enabled := false;
                  eb_Description1.enabled := false;
                  changing := false;
                end;

              if NoChangesAtAll then
                begin
                  a_Edit.enabled := false;
                  if PageControl1.activepage = p_detail then a_New.enabled := false;
                end;
              a_save.enabled := False;

              if m_EditSentPurchaseOrder.checked then m_EditSentPurchaseOrderClick(self);
              DbGrid1.Repaint;
            end;
                        */
            if (reopenPurchaseOrderToolStripMenuItem.Checked)
                editsentpo(false);
            if (tabControl1.SelectedTab == p_Detail)
            {
                cmb_Mat.Focus();
                /*
                if (C_Detail == null)
                {
                    //                TS_Delete.Enabled = false;
                    //                TS_Edit.Enabled = false;
                    //                MS_Delete.Enabled = false;
                    //                MS_Edit.Enabled = false;
                }*/
            }
        }
        #endregion

        #region Actions

        #region New
        private void TS_New_Click(object sender, EventArgs e)
        {
            New1();
        }
        private void MS_New_Click(object sender, EventArgs e)
        {
            New1();
        }
        public void New1()
        {
            updateValidatings();
            Already_Save_Header = false;
            MainVendor = DialogResult.No;
            if (viewMode1.Mode == ViewingMode.Inquiry)
                return;

            if (data.SystemOptionsDictionary["RELEASE_REMINDER"].ToBoolean() && this.eb_Po_No.Text != "0")
            {
                if (this.tabControl1.SelectedTab == this.p_Header && (this.PoReleasedActiveOrSent() == false))
                {
                    if (MessageBox.Show("This PO has not yet been released.Would you like to release it now?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        M_ReleasePurchaseOrder();
                        return;
                    }
                }
            }
            if (this.tabControl1.SelectedTab == this.p_Detail && this.ENABLE_ADD_ITEMS)
                m_addItems.Enabled = true;

            if (tabControl1.SelectedTab == p_Detail)
            {
                CanSwitch = false;
                m_addFreight.Enabled = true;
                SetDetailFields(true);
            }
            else
                this.SetHeaderFields(true);
            viewMode1.Mode = ViewingMode.Adding;
            cmb_Mat.ReadOnly = false;
            if (EOMWarned == false)
                EOMWarning();

            if (this.tabControl1.SelectedTab == this.p_Detail)
            {
                if (Detail != null && Detail.ItemCount != 0)
                    holdItem_Count = Detail.ItemCount;
                NewDetail();
                if (data.SystemOptionsDictionary["USE_VAT_CODE_PODETAIL"].ToBoolean())
                    cmb_DetailVatCode.Items = data.prefillCombos("Vat", "");
            }
            if (this.tabControl1.SelectedTab == this.p_Header)
            {
                if (this.USE_PO_GROUPS && !data.SystemOptionsDictionary["MUST_USE_DEFAULT_PO_GROUP"].ToBoolean())
                {
                    data.Fillcmb_POGroups();
                    this.lbl_PoGroup.Visible = true;
                    this.cmb_POGroup.Visible = true;
                    this.lbl_PoGroup.Top = this.lbl_PONo.Top;
                    this.cmb_POGroup.Top = this.lbl_PONo.Top - 3;
                    this.eb_Po_No.Visible = false;
                    this.eb_Status.Visible = false;
                    this.lbl_PONo.Visible = false;
                    this.lbl_Status.Visible = false;
                }
                this.LastLocUsed = "";
                //if (Header == null)// || Header.PONo != 0m)
                {
                    List_Header = _PHC.New();
                    bs1.DataSource = List_Header;
                    CurrPo = 0;
                }
                ///fix me///
                ///
                //this.fillcmbShipTo();


                //this.fillcmbTermsCode();
                //this.fillcmbPoType();
                //m_General_Vendor.enabled := true;
                this.ClearHeader();
                vendorFrm = new FrmVendor();
                ClearDetails();
                List_Detail = new FilteredBindingList<PoDetail>();
                bs2.DataSource = List_Detail;
                
                if (data.SystemOptionsDictionary["USE_VAT_CODE_POHEADER"].ToBoolean())
                    cmb_Vat_Code.Items = data.prefillCombos("Vat", "");
                if (this.cmb_Entity.Enabled)
                    this.cmb_Entity.Focus();
            }
            InDetail = false;
            //Pnl_Vendor.ReadOnly = false;
            cmb_vendor.ReadOnly = true;
            dbgrid1.Enabled = false;
        }
        private void NewDetail()
        {
            Already_Save_Header = false;
            this.Changing = true;
            if (dbgrid1.CurrentCellAddress.Y >= 0)
                dbgrid1.Rows[dbgrid1.CurrentCellAddress.Y].Selected = false;
            List_Detail.Add(_PDC.New());
            bs2.DataSource = List_Detail;
            bs2.MoveLast();
            //eb_PO_Number.Text = CurrPo.ToString();
            Detail.PONo = CurrPo;
            eb_Entity.Text = CurrEntity;
            eb_Vendor.Text = CurVendor;
            cmb_Mat.Text = "";
            CurMat = null;
            //ClearDetails();

            SetDetailFields(true);
            m_addFreight.Enabled = true;
            if (cmb_Mat.ReadOnly == false)
            {
                cmb_Mat.Items = list_Mat;
                cmb_Mat.Focus();
            }
            if (this.CurVendor.ToUpper() == data.SystemOptionsDictionary["GENERAL_VENDOR"].ToNonNullString().ToUpper())
            {
                m_CreateNonFileClick();
                this.Changing = true;
                cmb_Mat.ReadOnly = true;
                this.Changing = false;
            }
            if (this.eb_Nonfile_Contract.Text.Trim() != "")
            {
                m_CreateNonFileClick();
                this.Changing = true;
                cmb_Mat.ReadOnly = true;
                this.Changing = false;
            }
            eb_Conversion.Text = "1";
            if (cmb_Mat.ReadOnly == false)
                Mat_Enter();
        }

        #endregion New

        #region Edit
        private void TS_Edit_Click(object sender, EventArgs e)
        {
            edit1();
        }
        private void MS_Edit_Click(object sender, EventArgs e)
        {
            edit1();
        }
        public void edit1()
        {
            bool sender = true;
            if (viewMode1.Mode == ViewingMode.Inquiry)
                return;
            //
            viewMode1.Mode = ViewingMode.Editing;
            Already_Save_Header = false;
            if (EOMWarned == false)
                EOMWarning();
            if (this.tabControl1.SelectedTab == this.p_Header)
            {
                this.SetHeaderFields(true);
                cmb_vendor.ReadOnly = true;
                if (bs2.Count > 0)
                {
                    cmb_Entity.ReadOnly = true;
                    vendorFrm.ReadOnly = true;
                    if (PoReleasedActiveOrSent()
                        && (((ComboBoxPoType)cmb_Po_Type.CurrentItem.Value).Frequency
                        || (cmb_Po_Type.CurrentItem.Value as ComboBoxPoType).Prepay
                        || Not_Exceed
                        || AutoReceive))
                        cmb_Po_Type.ReadOnly = true;
                }
                if ((CurVendor == data.SystemOptionsDictionary["GENERAL_VENDOR"].ToNonNullString()) && data.SystemOptionsDictionary["GENERAL_VENDOR"].ToNonNullString().Length > 0)
                    vendorFrm.ReadOnly = false;
                vendorFrm.ReadOnly = data.GetNonFile(CurrPo);
                if (vendorFrm.ReadOnly == false)
                    vendorFrm.ReadOnly = data.GetVoucher(CurrPo);
            }
            else
            {
                #region Detail
                this.SetDetailFields(true);
                cmb_Mat.ReadOnly = true;

                if (bs2.Count == 0)
                {
                    SetViewing();
                    return;
                }
                holdItem_Count = Detail.ItemCount;
                CanSwitch = false;
                if (this.Detail.Contract.Trim() != "" && !(cmb_Po_Type.CurrentItem.Value as ComboBoxPoType).ReturnRepair && this.eb_Nonfile_Contract.Text.Trim() == "")
                {
                    this.Changing = true;
                    if (data.SystemOptionsDictionary["NOT_ALLOW_LOWER_CONTRACT_COST"].ToBoolean())
                        eb_Unit_Cost2.ReadOnly = true;
                    else
                        eb_Unit_Cost2.ReadOnly = false;
                    //cmb_Mfg_Name.ReadOnly = true;

                    //the only way to get in here is from the menu, to modify sent po
                    if (Header.Sent || cb_Substitute_Item.Checked)
                    {
                        cmb_Purchase.ReadOnly = false;
                        eb_Conversion.ReadOnly = false;
                        eb_Unit_Cost2.ReadOnly = false;
                        //cmb_Mfg_Name.ReadOnly = false;
                    }
                    if (Detail.SplitDetail)
                        eb_Unit_Cost2.ReadOnly = true;
                    Changing = false;
                }
                dbgrid1.Enabled = false;
                if (this.cmb_Loc.Enabled)
                {
                    this.cmb_Loc.Focus();
                }
                #endregion Detail
            }
            //string hold = this.Detail.Location;

            /*      if q_PoDetail.eof  then
    begin
      if (sender = PageControl1) or (sender = a_Save) then
        begin
          a_NewExecute(self);
          exit;
        end
      else
        begin
          SetViewing;
          exit;
        end;
    end
  else
   begin
    if q_PoDetail.FieldByName('Nonfile').asboolean = true then
      begin
        eb_Location.enabled := false;
        eb_Vendor_Catalog.enabled := true;
        eb_Mfg_Catalog.enabled := true;
        eb_Mfg_Name.enabled := true;
        eb_Description1.enabled := true;
        cb_Substitute_Item.enabled := false;
        if eb_Account_No.enabled then eb_Account_No.setfocus;
      end;
   end;
end;
            */
        }


        #endregion Edit

        #region Save
        private void TS_Save_Click(object sender, EventArgs e)
        {
            save1(false);
        }
        private void MS_Save_Click(object sender, EventArgs e)
        {
            save1(false);
        }
        void save1(bool alreadyvalidated)
        {
            int i, nextpo, calcnum;
            decimal hold;
            bool flag, UpdateFLines;//, firsttime

            int row_no = this.dbgrid1.CurrentCellAddress.Y;

            if (viewMode1.Mode == ViewingMode.Inquiry)
                return;
            //eb_Tab is a dummy edit box.  It is used here to make sure exit events occur
            //Clicking the save button does not make the exit events occur
            // if (PageControl1.ActivePage = p_Detail) and eb_Tab.enabled then eb_Tab.setfocus;

            // if sender = eb_Vendor_Catalog then eb_Vendor_Catalogexit(self);

            #region//Header Tab
            if (tabControl1.SelectedTab == p_Header)
            {
                eb_Po_No.Focus();
                if (alreadyvalidated == false)
                {
                    if (CheckHeader() == false)
                        return;
                    if (ValidateRecord() == false)
                        return;
                }
                CanSwitch = true;
                #region//Adding
                if (viewMode1.Mode == ViewingMode.Adding)
                {
                    if (Already_Save_Header)
                        return;
                    Changing = true;
                    #region//From Entity
                    if (data.SystemOptionsDictionary["NEXT_PO_FROM_ENTITY"].ToBoolean())
                    {
                        CurrPo = data.GetNextPoNo("Entity", CurrEntity);
                    }
                    #endregion
                    #region //From Po Groups
                    else if (USE_PO_GROUPS)
                    {
                        CurrPo = data.GetNextPoNo("Pogroups", Header.PoGroupId.ToString());
                    }
                    #endregion
                    #region//Next Po Number From SysFile
                    else
                    {
                        CurrPo = data.GetNextPoNo("SysFile", "");
                    }
                    #endregion
                    Changing = false;
                    if (SaveNewHeader() == false)
                        return;
                    //GetPo();
                    SetViewing();
                    if (CurVendor.ToUpper() != data.SystemOptionsDictionary["GENERAL_VENDOR"].ToNonNullString().ToUpper())
                    {
                        /// <summary>
                        /// After save if not general vendor switch and create a new blank detail automatically for user quickness
                        /// </summary>
                        /// <param name="sender"></param>
                        /// <param name="e"></param>
                        tabControl1.SelectedTab = p_Detail;
                        New1();
                        //I need a fix for this          tabControl1.SelectedTab = p_Detail;
                        //List_Detail.Add(new PoDetail());//new FilteredBindingList<PoDetail>();
                        //New1();
                    }
                }
                #endregion
                #region//Editing
                else if (viewMode1.Mode == ViewingMode.Editing)
                {
                    //FillHeaderQuery();
                    SaveEditHeader();
                    FillHeaderQuery();
                    //  FillHeaderQuery();
                    //  FillDetailQuery();
                    SetViewing();
                }
                #endregion
                return;
            }
            #endregion
            #region//Detail Tab
            if (tabControl1.SelectedTab == p_Detail)
            {
                label5.Focus();
                if (CurrentUOPPrime == "None")
                {
                    MessageBox.Show("Can't set UOM to NONE");
                    return;
                }
                if (alreadyvalidated == false)
                {
                    if (ValidateRecord() == false)
                        return;
                }
                #region//Adding
                if (viewMode1.Mode == ViewingMode.Adding)
                {
                    #region//Is Frequency
                    if ((cmb_Po_Type.CurrentItem.Value as ComboBoxPoType).Frequency)
                    {
                        FrequencySave();
                    }
                    #endregion
                    else
                    {
                        if (PoReleasedActiveOrSent())
                        {
                            SqlTransaction sqlTransaction = data._Com.Connection.BeginTransaction("SaveDetailActiveorsent");
                            data._Com.Transaction = sqlTransaction;
                            try
                            {
                                SaveNewDetail();
                                FillDetailQuery();
                                //SetViewing();
                                bs2.Position = bs2.Find("ItemCount", holdItem_Count);
                                //UpdateFilesForSingleItem(1);
                                //WriteToPoDetailChange("I");
                                //UpdatePoHeaderClosed_FillDate_ItemsRecv;
                                sqlTransaction.Commit();
                            }
                            catch (Exception SDAS)
                            {
                                sqlTransaction.Rollback();
                                MessageBox.Show("Error in transaction SDAS trying to save Detail.  Please Contact EHS."
                                          + "\nTransaction was Rolled Back\n" + SDAS.ToString(), "Error", MessageBoxButtons.OK);
                            }
                            data._Com.Transaction = null;
                            data.Close();
                            if (AutoReceive)
                            {
                                PoStatus = new EHS.POControl.PoStatus.FormPoStatus(data._Com.Connection.ConnectionString, CurrPo, Detail.ItemCount,                                     "ALL");
                                PoStatus.ProcessQuantityRec(0, "ALL", 0, false);
                                FillDetailQuery();
                            }
                        }
                        else
                            SaveNewDetail();
                    }
                    FillDetailQuery();
                    //SetViewing();
                    bs2.Position = bs2.Find("ItemCount", holdItem_Count);
                    //Fixed the jump around which happened with refreshing the grid
                    // dbgrid1.Rows[0].Selected = false;
                    //dbgrid1.Rows[row_no + 1].Selected = true;
                    if ((cmb_Po_Type.CurrentItem.Value as ComboBoxPoType).Frequency)
                        return;
                    #region//taken out in delphi
                    //                           if not q_PoDetail.fieldbyname('split_detail').asboolean and (eb_Account_No.text = splitacct) then
                    //          begin
                    //            m_SplitDetailLine1Click(self);
                    //            exit;
                    //          end;
                    #endregion
                    if (data.SystemOptionsDictionary["CAN_UPDATE_LOC_ACCOUNT"].ToBoolean())
                        UpdateAccount();
                    New1();
                    if (m_addItems.Checked)
                        AddItemsToIMF();
                    return;
                }
                #endregion
                #region//Editing
                if (viewMode1.Mode == ViewingMode.Editing)
                {
                    UpdateFLines = false;
                    if ((cmb_Po_Type.CurrentItem.Value as ComboBoxPoType).Frequency && (Detail.FrequencyBatch != 0))//Check for Frequency Batch for POs before this update
                    {
                        if (MessageBox.Show("Do you want to update future frequency lines with this change? \nDeliver date will not change.", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            UpdateFLines = true;
                            if (data.ValidateFrequency(CurrPo, Detail.FrequencyPeriod, Detail.FrequencyBatch))
                            {
                                MessageBox.Show("There are lines on an Invoice.  These lines will not be updated.", "Warning", MessageBoxButtons.OK);
                                UpdateFLines = false;
                            }
                        }
                    }
                    #region//Po  either sent, active, or released
                    if (PoReleasedActiveOrSent())
                    {
                        #region 
                        /*
                        SqlTransaction Trans2;
                        int y = this.dbgrid1.CurrentCellAddress.Y;

                        Trans2 = sqlConnection1.BeginTransaction("Transaction2");
                        q_Command.Transaction = Trans2;
                        try
                        {
                            holdItem_Count = Detail.ItemCount;
                            Changing = true;
                            //FillDetailQuery();//get current data
                            //so it only selects the one row. not the first and...
                            //dbgrid1.Rows[0].Selected = false;
                            //dbgrid1.Rows[y].Selected = true;
                            //bs2.Position = bs2.Find("ItemCount", holdItem_Count);
                            Changing = false;

                            #region//Is Frequency and updateflines
                            if ((cmb_Po_Type.CurrentItem.Value as ComboBoxPoType).Frequency && UpdateFLines)//updateFlines cannot be true if autoreceive
                            {
                                FillFrequencyQuery();
                                foreach (Frequency _F in _Freq)
                                {
                                    Changing = true;
                                    bs2.Position = bs2.Find("ItemCount", _F.Item_Count);
                                    Changing = false;
                                    if (_F.Qty_on_invoice > 0)
                                    {
                                        MessageBox.Show("Line " + _F.Item_Count + " Can't be changed.  It's on an invoice", "Warning", MessageBoxButtons.OK);
                                        continue;
                                    }
                                    if (_F.NonFile == false)
                                    {
                                        q_Command.Parameters.Clear();
                                        q_Command.CommandText = "SELECT On_Hand, Type From Loc WHERE Mat_Code = @Mat_Code AND Location = @Location";
                                        q_Command.Parameters.AddWithValue("Mat_Code", _F.Mat_Code);
                                        q_Command.Parameters.AddWithValue("Location", _F.Location);
                                        using (SqlDataReader Read1 = q_Command.ExecuteReader())
                                        {
                                            if (_F.Qty_received > 0)
                                            {
                                                Read1.Read();
                                                if (Read1.HasRows)
                                                {
                                                    if ((Read1[1].ToString() != "N") && (Read1[0].ToInt32() <
                                                        (_F.Qty_received * _F.Conversion)))
                                                    {
                                                        MessageBox.Show("In order to make this change we need to un-receive these lines."
                                                                  + "\nThere is not enough on hand to unreceive this line.  Changes "
                                                                  + "\nwill not be made for this line or any lines after."
                                                                  + "\nLine Number: " + _F.Item_Count, "warning", MessageBoxButtons.OK);
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    try
                                    {
                                        WriteToPoDetailChange("U");
                                        //if (AutoReceive())
                                        //   receiveline("NEGALL");
                                        UpdateFilesForSingleItem(-1);
                                        SaveEditDetail();

                                        Changing = true;
                                        //FillDetailQuery();
                                        bs2.Position = bs2.Find("ItemCount", _F.Item_Count);
                                        Changing = false;
                                        UpdateFilesForSingleItem(1);
                                        //if (AutoReceive)
                                        //  receiveline("ALL");
                                        //UpdatePoHeaderClosed_FillDate_ItemsRecv;
                                        Trans2.Commit();
                                    }
                                    catch (Exception ee)
                                    {
                                        System.ArgumentException arg2 = new ArgumentException("Error In Transaction (4) " + ee.ToString());
                                        throw arg2;
                                    }
                                }
                            }
                            #endregion
                            else
                            {
                                try
                                {
                                    WriteToPoDetailChange("U");
                                    UpdateFilesForSingleItem(-1);
                                    SaveEditDetail();
                                    FillDetailQuery();
                                    //so it only selects the one row. not the first and...
                                    dbgrid1.Rows[0].Selected = false;
                                    dbgrid1.Rows[y].Selected = true;
                                    Changing = true;
                                    bs2.Position = bs2.Find("ItemCount", holdItem_Count);
                                    Changing = false;

                                    UpdateFilesForSingleItem(1);
                                    //UpdatePoHeaderClosed_FillDate_ItemsRecv;
                                    Trans2.Commit();
                                }
                                catch (Exception ep)
                                {
                                    System.ArgumentException arg3 = new ArgumentException("Error In Transaction (5) " + ep.ToString());
                                    throw arg3;
                                }
                            }
                            if (AutoReceive)
                            {
                                PoStatus = new EHS.POControl.PoStatus.FormPoStatus(CurrPo.ToDecimal(), Detail.ItemCount, "ALL");
                                PoStatus.ProcessQuantityRec(0, "ALL", 0, false);
                                FillDetailQuery();
                            }
                            SetViewing();
                            //so it only selects the one row. not the first and...
                            dbgrid1.Rows[0].Selected = false;
                            dbgrid1.Rows[y].Selected = true;
                            //  dbgrid1.Rows[row_no].Selected = true;
                            if ((cmb_Po_Type.CurrentItem.Value as ComboBoxPoType).Frequency)
                                bs2.Position = bs2.Find("ItemCount", Item_Count);
                            else
                                bs2.Position = bs2.Find("ItemCount", holdItem_Count);
                        }
                        catch (Exception er)
                        {

                            MessageBox.Show("Transaction rolled back " + er.ToString(), "error", MessageBoxButtons.OK);
                            Trans2.Rollback();
                        }    */
                        #endregion
                    }

                    #endregion
                    else
                    {
                        decimal Item_Count = 0m;
                        if ((cmb_Po_Type.CurrentItem.Value as ComboBoxPoType).Frequency && UpdateFLines)//updateFlines cannot be true if autoreceive
                        {
                            Item_Count = Detail.ItemCount;
                            var _Freq = data.GetFrequency(CurrPo, Detail.FrequencyPeriod, Detail.FrequencyBatch);
                            FillDetailQuery();

                            foreach (Frequency _F in _Freq)
                            {
                                Changing = true;
                                ///Looks like you don't use frequency, you update the future items with current shit.
                                ///You keep the old deliver date though to match the frequency
                                ///
                                Detail.ItemCount = _F.Item_Count;
                                var temp = (List<Ehs.Models.PoDetail>)bs2.DataSource;
                                Detail.DeliverDate = temp.Find(r => r.ItemCount == Detail.ItemCount).DeliverDate;
                                Changing = false;

                                SaveEditDetail();
                            }
                        }
                        else
                        {
                            holdItem_Count = Detail.ItemCount;
                            Changing = true;

                            SaveEditDetail();
                            Changing = false;
                            if (data.SystemOptionsDictionary["CAN_UPDATE_LOC_ACCOUNT"].ToBoolean())
                                UpdateAccount();
                        }
                        //Fixed the jump around which happened with refreshing the grid
                        dbgrid1.Rows[0].Selected = false;
                        dbgrid1.Rows[row_no].Selected = true;
                        if ((cmb_Po_Type.CurrentItem.Value as ComboBoxPoType).Frequency)
                            Detail.ItemCount = Item_Count;//bs2.Position = bs2.Find("ItemCount", Item_Count);
                        else
                            Detail.ItemCount = holdItem_Count;//bs2.Position = bs2.Find("ItemCount", holdItem_Count);
                    }
                    #region//taken out of delphi
                    //                    {       if not q_PoDetail.fieldbyname('split_detail').asboolean and (eb_Account_No.text = splitacct) then
                    //          begin
                    //            m_SplitDetailLine1Click(self);
                    //            exit;
                    //          end;}
                    #endregion

                    if (GOTO_NEXT_LINE_EDIT)
                    {
                        //                                    q_PoDetail.next;
                        //            if q_PoDetail.eof then q_PoDetail.Locate('Item_Count',HoldItem_Count,[])
                        //            else
                        //              begin
                        //                if m_EditSentPurchaseOrder.checked then UpdateClosedIfReopened;
                        //                a_Editexecute(self);
                        //                exit;
                        //              end;
                    }
                }
                #endregion
                //if (reopenPurchaseOrderToolStripMenuItem.Checked)
                //   UpdateClosedIfReopened();
                SetViewing();
            }
            #endregion
        }

        bool SaveHeader()
        {
            Header.PODate = dt_PO_Date.Value;
            if (cb_overnight.Checked)
            {
                if (CommentToAdd.Length > 0)
                {
                    if (Header.VendorMemo.Contains(CommentToAdd) == false)
                        Header.VendorMemo = CommentToAdd + "\n" + Header.VendorMemo;
                }
            }
            if (cb_2ndDay.Checked)
            {
                if (CommentToAdd2nd.Length > 0)
                {
                    if (Header.VendorMemo.Contains(CommentToAdd2nd) == false)
                        Header.VendorMemo = CommentToAdd2nd + "\n" + Header.VendorMemo;
                }
            }
            return true;
        }

        bool SaveNewHeader()
        {
            if (Already_Save_Header)
                return false;
            if (SaveHeader() == false)
                return false;
            Header.CreationCode = "PoEntry9";
            Header.BuyerUsername = SqlUsername;
            Header.OriginalUsername = SqlUsername;
            Header.SystemDate = DateTime.Today;
            
            try
            {
                _PHC.Insert(Header);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            Already_Save_Header = true;
            return true;
        }

        public void SaveEditHeader()
        {
            data.Open();
            SqlTransaction sqlTransaction = data._Com.Connection.BeginTransaction("PoEditSave");
            data._Com.Transaction = sqlTransaction;

            try
            {
                if (PoReleasedActiveOrSent() && USE_SUBLEDGER_AMOUNT)
                {
                    if (cmb_Project.Text.Trim() != "")
                    {
                        if (eb_Total.Text.Trim() != "")
                        {
                            EhsUtil.Change_ProjectBudget(ref data._Com, cmb_Project.Text, data.SystemOptionsDictionary["MM_YEAR"].ToDecimal(),
                                                         data.SystemOptionsDictionary["MM_PERIOD"].ToDecimal(), eb_Total.Text.ToDecimal(), 'E');
                        }
                        else
                        {
                            EhsUtil.Change_ProjectBudget(ref data._Com, cmb_Project.Text, data.SystemOptionsDictionary["MM_YEAR"].ToDecimal(),
                                                         data.SystemOptionsDictionary["MM_PERIOD"].ToDecimal(), 0, 'E');
                        }
                    }
                }

                SaveHeader();
                _PHC.Update(Header);

                if (Header.ProjectNo != cmb_Project.Text)
                {
                    data._Com.Parameters.Clear();
                    data._Com.CommandText = "UPDATE PoDetail SET cer_id = @cer_id WHERE PO_NO = @Po_No";
                    data._Com.Parameters.AddWithValue("Po_No", CurrPo);
                    data._Com.Parameters.Add("cer_id", SqlDbType.Int).Value = data.GetCerId(cmb_Project.Text);
                    data._Com.ExecuteNonQuery();

                    data._Com.Parameters.Clear();
                    data._Com.CommandText = "UPDATE PoDetail SET Vendor_Id = @Vendor_Id, Return_Repair = @Return_Repair, ";
                    data._Com.CommandText += " interfaced = case when interfaced = 'I' then 'C' when interfaced = 'C' then 'C' else '' end WHERE PO_NO = @Po_No";
                    data._Com.Parameters.AddWithValue("Po_No", CurrPo);
                    data._Com.Parameters.AddWithValue("Vendor_Id", CurVendor);
                    data._Com.Parameters.AddWithValue("Return_Repair", (cmb_Po_Type.CurrentItem.Value as ComboBoxPoType).ReturnRepair);
                    data._Com.ExecuteNonQuery();
                }
                sqlTransaction.Commit();
            }
            catch (Exception ee)
            {
                sqlTransaction.Rollback();
                data.Close();
                MessageBox.Show("Error in Transaction, Save EditHeader, Please Contact EHS.\n" + ee.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            data._Com.Transaction = null;
            data.Close();
        }

        void SaveDetail()
        {
            /*
            q_Command.Parameters.Clear();
            q_Command.CommandText = "UPDATE ItemVend SET Vendor_Memo = @VM WHERE Mat_Code = @Mat AND Vendor_Id = @VI";
            q_Command.Parameters.AddWithValue("VM", Item_Memo.ToNonNullString());
            q_Command.Parameters.AddWithValue("Mat", Detail.MatCode);
            q_Command.Parameters.AddWithValue("VI", Detail.VendorID);
            q_Command.ExecuteNonQuery();
            */
            Detail.PONo = CurrPo;
            Detail.Entity = CurrEntity;
            Detail.VendorID = eb_Vendor.Text;
            Detail.UnitPurchase = CurrentUOPPrime;
            if (Detail.Contract == "")
                Detail.Contract = eb_Nonfile_Contract.Text;
            //Detail.BuyerMemo = DetailBuyerMemo;
            //Detail.VendorMemo = DetailVendorMemo;
            Detail.VATAmount = Detail.UnitCost / 100 * Detail.VatPercentage;
            Detail.OriginalQuantityOrdered = Detail.QtyOrder;
            Detail.ProgramEdited = "PoEntry9";
            Detail.LastUsername = SqlUsername;
            Detail.DateEdited = DateTime.Today;
        }

        public void SaveNewDetail()
        {
            string_Build.Length = 0;

            SaveDetail();

            Detail.CERId = data.GetCerId(Header.ProjectNo);
            Detail.Description2 = _IMF?.Description2 ?? "";

            if (!InsertDetailLineMode)
            {
                Detail.ItemCount = data.GetItemCount(CurrPo);
            }

            Detail.BuyerUsername = SqlUsername;
            Detail.ReturnRepair = (cmb_Po_Type.CurrentItem.Value as ComboBoxPoType).ReturnRepair;

            if (Detail.MatCode == data.SystemOptionsDictionary["FREIGHT_MAT_CODE"])
            {
                Detail.QtyReceived = Detail.QtyOrder;
                Detail.LastQtyReceived = Detail.QtyOrder;
                Detail.ReceiptDate = DateTime.Today;
            }
            else
            {
                if ((cmb_Po_Type.CurrentItem.Value as ComboBoxPoType).Frequency && AutoReceive)
                    Detail.ReceiptDate = DeliverDate;
            }
            Detail.DeliverDate = DeliverDate;

            _PDC.Insert(Detail);

            if (Price_Changed)
            {
                /*
                sys.Read("EOD_BATCH_NUMBER");
                string_Build.Length = 0;

                q_Command.Parameters.Clear();
                string_Build.Append("INSERT INTO PriceChange (Program_Name,Field_Name,Mat_Code,Location,Vendor_ID,Old_Price, New_Price,Username,EOD_Batch_No,Memo,");
                string_Build.Append(" PO_No,Contract,Vendor_Catalog,Unit_Of_Measure,Conversion, LUOI_Old_Price,LUOI_New_Price,Table_Name,Period,Year,Master_File_Updated) VALUES ");
                string_Build.Append(" (@Program_Name,@Field_Name,@Mat_Code,@Location,@Vendor_ID,@Old_Price,@New_Price,@Username,@EOD_Batch_No,@Memo,");
                string_Build.Append(" @PO_No,@Contract,@Vendor_Catalog,@Unit_Of_Measure,@Conversion,@LUOI_Old_Price,@LUOI_New_Price,@Table_Name,@Period,@Year,@Master_File_Updated) ");
                q_Command.CommandText = string_Build.ToString();
                q_Command.Parameters.Add("Program_Name", SqlDbType.VarChar).Value = "PoEntry";
                q_Command.Parameters.Add("Field_Name", SqlDbType.VarChar).Value = "Unit_Cost";
                q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = Detail.MatCode;
                q_Command.Parameters.Add("Vendor_ID", SqlDbType.VarChar).Value = _WVendor.Vendor.VendorID;
                q_Command.Parameters.Add("Location", SqlDbType.VarChar).Value = "";
                q_Command.Parameters.Add("Old_Price", SqlDbType.Float).Value = holdPO_Cost;
                q_Command.Parameters.Add("New_Price", SqlDbType.Float).Value = eb_Unit_Cost2.Text.ToDecimal();
                q_Command.Parameters.Add("Username", SqlDbType.VarChar).Value = SqlUsername;
                q_Command.Parameters.Add("EOD_Batch_No", SqlDbType.Int).Value = sys.SysFileValue.ToInt32();
                q_Command.Parameters.Add("Memo", SqlDbType.Text).Value = DBNull.Value;
                q_Command.Parameters.Add("PO_No", SqlDbType.Int).Value = eb_Po_No.Text.ToInt32();
                if (Detail.Contract.Trim() != "")
                { q_Command.Parameters.Add("Contract", SqlDbType.VarChar).Value = Detail.Contract; }
                else
                { q_Command.Parameters.Add("Contract", SqlDbType.VarChar).Value = eb_Nonfile_Contract.Text; }
                q_Command.Parameters.Add("Vendor_Catalog", SqlDbType.VarChar).Value = eb_Vendor_Catalog.Text;
                q_Command.Parameters.Add("Unit_Of_Measure", SqlDbType.VarChar).Value = CurrentUOPPrime;
                if (eb_Conversion.Text.ToDecimal() == 0m)
                {
                    q_Command.Parameters.Add("LUOI_Old_Price", SqlDbType.Float).Value = holdPO_Cost;
                    q_Command.Parameters.Add("LUOI_New_Price", SqlDbType.Float).Value = eb_Unit_Cost2.Text.ToDecimal();
                }
                else
                {
                    q_Command.Parameters.Add("LUOI_Old_Price", SqlDbType.Decimal).Value = holdPO_Cost / eb_Conversion.Text.ToDecimal();
                    q_Command.Parameters.Add("LUOI_New_Price", SqlDbType.Decimal).Value = eb_Unit_Cost2.Text.ToDecimal() / eb_Conversion.Text.ToDecimal();
                }
                q_Command.Parameters.Add("Conversion", SqlDbType.Decimal).Value = eb_Conversion.Text.ToDecimal();
                q_Command.Parameters.Add("Table_Name", SqlDbType.VarChar).Value = "PoDetail";
                q_Command.Parameters.Add("Period", SqlDbType.Int).Value = Fiscal_Period;
                q_Command.Parameters.Add("Year", SqlDbType.Int).Value = Fiscal_Year;
                q_Command.Parameters.Add("Master_File_Updated", SqlDbType.Bit).Value = Updated_Master;

                q_Command.ExecuteNonQuery();
                */
            }

            /*UPDATE PoHeader SET Item_count = (SELECT MAX(PoDetail.Item_Count) FROM PoDetail WHERE PoHeader.PO_No = PoDetail.PO_No),
Total =  (SELECT SUM(PoDetail.Qty_Order * (PoDetail.Unit_Cost + PoDetail.VAT_Amount)) as Total FROM PoDetail WHERE PoHeader.PO_No = PoDetail.PO_No)
WHERE PoHeader.PO_No = */

            eb_Total.Text = data.GetPoTotal(CurrPo).ToNonNullString();
            eb_Total2.Text = eb_Total.Text;
            data.UpdateHeaderFromDetail(CurrPo, Detail.ItemCount, Header.Total, (cmb_Act.CurrentItem.Value as AccountNo).BankAccount);

            /*
                        if ((DetailPatientMemo.Trim() != "") || (Patient_Name.Trim() != ""))
                        {
                            q_Command.Parameters.Clear();
                            q_Command.CommandText = "insert PatientPODetail (PO_No, Item_Count, Patient_Id, Patient_Memo, Patient_Name, Serial_No, Surgery_Date) values (@PO_No, @Item_Count, @Patient_Id, @Patient_Memo,  @Patient_Name, @Serial_No, @Surgery_Date) ";
                            q_Command.Parameters.AddWithValue("po_no", CurrPo);
                            q_Command.Parameters.Add("item_count", SqlDbType.Int).Value = holdItem_Count;
                            q_Command.Parameters.Add("patient_id", SqlDbType.VarChar).Value = Patient_Id;
                            q_Command.Parameters.Add("patient_memo", SqlDbType.Text).Value = DetailPatientMemo;
                            q_Command.Parameters.Add("Patient_Name", SqlDbType.VarChar).Value = Patient_Name;
                            q_Command.Parameters.Add("Serial_No", SqlDbType.VarChar).Value = Serial_No;
                            q_Command.Parameters.Add("Surgery_Date", SqlDbType.DateTime).Value = Surgery_Date;
                            q_Command.ExecuteNonQuery();

                            q_Command.Parameters.Clear();
                            q_Command.CommandText = "Insert Into PatientMemoAudit (Po_No, Item_Count, Username, Event_Type) Values (@Po_No, @Item_Count, @Username, @Event_Type)";
                            q_Command.Parameters.AddWithValue("po_no", CurrPo);
                            q_Command.Parameters.Add("item_count", SqlDbType.Int).Value = holdItem_Count;
                            q_Command.Parameters.Add("Username", SqlDbType.VarChar).Value = SqlUsername;
                            q_Command.Parameters.Add("Event_Type", SqlDbType.VarChar).Value = "I";
                            q_Command.ExecuteNonQuery();

                        } */
        }

        public void SaveEditDetail()
        {
            SaveDetail();
            //if (UpdateFLines == false)
            //    Detail.DeliverDate = DeliverDate;
            if (Detail.Interfaced == "I")
                Detail.Interfaced = "C";
            _PDC.Update(Detail);

            try
            {
                if (Detail.UnitCost != eb_Unit_Cost2.Text.ToDecimal())
                {/*
                    sys.Read("EOD_BATCH_NUMBER");
                    q_Command.Parameters.Clear();
                    string_Build.Length = 0;
                    string_Build.Append("INSERT INTO PriceChange");
                    string_Build.Append("(Program_Name,Field_Name,Mat_Code,Location,Vendor_ID,Old_Price,");
                    string_Build.Append("New_Price,Username,EOD_Batch_No,Memo,PO_No,Contract,");
                    string_Build.Append("Vendor_Catalog,Unit_Of_Measure,Conversion,LUOI_Old_Price,LUOI_New_Price,");
                    string_Build.Append("Table_Name,Period,Year,Master_File_Updated) Values ");
                    string_Build.Append("(@Program_Name,@Field_Name,@Mat_Code,@Location,@Vendor_ID,@Old_Price,");
                    string_Build.Append("@New_Price,@Username,@EOD_Batch_No,@Memo,@PO_No,");
                    string_Build.Append("@Contract,@Vendor_Catalog,@Unit_Of_Measure,@Conversion,@LUOI_Old_Price,");
                    string_Build.Append("@LUOI_New_Price,@Table_Name,@Period,@Year,@Master_File_Updated)");
                    q_Command.CommandText = string_Build.ToString();
                    q_Command.Parameters.Add("Program_Name", SqlDbType.VarChar).Value = "PoEntry";
                    q_Command.Parameters.Add("Field_Name", SqlDbType.VarChar).Value = "Unit_Cost";
                    q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = Detail.MatCode;
                    q_Command.Parameters.Add("Vendor_ID", SqlDbType.VarChar).Value = _WVendor.Vendor.VendorID;
                    q_Command.Parameters.Add("Location", SqlDbType.VarChar).Value = "";//why not put location in?
                    q_Command.Parameters.Add("Old_Price", SqlDbType.Float).Value = Detail.UnitCost;
                    q_Command.Parameters.Add("New_Price", SqlDbType.Float).Value = eb_Unit_Cost2.Text.ToDecimal();
                    q_Command.Parameters.Add("Username", SqlDbType.VarChar).Value = SqlUsername;
                    q_Command.Parameters.Add("EOD_Batch_No", SqlDbType.Int).Value = sys.SysFileValue.ToInt32();
                    q_Command.Parameters.Add("Memo", SqlDbType.Text).Value = DBNull.Value;
                    q_Command.Parameters.AddWithValue("po_no", CurrPo);
                    if (Detail.Contract.Trim() != "")
                        q_Command.Parameters.Add("Contract", SqlDbType.VarChar).Value = Detail.Contract;
                    else
                        q_Command.Parameters.Add("Contract", SqlDbType.VarChar).Value = eb_Nonfile_Contract.Text;
                    q_Command.Parameters.Add("Vendor_Catalog", SqlDbType.VarChar).Value = eb_Vendor_Catalog.Text;
                    q_Command.Parameters.Add("Unit_Of_Measure", SqlDbType.VarChar).Value = CurrentUOPPrime;
                    if (eb_Conversion.Text.ToInt32() == 0)
                    {
                        q_Command.Parameters.Add("LUOI_Old_Price", SqlDbType.Float).Value = Detail.UnitCost;
                        q_Command.Parameters.Add("LUOI_New_Price", SqlDbType.Float).Value = eb_Unit_Cost2.Text.ToDecimal();
                    }
                    else
                    {
                        q_Command.Parameters.AddWithValue("LUOI_Old_Price", Detail.UnitCost / eb_Conversion.Text.ToDecimal());
                        q_Command.Parameters.AddWithValue("LUOI_New_Price", eb_Unit_Cost2.Text.ToDecimal() / eb_Conversion.Text.ToDecimal());
                    }
                    q_Command.Parameters.Add("Conversion", SqlDbType.Float).Value = eb_Conversion.Text.ToInt32();
                    q_Command.Parameters.Add("Table_Name", SqlDbType.VarChar).Value = "PoDetail";
                    q_Command.Parameters.Add("Period", SqlDbType.Int).Value = Fiscal_Period;
                    q_Command.Parameters.Add("Year", SqlDbType.Int).Value = Fiscal_Year;
                    q_Command.Parameters.Add("Master_File_Updated", SqlDbType.Bit).Value = Updated_Master;
                    q_Command.ExecuteNonQuery();
                    */
                }
            }
            catch { }
            if (Detail.ReqNo != 0m)
                data.UpdateReqDetail(Detail);
            eb_Total.Text = data.GetPoTotal(CurrPo).ToNonNullString();
            eb_Total2.Text = eb_Total.Text;

            data.UpdateHeaderFromDetail(CurrPo, Detail.ItemCount, Header.Total, (cmb_Act.CurrentItem.Value as AccountNo).BankAccount);
            CanSwitch = true;
        }

        #endregion Save

        #region Cancel
        private void TS_Cancel_Click(object sender, EventArgs e)
        {
            Cancel1();
        }

        private void MS_Cancel_Click(object sender, EventArgs e)
        {
            Cancel1();
        }
        public void Cancel1()
        {
            InDetail = false;
            updateValidatings();
            int i = this.dbgrid1.CurrentCellAddress.Y;
            FormController.Canceling = true;
            Already_Save_Header = false;
            m_addItems.Checked = false;
            reopenPurchaseOrderToolStripMenuItem.Checked = false;
            gettingitem = false;
            //m_EditSentPurchaseOrder.checked := false;
            this.FillHeaderQuery();

            if (Header.PONo > 0 && tabControl1.SelectedTab == p_Detail)
            {
                if (bs2.Count == 0)
                {
                    ClearDetails();
                    SetViewing();
                }
                else
                {
                    holdItem_Count = Detail.ItemCount;
                    SetViewing();
                    Changing = true;
                    FillDetailQuery();
                    bs2.Position = bs2.Find("ItemCount", holdItem_Count);
                    FillDetails();
                    Changing = false;
                }
                CanSwitch = true;
            }
            else
            {
                List_Detail = new FilteredBindingList<PoDetail>();
                try
                {
                    bs2.Clear();
                    //bs2.DataSource = List_Detail;
                }
                catch (Exception exception1)
                {
                    MessageBox.Show(exception1.ToString());
                }
                ClearDetails();
                SetViewing();
            }
            if (viewMode1.Mode == ViewingMode.Editing && (tabControl1.SelectedTab == p_Detail))
            {
                if (Detail.SplitDetail == false)
                {
                    if (Detail.AccountNo == splitacct)
                    {
                        MessageBox.Show("You must enter split, or change the account", "information", MessageBoxButtons.OK);
                        //a_splitexecture();
                        return;
                    }
                }
            }
            //SetViewing();
            errorProvider1.Clear();
            FormController.Canceling = false;

            if (tabControl1.SelectedTab == p_Detail)
                eb_PO_Number.Focus();
            else
                eb_Po_No.Focus();
        }
        #endregion

        #region Delete
        private void TS_Delete_Click(object sender, EventArgs e)
        {
            delete1();
        }
        private void MS_Delete_Click(object sender, EventArgs e)
        {
            delete1();
        }
        private void delete1()
        {
            #region //Header
            if (this.tabControl1.SelectedTab == this.p_Header)
            {
                if (data.CanDelete(CurrPo, 0))
                {
                    MessageBox.Show("This Purchase Order can not be cancelled.  At least 1 item has been received");
                    return;
                }
                if (MessageBox.Show("Are you sure you want to Cancel this Purchase Order?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (USE_CANCEL_MEMO)
                    {
                        if (MessageBox.Show("Do you want to enter a reason you're cancelling?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                        }
                    }
                    if (this.PoReleasedActiveOrSent())
                        this.UpdateFiles(-1);
                    FillDetailQuery();
                    if (bs2.Count > 0)
                    {
                        foreach (PoDetail pd in bs2)
                        {
                            data.WriteToPoDetailChange(pd, "D");
                        }
                    }
                    data.Delete(CurrPo, 0);
                }

                this.eb_Po_No.Text = "0";
                this.FillHeaderQuery();
                this.FillDetailQuery();
                this.SetViewing();
            }
            #endregion //

            #region//Detail
            if (this.tabControl1.SelectedTab == this.p_Detail)
            {
                if (bs2.Count == 0)
                    return;

                #region//Multi Delete
                if (this.dbgrid1.SelectedRows.Count > 1)
                {
                    if (MessageBox.Show("Any lines that have been Received or are on an Invoice will not \nbe deleted.  \nDo you want to continue?", "Warning", MessageBoxButtons.YesNo) != DialogResult.No)
                    {
                        foreach (DataGridViewRow row in this.dbgrid1.SelectedRows)
                        {
                            var temp = (Ehs.Models.PoDetail)bs2[row.Index];
                            if (data.CanDelete(CurrPo, temp.ItemCount) == false)
                                data.Delete(CurrPo, temp.ItemCount);
                        }
                        this.UpdateHeaderTotal();
                        this.FillDetailQuery();
                    }
                }
                #endregion
                else
                {
                    this.holdItem_Count = Detail.ItemCount;
                    if (MessageBox.Show("Are you sure you want to delete this item?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (data.CanDelete(CurrPo, Detail.ItemCount) == false)
                            data.Delete(CurrPo, Detail.ItemCount);
                        this.UpdateHeaderTotal();
                        this.FillDetailQuery();
                        this.SetViewing();
                    }
                }
            }
            #endregion
        }


        #endregion delete

        #region Close
        private void exToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CloseForm())
            {
                AutoValidate = AutoValidate.Disable;
                closing = true;
                this.Close();
            }
        }

        bool CloseForm()
        {
            if (viewMode1.Mode == ViewingMode.Adding || viewMode1.Mode == ViewingMode.Editing)
            {
                DialogResult result = MessageBox.Show("Do you wish to save this record first?", "Warning", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Cancel:
                        {
                            return false;
                        }
                    case DialogResult.Yes:
                        {
                            if (ValidateRecord())
                                save1(true);
                            else
                                return false;
                            break;
                        }
                }
            }
            if (data.SystemOptionsDictionary["RELEASE_REMINDER"].ToBoolean() && CurrPo != 0)
            {
                if (PoReleasedActiveOrSent() == false)
                {
                    if (MessageBox.Show("This Po hasn't been released yet.  Would you like to release it now?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        M_ReleasePurchaseOrder();
                }
            }
            return true;
        }
        #endregion close

        #endregion Actions

        #region Menu
        private void reopenPurchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editsentpo(true);
        }
        public void editsentpo(bool a)
        {
            if (a && Header.Closed)
            {
                MessageBox.Show("This PO's Status will be Reopened when you increase any quantities ordered", "info", MessageBoxButtons.OK);
            }
            this.reopenPurchaseOrderToolStripMenuItem.Checked = true;
            this.Changing = true;
            this.eb_PO_Number.ReadOnly = true;
            this.Changing = false;
            NewEnabled = true;
            EditEnabled = true;
            DeleteEnabled = true;
        }

        private void releasePurchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            M_ReleasePurchaseOrder();
        }
        private void M_ReleasePurchaseOrder()
        {
            if ((viewMode1.Mode == ViewingMode.Adding && Detail.MatCode != "") || viewMode1.Mode == ViewingMode.Editing)
            {
                MessageBox.Show("You are in Editing or Adding Mode. \nYou must save or cancel before you can release the PO.", "Information", MessageBoxButtons.OK);
                return;
            }
            if (Header.EDIStatus == ' ' && Header.FaxStatus == ' ' && Header.EmailStatus == ' ' && Header.PrintStatus == ' ')
            {
                if (bs2.Count == 0)
                {
                    MessageBox.Show("There are no Detail Items. \n You can't release this Purchase Order.", "Information", MessageBoxButtons.OK);
                    return;
                }
            }

            if (Header.EDIStatus != ' ' || Header.FaxStatus != ' ' || Header.EmailStatus != ' ' || Header.PrintStatus != ' ')
            {
                if (AutoReceive)
                {
                    if (data.GetInvoiceReceived(CurrPo) > 0)
                    {
                        MessageBox.Show("Can't un-release this PO.  There are items on a voucher.", "Warning", MessageBoxButtons.OK);
                        return;
                    }
                }
                else
                {
                    if (data.GetQuantityReceived(CurrPo) > 0)
                    {
                        MessageBox.Show("Can't un-release this PO.  There were items received.", "Warning", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
            else
            {
                bool ev = false;
                decimal temp_Min;
                decimal temp_Fee;
                ev = data.GetMinOrder(out temp_Min, out temp_Fee, CurVendor, CurrEntity, "VendorMinOrder");
                if (temp_Min > this.eb_Total.Text.ToDecimal())
                {
                    if (MessageBox.Show(string.Concat(new string[]
                                {
                                    "The total on this PO doesn't meet the minimum value of ",temp_Min.ToString(), "\nset for this vendor/entity.This Vendors/Entities Fee is ", temp_Fee.ToString(),
                                    "\n Do you want to release this PO anyway?"
                                }), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
                if (ev)
                {
                    ev = data.GetMinOrder(out temp_Min, out temp_Fee, CurVendor, CurrEntity, "Vendor");
                    if (temp_Min > this.eb_Total.Text.ToDecimal())
                    {
                        if (MessageBox.Show(string.Concat(new string[]
                                    {
                                    "The total on this PO doesn't meet the minimum value of ", temp_Min.ToString(), "\nset for this Vendor.  This Vendor's Fee is ", temp_Fee.ToString(),
                                    "\n Do you want to release this PO anyway?"
                                    }), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
                if ((cmb_Entity.CurrentItem.Value as ComboBoxEntity).MinimumOrder > eb_Total.Text.ToDecimal())
                {
                    if (MessageBox.Show("The total on this PO doesn't meet the minimum value of " + temp_Min.ToString() + "\n set for this Entity.\n Do you want to release this PO anyways?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
            }
            if (!this.AutoReceive)
            {
                if (data.GetClosed(CurrPo))
                {
                    MessageBox.Show("Can't un-release a closed PO. ", "Warning", MessageBoxButtons.OK);
                    return;
                }
            }
            //need to refresh data in case anyone else released PO
            decimal holdItemCount = this.Detail.ItemCount;
            this.FillHeaderQuery();
            this.FillDetailQuery();
            this.SetViewing();
            //q_PoDetail.locate('Item_Count',holdItemCount,[]);
            if (Header.EDIStatus == 'A' || Header.EDIStatus == 'S' || Header.FaxStatus == 'A' ||
                Header.FaxStatus == 'S' || Header.EmailStatus == 'A' || Header.EmailStatus == 'S' ||
                Header.PrintStatus == 'A' || Header.PrintStatus == 'S' || (Header.Closed && !this.AutoReceive))
            {
                MessageBox.Show("PO status has changed", "warning", MessageBoxButtons.OK);
            }

            bool flag = false;

            PoStatus = new EHS.POControl.PoStatus.FormPoStatus(data._Com.Connection.ConnectionString, CurrPo.ToDecimal(), holdItemCount, "");
            try
            {
                if (PoStatus.ShowDialog() == DialogResult.OK)
                    flag = true;
            }
            finally
            { PoStatus.Close(); PoStatus.Dispose(); }
            FillHeaderQuery();
            FillDetailQuery();
            SetViewing();
            bs2.Position = bs2.Find("ItemCount", holdItem_Count);
            if (flag && (cmb_Po_Type.CurrentItem.Value as ComboBoxPoType).ReturnRepair)
                WriteReturn();
        }

        public void m_CreateNonFileClick()
        {
            PoDetail temp = new PoDetail();
            this.m_addItems.Checked = false;
            this.ClearDetails();

            this.Changing = true;
            if (Detail.MatCode != data.SystemOptionsDictionary["FREIGHT_MAT_CODE"])
                cmb_Mat.CurrentItem = null;
            if (bs2.Count > 1)
            {
                temp = ((PoDetail)bs2[bs2.Count - 2]);
                if (data.SystemOptionsDictionary["USE_LAST_ACCT_FOR_EVERYTHING_BUT_STOCK"].ToBoolean() && temp.AccountNo != "" && temp.AccountNo != this.splitacct)
                    this.cmb_Act.Text = temp.AccountNo;
            }

            if (data.SystemOptionsDictionary["USE_LAST_ACCT_FOR_EVERYTHING_BUT_STOCK"].ToBoolean())
            {
                try { this.cmb_Deliver.Text = temp.DeliverTo; }
                catch { cmb_Deliver.Text = ""; }
            }
            else
                data.GetDefaultDeliverTo(CurrEntity, cmb_Act.CurrentItem.Key);//FIX ME

            if (data.SystemOptionsDictionary["USE_LAST_PROFILE_ID"].ToBoolean() && (CurProfile == null || CurProfile.Trim() == ""))
            {
                CurProfile = temp.ProfileID;
            }

            this.Changing = false;
            if (this.cmb_Deliver.Text.Trim() == "")
                data.GetDefaultDeliverTo(CurrEntity, cmb_Act.CurrentItem.Key);
            Detail.NonFile = true;
            this.FillPrimeCombo("");
            //this.cb_Substitute_Item.Enabled = false;
            cmb_Loc.ReadOnly = true;
            if (Detail.MatCode != data.SystemOptionsDictionary["FREIGHT_MAT_CODE"])
            {
                this.Changing = true;
                cmb_Mat.Text = data.SystemOptionsDictionary["NONFILE_PREFIX"] + data.IncSys("NEXT_NONFILE_NUMBER").ToString();
                this.Changing = false;
            }
            if (this.cmb_Act.Text.Trim() == "" && this.cmb_Act.ReadOnly == false)
                this.cmb_Act.Focus();
            else
            {
                if (this.cmb_Deliver.Text.Trim() == "" && this.cmb_Deliver.ReadOnly == false)
                { this.cmb_Deliver.Focus(); }
            }
            //if (this.VatDetMustMatchHead && this.cmb_Vat_Code.SelectedIndex != 0)
            {
            }
        }

        private void m_createNon_Click(object sender, EventArgs e)
        {
            m_CreateNonFileClick();
        }

        private void togglePagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((tabControl1.SelectedTab == p_Header) && Header.VendorID == "")
            {
                MessageBox.Show("This is a reserved PO. \n You must set a Vendor before proceeding.", "Warning", MessageBoxButtons.OK);
                return;
            }
            //if (CanSwitch == false) { tabControl1.SelectedTab = p_Detail; return; }
            if (tabControl1.SelectedTab == p_Header)
                tabControl1.SelectedTab = p_Detail;
            else
                tabControl1.SelectedTab = p_Header;
        }
        private void m_addItems_Click(object sender, EventArgs e)
        {

        }

        private void addToCatologToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemToCatalog();
        }
        void AddItemToCatalog()
        {
            if (viewMode1.Mode != ViewingMode.Viewing)
                return;

            AddCatalog FrmAddCatalog = new AddCatalog(this);

            if (FrmAddCatalog.ShowDialog() != DialogResult.OK)
                return;

            string GoodMats = "", BadMats = "";
            foreach (PoDetail row in this.dbgrid1.SelectedRows)
            {
                if (data.InsertItemsToCatalog(FrmAddCatalog.CurCatalog, row.MatCode, row.Location))
                    GoodMats += "\n" + row.MatCode;
                else
                    BadMats += "\n" + row.MatCode;
            }

            MessageBox.Show("Items:" + GoodMats + " Have been added to the Catalog\nWhile Items: " + BadMats + "were already on the Catalog");
        }

        private void addItemToRSLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == p_Header)
                return;
            Ehs.Forms.AddToRSL AddItemToRsl = new Ehs.Forms.AddToRSL(data._Com.Connection, Detail.MatCode, CurrEntity, Detail.Location, cmb_Act.Text);
            AddItemToRsl.ShowDialog();
        }
        private void viewPOInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form FrmPoInfo = new POInfo(this);
            FrmPoInfo.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void resequenceLineNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Header.OriginalReleaseDate != null)
            {
                MessageBox.Show("Can't resequence this po.  It was previously release.", "warning", MessageBoxButtons.OK);
                return;
            }

        }

        private void viewBreakdownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetailBreakdown detailBreakdown = new DetailBreakdown(data.getDetailBreakdown(CurrPo));
            detailBreakdown.ShowDialog();
        }

        private void m_addFreight_Click(object sender, EventArgs e)
        {

        }

        private void addItemToToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void changeDeliverDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CngDeliverDate FrmCngDeliver = new CngDeliverDate(data, CurrPo);
            try
            {
                if (FrmCngDeliver.ShowDialog() == DialogResult.OK)
                {
                    DeliverDate = FrmCngDeliver.DeliverDate;
                }
            }
            finally { FrmCngDeliver.Close(); }
        }

        private void changeEntityToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editPOInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void poReportOnScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Ts_Putonhold_Click(object sender, EventArgs e)
        {

        }

        private void returnCancelledItemToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void splitDetailLineToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            data.Open();
            data._Com.Parameters.Clear();
            data._Com.CommandText = "SELECT min(po_no) as po_no FROM poheader WHERE po_no > @po_no";
            data._Com.Parameters.AddWithValue("po_no", CurrPo);
            CurrPo = data._Com.ExecuteScalar().ToDecimal();
            this.GetPo();
        }
        private void ts_previouspo_Click(object sender, EventArgs e)
        {
            data.Open();
            data._Com.Parameters.Clear();
            data._Com.CommandText = "SELECT max(po_no) as po_no FROM poheader WHERE po_no < @po_no";
            data._Com.Parameters.AddWithValue("po_no", CurrPo);
            CurrPo = data._Com.ExecuteScalar().ToDecimal();
            this.GetPo();
        }

        #endregion Menu

        #region Right Click Menu
        private void addToCatalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemToCatalog();
        }
        #endregion

        #region Procedures

        public bool PoReleasedActiveOrSent()
        {
            if (bs1.Count == 0)
                return false;
            else
            {
                if (Header.EDIStatus != '\0' || Header.FaxStatus != '\0' || Header.EmailStatus != '\0' || Header.PrintStatus != '\0')
                    return true;
                else
                    return false;
            }
        }

        #endregion Procedures

        public void WriteReturn()
        {
            /* Fix me
            int HoldInteger, Next_Return_No;

            q_Command.Parameters.Clear();
            q_Command.CommandText = "SELECT * FROM ReturnHeader WHERE Po_NO = @Po_No";
            q_Command.Parameters.AddWithValue("po_no", CurrPo);
            using (SqlDataReader Read = q_Command.ExecuteReader())
            {
                Read.Read();//a return for this po was already written
                if (Read.HasRows) { return; }
            }
            q_Command.Parameters.Clear();
            q_Command.CommandText = "SELECT Edi_Status, Email_Status, Fax_Status, Print_Status FROM PoHeader WHERE Po_No = @Po_No";
            q_Command.Parameters.AddWithValue("po_no", CurrPo);
            using (SqlDataReader Read2 = q_Command.ExecuteReader())
            {
                Read2.Read();
                if (!Read2.HasRows) { return; }
                if (Read2["Edi_Status"].ToString() == "" &&
                    Read2["Fax_Status"].ToString() == "" &&
                    Read2["Email_Status"].ToString() == "" &&
                    Read2["Print_Status"].ToString() == "") { return; }
            }
            if (sys.Inc("NEXT_RETURN_NUMBER"))
            {
                HoldInteger = 0;
                Next_Return_No = sys.SysFileValue.ToInt32();
                while (Next_Return_No != HoldInteger)
                {
                    q_Command.Parameters.Clear();
                    q_Command.CommandText = "SELECT Return_No FROM ReturnHeader WHERE Return_No = @Return_No";
                    q_Command.Parameters.Add("Return_No", SqlDbType.Int).Value = Next_Return_No;
                    using (SqlDataReader Read3 = q_Command.ExecuteReader())
                    {
                        Read3.Read();
                        if (Read3.HasRows)
                        {
                            sys.Inc("NEXT_RETURN_NUMBER");
                            Next_Return_No = sys.SysFileValue.ToInt32();
                        }
                        else
                        { HoldInteger = Next_Return_No; }
                    }
                }
            }
            else
            {
                MessageBox.Show("Next Return Number could not be generated."
                          + "\nYou must use the Return program to make a return"
                          + "\nfor this PO.", "warning", MessageBoxButtons.OK);
                return;
            }
            q_Command.Parameters.Clear();
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO ReturnHeader (Return_No, Return_Date, PO_No, PO_Date, Entity, FOB, ");
            builder.Append("Vendor_Id, Vendor_name, Vendor_Address1, Vendor_Address2, Vendor_Address3, ");
            builder.Append("Vendor_City, Vendor_State, Vendor_Zip, Vendor_Phone, Vendor_Account, Username, ");
            builder.Append("Credit_Type, Non_System_Return) VALUES (@Return_No, getdate(), @PO_No, @PO_Date, ");
            builder.Append("@Entity, @FOB, @Vendor_Id, @Vendor_Name, @Vendor_Address1, @Vendor_Address2, ");
            builder.Append("@Vendor_Address3, @Vendor_City, @Vendor_State, @Vendor_Zip, @Vendor_Phone, ");
            builder.Append("@Vendor_Account, @Username,  06, 0)");
            q_Command.CommandText = builder.ToString();
            q_Command.Parameters.Add("Return_No", SqlDbType.Int).Value = Next_Return_No;
            q_Command.Parameters.AddWithValue("po_no", CurrPo);
            q_Command.Parameters.AddWithValue("Po_Date", PoDate.AsDbDateTime());
            q_Command.Parameters.Add("Entity", SqlDbType.VarChar).Value = CurrEntity;
            q_Command.Parameters.Add("FOB", SqlDbType.VarChar).Value = eb_FOB.Text;
            q_Command.Parameters.Add("Vendor_Id", SqlDbType.VarChar).Value = _WVendor.Vendor.VendorID;
            q_Command.Parameters.Add("Vendor_Name", SqlDbType.VarChar).Value = _WVendor.Vendor.VendorName;
            q_Command.Parameters.Add("Vendor_Address1", SqlDbType.VarChar).Value = _WVendor.Vendor.Address1;
            q_Command.Parameters.Add("Vendor_Address2", SqlDbType.VarChar).Value = _WVendor.Vendor.Address2;
            q_Command.Parameters.Add("Vendor_Address3", SqlDbType.VarChar).Value = _WVendor.Vendor.Address3;
            q_Command.Parameters.Add("Vendor_City", SqlDbType.VarChar).Value = _WVendor.Vendor.City;
            q_Command.Parameters.Add("Vendor_State", SqlDbType.VarChar).Value = _WVendor.Vendor.State;
            q_Command.Parameters.Add("Vendor_Zip", SqlDbType.VarChar).Value = _WVendor.Vendor.Zip;
            q_Command.Parameters.Add("Vendor_Phone", SqlDbType.VarChar).Value = _WVendor.Vendor.PhoneNo;
            if (eb_VMShip_Account.Text.Trim() != "")
            { q_Command.Parameters.Add("Vendor_Account", SqlDbType.VarChar).Value = eb_VMShip_Account.Text; }
            else { q_Command.Parameters.Add("Vendor_Account", SqlDbType.VarChar).Value = eb_Vendor_Account.Text; }
            q_Command.Parameters.Add("Username", SqlDbType.VarChar).Value = SqlUsername;
            q_Command.ExecuteNonQuery();

            for (int a = 0; a < bs2.Count; a++)
            {
                bs2.Position = a;

                q_Command.Parameters.Clear();
                builder.Length = 0;
                builder.Append("INSERT INTO ReturnDetail (Return_No, Mat_Code, Description1, Description2, ");
                builder.Append("Vendor_Catalog, Mfg_Name, Location, Entity, Department, Sub_Account, Account_No, ");
                builder.Append("Profile_Id, Po_No, Vendor_Id, Qty_Order, Qty_Return, Unit_Purchase, Conversion, ");
                builder.Append("Unit_Cost, Vat_Amount, Qty_Received, Reason_Code, Qty_Replace, Replace_Item, ");
                builder.Append("Item_Count) VALUES (@Return_No, @Mat_Code, @Description1, @Description2, @Vendor_Catalog, ");
                builder.Append("@Mfg_Name, @Location, @Entity, @Department, @Sub_Account, @Account_No, @Profile_Id, @Po_No, ");
                builder.Append("@Vendor_id, @Qty_Order, @Qty_Return, @Unit_Purchase, @Conversion, @Unit_Cost, ");
                builder.Append("@Vat_Amount, 0, 09, 0, 0, @Item_Count)");
                q_Command.CommandText = builder.ToString();
                q_Command.Parameters.Add("Return_No", SqlDbType.Int).Value = Next_Return_No;
                q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = Detail.MatCode;
                q_Command.Parameters.Add("Description1", SqlDbType.VarChar).Value = Detail.Description1;
                q_Command.Parameters.Add("Description2", SqlDbType.VarChar).Value = Detail.Description2;
                q_Command.Parameters.Add("Vendor_Id", SqlDbType.VarChar).Value = _WVendor.Vendor.VendorID;
                q_Command.Parameters.Add("Vendor_Catalog", SqlDbType.VarChar).Value = Detail.VendorCatalog;
                q_Command.Parameters.Add("Mfg_Name", SqlDbType.VarChar).Value = Detail.MFGName;
                q_Command.Parameters.Add("Location", SqlDbType.VarChar).Value = Detail.Location;
                q_Command.Parameters.Add("Entity", SqlDbType.VarChar).Value = Detail.Entity;
                q_Command.Parameters.Add("Department", SqlDbType.VarChar).Value = Detail.Department;
                q_Command.Parameters.Add("Sub_Account", SqlDbType.VarChar).Value = Detail.SubAccount;
                q_Command.Parameters.Add("Account_No", SqlDbType.VarChar).Value = Detail.AccountNo;
                q_Command.Parameters.Add("Profile_Id", SqlDbType.VarChar).Value = Detail.ProfileID;
                q_Command.Parameters.AddWithValue("po_no", CurrPo);
                q_Command.Parameters.Add("Qty_Order", SqlDbType.Int).Value = Detail.QtyOrder;
                q_Command.Parameters.Add("Qty_Return", SqlDbType.Int).Value = Detail.QtyOrder;
                q_Command.Parameters.Add("Unit_Purchase", SqlDbType.VarChar).Value = Detail.UnitPurchase;
                q_Command.Parameters.Add("Conversion", SqlDbType.Int).Value = Detail.Conversion;
                q_Command.Parameters.Add("Unit_Cost", SqlDbType.Float).Value = Detail.UnitCost;
                q_Command.Parameters.Add("Vat_Amount", SqlDbType.Float).Value = Detail.VATAmount;
                q_Command.Parameters.Add("Item_Count", SqlDbType.Int).Value = Detail.ItemCount;
                q_Command.ExecuteNonQuery();
            }*/
        }

        void EOMWarning()
        {
            switch (EhsUtil.EOMWarning("MM", CurrEntity??cmb_Entity.Items[0].Key))
            {
                case 0:
                    {
                        EOMWarned = true;
                        break;
                    }
                case 1:
                    {
                        EOMWarned = false;
                        Cancel1();
                        return;
                    }
                case 2:
                    {
                        EOMWarned = false;
                        Cancel1();
                        return;
                    }
            }
        }

        public bool ValidateRecord()
        {
            decimal HoldIssCost = 0m, HoldAvgCost = 0m;
            bool rslrecord = false;
            bool result;

            errorProvider1.Clear();

            if (this.tabControl1.SelectedTab == this.p_Header)
            {
                #region//header
                try
                {
                    if (this.Not_Exceed_Header)
                    {
                        if (eb_Total.Text.ToDecimal() > eb_Not_Total.Text.ToDecimal())
                        {
                            if (MessageBox.Show("This a not to exceed header total PO type.\nThe total of this PO is greater than your not to exceed total.\nDo you want continue?", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                            { result = false; return result; }
                        }
                    }
                    if (cmb_vendor.HasValidated == false && CurVendor == null)
                    {
                        errorProvider1.SetError(cmb_vendor, "Pick a vendor");
                        return false;
                    }
                    if (cmb_Po_Type.HasValidated == false && CurrPoType == null)
                    {
                        errorProvider1.SetError(cmb_Po_Type, "Pick a PoType");
                        return false;
                    }
                    if (cmb_Ship_To.HasValidated == false && CurrShipTo == null)
                    {
                        errorProvider1.SetError(cmb_Ship_To, "Pick a ShipTo");
                        return false;
                    }

                    result = true;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("An Error Occurred while Verifying Header.\n" + ee.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    result = false;
                }
                #endregion
                ////////////////////////////
                ///////////////////////////////
                //////////////////////////////////
                /////////////////////////////////////
            }
            if (this.tabControl1.SelectedTab == this.p_Detail)
            {
                /*
                #region//detail
                ////////////////////////////Details Tab
                try
                {
                    ////////////////////Temp Holders
                    decimal temp_u_cost = 0m, temp_qty_order = 0m;
                    string temp_MFG_Name = "";
                    ///////////////////
                    if (viewMode1.Mode != ViewingMode.Adding)
                    {
                        temp_u_cost = Detail.UnitCost;
                        temp_MFG_Name = this.Detail.MFGName;
                        temp_qty_order = Detail.QtyOrder;
                    }
                    ///////////////////////////////////////

                    if ((cmb_Po_Type.CurrentItem.Value as ComboBoxPoType).ReturnRepair)
                        this.NonFile_Item = true; 

                    if (!this.NonFile_Item)
                    {
                        #region//Not NonFile


                        this.q_Command.Parameters.Clear();
                        q_Command.CommandText = "SELECT Active, Main_Vendor, dateadd(month, @dateadd1, last_ordered_date) as compare_date FROM ItemVend WHERE Mat_Code = @Mat_Code AND Vendor_Id = @Vendor_Id ";
                        this.q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = Detail.MatCode;
                        this.q_Command.Parameters.Add("Vendor_Id", SqlDbType.VarChar).Value = CurVendor;
                        this.q_Command.Parameters.Add("dateadd1", SqlDbType.Int).Value = this.POENTRY_NOT_ORDERED_MONTHS;
                        using (SqlDataReader q = this.q_Command.ExecuteReader())
                        {
                            q.Read();
                            if (q.HasRows)
                            {
                                if (viewMode1.Mode == ViewingMode.Adding && !q["Active"].ToBoolean())
                                {
                                    MessageBox.Show("This Item is Set Inactive for this Vendor", "Information", MessageBoxButtons.OK);
                                    if (cmb_Mat.ReadOnly == false)
                                        cmb_Mat.SelectAll();
                                    result = false;
                                    return result;
                                }
                                if (data.SystemOptionsDictionary["ASK_IF_NOT_MAIN_VENDOR"].ToBoolean() && viewMode1.Mode == ViewingMode.Adding && !q["Main_Vendor"].ToBoolean())
                                {
                                    if (MainVendor != DialogResult.Yes)
                                    {
                                        if (MessageBox.Show("This is not the Main Vendor for this item.\nDo you want to continue?", "Information", MessageBoxButtons.YesNo) == DialogResult.No)
                                        { return false; }
                                        MainVendor = DialogResult.Yes;
                                    }
                                }
                                if (this.POENTRY_NOT_ORDERED_MONTHS > 0)
                                {
                                    try
                                    {
                                        if (q["compare_date"].ToDateTime() < DateTime.Today)
                                        {
                                            if (MessageBox.Show("This item hasn't been ordered in the past\n" + this.POENTRY_PRICE_CHECK_AMOUNT + " month.\nDo you want to continue?", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                                            { return false; }
                                        }
                                    }
                                    catch
                                    {
                                        if (MessageBox.Show("This item hasn't been ordered in the past\n" + this.POENTRY_PRICE_CHECK_AMOUNT + " month.\nDo you want to continue?", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                                        { return false; }
                                    }
                                }
                            }
                            else
                            {
                                q.Close();
                                if (this.ENABLE_ADD_ITEMS && MessageBox.Show("This item you are attempting to add is not on file for this vendor. \nWould you like to add it now?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    LastLocUsed = cmb_Loc.Text;
                                    this.AddItemVend();
                                    this.FillDetailsWithMatCode("");
                                    result = false;
                                    return result;
                                }
                                else
                                {
                                    if (!this.Enable_Add_ItemVend)
                                    { MessageBox.Show("This Item isn't on file for this Vendor.", "Warning", MessageBoxButtons.OK); }
                                    this.Changing = true;
                                    this.eb_Vendor_Catalog.Text = "";
                                    this.b_MUOP.Visible = false;
                                    this.eb_MFG_Catalog.Text = "";
                                    this.eb_Description.Text = "";
                                    this.ClearDetails();
                                    if (cmb_Mat.ReadOnly == false)
                                        cmb_Mat.SelectAll();
                                    this.Changing = false;
                                    result = false;
                                    return result;
                                }
                            }
                        }
                        if (viewMode1.Mode == ViewingMode.Adding || (this.EditingRecord &&
                            eb_Quantity2.Text.ToDecimal() != temp_qty_order))
                        {
                            this.q_Command.Parameters.Clear();
                            this.q_Command.CommandText = "SELECT purchase_in_multiples_of FROM uop WHERE Mat_Code = @Mat_Code ";
                            this.q_Command.CommandText += "AND Vendor_Id = @Vendor_Id AND unit_purchase = @unit_purchase AND Vendor_catalog = @Vendor_catalog AND active = 1 ";
                            this.q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = Detail.MatCode;
                            this.q_Command.Parameters.Add("Vendor_Id", SqlDbType.VarChar).Value = this._WVendor.Vendor.VendorID;
                            this.q_Command.Parameters.Add("unit_purchase", SqlDbType.VarChar).Value = this.CurrentUOPPrime;
                            this.q_Command.Parameters.Add("Vendor_catalog", SqlDbType.VarChar).Value = this.eb_Vendor_Catalog.Text;
                            using (SqlDataReader q = this.q_Command.ExecuteReader())
                            {
                                q.Read();
                                if (q.HasRows)
                                {
                                    int v = q["purchase_in_multiples_of"].ToInt32();
                                    if (v > 1)
                                    {
                                        int z = eb_Quantity2.Text.ToInt32();
                                        if (v != 0)
                                        {
                                            if ((z % v) != 0)
                                            {
                                                if (MessageBox.Show("This vendor is requesting that you purchase this item in multiples of " + v.ToString() + "\n Do you want to continue?", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                                                {
                                                    this.eb_Quantity2.Focus();
                                                    result = false;
                                                    return result;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (Detail.Contract != "" && (temp_u_cost != eb_Unit_Cost2.Text.ToDecimal()) &&
                            this.cb_Substitute_Item.Checked == false)
                        {
                            this.q_Command.Parameters.Clear();
                            this.q_Command.CommandText = "SELECT Purchase_Cost FROM ContractDetail WHERE contract = @contract AND mat_code = @mat_code";
                            this.q_Command.Parameters.Add("contract", SqlDbType.VarChar).Value = Detail.Contract;
                            this.q_Command.Parameters.Add("mat_code", SqlDbType.VarChar).Value = Detail.MatCode;
                            using (SqlDataReader q = this.q_Command.ExecuteReader())
                            {
                                if (q.Read())
                                {
                                    if (q["Purchase_cost"].ToDecimal() < eb_Unit_Cost2.Text.ToDecimal())
                                    {
                                        MessageBox.Show("The unit cost cannot be greater than the contract cost, \nwhich is " + q["Purchase_cost"].ToString(), "Information", MessageBoxButtons.OK);
                                        if (this.eb_Unit_Cost2.Enabled)
                                        { this.eb_Unit_Cost2.Focus(); }
                                        result = false;
                                        return result;
                                    }
                                }
                            }
                        }

                        #endregion
                    }
                    if (Detail.Location != "" && (this.eb_Quantity2.Text.ToDecimal() != 0m))
                    {
                        this.q_Command.Parameters.Clear();
                        this.q_Command.CommandText = "SELECT Active, On_Order, Type, Entity, Vat_Code, Issue_Cost, Average_Cost FROM Loc join UserToLocation ul ";
                        this.q_Command.CommandText += "on ul.Location = loc.Location WHERE loc.Location = @Location AND Mat_Code = @Mat_Code AND ul.username = @username ";
                        this.q_Command.Parameters.Add("Location", SqlDbType.VarChar).Value = Detail.Location;
                        this.q_Command.Parameters.Add("Username", SqlDbType.VarChar).Value = this.SqlUsername;
                        this.q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = Detail.MatCode;
                        using (SqlDataReader q = this.q_Command.ExecuteReader())
                        {
                            q.Read();

                            HoldIssCost = q["Issue_Cost"].ToDecimal();
                            HoldAvgCost = q["Average_Cost"].ToDecimal();

                            if (this.VatDetMustMatchHead)
                            {
                                string VatCode = this.cmb_Vat_Code.SelectedItem.ToString().Substring(0, this.VatFieldSize).Trim();
                                if (this.cmb_Vat_Code.SelectedIndex == 0) { VatCode = ""; }
                                string VatCode2 = q["Vat_Code"].ToString();
                                if (VatCode2.Trim() == "") { VatCode2 = "NONE"; }
                                if (VatCode2 != VatCode)
                                {
                                    if (VatCode.Trim() == "")
                                    { VatCode = "NONE"; }
                                    if (MessageBox.Show(string.Concat(new string[]
                                        {
                                            "The Vat on file for this item is ",
                                            VatCode2,
                                            "\nwhich is different than the vat set on the header,\nwhich is ",
                                            VatCode,
                                            "\nAre you sure you want to save this item with Vat ",
                                            VatCode,
                                            "?"
                                        }), "error", MessageBoxButtons.YesNo) == DialogResult.No)
                                    {
                                        result = false;
                                        return result;
                                    }
                                }
                            }
                        }
                        this.q_Command.Parameters.Clear();
                        this.q_Command.CommandText = "SELECT rsl FROM deliverto WHERE deliver_to = @deliver_to AND entity = @entity ";
                        this.q_Command.Parameters.Add("deliver_to", SqlDbType.VarChar).Value = this.cmb_Deliver.Text;
                        this.q_Command.Parameters.Add("entity", SqlDbType.VarChar).Value = CurrEntity;
                        using (SqlDataReader read2 = this.q_Command.ExecuteReader())
                        {
                            read2.Read();
                            if (read2.HasRows) { rslrecord = read2["rsl"].ToBoolean(); }
                            else { rslrecord = false; }
                        }
                        if (rslrecord == false)
                        {
                            this.q_Command.Parameters.Clear();
                            this.q_Command.CommandText = "SELECT pd.Po_No, ph.Po_Date, Account_No, Qty_Order, Location, pd.Unit_Purchase, pd.Qty_Received FROM PoDetail pd   ";
                            this.q_Command.CommandText += "JOIN PoHeader ph   on ph.po_no = pd.po_no WHERE Mat_Code = @Mat_Code and ph.Entity = @Entity ";
                            this.q_Command.CommandText += "AND Location = @Location AND Account_No = @Account_No AND ph.Closed = @false and ph.Cancelled = @false ";
                            this.q_Command.CommandText += "AND (Qty_Order - Qty_Received) > 0 ORDER BY pd.PO_No ";
                            this.q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = Detail.MatCode;
                            this.q_Command.Parameters.Add("Location", SqlDbType.VarChar).Value = Detail.Location;
                            this.q_Command.Parameters.Add("Account_No", SqlDbType.VarChar).Value = Detail.AccountNo;
                            this.q_Command.Parameters.Add("Entity", SqlDbType.VarChar).Value = CurrEntity;
                            this.q_Command.Parameters.Add("false", SqlDbType.Bit).Value = false;
                            using (SqlDataReader read3 = this.q_Command.ExecuteReader())
                            {
                                read3.Read();
                                if (read3.HasRows)
                                {
                                    if (viewMode1.Mode == ViewingMode.Adding)
                                    {
                                        variables.Mat_Code = Detail.MatCode;
                                        variables.Location = Detail.Location;
                                        variables.Account_No = Detail.AccountNo;
                                        variables.Entity = CurrEntity;
                                        Form OnOrder = new OnOrder();
                                        try { OnOrder.ShowDialog(); }
                                        catch { }
                                        finally { OnOrder.Close(); }
                                        try
                                        {
                                            if (variables.result) { }
                                            else { return false; }
                                        }
                                        catch { return false; }
                                    }
                                }
                            }
                        }

                    }

                    if (this.Allow_Update_Master)
                    {
                        if (!this.NonFile_Item)
                        {
                            if (viewMode1.Mode == ViewingMode.Adding)
                            { //checkMUOP();
                            }
                            if (EditingRecord && (Detail.VendorCatalog != eb_Vendor_Catalog.Text ||
                                this.Detail.UnitPurchase != this.CurrentUOPPrime))
                            {                            //CheckMUOP();
                            }
                        }
                    }

                    try
                    {
                        Detail.AccountNo = cmb_Act.Text.Substring(0, cmb_Act.Text.IndexOf("     "));
                    }
                    catch
                    {
                        Detail.AccountNo = cmb_Act.Text;
                    }

                    this.q_Command.Parameters.Clear();
                    this.q_Command.CommandText = "SELECT Sub_Account, Department FROM AccountNo with(nolock) WHERE "
                                               + "Entity = @Entity AND Account_No = @Account_No ";
                    this.q_Command.Parameters.Add("Entity", SqlDbType.VarChar).Value = CurrEntity;
                    this.q_Command.Parameters.AddWithValue("Account_No", Detail.AccountNo);
                    using (SqlDataReader q2 = this.q_Command.ExecuteReader())
                    {
                        if (q2.HasRows)
                        {
                            q2.Read();
                            Detail.SubAccount = q2[0].ToNonNullString();
                        }
                    }

                    this.q_Command.Parameters.Clear();
                    q_Command.CommandText = "SELECT require_profile_id FROM subaccount with(nolock) WHERE sub_account = @sub_account ";
                    this.q_Command.Parameters.AddWithValue("sub_account", Detail.SubAccount);
                    using (SqlDataReader q = this.q_Command.ExecuteReader())
                    {
                        q.Read();
                        if ((this.eb_Profile_Id.Text.Trim() == "") && (q["require_profile_id"].ToBoolean() == true))
                        {
                            MessageBox.Show("This Sub Account requires a Profile ID \nYou must enter a Profile ID?", "Confirmation", MessageBoxButtons.OK);
                            if (this.eb_Profile_Id.Enabled) { this.eb_Profile_Id.Focus(); }
                            result = false; return result;
                        }
                    }
                    if (this.Require_Profile_Id)
                    {
                        if (this.eb_Profile_Id.Text.Trim() == "")
                        {
                            MessageBox.Show("This PO Type requires a Profile ID \nYou must enter a Profile ID", "Confirmation", MessageBoxButtons.OK);
                            if (this.eb_Profile_Id.Enabled) { this.eb_Profile_Id.Focus(); }
                            result = false; return result;
                        }
                    }

                    if (EditingRecord)
                    {
                        if (((this.Detail.QtyOnInvoice != 0m) && (this.CurrentUOPPrime != Detail.UnitPurchase
                            || (eb_Conversion.Text.ToDecimal() != Detail.Conversion))))
                        {
                            MessageBox.Show("There are quantities on an invoice\nYou can't change the Unit of Purchase or Conversion", "Warning", MessageBoxButtons.OK);
                            result = false; return result;
                        }
                    }
                    if (!this.Not_Exceed)
                    {
                        if (EditingRecord)
                        {
                            if ((Detail.QtyMatched != 0m) && eb_Unit_Cost2.Text.ToDecimal() != Detail.UnitCost)
                            {
                                MessageBox.Show("There are quantities matched on an invoice.\nYou can't change the Unit Cost", "Warning", MessageBoxButtons.OK);
                                result = false; return result;
                            }

                            if ((Detail.QtyMatched != 0m) && eb_Unit_Cost2.Text.ToDecimal() < Detail.UnitCost)
                            {
                                MessageBox.Show("There is a quantity of " + Detail.QtyMatched.ToString() + " matched on an invoice. \nYou can't change the quantity ordered to less than the quantity matched", "warning", MessageBoxButtons.OK);
                                result = false; return result;
                            }
                        }
                    }
                    else
                    {
                        decimal PoTotal;
                        decimal VoucherTotal;
                        if (bs2.Count > 0)
                        {
                            this.GetTotalCosts(CurrPo, this.Detail.ItemCount, out PoTotal, out VoucherTotal);
                            if (this.EditingRecord && PoTotal < VoucherTotal)
                            {
                                MessageBox.Show("This is a Not To Exceed Purchase Order.  Changing the Unit \nCost or Qty Ordered will cause the purchase order total to fall below what has\nalready been vouchered.  This is not allowed", "Information", MessageBoxButtons.OK);
                                result = false; return result;
                            }
                        }
                    }

                    if (EditingRecord)
                    {
                        if (Detail.QtyReceived - Detail.TotalReplaced > eb_Quantity2.Text.ToDecimal())
                        {
                            MessageBox.Show("Quantity Ordered can't be less than Quantity Received\nThere have been "
                                + this.Detail.QtyReceived + " items received", "error", MessageBoxButtons.OK);
                            result = false;
                            return result;
                        }
                    }
                    if (this.USE_SUBLEDGER_AMOUNT)
                    {
                        decimal AddAmount = 0m;
                        if (viewMode1.Mode == ViewingMode.Adding)
                        {
                            AddAmount = this.eb_Unit_Cost2.Text.ToDecimal() * this.eb_Quantity2.Text.ToDecimal() *
                                (1m + this.eb_Vat2.Text.ToDecimal() / 100m);
                        }
                        if (this.EditingRecord)
                        {
                            AddAmount = this.eb_Unit_Cost2.Text.ToDecimal() * this.eb_Quantity2.Text.ToDecimal() *
                                (1m + this.eb_Vat2.Text.ToDecimal() / 100m) - this.Detail.UnitCost * this.Detail.QtyOrder *
                                (1m + this.Detail.VatPercentage / 100m);
                        }

                        this.q_Command.Parameters.Clear();
                        q_Command.CommandText = "SELECT Sum((Unit_Cost * Qty_Order) * (  1 + Vat_Percentage /100)) as Total "
                                 + "FROM PoDetail WHERE Po_No = @Po_No";
                        q_Command.Parameters.AddWithValue("Po_No", CurrPo);
                        using (SqlDataReader q = this.q_Command.ExecuteReader())
                        {
                            q.Read();
                            if (Header.EDIStatus == ' ' && Header.FaxStatus == ' ' &&
                                Header.PrintStatus == ' ' && Header.EmailStatus == ' ')
                            {
                                try { AddAmount += q["Total"].ToDecimal(); }
                                catch { AddAmount += 0m; }
                            }
                        }
                        if (this.cmb_Project.Text.Trim() != "")
                        {
                            decimal CurrentAmount;

                            this.q_Command.Parameters.Clear();
                            this.q_Command.CommandText = "SELECT sum(encumbered_amount + manual_amount) as currentamount from projectbudget WHERE Project_no = @Project ";
                            this.q_Command.Parameters.Add("Project", SqlDbType.VarChar).Value = this.cmb_Project.Text;

                            using (SqlDataReader q = this.q_Command.ExecuteReader())
                            {
                                q.Read();
                                CurrentAmount = q["CurrentAmount"].ToDecimal();
                            }

                            this.q_Command.Parameters.Clear();
                            if (this.Use_Project_Spend_Amount)
                                this.q_Command.CommandText = "SELECT Project_Spend_Amount as budget_amount ";
                            else
                                this.q_Command.CommandText = "SELECT Budget_Amount + total_adjustments as budget_amount ";
                            q_Command.CommandText += "FROM Project with(nolock) WHERE Project_no = @Project AND Active = 1 ";
                            this.q_Command.Parameters.Add("Project", SqlDbType.VarChar).Value = this.cmb_Project.Text;
                            using (SqlDataReader q2 = this.q_Command.ExecuteReader())
                            {
                                q2.Read();
                                if (!q2.HasRows)
                                {
                                    MessageBox.Show("Can't save, this project doesn't exist or is inactive", "information", MessageBoxButtons.OK);
                                    result = false;
                                    return result;
                                }
                                decimal current_Budget = q2["Budget_Amount"].ToDecimal();
                                if (current_Budget != 0m)
                                {
                                    if (CurrentAmount + AddAmount > current_Budget)
                                    {
                                        if (data.SystemOptionsDictionary["USE_SUBLEDGER_AMOUNT_OVERRIDE"].ToBoolean())
                                        {
                                            if (MessageBox.Show("Saving this line will put you over your budget amount for this project\nDo you want to continue?", "confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                                            {
                                                result = false;
                                                return result;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("You can't save this line for Project " + this.cmb_Project.Text + "\nIt would put you over your budget amount for this project", "information", MessageBoxButtons.OK);
                                            result = false;
                                            return result;
                                        }

                                    }
                                }
                            }
                        }
                    }
                    if (this.Not_Exceed_Header)
                    {
                        decimal AddAmount = 0m;
                        if (viewMode1.Mode == ViewingMode.Adding)
                            AddAmount = ((1 + (eb_Vat2.Text.ToDecimal() / 100m)) * (eb_Unit_Cost2.Text.ToDecimal() * eb_Quantity2.Text.ToDecimal()));
                        if (this.EditingRecord)
                        {
                            AddAmount = ((1 + (eb_Vat2.Text.ToDecimal() / 100m)) * (eb_Unit_Cost2.Text.ToDecimal()
                                * eb_Quantity2.Text.ToDecimal())) - ((Detail.UnitCost * Detail.QtyOrder)
                                * (1 + (Detail.VatPercentage / 100m)));
                        }
                        if (eb_Total.Text.ToDecimal() + AddAmount > eb_Not_Total.Text.ToDecimal())
                        {
                            if (MessageBox.Show("Saving this line will put you over your Not to exceed Amount\nDo you want to continue", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                result = false;
                                return result;
                            }
                        }
                    }
                    if (this.poentry_dollar_limit > 0m)
                    {
                        decimal AddAmount = 0m;
                        if (viewMode1.Mode == ViewingMode.Adding)
                        {
                            AddAmount = eb_Unit_Cost2.Text.ToDecimal() * eb_Quantity2.Text.ToDecimal() *
                                (1m + eb_Vat2.Text.ToDecimal() / 100m);
                        }
                        if (this.EditingRecord)
                        {
                            AddAmount = eb_Unit_Cost2.Text.ToDecimal() * eb_Quantity2.Text.ToDecimal() * (1m +
                                eb_Vat2.Text.ToDecimal() / 100m) - Detail.UnitCost *
                                Detail.QtyOrder * (1m + Detail.VatPercentage / 100m);
                        }
                        if (eb_Total.Text.ToDecimal() + AddAmount > this.poentry_dollar_limit)
                        {
                            MessageBox.Show("Saving this line will put you over your dollar limit\nYou can't continue", "Confirmation", MessageBoxButtons.OK);
                            result = false;
                            return result;
                        }
                    }
                    if (!this.NonFile_Item)
                    {
                        if (this.POENTRY_PRICE_CHECK_AMOUNT != 0m)
                        {
                            if (data.SystemOptionsDictionary["USE_AVERAGE_COST"].ToBoolean() && Math.Abs(HoldAvgCost - eb_Unit_Cost2.Text.ToDecimal() /
                                eb_Conversion.Text.ToDecimal()) >= this.POENTRY_PRICE_CHECK_AMOUNT)
                            {
                                if (MessageBox.Show(string.Concat(new string[]
                                {
                                    "PRICE CHECK WARNING!!! \nBy ordering this item with this UOP conversion and price, \nyou will be changing your current LUOI Cost from ",
                                    HoldAvgCost.ToString("0.00"),
                                    "\nto ",
                                    (eb_Unit_Cost2.Text.ToDecimal()/ eb_Conversion.Text.ToDecimal()).ToString("0.00"),
                                    "\n Do you want to continue?"
                                }), "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                                {
                                    result = false;
                                    return result;
                                }
                            }
                            if (!data.SystemOptionsDictionary["USE_AVERAGE_COST"].ToBoolean() && Math.Abs(HoldIssCost - eb_Unit_Cost2.Text.ToDecimal() /
                                eb_Conversion.Text.ToDecimal()) >= this.POENTRY_PRICE_CHECK_AMOUNT)
                            {
                                if (MessageBox.Show(string.Concat(new string[]
                                {
                                    "PRICE CHECK WARNING!!! \nBy ordering this item with this UOP conversion and price, \nyou will be changing your current LUOI Cost from ",
                                    HoldIssCost.ToString("0.00"),
                                    "\nto ",
                                    (eb_Unit_Cost2.Text.ToDecimal()/ eb_Conversion.Text.ToDecimal()).ToString("0.00"),
                                    "\n Do you want to continue?"
                                }), "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                                {
                                    result = false;
                                    return result;
                                }
                            }
                        }
                        if (this.POENTRY_PRICE_CHECK_PERCENT != 0m && eb_Unit_Cost2.Text.ToDecimal() != 0m)
                        {
                            if (this.data.SystemOptionsDictionary["USE_AVERAGE_COST"].ToBoolean() && Math.Abs((HoldAvgCost - eb_Unit_Cost2.Text.ToDecimal() /
                                eb_Conversion.Text.ToDecimal()) / (eb_Unit_Cost2.Text.ToDecimal() /
                                eb_Conversion.Text.ToDecimal())) >= this.POENTRY_PRICE_CHECK_PERCENT)
                            {
                                if (MessageBox.Show(string.Concat(new string[]
                                {
                                    "PRICE CHECK WARNING!!! \nBy ordering this item with this UOP conversion and price, \nyou will be changing your current LUOI Cost from ",
                                    HoldAvgCost.ToString("0.00"),
                                    "\nto ",
                                    (eb_Unit_Cost2.Text.ToDecimal()/ eb_Conversion.Text.ToDecimal()).ToString("0.00"),
                                    "\n Do you want to continue?"
                                }), "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                                {
                                    result = false;
                                    return result;
                                }
                            }
                            if (!this.data.SystemOptionsDictionary["USE_AVERAGE_COST"].ToBoolean() && Math.Abs((HoldIssCost - eb_Unit_Cost2.Text.ToDecimal() /
                                eb_Conversion.Text.ToDecimal()) / (eb_Unit_Cost2.Text.ToDecimal() /
                                eb_Conversion.Text.ToDecimal())) >= this.POENTRY_PRICE_CHECK_PERCENT)
                            {
                                if (MessageBox.Show(string.Concat(new string[]
                                {
                                    "PRICE CHECK WARNING!!! \nBy ordering this item with this UOP conversion and price, \nyou will be changing your current LUOI Cost from ",
                                    HoldIssCost.ToString("0.00"),
                                    "\nto ",
                                    (this.eb_Unit_Cost2.Text.ToDecimal()/ eb_Conversion.Text.ToDecimal()).ToString("0.00"),
                                    "\n Do you want to continue?"
                                }), "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                                {
                                    result = false;
                                    return result;
                                }
                            }
                        }
                    }
                }

                catch (Exception newerror)
                {
                    MessageBox.Show("Detail Validate error: " + newerror.ToString());
                }
                #endregion
                */
            }
            MainVendor = DialogResult.No;
            return true;
        }

        bool ismanualrun = false;

        #region Enter
        #region Header
        private void cmb_Entity_Enter(object sender, EventArgs e)
        {
            if (cmb_Entity.Items.Count == 1)
            {
                CurrEntity = cmb_Entity.Items[0].Key;
                SendKeys.Send("{TAB}");
            }
        }

        private void cmb_Po_Type_Enter(object sender, EventArgs e)
        {
            if (cmb_Po_Type.Items.Count == 1)
            {
                CurrPoType = cmb_Po_Type.Items[0].Key;
                SendKeys.Send("{TAB}");
                return;
            }
            else
            {
                if (ismanualrun)
                {
                    cmb_Po_Type.SelectAll();
                    return;
                }
                if (CurrPoType == null)
                    CurrPoType = data.SystemOptionsDictionary["DEFAULT_PO_TYPE"].ToNonNullString();
            }
            if (CurrPoType != null)
            {
                SendKeys.Send("{TAB}");
                return;
            }
            else
            {
                ismanualrun = true;
                cmb_Po_Type.SelectAll();
            }
        }

        private void cmb_Ship_To_Enter(object sender, EventArgs e)
        {
            if (ismanualrun)
            {
                cmb_Ship_To.SelectAll();
                return;
            }
            CurrShipTo = (cmb_Entity.CurrentItem.Value as ComboBoxEntity).DefaultShipTo;
            if (CurrShipTo == null || CurrShipTo == "<NONE>" || CurrShipTo.Trim().Length == 0)
            {
                ismanualrun = true;
                cmb_Ship_To.SelectAll();
                return;
            }
            ismanualrun = true;
                SendKeys.Send("{TAB}");
        }

        private void cmb_vendor_Enter(object sender, EventArgs e)
        {
            NewVendorFrm();
        }
        private void cmb_vendor_Click(object sender, EventArgs e)
        {
            NewVendorFrm();
        }
        void NewVendorFrm()
        {
            if (cmb_vendor.ReadOnly)
                return;
            var dialogResult = vendorFrm.ShowDialog();
            cmb_vendor.Items = new List<ComboBoxString>(1) { new ComboBoxString(Header.VendorID, Header.VendorName) };
            cmb_vendor.Text = vendorFrm.cmb_vendor.Text;
            if (dialogResult == DialogResult.OK && (vendorFrm.Updated || viewMode1.Mode == ViewingMode.Adding))
            {
                Header.VendorAddress1 = vendorFrm.Vendor.Address1;
                Header.VendorAddress2 = vendorFrm.Vendor.Address2;
                Header.VendorAddress3 = vendorFrm.Vendor.Address3;
                Header.VendorCity = vendorFrm.Vendor.City;
                Header.VendorState = vendorFrm.Vendor.State;
                Header.VendorZip = vendorFrm.Vendor.Zip;
                Header.VendorEmail = vendorFrm.Vendor.VendorEmail;
                Header.TermsCode = vendorFrm.CurrTermsCode;
                Header.VendorAccount = vendorFrm.Vendor.VendorAccount;
                Header.VMShipVendorAccount = vendorFrm.eb_VMShip_Account.Text;
                Header.FOB = vendorFrm.Vendor.FOB;
                cmb_vendor.HasValidated = true;
            }
        }

        #endregion

        #region Detail
        private void cmb_Mat_Enter(object sender, EventArgs e)
        {
            if (viewMode1.Mode == ViewingMode.Adding)
                Mat_Enter();
            if (viewMode1.Mode == ViewingMode.Viewing)
                cmb_Mat.Items = data.prefillCombos("MatDetail", CurrPo.ToString());
            //if (viewMode1.Mode == ViewingMode.Editing)
            //    cmb_Mat.Items = list_Mat;
           // else
        }
        private void cmb_Mat_Click(object sender, EventArgs e)
        {
            Mat_Enter();
        }
        void Mat_Enter()
        {
            DialogResult returnvalue = new DialogResult();

            if (viewMode1.Mode == ViewingMode.Adding)
            {
                if (cmb_Mat.CurrentItem != null && CurMat.Length > 0)
                    return;
                if (gettingitem == false)
                {
                    ItemSelection _Is = new ItemSelection(this);
                    try
                    {
                        returnvalue = _Is.ShowDialog();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }

                try
                {
                    if (returnvalue != null && returnvalue == DialogResult.OK)
                    {
                        if (Detail.NonFile || AddItemFromVendor)
                        {
                            cmb_Mat.Items.Add(new ComboBoxString(Detail.MatCode));
                            CurMat = Detail.MatCode;
                        }
                        SetDetailFocus();
                        gettingitem = false;
                        //return;
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                }
            }
        }
        private void cmb_Loc_Enter(object sender, EventArgs e)
        {
            if (cmb_Loc.Items.Count == 0)
            {
                errorProvider1.SetError(cmb_Loc, "No available locations for this mat");
            }
            if (cmb_Loc.Items.Count == 1)
            {
                CurLoc = cmb_Loc.Items[0].Key;
                SendKeys.Send("{TAB}");
                return;
            }
        }
        private void cmb_Act_Enter(object sender, EventArgs e)
        {
            if (ismanualrun)
            {
                cmb_Act.SelectAll();
                return;
            }
            else
            {
                if (data.SystemOptionsDictionary["ENTER_DEPT_USE_LOC_SUB"].ToBoolean() && (m_addItems.Checked == false) && (_CurLocationDetail?.Type == "N"))
                {
                    b_Dept.Visible = true;
                    if (cmb_Act.Text.Trim() == "")
                    {
                        b_Dept.Focus();
                        if (Detail.Department.Trim() == "")
                            Detail.Department = data.Lookup("Dept", false, CurrEntity);
                        Detail.SubAccount = _CurLocationDetail.SubAccount;
                        CurAct = Detail.Department + data.SystemOptionsDictionary["ACCOUNT_NO_DELIMITER"].ToNonNullString() + Detail.SubAccount;
                        if (null == CurAct)
                        {
                            cmb_Act.Text = Detail.Department + data.SystemOptionsDictionary["ACCOUNT_NO_DELIMITER"].ToNonNullString();
                            cmb_Act.Focus();
                            cmb_Act.SelectionStart = cmb_Act.TextLength;
                            return;
                        }
                        else
                        {
                            SetDetailFocus();//                            SendKeys.Send("{TAB}");
                        }
                    }
                    return;
                }
                if (Detail.NonFile == false)
                    CurAct = _CurLocationDetail.AccountNo;
                if (_CurLocationDetail?.Type != "S")
                {
                    if (data.SystemOptionsDictionary["USE_LAST_ACCT_FOR_EVERYTHING_BUT_STOCK"].ToBoolean() && bs2.Count > 1)
                    {
                        CurAct = (splitacct != ((PoDetail)bs2[bs2.Position - 1]).AccountNo) ? ((PoDetail)bs2[bs2.Position - 1]).AccountNo : "";
                    }
                }
                if (CurAct != null)
                {
                    //Detail.Department = (cmb_Act.CurrentItem.Value as AccountNo).Department;
                    //Detail.SubAccount = (cmb_Act.CurrentItem.Value as AccountNo).SubAccount;
                    SetDetailFocus();
                }
            }
        }
        private void cmb_Deliver_Enter(object sender, EventArgs e)
        {
            //if (CurAct == null)
            //{
            //    CurDeliver = data.GetDeliverTo(Detail.Department);
            //    return;
            //}
            if (ismanualrun)
            {
                cmb_Deliver.SelectAll();
                return;
            }
            CurDeliver = ((cmb_Act.CurrentItem.Value as AccountNo).DefaultDeliverTo == "") ? data.GetDeliverTo(Detail.Department) : (cmb_Act.CurrentItem.Value as AccountNo).DefaultDeliverTo;
            if (CurDeliver != null)
                SendKeys.Send("{TAB}");
        }
        private void cmb_ProfileId_Enter(object sender, EventArgs e)
        {
            switch (data.SystemOptionsDictionary["PROFILE_TYPE"].ToChar())
            {
                case 'A':
                    {
                        cmb_ProfileId.Items = data.GetProfileId('A', CurrEntity, CurAct);
                        break;
                    }
                case 'G':
                    {
                        cmb_ProfileId.Items = data.GetProfileId('G', CurrEntity, Detail.Department);
                        break;
                    }
                default:
                    {
                        cmb_ProfileId.Items = data.GetProfileId('z', "", "");
                        break;
                    }
            }
        }

        #endregion detail

        #endregion

        #region Validating

        #region Header
        private void cmb_Entity_Validating(object sender, CancelEventArgs e)
        {
            if (CurrEntity==null)
            {
                errorProvider1.SetError(cmb_Entity, "You Must choose an Entity");
                e.Cancel = true;
            }
        }
        private void cmb_POGroup_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (data.SystemOptionsDictionary["MUST_USE_DEFAULT_PO_GROUP"].ToBoolean() == false && this.USE_PO_GROUPS)
            {
                if (Header.PoGroupId == 0)
                {
                    errorProvider1.SetError(cmb_POGroup, "You must choose a PO Group");
                    e.Cancel = true;
                }
            }
        }
        private void cmb_Po_Type_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CurrPoType == "" || CurrPoType == "<NONE>")
            {
                errorProvider1.SetError(cmb_Po_Type, " You must choose a PO Type.");
                e.Cancel = true;
                return;
            }
            if ((cmb_Po_Type.CurrentItem.Value as ComboBoxPoType).ReturnRepair)
            {
                if (data.GetNonFile(CurrPo))
                {
                    errorProvider1.SetError(cmb_Po_Type, "Can't change this Po Type.  All items on a Return/Repair Po Type must be non-file");
                    e.Cancel = true;
                    return;
                }
            }
            if (viewMode1.Mode == ViewingMode.Editing && Header.POType != CurrPoType)
            {
                if ((cmb_Po_Type.CurrentItem.Value as ComboBoxPoType).Prepay)
                {
                    if (data.GetInvoiced(CurrPo) > 0)
                    {
                        errorProvider1.SetError(cmb_Po_Type, "Can't change this Po Type to Prepay.  This PO is already on an invoice.");
                        e.Cancel = true;
                        return;
                    }
                }
            }

            errorProvider1.Clear();
        }
        private void cmb_Ship_To_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CurrShipTo == "<NONE>" || CurrShipTo.Trim().Length == 0)
            {
                errorProvider1.SetError(cmb_Ship_To, "Must Select a Ship To");
                e.Cancel = true;
                return;
            }
            errorProvider1.Clear();
        }
        private void eb_Req_No_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (viewMode1.Mode == ViewingMode.Adding)
            {
                if (eb_Req_No.Text.Length > 0)
                {
                    var temp = data.GetReqNo(eb_Req_No.Text);
                    if (temp.Length > 0)
                    {
                        if (MessageBox.Show("This Req No has been used on PO " + temp + "\nDo you want to continue?", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            errorProvider1.SetError(eb_Req_No, "Error");
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }
            errorProvider1.Clear();
        }
        private void cmb_Project_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((ComboBoxPoType)cmb_Po_Type.CurrentItem.Value).SubLedger && cmb_Project.CurrentItem == null)
            {
                errorProvider1.SetError(cmb_Project, "You must enter a Sub Ledger Number");
                e.Cancel = true;
            }
            errorProvider1.Clear();
        }
        private void cmb_vendor_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CurVendor == "")
            {
                errorProvider1.SetError(cmb_vendor, "You must enter a Vendor ID");
                e.Cancel = true;
            }
            errorProvider1.Clear();
        }
        private void Pnl_Vendor_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        private void eb_Nonfile_Contract_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((ComboBoxPoType)cmb_Po_Type.CurrentItem.Value).ServiceContract)
            {
                if (eb_Nonfile_Contract.Text.Trim() == "")
                {
                    errorProvider1.SetError(eb_Nonfile_Contract, "You must enter a Service Contract when using this PO Type");
                    e.Cancel = true;
                }
            }
            errorProvider1.Clear();
        }
        #endregion Header

        #region Detail
        private void cmb_Mat_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (viewMode1.Mode == ViewingMode.Viewing)
            {
                //bs2.Position = cmb_Mat.CurrentItem.
                return;
            }
            if (cmb_Mat.CurrentItem == null)
            {
                errorProvider1.SetError(cmb_Mat, "You must enter a Material Code");
                e.Cancel = true;
                return;
            }
            Detail.MatCode = cmb_Mat.CurrentItem.Key;
            if (Detail.NonFile)
                return;
            if (_IMF == null || _IMF.Mfg_Name == "")
            {
                if ((ENABLE_ADD_ITEMS) && (MessageBox.Show("This item you are attempting to add is not on file for this vendor."
                    + "\nWould you like to add it now?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes))
                {
                    LastLocUsed = Detail.Location;
                    AddItemVend();
                    GetMatDetails("");
                    FillDetailsWithMatCode("");
                }
                else
                {
                    if (Enable_Add_ItemVend == false)
                        MessageBox.Show("This Item is not on file for this Vendor.", "Warning", MessageBoxButtons.OK);
                    Changing = true;
                    ClearDetails();
                    if (cmb_Mat.Enabled)
                        cmb_Mat.Focus();
                    Changing = false;
                    return;
                }
            }
            if (data.GetActive(Detail.MatCode, Detail.VendorID) == false)
            {
                errorProvider1.SetError(cmb_Mat, "This item is set inactive for this Vendor");
                e.Cancel = true;
                return;
            }
            errorProvider1.Clear();
        }
        private void cmb_Loc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (viewMode1.Mode == ViewingMode.Viewing)
                return;
            if (Detail.NonFile)
                return;
            if (viewMode1.Mode == ViewingMode.Editing)
            {
                if (CurLoc == Detail.Location)
                    return;
            }
            if (cmb_Loc.Text.Trim() == "")
            {
                errorProvider1.SetError(cmb_Loc, "You must set a Location");
                e.Cancel = true;
                return;
            }
            else
            {
                try
                {
                    _CurLocationDetail = data.getLoc(CurMat, CurLoc, Header.Entity);

                    if (_CurLocationDetail == null)
                    {
                        errorProvider1.SetError(cmb_Loc, "This Location Does Not Exist for this item,\n or you do not have rights to this Location");
                        e.Cancel = true;
                        return;
                    }
                }
                catch
                {
                    errorProvider1.SetError(cmb_Loc, "This Location Does Not Exist for this item,\n or you do not have rights to this Location");
                    e.Cancel = true;
                    return;
                }
                if (viewMode1.Mode == Ehs.Controls.ViewingMode.Adding && _CurLocationDetail.Stockless && ListUop != null)
                {
                    if (ListUop.Exists(r => r.StocklessUOP = true))
                    {
                        var temp = ListUop.Find(r => r.StocklessUOP = true);
                        Detail.VendorCatalog = temp.VendorCatalog;
                        Detail.MFGCatalog = temp.MFGCatalog;
                    }
                }
                /*
                    if (this.AddingRecord && (q["Active"].ToBoolean() == false))
                    {
                        errorProvider1.SetError(cmb_Loc, "This items Location isn't Active");
                        e.Cancel = true;
                        return;
                    }*/
            }

            errorProvider1.Clear();
        }
        private void cmb_Act_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (viewMode1.Mode == ViewingMode.Viewing || viewMode1.Mode == ViewingMode.Inquiry)
                return;
            if (Changing)
                return;

            if (cmb_Act.Text == splitacct)
            {
                if (viewMode1.Mode == ViewingMode.Adding)
                {
                    errorProvider1.SetError(cmb_Act, "You cannot use the split account when adding a detail line.  \nYou must first choose a valid account then split the line.");
                    e.Cancel = true;
                    return;
                }
                if (Detail.SplitDetail == false)
                {
                    errorProvider1.SetError(cmb_Act, "You cannot use the split account when adding a detail line.  \nYou must first choose a valid account then split the line.");
                    e.Cancel = true;
                    return;
                }
            }

            if (Header.BankAccount.Trim().Length > 0)
            {
                if (Header.BankAccount != (cmb_Act.CurrentItem.Value as AccountNo).BankAccount)
                {
                    errorProvider1.SetError(cmb_Act, "This account's bank account is not the same as the bank account in the header.");
                    e.Cancel = true;
                    return;
                }
            }
            if (CurAct == null)
            {
                errorProvider1.SetError(cmb_Act, "Act Number is needed");
                e.Cancel = true;
                return;
            }
            errorProvider1.Clear();
        }
        private void cmb_Deliver_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (viewMode1.Mode == ViewingMode.Viewing || viewMode1.Mode == ViewingMode.Inquiry)
                return;
            if (Changing)
                return;

            if (data.GetRsl(CurrEntity, CurDeliver))
            {
                if (viewMode1.Mode == ViewingMode.Editing && (Detail.DeliverTo != CurDeliver))
                {
                    if (MessageBox.Show("The Deliver To has changed, and it was an RSL. Do you want to save record anyway?  The previous Deliver To was " + Detail.DeliverTo + "\nNote: If you allow "
                        + "the change, the On Order value for the Rsl\nwill change, and if you are using scanners\nit may be ordered again", "warning", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        errorProvider1.SetError(cmb_Deliver, "Error");
                        e.Cancel = true;
                        return;
                    }
                }
                if (!this.ValidateRSL())
                {
                    errorProvider1.SetError(cmb_Deliver, "The Deliver To doesn't match RSL");
                    e.Cancel = true;
                    return;
                }
            }
            errorProvider1.Clear();
        }
        private void cmb_Purchase_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CurrentUOPPrime == null)
            {
                errorProvider1.SetError(cmb_Purchase, "Must Set a UOP");
                e.Cancel = true;
                return;
            }
            errorProvider1.Clear();
        }
        private void eb_Unit_Cost2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string Batch_No, contract;
            bool Default_UOP;

            Updated_Master = false;
            Price_Changed = false;
            if (Changing)
                return;
            if ((cmb_Po_Type.CurrentItem.Value as ComboBoxPoType)?.ReturnRepair??false)
                return;
            if (viewMode1.Mode == ViewingMode.Editing)
            {
                if (Detail.UnitCost == eb_Unit_Cost2.Text.ToDecimal())
                    return;
            }
            if (cb_Substitute_Item.Checked)
                return;

            //Batch_No = sys.Read2("EOD_BATCH_NUMBER");

            if (_IMF?.UseContract.Trim().Length > 0)
            {
                if (CD.Purchase_Cost < eb_Unit_Cost2.Text.ToDecimal())
                {
                    errorProvider1.SetError(eb_Unit_Cost2, "The Unit cost can't be greater than the contract cost");
                    e.Cancel = true;
                    return;
                }
            }

            Default_UOP = false;
            var temp = List_Uop?.Find(r => r.Vendor_Catalog == Detail.VendorCatalog && r.Unit_Purchase == CurrentUOPPrime);
            if (temp != null &&  temp.PO_Cost != eb_Unit_Cost2.Text.ToDecimal())
            {
                Price_Changed = true;
                if (Allow_Update_Master)
                {
                    if (CurPoClass != null)
                    {
                        if ((cmb_PoClass.Items.Find(r => r.Key == CurPoClass).Value as ComboBoxPoClass).Capitation)
                            return;
                    }
                    /*Ripping out...If I place Back in, needs redone, with a modal everything updated and needs table redone for pricechange...for one it should have a field of why you changed blah vs master
                     * regardless if its a permanent change or not
                     * 
                    if (MessageBox.Show("Unit Cost was changed. \n" + "The current value is " + temp.PO_Cost.ToString("#####0.0000") +
                        "\nWould you like to automatically update the Item/Vendor" + "\ntable to " + eb_Unit_Cost2.Text.ToDecimal().ToString("#####0.0000")
                        , "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Updated_Master = true;

                        if (Default_UOP)
                        {
                            q_Command.Parameters.Clear();
                            q_Command.CommandText = "UPDATE ItemVend SET PO_Cost = @PO_Cost WHERE Mat_Code = @Mat AND Vendor_Id = @Vendor";
                            q_Command.Parameters.AddWithValue("Po_Cost", eb_Unit_Cost2.Text.ToDecimal());
                            q_Command.Parameters.AddWithValue("Mat", Detail.MatCode);
                            q_Command.Parameters.AddWithValue("Vendor", _WVendor.Vendor.VendorID);
                            q_Command.ExecuteNonQuery();

                            EhsUtil.WriteToPriceChange("O", "PoEntry9", SqlUsername, Detail.MatCode, _WVendor.Vendor.VendorID, "P", CurrPo.ToString(),
                                eb_Vendor_Catalog.Text, CurrentUOPPrime, eb_Conversion.Text.ToDecimal(), holdPO_Cost, eb_Unit_Cost2.Text.ToDecimal(),
                                "", "ItemVend", Fiscal_Period, Fiscal_Year, Updated_Master);
                        }

                        q_Command.Parameters.Clear();
                        q_Command.CommandText = "UPDATE UOP SET PO_Cost = @PO_Cost WHERE Mat_Code = @Mat AND Vendor_Id = @Vendor "
                            + "AND Unit_Purchase = @UOP AND Vendor_Catalog = @Vend_Cat";
                        q_Command.Parameters.AddWithValue("Po_Cost", eb_Unit_Cost2.Text.ToDecimal());
                        q_Command.Parameters.AddWithValue("Mat", Detail.MatCode);
                        q_Command.Parameters.AddWithValue("Vendor", _WVendor.Vendor.VendorID);
                        q_Command.Parameters.AddWithValue("UOP", CurrentUOPPrime);
                        q_Command.Parameters.AddWithValue("Vend_Cat", eb_Vendor_Catalog.Text);
                        q_Command.ExecuteNonQuery();

                        EhsUtil.WriteToPriceChange("O", "PoEntry9", SqlUsername, Detail.MatCode, _WVendor.Vendor.VendorID, "P", CurrPo.ToString(),
                eb_Vendor_Catalog.Text, CurrentUOPPrime, eb_Conversion.Text.ToDecimal(), holdPO_Cost, eb_Unit_Cost2.Text.ToDecimal(),
                "", "UOP", Fiscal_Period, Fiscal_Year, Updated_Master);
                        */
                }
            }
            errorProvider1.Clear();
        }
        private void cmb_DetailVatCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (data.SystemOptionsDictionary["VAT_HEADER_MATCH_DETAIL"].ToBoolean())
            {
                if (CurHeaderVat == "None")
                    return;
                if (CurHeaderVat != CurDetailVat)
                {
                    errorProvider1.SetError(cmb_DetailVatCode, "This vat code doesn't match the header/ changing to " + CurHeaderVat);
                    cmb_DetailVatCode.Focus();
                    CurDetailVat = CurHeaderVat;
                    e.Cancel = true;
                    return;
                }
            }

            errorProvider1.Clear();
        }
        private void eb_Quantity2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (data.SystemOptionsDictionary["POPUP_MSG_ZERO_QTYORD"].ToBoolean())
            {
                if (eb_Quantity2.Text.ToDecimal() == 0m)
                {
                    if (MessageBox.Show("Are you sure you want to order zero items?\nIf you do not want to see this message again, \ncontact your administrator.", "Confirmation", MessageBoxButtons.YesNo)
                        == DialogResult.No)
                    {
                        errorProvider1.SetError(eb_Quantity2, "Ordering zero items");
                        e.Cancel = true;
                        return;
                    }
                }
            }
            errorProvider1.Clear();
        }
        #endregion Detail

        #endregion

        #region Validated

        #region Header
        private void cmb_Entity_Validated(object sender, EventArgs e)
        {/*
            if (firstload)
                return;
                */
            ismanualrun = false;
            Header.Entity = CurrEntity;
            CurrShipTo = ((ComboBoxEntity)cmb_Entity.CurrentItem.Value).DefaultShipTo;//set
            Default_NonStock_Location = ((ComboBoxEntity)cmb_Entity.CurrentItem.Value).DefaultNonStockLocation;
            splitacct = data.GetSplit(CurrEntity);
            FillVendor();
            cmb_vendor.ReadOnly = false;
        }
        private void cmb_Po_Type_Validated(object sender, EventArgs e)
        {
            Header.POType = CurrPoType;

            if (((ComboBoxPoType)cmb_Po_Type.CurrentItem.Value).NotifyAP && Header.NotifyAPMemo == "")
            {
                if (MessageBox.Show("This PO Type is set to Notify AP and you have not entered a Memo.\n Would you"
              + "like to enter one now?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ///please add me
                }
            }
            if (((ComboBoxPoType)cmb_Po_Type.CurrentItem.Value).ServiceContract)
                eb_Nonfile_Contract.Focus();
            ismanualrun = false;
        }
        private void cmb_Ship_To_Validated(object sender, EventArgs e)
        {
            Header.ShipTo = CurrShipTo;
            Header.ShipToName = ((ComboBoxShipTo)cmb_Ship_To.CurrentItem.Value).Name;
            Header.ShipToAddress1 = ((ComboBoxShipTo)cmb_Ship_To.CurrentItem.Value).Address1;
            Header.ShipToAddress2 = ((ComboBoxShipTo)cmb_Ship_To.CurrentItem.Value).Address2;
            Header.ShipToAddress3 = ((ComboBoxShipTo)cmb_Ship_To.CurrentItem.Value).Address3;
            Header.ShipToCity = ((ComboBoxShipTo)cmb_Ship_To.CurrentItem.Value).City;
            Header.ShipToState = ((ComboBoxShipTo)cmb_Ship_To.CurrentItem.Value).State;
            Header.ShipToZip = ((ComboBoxShipTo)cmb_Ship_To.CurrentItem.Value).Zip;
            ismanualrun = true;
        }
        private void cmb_Project_Validated(object sender, EventArgs e)
        {
            //Header.ProjectNo;
        }
        private void dt_Po_Date_Validated(object sender, EventArgs e)
        {
            //Header.PODate = PoDate;
        }
        private void cmb_vendor_Validated(object sender, EventArgs e)
        {
            cmb_vendor.HasValidated = true;
        }

        #endregion header

        #region Detail
        private void cmb_Mat_Validated(object sender, EventArgs e)
        {
            prefillDetailCombos();
            if (Changing)
                return;
            if (viewMode1.Mode == ViewingMode.Viewing || viewMode1.Mode == ViewingMode.Inquiry)
                return;

            GetItemMemo();


            FillDetailsWithMatCode("");
            Detail.MatCode = CurMat;
            SetDetailFocus();
        }
        public void GetMatDetails(string UOP)
        {
            if (Detail.NonFile)
                return;
            _IMF = data.FillImf(CurMat, CurVendor, UOP);
            if (_IMF.UseContract != "" && (cmb_Po_Type.CurrentItem.Value as ComboBoxPoType).ReturnRepair == false)
            {
                CD = data.GetContract(_IMF.UseContract, CurMat);
                if (CD == null)
                {
                    MessageBox.Show("This item is set to be on a contract, but the contract doesn't exist", "error", MessageBoxButtons.OK);
                    return;
                }
            }
            List_Uop = data.GetUop(CurMat, CurVendor);
        }
        private void cmb_Loc_Validated(object sender, EventArgs e)
        {
            if (Changing)
                return;
            b_Dept.Visible = false;
            cmb_Act.Text = "";

            //if (_CurLocationDetail == null)
            //    return; Fix ME
            if (_CurLocationDetail?.MatCode == null)
            {
                cmb_Act.Text = "";
                return;
            }
            if (viewMode1.Mode == ViewingMode.Adding && _CurLocationDetail.Stockless)///fix me
            //          if addingrecord and fieldbyname('stockless').asboolean and not cb_Substitute_Item.checked then
            {
                #region
                data.Open();
                ListUop = Ehs.Models.Util.getUOPList(data._Com, Detail.MatCode, Detail.VendorID);
                data.Close();
                if (ListUop != null)
                {
                    if (ListUop.Exists(r => r.StocklessUOP = true))
                    {
                        var temp = ListUop.Find(r => r.StocklessUOP = true);
                        Detail.MFGName = temp.MFGName;
                        FillPrimeCombo(temp.UnitPurchase);
                        eb_Conversion.Text = temp.Conversion.ToString();
                        eb_Unit_Cost2.Text = temp.POCost.ToString();
                        if (temp.DefaultUOP == false)
                        {
                            Detail.Contract = "";
                            eb_Unit_Cost2.ReadOnly = false;
                            cmb_Purchase.ReadOnly = false;
                            eb_Conversion.ReadOnly = false;
                        }
                    }
                }
                #endregion
            }
            if (bs2.Count > 1)
            {
                if (data.SystemOptionsDictionary["USE_LAST_PROFILE_ID"].ToBoolean())
                {
                    cmb_ProfileId.Focus();
                    if (CurProfile == null || CurProfile.Trim() == "")
                    {
                        CurProfile = ((PoDetail)bs2[bs2.Position - 1]).ProfileID;
                    }
                }
            }
            Detail.Location = CurLoc;
            SetDetailFocus();
        }
        private void cmb_Act_Validated(object sender, EventArgs e)
        {
            Detail.AccountNo = CurAct;
            Detail.Department = (cmb_Act.CurrentItem.Value as AccountNo).Department;
            Detail.SubAccount = (cmb_Act.CurrentItem.Value as AccountNo).SubAccount;
            ismanualrun = false;
            SetDetailFocus();
        }
        private void cmb_Deliver_Validated(object sender, EventArgs e)
        {
            Detail.DeliverTo = CurDeliver;
            ismanualrun = false;
            SetDetailFocus();
        }
        private void od_DeliverDate_ValueChanged_1(object sender, EventArgs e)
        {
            if (Changing)
                return;
            if (od_DeliverDate.Checked)
            {
                od_DeliverDate.Format = DateTimePickerFormat.Short;
                if (Detail != null)
                    Detail.DeliverDate = od_DeliverDate.Value;
            }
            else
            {
                od_DeliverDate.CustomFormat = " ";
                od_DeliverDate.Format = DateTimePickerFormat.Custom;
                if (Detail != null)
                    Detail.DeliverDate = null;
            }
            SetDetailFocus();
        }
        private void eb_Vendor_Catalog_Validated(object sender, EventArgs e)
        {
            SetDetailFocus();
        }
        private void eb_MFG_Catalog_Validated(object sender, EventArgs e)
        {
            SetDetailFocus();
        }
        private void cmb_PoClass_Validated(object sender, EventArgs e)
        {
            Detail.PoClass = CurPoClass ?? "";
            SetDetailFocus();
        }
        private void cmb_ProfileId_Validated(object sender, EventArgs e)
        {
            Detail.ProfileID = CurProfile ?? "";
            SetDetailFocus();
        }
        private void eb_Doctor_Id_Validated(object sender, EventArgs e)
        {
            SetDetailFocus();
        }
        private void cmb_Purchase_Validated(object sender, EventArgs e)
        {
            Detail.UnitPurchase = CurrentUOPPrime;
            SetDetailFocus();
        }
        private void eb_Conversion_Validated(object sender, EventArgs e)
        {
            SetDetailFocus();
        }
        private void eb_Unit_Cost2_Validated(object sender, EventArgs e)
        {
            SetDetailFocus();
        }
        private void cmb_DetailVatCode_Validated(object sender, EventArgs e)
        {
            Detail.VatCode = CurDetailVat;
            SetDetailFocus();
        }


        #endregion detail

        #endregion

        private void GetItemMemo()
        {
            if (Detail == null)
                return;
            if (Detail.MatCode == "")
                return;
            try
            {
                Item_Memo = _ItemMemo[_ItemMemo.FindIndex(item => item.Mat_Code == CurMat)].Memo.ToNonNullString();
            }
            catch
            {
                Item_Memo = "";
            }
        }

        private void FillDetailsWithMatCode(string UOP)
        {
            string holdIMF;
            //this.holdCur = this.eb_Mat_Code.SelectionStart;
            if ((viewMode1.Mode != ViewingMode.Adding) && (viewMode1.Mode != ViewingMode.Editing))
            {
                skipme = true;
                this.Changing = true;
                //this.bs2.SuspendBinding();
                bs2.Position = bs2.Find("MatCode", Detail.MatCode);
                //bs2.ResumeBinding();
                //this.eb_Mat_Code.SelectionStart = this.holdCur;
                skipme = false;
                return;
            }
            else if (Detail.NonFile)
                return;
            ///should be okay here. test it. could be wrong

            FillDetailsImf();
            //GetImf();
            GetLoc();

            //eb_Mat_Code.SelectionStart = holdCur;
            //SendKeys.Send("{TAB}");///Have to verify this isn't when leaving mat, but when leaving location...otherwise need another
            //////fix me
        }

        #region Extra Forms
        private void AddItemVend()
        {
            //variables.MUST_ENTER_MFG_NAME = MUST_ENTER_MFG_NAME;
            //variables.CurrentUOPPrime = CurrentUOPPrime;
            //variables.COPY_VC_TO_MFG = COPY_VC_TO_MFG;
            //variables.AddingRecord = AddingRecord;
            //variables.Mat_Code = Detail.MatCode;
            //variables.MFG_Cat = eb_MFG_Catalog.Text;
            //variables.MFG_Name = eb_Mfg_Name.Text;
            //variables.Conversion = eb_Conversion.Text;
            //variables.Description = eb_Description.Text;
            //variables.Vendor_Cat = eb_Vendor_Catalog.Text;
            //variables.Vendor_Id = _WVendor.Vendor.VendorID;
            //variables.Vendor_Name = _WVendor.Vendor.VendorName;
            //variables.Unit_Cost = eb_Unit_Cost2.Text;

            //Form AddItemVend = new AddItemVend();
            //try { AddItemVend.ShowDialog(); }
            //catch { }

            /*begin
  Application.CreateForm(Tfrm_AddItemVend, frm_AddItemVend);
  With frm_AddItemVend Do
    Begin
      Try

        If ShowModal = mrOk Then
          begin
          end;
      Finally
          release;
      End;
    end;
            */
        }
        #endregion extra

        #region Buttons
        #region Header
        private void btn_Notify_Click(object sender, EventArgs e)
        {

        }
        private void b_buyer_memo_Click(object sender, EventArgs e)
        {

        }

        private void b_rec_memo_Click(object sender, EventArgs e)
        {

        }

        private void b_vendor_memo_Click(object sender, EventArgs e)
        {

        }
        string testing = "";
        private void b_pat_memo_Click(object sender, EventArgs e)
        {
            testing = Header.PatientMemo;
        }
        #endregion header
        #region Detail


        private void b_Dept_Leave(object sender, EventArgs e)
        {
            if (Detail.Department.Trim() == "")
            {
                b_Dept.Focus();
                errorProvider1.SetError(b_Dept, "Department must not be blank");
                return;
            }
            errorProvider1.Clear();
        }
        private void b_Dept_Click(object sender, EventArgs e)
        {
            Detail.Department = data.Lookup("Dept", false, CurrEntity);
        }
        private void b_D_buyer_memo_Click(object sender, EventArgs e)
        {

        }

        private void b_D_Vendor_memo_Click(object sender, EventArgs e)
        {

        }

        private void b_d_ItemMemo_Click(object sender, EventArgs e)
        {

        }

        private void b_req_info_Click(object sender, EventArgs e)
        {

        }

        private void b_Detail_Patient_Memo_Click(object sender, EventArgs e)
        {

        }
        #endregion detail
        #endregion buttons

        public bool ValidateRSL()
        {
            bool result = true;

            data.Open();
            data._Com.Parameters.Clear();
            data._Com.CommandText = "SELECT R.*, Loc.type FROM RSLDetail AS R JOIN Loc on R.mat_code = Loc.mat_code AND R.location = Loc.location WHERE R.mat_Code = @mat_code AND R.RSL = @Deliver_To ";
            data._Com.Parameters.AddWithValue("mat_code", Detail.MatCode);
            data._Com.Parameters.AddWithValue("Deliver_To", CurDeliver);
            using (SqlDataReader q = this.data._Com.ExecuteReader())
            {
                if (q.HasRows)
                {
                    q.Read();
                    if (q["type"].ToString().ToUpper() == "N")
                    {
                        var rslLoc = q["Location"].ToNonNullString();
                        var rslAccount = q["Account_No"].ToNonNullString();
                        var par_level = q["par_level"].ToInt32();
                        var on_hand = q["on_hand"].ToInt32();
                        var refill_expected = q["refill_expected"].ToInt32();

                        if (rslLoc != Detail.Location || rslAccount != CurAct)
                        {
                            MessageBox.Show("This Deliver To is an RSL. \nThe Account No and Loc will now be set to match the RSL.", "Warning", MessageBoxButtons.OK);

                            this.Changing = true;
                            CurLoc = rslLoc;
                            CurAct = rslAccount;
                            this.Changing = false;

                            if (par_level - on_hand < refill_expected + eb_Quantity2.Text.ToDecimal())
                            {
                                if (MessageBox.Show(string.Concat(new string[]
                                {
                                    "What you're ordering plus what was already ordered is greater than the \npar level minus what you have on hand for this item.\n Par Level minus On Hand  =  ",
                                    (par_level / this.eb_Conversion.Text.ToInt32()- on_hand / eb_Conversion.Text.ToDecimal()).ToString(),
                                    "\nOn Order plus This Order  =  ",
                                    (refill_expected / this.eb_Conversion.Text.ToInt32()+ this.eb_Quantity2.Text.ToDecimal()/ eb_Conversion.Text.ToDecimal()).ToString(),
                                    "\nDo you want to continue?"
                                }), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                                {
                                    result = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("This Deliver To is an RSL and its not a Non-Stock Item.\nYou can only enter RSL items that are Non-Stock through PoEntry.", "information", MessageBoxButtons.OK);
                        result = false;
                    }

                }
                else
                {
                    MessageBox.Show("This Deliver To is an RSL.\nThis Material Code is not on this RSL, the Deliver To is checked as \nan RSL but there is no associated RSL, or the Entities do not match.\nCheck the RSL, and Deliver To Master File programs to verify.", "Information", MessageBoxButtons.OK);
                    result = false;
                }
            }
            data.Close();
            return result;
        }

        #region Save Procedures
        void FrequencySave()
        {

            /*
            //firsttime = true;
            FrmFrequency Frm_Freq = new FrmFrequency();
            if (Frm_Freq.ShowDialog() == DialogResult.OK)
            {
                DateTime temp_date = new DateTime(Frm_Freq.Year, Frm_Freq.Month, Frm_Freq.Day);
                Begin_Date = temp_date;
                End_Date = temp_date;
                Frequency1 = variables.Frequency01;
                Frequency_Period = 1;

                q_Command.Parameters.Clear();
                q_Command.CommandText = "SELECT MAX(frequency_batch) as maxb FROM podetail WHERE po_no = @po_no";
                q_Command.Parameters.AddWithValue("po_no", CurrPo);
                using (SqlDataReader Read2 = q_Command.ExecuteReader())
                {
                    Read2.Read();
                    Frequency_Batch = Read2[0].ToInt32() + 1;
                }
                i = 0;
                calcnum = 1;
                #region//taken out of delphi

                //        if (this.Frequency1 == "Monthly") { calcnum = 1; }
                //        else if (this.Frequency1 == "Bi-Monthly") { calcnum = 2; }
                //                                    else if (this.Frequency1 == "Quarterly") { calcnum = 1; }
                //                                    else if (this.Frequency1 == "Semi-Annually") { calcnum = 6; }
                //                                    else if (this.Frequency1 == "Yearly") { calcnum = 1; }
                //                                    else if (this.Frequency1 == "Weekly") { calcnum = 52; }
                //                                    else if (this.Frequency1 == "Bi-Weekly") { calcnum = 104; }
                //                                    else { calcnum = 1; }
                #endregion
                while (i != (calcnum * variables.period))
                {
                    ProcessDatesAndFrequencyRecords();
                    DeliverDate = Begin_Date.AddDays((double)Vendor.LeadDays);
                    SaveNewDetail();

                    FillDetailQuery();
                    Changing = true;
                    bs2.Position = bs2.Find("ItemCount", holdItem_Count);
                    Changing = false;

                    LastLocUsed = Detail.Location;

                    #region//Removed Delphi
                    //                                   if (!this.Detail.SplitDetail&& this.eb_Account_No.Text == this.splitacct)
                    //                                           {
                    //                                               if (firsttime)
                    //                                               {
                    //                                                   splitDetailLine();
                    //                                                   firsttime = false;
                    //                                               }
                    //                                               else
                    //                                               {
                    //                                                   autodone = true;
                    //                                                   splitDetailLine();
                    //                                                   autodone = false;
                    //                                               }
                    //                                           }
                    #endregion

                    if (PoReleasedActiveOrSent())
                    {
                        FillDetailQuery();
                        bs2.Position = bs2.Find("ItemCount", holdItem_Count);
                        UpdateFilesForSingleItem(1);
                        //WriteToPoDetailChange('I');

                        if (AutoReceive)
                        {
                            PoStatus = new EHS.POControl.PoStatus.FormPoStatus(CurrPo.ToDecimal(),
                                       Detail.ItemCount, "ALL");
                            PoStatus.ProcessQuantityRec(0, "ALL", 0, false);
                            //FillDetailQuery();
                        }
                    }
                    Begin_Date = End_Date.AddDays(1);
                    Frequency_Period += 1;
                    i++;
                }
            }*/
        }

        public void UpdateAccount()
        {
            /*
            if (Detail.AccountNo == splitacct)
                return;
            SqlTransaction transaction = this.sqlConnection1.BeginTransaction("Update");
            this.q_Command.Transaction = transaction;

            this.q_Command.Parameters.Clear();
            this.q_Command.CommandText = "SELECT Account_No FROM Loc WHERE Mat_Code = @Mat_Code AND Location = @Location";
            this.q_Command.Parameters.AddWithValue("Mat_Code", Detail.MatCode);
            this.q_Command.Parameters.AddWithValue("Location", Detail.Location);
            string holdaccount;
            using (SqlDataReader q_query = this.q_Command.ExecuteReader())
            {
                if (!q_query.Read())
                { return; }
                holdaccount = q_query["Account_No"].ToString();
            }
            if (holdaccount != Detail.AccountNo && this.CAN_UPDATE_LOC_ACCOUNT)
            {
                if (MessageBox.Show("The Account was changed, would you like to automatically update the Loc master\n table with the new value? ", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        this.q_Command.Parameters.Clear();
                        this.q_Command.CommandText = "UPDATE Loc SET Account_No = @Account_No WHERE Mat_Code = @Mat_Code AND Location = @Location";
                        this.q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = Detail.MatCode;
                        this.q_Command.Parameters.Add("Location", SqlDbType.VarChar).Value = Detail.Location;
                        this.q_Command.Parameters.Add("Account_No", SqlDbType.VarChar).Value = Detail.AccountNo;
                        this.q_Command.ExecuteNonQuery();

                        this.q_Command.Parameters.Clear();
                        this.q_Command.CommandText = "INSERT INTO ChangeLog (Type,Table_Name,User_Name,Key1,Key2,Old_Data,New_Data,Field_Name) VALUES (@Type,@Table_Name,@System_User,@Mat_Code,@Location,@Old,@New,@Field_Name)";
                        this.q_Command.Parameters.Add("Type", SqlDbType.VarChar).Value = "U";
                        this.q_Command.Parameters.Add("Table_Name", SqlDbType.VarChar).Value = "Loc";
                        this.q_Command.Parameters.Add("System_User", SqlDbType.VarChar).Value = this.SqlUsername;
                        this.q_Command.Parameters.Add("Mat_Code", SqlDbType.VarChar).Value = Detail.MatCode;
                        this.q_Command.Parameters.Add("Location", SqlDbType.VarChar).Value = this._WVendor.Vendor.VendorID;
                        this.q_Command.Parameters.Add("Old", SqlDbType.VarChar).Value = holdaccount;
                        this.q_Command.Parameters.Add("New", SqlDbType.VarChar).Value = Detail.AccountNo;
                        this.q_Command.Parameters.Add("Field_Name", SqlDbType.VarChar).Value = "Account_No";
                        this.q_Command.ExecuteNonQuery();
                    }
                    catch (Exception ee)
                    {
                        this.q_Command.Parameters.Clear();
                        transaction.Rollback();
                        MessageBox.Show("Error in Transaction, Please Contact EHS.\nTransaction was Rolled Back. " + ee.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }
            }
            transaction.Commit();
            this.q_Command.Transaction = null;*/
        }

        public void AddItemsToIMF()
        {/*
            FrmAddImf AddImf = new FrmAddImf();

            AddImf.ImfData.VendorId = _WVendor.Vendor.VendorID;
            AddImf.ImfData.VendorName = _WVendor.Vendor.VendorName;
            AddImf.ImfData.Loc = Default_Location;
            AddImf.ImfData.NSLoc = Default_NonStock_Location;
            AddImf.ImfData.Vendor_Catalog = Detail.VendorCatalog;

            /*
            variables.SQLusername = SqlUsername;
            variables.MFG_Cat = eb_MFG_Catalog.Text;
            variables.Account_No = cmb_Act.Text;
            variables.Mat_Code_Prefix = Mat_Code_Prefix;
            variables.NMat_Code_Prefix = NMat_Code_Prefix;
            variables.stock = AddItemsStockChecked;
            variables.MUST_ENTER_MFG_NAME = MUST_ENTER_MFG_NAME;
            variables.CurrentUOPPrime = CurrentUOPPrime;
            variables.COPY_VC_TO_MFG = COPY_VC_TO_MFG;
            variables.AddingRecord = AddingRecord;
            variables.Can_Insert_MFG = Can_Insert_MFG;
            variables.UseUsertoDept = USE_USERTODEPT;
        ////

            try { AddImf.ShowDialog(); }
            catch { }
            m_addItems.Checked = variables.add_items;
            if (variables.result)
            {
                cmb_Mat.Text = variables.Mat_Code;
                FillDetailsWithMatCode("");
                if (eb_Quantity2.Enabled) { eb_Quantity2.Focus(); }
            }*/
        }
        #endregion save

        


        private void bs2_PositionChanged(object sender, EventArgs e)
        {
            if (dbgrid1.Rows.Count > 0 && bs2.Position >= 0)
            {
                FillDetails();
                // GetItemMemo();

                dbgrid1.Rows[bs2.Position].Selected = true;
            }
        }

        #region Tab
        void p_Header_Enter(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = p_Header;
                this.Height = 380;
                eb_Po_No.SelectAll();
            }
            catch { }
        }

        void p_Detail_Enter(object sender, EventArgs e)
        {
            try
            {
                if (InDetail == false)
                {
                    InDetail = true;
                    if (list_Mat == null || list_Mat.Count < 1)
                        list_Mat = data.prefillCombos("Mat", Header.VendorID);
                    cmb_Act.Items = data.GetAct(CurrEntity);
                    cmb_Deliver.Items = data.prefillCombos("Deliver", CurrEntity);
                    cmb_PoClass.Items = data.GetPoClass();
                    if (viewMode1.Mode == ViewingMode.Adding)
                        New1();
                    else
                    {
                        FillDetailQuery();
                        FillDetails();
                        lbl49.Text = "Contract: " + ((Detail == null) ? "" : Detail.Contract);
                        //GetMemoData();
                        GetItemMemo();
                    }
                    this.Height = 550;
                    this.ActiveControl = p_Detail;
                }
                cmb_Mat.SelectAll();
            }
            catch { }
        }

        void p_Detail_Validating(object sender, CancelEventArgs e)
        {
            if (closing)
                return;
            if (CanSwitch == false)
                e.Cancel = true;
            if (bs1.Count == 0)
                e.Cancel = true;
        }

        void p_Detail_Validated(object sender, EventArgs e)
        {
            FillHeader();
        }

        void p_Header_Validating(object sender, CancelEventArgs e)
        {
            if (closing)
                return;
            if (InDetail)
                return;
            if (viewMode1.Mode == ViewingMode.Adding || viewMode1.Mode == ViewingMode.Editing)
                save1(false);
            if (CanSwitch == false)
                e.Cancel = true;
        }

        void p_Header_Validated(object sender, EventArgs e)
        {
            if (InDetail)
                return;
            Changing = true;
            eb_PO_Number.Text = CurrPo.ToString();
            Changing = false;
        }
        #endregion

        bool CheckHeader()
        {
            if (cmb_Entity.HasValidated == false)
            {
                if (CurrEntity == null)
                {
                    cmb_Entity.Focus();
                    SendKeys.Send("{TAB}");
                }
            }
            if (cmb_Po_Type.HasValidated == false)
            {
                    cmb_Po_Type.Focus();
                    SendKeys.Send("{TAB}");
                if (CurrPoType == null)
                    return false;
                else
                    cmb_Po_Type_Validated(new object(), new EventArgs());
            }
            if (cmb_Ship_To.HasValidated == false)
            {
                    cmb_Ship_To.Focus();
                    SendKeys.Send("{TAB}");
                if (CurrShipTo == null || CurrShipTo == "<NONE>")
                    return false;
                else
                    cmb_Ship_To_Validated(new object(), new EventArgs());
            }
            return true;
        }

        void SetDetailFocus()
        {
            if (cmb_Mat.HasValidated == false)
            {
                //cmb_Mat.Focus();
                //SendKeys.Send("{TAB}");
                if (CurMat != null)
                    SendKeys.Send("{TAB}");
                return;
            }
            if (cmb_Loc.HasValidated == false && Detail.NonFile == false)
            {
                cmb_Loc.Focus();
                if (CurLoc != null)
                    SendKeys.Send("{TAB}");
                return;
            }
            if (cmb_Act.HasValidated == false)
            {
                cmb_Act.Focus();
                if (CurAct != null)
                    SendKeys.Send("{TAB}");
                return;
            }
            if (cmb_Deliver.HasValidated == false)
            {
                cmb_Deliver.Focus();
                if (CurDeliver != null)
                    SendKeys.Send("{TAB}");
                return;
            }
            //Deliver Date
            if (cmb_PoClass.HasValidated == false)
            {
                cmb_PoClass.Focus();
                SendKeys.Send("{TAB}");
                return;
            }
            if (cmb_ProfileId.HasValidated == false)
            {
                cmb_ProfileId.Focus();
                SendKeys.Send("{TAB}");
                return;
            }
            if (eb_Doctor_Id.HasValidated == false)
            {
            }
            if (cmb_Purchase.HasValidated == false)
            {
                cmb_Purchase.Focus();
                if (CurrentUOPPrime != null)
                    SendKeys.Send("{TAB}");
                return;
            }
            if (eb_Conversion.HasValidated == false || Detail.NonFile)
            {
                eb_Conversion.Focus();
                if (eb_Conversion.Text.ToDecimal() > 0m && Detail.NonFile == false)
                    SendKeys.Send("{TAB}");
                return;
            }
            if (eb_Unit_Cost2.HasValidated == false)
            {
                eb_Unit_Cost2.Focus();
                if (eb_Unit_Cost2.Text.ToDecimal() > 0m)
                    SendKeys.Send("{TAB}");
                return;
            }
            /*
            if (cmb_DetailVatCode.HasValidated == false)
            {
                cmb_DetailVatCode.Focus();
                if (CurDetailVat != "" || Detail.NonFile)
                    SendKeys.Send("{TAB}");
                return;
            }*/
            answer = m_filter.IsKeyPressed(Keys.Tab);
            if (answer)
                SendKeys.Flush();
            eb_Quantity2.Focus();
        }
        bool answer = false;

        private void UpdateFiles(int posneg)
        {
            /*
            DataTable DetailHold = new DataTable();
            DataTable Hold = new DataTable();
            int i = 0, d = 0;
            decimal QuantityTimesCost = 0m, Quantity = 0m;

            if (Is_Frequency == false)
            {
                EhsUtil.Change_VendorPurchase(ref q_Command, CurrEntity, _WVendor.Vendor.VendorID, Fiscal_Year, Fiscal_Period,
                    eb_Total.Text.ToDecimal() * (decimal)posneg, 'D', 'P');
            }

            if (this.USE_SUBLEDGER_AMOUNT && Header.ProjectNo != "")
            {
                EhsUtil.Change_ProjectBudget(Header.ProjectNo, (decimal)this.Fiscal_Year,
                    (decimal)this.Fiscal_Period, eb_Total.Text.ToDecimal() * (decimal)posneg, 'E');
            }

            this.Changing = true;
            q_Command.CommandText = "SELECT Mat_Code,Location,Account_No, Profile_Id, Contract, Deliver_To, "
            + "Nonfile,Qty_Order,Conversion,Vat_Percentage,Unit_Cost,Entity, Item_Count From PoDetail   where PO_No = @Po_No";
            q_Command.Parameters.AddWithValue("Po_No", CurrPo);
            using (SqlDataAdapter da = new SqlDataAdapter(q_Command))
            {
                da.Fill(DetailHold);
            }
            i = 0;
            while (i < DetailHold.Rows.Count)
            {
                QuantityTimesCost = DetailHold.Rows[i]["Qty_Order"].ToDecimal() * DetailHold.Rows[i]["Unit_Cost"].ToDecimal() * (decimal)posneg;
                QuantityTimesCost += QuantityTimesCost * DetailHold.Rows[i]["Vat_Percentage"].ToDecimal() / 100;
                Quantity = DetailHold.Rows[i]["Qty_Order"].ToDecimal() * DetailHold.Rows[i]["Conversion"].ToDecimal() * (decimal)posneg;

                q_Command.Parameters.Clear();
                q_Command.CommandText = "SELECT * FROM PoDetailSplit WHERE po_no = @po_no AND item_count = @item_count";
                q_Command.Parameters.AddWithValue("po_no", CurrPo);
                q_Command.Parameters.AddWithValue("item_count", DetailHold.Rows[i]["item_count"].ToInt32());
                using (SqlDataAdapter da = new SqlDataAdapter(q_Command))
                {
                    da.Fill(Hold);
                }

                if (!this.Is_Frequency)
                {
                    if (DetailHold.Rows[i]["NonFile"].ToBoolean() == false)
                    {
                        this.EhsUtil.Change_ItemUsage(ref q_Command, DetailHold.Rows[i]["Location"].ToString(),
                            DetailHold.Rows[i]["Mat_Code"].ToString(), Fiscal_Year, Fiscal_Period, QuantityTimesCost,
                            Quantity, 'D', 'P');

                        if (Hold.Rows.Count == 0)
                        {
                            EhsUtil.Change_ItemBudget(ref q_Command, DetailHold.Rows[i]["Entity"].ToString(),
                                   DetailHold.Rows[i]["Account_No"].ToString(), DetailHold.Rows[i]["Profile_Id"].ToString(),
                                   DetailHold.Rows[i]["Location"].ToString(), DetailHold.Rows[i]["Mat_Code"].ToString(),
                                   Fiscal_Year, Fiscal_Period, QuantityTimesCost, Quantity, 'D', 'P', Cross_Account_No);
                        }
                        else
                        {
                            d = 0;
                            while (d < Hold.Rows.Count)
                            {
                                EhsUtil.Change_ItemBudget(ref q_Command, Hold.Rows[d]["entity"].ToString(),
                                    Hold.Rows[d]["account_No"].ToString(), DetailHold.Rows[i]["Profile_Id"].ToString(),
                                    DetailHold.Rows[i]["Location"].ToString(), DetailHold.Rows[i]["Mat_Code"].ToString(),
                                    Fiscal_Year, Fiscal_Period, (decimal)posneg * (Hold.Rows[d]["Extended_Amount"].ToDecimal() +
                                    Hold.Rows[d]["Vat_Amount"].ToDecimal()), Quantity * (Hold.Rows[d]["percentage"].ToDecimal()
                                    / 100m), 'D', 'P', Cross_Account_No);
                                d++;
                            }
                        }
                    }
                    if (Hold.Rows.Count == 0)
                    {
                        this.EhsUtil.Change_Budget(ref q_Command, CurrEntity, DetailHold.Rows[i]["Account_No"].ToString(),
                            DetailHold.Rows[i]["Profile_Id"].ToString(), Fiscal_Year, Fiscal_Period, QuantityTimesCost, 'D', 'P',
                            DetailHold.Rows[i]["Mat_Code"].ToString(), Cross_Account_No);
                    }
                    else
                    {
                        d = 0;
                        while (d < Hold.Rows.Count)
                        {
                            EhsUtil.Change_Budget(ref q_Command, Hold.Rows[d]["entity"].ToString(),
                                   Hold.Rows[d]["account_No"].ToString(), DetailHold.Rows[i]["Profile_Id"].ToString(), Fiscal_Year,
                                   Fiscal_Period, (decimal)posneg * Hold.Rows[d]["Extended_Amount"].ToDecimal() +
                                   Hold.Rows[d]["Vat_Amount"].ToDecimal(), 'D', 'P', DetailHold.Rows[i]["Mat_Code"].ToString(),
                                   Cross_Account_No);
                            d++;
                        }
                    }


                }
                if (Detail.NonFile == false)
                {
                    this.Change_Inventory(DetailHold.Rows[i]["Qty_Order"].ToDecimal() *
                         DetailHold.Rows[i]["Conversion"].ToDecimal(), posneg);
                    if (this.rsl)
                    {
                        if (Hold.Rows.Count == 0)
                        {
                            this.EhsUtil.Change_RSLUsage(ref q_Command, DetailHold.Rows[i]["Deliver_To"].ToString(),
                                DetailHold.Rows[i]["Mat_Code"].ToString(), DetailHold.Rows[i]["Location"].ToString(),
                                DetailHold.Rows[i]["Entity"].ToString(), DetailHold.Rows[i]["Account_No"].ToString(),
                                Fiscal_Year, Fiscal_Period, QuantityTimesCost, Quantity, 'D', 'P');
                        }
                        else
                        {
                            d = 0;
                            while (d < Hold.Rows.Count)
                            {
                                this.EhsUtil.Change_RSLUsage(ref q_Command, DetailHold.Rows[i]["Deliver_To"].ToString(),
                                    DetailHold.Rows[i]["Mat_Code"].ToString(), DetailHold.Rows[i]["Location"].ToString(),
                                    Hold.Rows[d]["entity"].ToString(), Hold.Rows[d]["account_No"].ToString(), Fiscal_Year,
                                    Fiscal_Period, (decimal)posneg * (Hold.Rows[d]["Extended_Amount"].ToDecimal() +
                                    Hold.Rows[d]["Vat_Amount"].ToDecimal()), Quantity * (Hold.Rows[d]["percentage"].ToDecimal()
                                    / 100m), 'D', 'P');
                                d++;
                            }
                        }
                    }
                }
                if (Detail.Contract != "")
                {
                    if (!this.Return_Repair)
                    {
                        EhsUtil.Change_ContractUsage(ref q_Command, DetailHold.Rows[i]["Contract"].ToString(),
                               DetailHold.Rows[i]["Location"].ToString(), DetailHold.Rows[i]["Mat_Code"].ToString(),
                               Fiscal_Year, Fiscal_Period, QuantityTimesCost, Quantity, 'D', 'P');
                    }
                }
                i++;
            }*/
        }

        void UpdateHeaderTotal()
        {
            eb_Total.Text = data.GetPoTotal(Header.PONo).ToNonNullString();
            eb_Total2.Text = eb_Total.Text;
            data.UpdateHeaderTotal(Header.PONo, eb_Total.Text.ToDecimal());
        }

        void MenuItemsVisibility()
        {
            if (tabControl1.SelectedTab == p_Detail)
            {
                reopenPurchaseOrderToolStripMenuItem.Visible = true;

            }
            else
            {
                reopenPurchaseOrderToolStripMenuItem.Visible = false;
            }
            if (viewMode1.Mode == ViewingMode.Viewing)
            {
                releasePurchaseOrderToolStripMenuItem.Visible = true;
            }
            else
            {
                releasePurchaseOrderToolStripMenuItem.Visible = false;
            }

        }

        private void eb_Quantity2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                eb_PO_Number.Focus();
                save1(false);
            }
        }

        void updateValidatings()
        {
            cmb_Entity.HasValidated = false;
            cmb_Po_Type.HasValidated = false;
            cmb_Ship_To.HasValidated = false;
            cmb_vendor.HasValidated = false;
            for(int i = 0; i< p_Detail.Controls.Count; i++)
            {
                if (p_Detail.Controls[i] is AutoCompleteTextBox)
                    (p_Detail.Controls[i] as AutoCompleteTextBox).HasValidated = false;
            }
        }
        private void eb_Quantity2_Enter(object sender, EventArgs e)
        {
            eb_Quantity2.SelectAll();
        }

        private KeyMessageFilter m_filter = new KeyMessageFilter();


        /*procedure Tform1.Change_Inventory(Amount: real; Pos_Neg: integer);
begin
  if Amount < 0 then exit;
  with q_Query1 do
    begin
      sql.clear;
      sql.add('select rsl from deliverto');
      sql.add('where entity = :entity ');
      sql.add('and deliver_to = :Deliver_To ');
      parambyname('Deliver_To').asstring := q_PoDetail.fieldbyname('deliver_to').asstring;
      parambyname('entity').asstring := q_PoDetail.fieldbyname('entity').asstring;
      active := true;
      if (eof = false) and (fieldbyname('rsl').asboolean = true) then rsl := true
      else rsl := false;

      sql.clear;
      sql.add('select On_Order, Fill_and_Kill from Loc ');
      sql.add('where Mat_Code = :Mat_Code ');
      sql.add('and Location = :Location ');
      ParamByName('Mat_Code').asstring := q_PoDetail.FieldByName('Mat_Code').asstring;
      ParamByName('Location').asstring := q_PoDetail.FieldByName('Location').asstring;
      active := true;
      if (eof = false) and (not rsl) and (FieldByName('Fill_and_Kill').asboolean = true) then exit;//dont update on_order quantities if fill n kill item
      if eof then MessageDlg('Loc record does not exist for Mat Code ' + q_PoDetail.FieldByName('Mat_Code').asstring
        +#13+#10+'and Location ' + q_PoDetail.FieldByName('Location').asstring, mtError, [mbOK], 0)
      else
        with q_Query2 do
          begin
            sql.clear;
            sql.add('update Loc ');
            sql.add('set On_Order = case when (on_order + :on_order) < 0 then 0 else (on_order + :on_order) end ');
            sql.add('where Mat_Code = :Mat_Code ');
            sql.add('and Location = :Location ');
            ParamByName('Mat_Code').asstring := q_PoDetail.FieldByName('Mat_Code').asstring;
            ParamByName('Location').asstring := q_PoDetail.FieldByName('Location').asstring;
            ParamByName('On_Order').asfloat := (Amount * Pos_Neg);
            execute;

            if rsl then
              begin
                sql.clear;
                sql.add('update RslDetail ');
                sql.add('set Refill_Expected = case when (Refill_Expected + :Refill_Expected) < 0 then 0 else (Refill_Expected + :Refill_Expected) end ');
                sql.add('where Mat_Code = :Mat_Code ');
                sql.add('and RSL = :RSL ');
                ParamByName('Mat_Code').asstring := q_PoDetail.FieldByName('Mat_Code').asstring;
                ParamByName('RSL').asstring := q_PoDetail.FieldByName('Deliver_To').asstring;
                ParamByName('Refill_Expected').asfloat := (Amount * Pos_Neg);
                execute;
              end;
          end;
    end;
end;*/

    }
}

class ItemMemo
{
    public string Mat_Code;
    public string Memo;

    public ItemMemo(string Mat_Code, string Memo)
    {
        this.Mat_Code = Mat_Code;
        this.Memo = Memo;
    }
}

public class KeyMessageFilter : IMessageFilter
{
    private const int WM_KEYDOWN = 0x0100;
    private const int WM_KEYUP = 0x0101;
    private bool m_keyPressed = false;

    private Dictionary<Keys, bool> m_keyTable = new Dictionary<Keys, bool>();

    public Dictionary<Keys, bool> KeyTable
    {
        get { return m_keyTable; }
        private set { m_keyTable = value; }
    }

    public bool IsKeyPressed()
    {
        return m_keyPressed;
    }

    public bool IsKeyPressed(Keys k)
    {
        bool pressed = false;

        if (KeyTable.TryGetValue(k, out pressed))
        {
            return pressed;
        }

        return false;
    }

    public bool PreFilterMessage(ref Message m)
    {
        if (m.Msg == WM_KEYDOWN)
        {
            KeyTable[(Keys)m.WParam] = true;

            m_keyPressed = true;
        }

        if (m.Msg == WM_KEYUP)
        {
            KeyTable[(Keys)m.WParam] = false;

            m_keyPressed = false;
        }

        return false;
    }
}
 