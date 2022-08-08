using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATMLib;
using System.Configuration;

namespace Simple_ATM_Software
{
    public partial class ATMUI : Form
    {
        public static int cardNoSTAT;
        public static int pinSTAT;



        SqlConnection connection = null;
        SqlCommand command = null;  
        AccHolderInfo accHolderInfo=new AccHolderInfo();
        public ATMUI()
        {
             InitializeComponent();
             connection = new SqlConnection(@"server =192.168.200.76\SQL2016EXPRESS;database=pitech; user id=test; password=test");

         }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            AccHolderInfo accHolderInfo = new AccHolderInfo();
            try
            {
                accHolderInfo.CardNumber = Convert.ToInt32(txtCardNumber.Text);
                accHolderInfo.Pin = Convert.ToInt32(txtPin.Text);



                cardNoSTAT = accHolderInfo.CardNumber;
                pinSTAT = accHolderInfo.Pin;



                AccHolderDataStore accHolderDataStore = new AccHolderDataStore();
                bool vaLidationResult = accHolderDataStore.ValidateAccHolder(accHolderInfo);
                txtTemp.Text = vaLidationResult.ToString();

                if (vaLidationResult)
                {
                    lblHomeServices lblHomeServices = new lblHomeServices();

                  //  this.Hide();
                    lblHomeServices.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please enter correct Credential");
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("card number must be 8 digit and pin 4 digit");
            }

            
            
            
           


        }

        private void ATMUI_Load(object sender, EventArgs e)
        {

        }




        
    }
}
