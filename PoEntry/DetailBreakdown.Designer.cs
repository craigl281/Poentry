namespace PoEntry
{
    partial class DetailBreakdown
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetailBreakdown));
            this.dataGridView2 = new Ehs.Controls.DragOrderedDataGridView();
            this.ItemCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubTotal2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxTotal2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.q_Detail = new System.Data.SqlClient.SqlCommand();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemCount,
            this.MatCode,
            this.Qty,
            this.UnitCost,
            this.SubTotal2,
            this.Vat,
            this.TaxTotal2,
            this.Total2});
            this.dataGridView2.Location = new System.Drawing.Point(0, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(596, 85);
            this.dataGridView2.TabIndex = 2;
            // 
            // ItemCount
            // 
            this.ItemCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ItemCount.DataPropertyName = "Item_Count";
            this.ItemCount.HeaderText = "Line";
            this.ItemCount.Name = "ItemCount";
            this.ItemCount.Width = 37;
            // 
            // MatCode
            // 
            this.MatCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MatCode.DataPropertyName = "Mat_Code";
            this.MatCode.HeaderText = "Mat Code";
            this.MatCode.Name = "MatCode";
            this.MatCode.Width = 77;
            // 
            // Qty
            // 
            this.Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Qty.DataPropertyName = "Qty_Order";
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.Width = 60;
            // 
            // UnitCost
            // 
            this.UnitCost.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.UnitCost.DataPropertyName = "Unit_Cost";
            this.UnitCost.HeaderText = "UnitCost";
            this.UnitCost.Name = "UnitCost";
            this.UnitCost.Width = 75;
            // 
            // SubTotal2
            // 
            this.SubTotal2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SubTotal2.DataPropertyName = "Sub_Total";
            this.SubTotal2.HeaderText = "Sub Total";
            this.SubTotal2.Name = "SubTotal2";
            this.SubTotal2.Width = 80;
            // 
            // Vat
            // 
            this.Vat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Vat.DataPropertyName = "Vat_Percentage";
            this.Vat.HeaderText = "Vat %";
            this.Vat.Name = "Vat";
            this.Vat.Width = 70;
            // 
            // TaxTotal2
            // 
            this.TaxTotal2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TaxTotal2.DataPropertyName = "Tax_Total";
            this.TaxTotal2.HeaderText = "Tax Total";
            this.TaxTotal2.Name = "TaxTotal2";
            this.TaxTotal2.Width = 80;
            // 
            // Total2
            // 
            this.Total2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Total2.DataPropertyName = "Total";
            this.Total2.HeaderText = "Total";
            this.Total2.Name = "Total2";
            this.Total2.Width = 90;
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // q_Detail
            // 
            this.q_Detail.CommandText = "      ";
            // 
            // DetailBreakdown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 86);
            this.Controls.Add(this.dataGridView2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DetailBreakdown";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DetailBreakdown";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Ehs.Controls.DragOrderedDataGridView dataGridView2;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private System.Data.SqlClient.SqlCommand q_Detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubTotal2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vat;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxTotal2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total2;
    }
}