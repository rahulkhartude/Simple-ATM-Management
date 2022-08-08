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
    public partial class Deposite : Form
    {
        public Deposite()
        {
            InitializeComponent();
        }

        private void Deposite_Load(object sender, EventArgs e)
        {

        }

        public static decimal balanceStat;
        private void btnDeposite_Click(object sender, EventArgs e)
        {
            try
            {
                
                AccHolderDataStore accHolderDataStore = new AccHolderDataStore();
                decimal balance = accHolderDataStore.GetBalance(ATMUI.cardNoSTAT, ATMUI.pinSTAT);
                balanceStat = balance;
                int depositeAmount = Convert.ToInt32(txtDeposite.Text);

                if (depositeAmount<50000)
                {
                    int rows = accHolderDataStore.DepositeAmount(ATMUI.cardNoSTAT, ATMUI.pinSTAT, balanceStat, depositeAmount);

                    if (rows > 0)
                    {
                        MessageBox.Show("Amount Deposited Successfully...");
                    }
                    else
                    {
                        MessageBox.Show("Transaction Failed...");
                    }
                }
                else 
                {
                    MessageBox.Show("Transaction Failed X\n Deposite amount should be less than 50000 ");
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Amount is High !\nPlease contact with BANK to Deposite");
            }
            
        }
    }
}
