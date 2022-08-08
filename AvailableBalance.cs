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
    public partial class HeadAvailableBalance : Form
    {
      
        public HeadAvailableBalance()
        {
            InitializeComponent();
        }
        
    private void lblAvailableBalance_Load(object sender, EventArgs e)
        {
            txtAvailabelBalance.Text= lblHomeServices.balanceSTAT.ToString();
        }

        private void txtAvailabelBalance_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void lblYourBalance_Click(object sender, EventArgs e)
        {

        }
    }
}
