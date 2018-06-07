using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PoEntry
{
    public partial class POInfo : Form
    {
        Form1 form1;
        string EdiMemo;

        public POInfo(Form1 frm)
        {
            InitializeComponent();
            form1 = frm;
            T_PO.Text = frm.Header.PONo.ToString();
            t_fill.Text = frm.Header.FillDate.Value.Date.ToString();
            t_Release.Text = frm.Header.OriginalReleaseDate.Value.Date.ToString();
            t_User.Text = frm.Header.OriginalUsername;
            t_program.Text = frm.Header.CreationCode;
            t_Buyer.Text = frm.Header.BuyerUsername;
            t_last.Text = frm.data.getLastEdited(frm.Header.PONo).Date.ToString();
            EdiMemo = frm.data.getEdiNote(frm.Header.PONo);
        }

        private void button1_Click(object sender, EventArgs e)
        {    
            Ehs.Forms.Memo memo1 = new Ehs.Forms.Memo(form1.data._Com.Connection, EdiMemo, false);
            memo1.ShowDialog();
        }
    }
}
