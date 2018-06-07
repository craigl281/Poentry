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
    public partial class DetailBreakdown : Form
    {
        public SqlDataAdapter da_POE;
        public DataSet ds_POE = new DataSet();

        public DetailBreakdown(DataTable dt)
        {
            InitializeComponent();
            this.TaxTotal2.DefaultCellStyle.Format = "#,###.##";
            this.SubTotal2.DefaultCellStyle.Format = "#,###.##";
            this.Total2.DefaultCellStyle.Format = "#,###.##";
            this.UnitCost.DefaultCellStyle.Format = "#,###.##";
            this.Vat.DefaultCellStyle.Format = "###.##";
            this.Qty.DefaultCellStyle.Format = "#,###.##";
            dataGridView2.DataSource = dt;
        }
    }
}
