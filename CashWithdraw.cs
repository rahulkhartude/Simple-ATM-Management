using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATMLib;

namespace Simple_ATM_Software
{
    public partial class CashWithdraw : Form
    {
        public CashWithdraw()
        {
            InitializeComponent();
        }

        private void CashWithdraw_Load(object sender, EventArgs e)
        {

        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            try
            {
                int wAmount = Convert.ToInt32(txtEnterTheAmount.Text);

                int cardNumber = ATMUI.cardNoSTAT;
                int pin = ATMUI.pinSTAT;

                CashWithdraw cashWithdraw = new CashWithdraw();
                AccHolderDataStore accHolderDataStore = new AccHolderDataStore();
                decimal balance = accHolderDataStore.GetBalance(cardNumber, pin);
                if (wAmount < 20000)
                {
                    if (balance > wAmount)
                    {
                        accHolderDataStore.WithdrawAmount(cardNumber,pin,balance, wAmount);
                        MessageBox.Show("Transaction Successfully Completed");
                        this.Close();   
                    }
                    else
                    {
                        MessageBox.Show("not sufficiet balance");
                        this.Close();
                    }
                }
                else 
                {
                    MessageBox.Show("Please enter Amount less than 20000");
                    this.Close();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("not sufficiet balance");
                this.Close();
            }
            this.Close();



        }
    }
}
