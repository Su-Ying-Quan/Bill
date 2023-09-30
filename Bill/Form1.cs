using System;
using System.Windows.Forms;

namespace Bill
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 1;
        }

        private void btnBillCalculator_Click(object sender, EventArgs e)
        {
            var IsInteagerTotalAmount = int.TryParse(txtTotalAmount.Text, out int totalAmount);
            var IsInteagerTipRate = int.TryParse(txtPie.Text, out int tipRate);
            var IsInteagerNumberOfPeople = int.TryParse(txtTotalPeople.Text, out int numberOfPeople);
            var SelectedIndex = comboBox1.SelectedIndex;

            if (IsInteagerTotalAmount && IsInteagerTipRate && IsInteagerNumberOfPeople)
            {
                Bill bill = new Bill()
                {
                    TotalAmount = totalAmount,
                    TipRate = tipRate,
                    NumberOfPeople = numberOfPeople
                };

                BillCalculator billCalculator = new BillCalculator(bill);
                CalculateAmount calculateAmount = null;
                
                switch (SelectedIndex)
                {
                    case 0:
                        calculateAmount = billCalculator.最後一個人多出一點錢;
                        break;
                    case 1:
                        calculateAmount = billCalculator.前面N個人多出一元;
                        break;
                }

                txtPayableAmounts.Text = billCalculator.GetResult(calculateAmount);
            }
            else
            {
                MessageBox.Show("輸入有誤，請重新輸入");
            }
        }
    }
}
