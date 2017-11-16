using System;
using System.Windows.Forms;

namespace PoEntry
{
    public partial class AddCatalog : Form
    {
        public string CurCatalog
        {
            get
            {
                return cmb_Catalog.CurrentItem.Key;
            }
            set
            {
                cmb_Catalog.CurrentItem = (cmb_Catalog.Items.Exists(r => r.Key == value)) ? cmb_Catalog.Items.Find(r => r.Key == value) : null;
            }
        }

        public AddCatalog(Form1 frm)
        {
            InitializeComponent();
            cmb_Catalog.Items = frm.data.prefillCombos("Catalog", "");
        }

        private void B_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

    }
}
