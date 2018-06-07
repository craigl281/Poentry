using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ehs.Util;
using System.Data.SqlClient;

namespace PoEntry
{
    public partial class CngDeliverDate : Form
    {
        decimal Po;
        Data Data;
        public DateTime DeliverDate => od_Deliver_Date.Value;

        public CngDeliverDate(Data data, decimal po)
        {
            InitializeComponent();
            this.Data = data;
            Po = po;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Data.UpdateDeliveryDate(od_Deliver_Date.Value, Po);
            this.DialogResult = DialogResult.OK;
        }
    }
}