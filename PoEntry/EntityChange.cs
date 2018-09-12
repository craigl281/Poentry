using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Ehs.Controls;


namespace PoEntry
{
    public partial class EntityChange : Form
    {
        public string result => comboBox1.CurrentItem.Key;

        public EntityChange(List<ComboBoxString> list)
        {
            InitializeComponent();
            comboBox1.Items = list;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;

        }
    }
}
