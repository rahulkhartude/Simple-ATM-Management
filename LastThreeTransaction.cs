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
    public partial class LastThreeTransaction : Form
    {
        public LastThreeTransaction()
        {
            InitializeComponent();
        }

        private void gridLastThreeTransaction_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LastThreeTransaction_Load(object sender, EventArgs e)
        {
            AccHolderDataStore accHolderDataStore = new AccHolderDataStore();
            gridLastThreeTransaction.DataSource = accHolderDataStore.LastThreeTransactionMetho(ATMUI.cardNoSTAT, ATMUI.pinSTAT);
        }
    }
}
