using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using ATMLib;

namespace Simple_ATM_Software
{
    public partial class lblHomeServices : Form
    {
        public static decimal balanceSTAT;
    
        SqlCommand command = null;
        SqlDataReader reader = null;
        SqlConnection connection = new SqlConnection(@"server =192.168.200.76\SQL2016EXPRESS;database=pitech; user id=test; password=test");

        public lblHomeServices()
        {
            InitializeComponent();
            

        }

        private void lblHomeServices_Load(object sender, EventArgs e)
        {

        }

        private void btnBalance_Click(object sender, EventArgs e)
        {


            AccHolderDataStore accHolderDataStore=new AccHolderDataStore();
            decimal balance =accHolderDataStore.GetBalance(ATMUI.cardNoSTAT,ATMUI.pinSTAT) ;
            balanceSTAT = balance;

            HeadAvailableBalance lblAvailableBalance =   new HeadAvailableBalance();

           
             lblAvailableBalance.ShowDialog();
            
           }

        private void btnLastThreeTransaction_Click(object sender, EventArgs e)
        {

            AccHolderDataStore accHolderDataStore = new AccHolderDataStore();
            decimal balance = accHolderDataStore.GetBalance(ATMUI.cardNoSTAT, ATMUI.pinSTAT);
            LastThreeTransaction lastThreeTransaction = new LastThreeTransaction();

           accHolderDataStore.LastThreeTransactionMetho(ATMUI.cardNoSTAT, ATMUI.pinSTAT);
            
            lastThreeTransaction.ShowDialog();

        }

        private void btnCashWithdraw_Click(object sender, EventArgs e)
        {

            CashWithdraw cashWithdraw = new CashWithdraw();
          
            cashWithdraw.ShowDialog();
        }

        private void btnDeposite_Click(object sender, EventArgs e)
        {


            AccHolderDataStore accHolderDataStore = new AccHolderDataStore();
            decimal balance = accHolderDataStore.GetBalance(ATMUI.cardNoSTAT, ATMUI.pinSTAT);
            balanceSTAT = balance;
            Deposite deposite = new Deposite();
            deposite.ShowDialog();
        }
    }
}
