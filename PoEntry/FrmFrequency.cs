using System;
using System.Windows.Forms;

namespace PoEntry
{
    public partial class FrmFrequency : Form
    {
        string[] FreqArray = { "Monthly", "Yearly", "Weekly", "Bi-Monthly", "Bi-Weekly", "Quarterly", "Semi-Annually", "Every-4-Weeks", "Every-8-Weeks", "Every-12-Weeks" };
        public int Month, Day, Year, Period;
        public string Frequency;

        public FrmFrequency()
        {
            InitializeComponent();
        }

        private void FrmFrequency_Load(object sender, EventArgs e)
        {
            int i = 0;
            while (i < 10)
            {
                cmb_Frequency.Items.Add(FreqArray[i]);
                i++;
            }
            cmb_Frequency.SelectedIndex = 0;
            se_Period.Value = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int day;

            day = dateTimePicker1.Value.Day;

            //if (day > 28)
            //{
            //    MessageBox.Show("The day can't be greater than 28.", "Error", MessageBoxButtons.OK);
            //    dateTimePicker1.Focus();
            //    return;
            //}
            //store to variables
            Month = dateTimePicker1.Value.Month;
            Day = dateTimePicker1.Value.Day;
            Year = dateTimePicker1.Value.Year;
            Frequency = cmb_Frequency.SelectedItem.ToString();
            Period = Convert.ToInt32(se_Period.Value);
            this.DialogResult = DialogResult.OK;
        }

        /*
  od_Start_Date.Date := Date;

        
procedure Tfrm_Frequency.Button1Click(Sender: TObject);
var
year, month, day: word;
begin
  if trim(od_Start_Date.text) = '' then
    begin
     MessageDlg('Po Date can not be blank.', mtError, [mbOK], 0);
     if od_Start_Date.enabled then od_Start_Date.SetFocus;
     exit;
    end;


  ModalResult := mrOK;
end;

end.*/
    }
}
