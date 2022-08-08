using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
 
namespace ATMLib
{
    public class AccHolderDataStore
    {
        //  SqlConnection connection = null;
        SqlCommand command = null;
        SqlDataReader reader = null;
        SqlConnection connection = new SqlConnection(@"server =192.168.200.76\SQL2016EXPRESS;database=pitech; user id=test; password=test");


        public AccHolderDataStore() { }
        public AccHolderDataStore(string connectionString)
        {
            // connection = new SqlConnection(connectionString);
        }
        public bool ValidateAccHolder(AccHolderInfo accHolderInfo)
        {
            try
            {
                string sql = "select * from ATMAUTHDATA where cardnumber=@cardnumber and pin=@pin";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@cardnumber", accHolderInfo.CardNumber);
                command.Parameters.AddWithValue("@pin", accHolderInfo.Pin);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                throw ex;
            }




        }
        

        public decimal GetBalance(int cardNumber,int pin)
        {
            try {

                String sql = "select balance from BankAccInfo b join ATMAUTHDATA A ON B.AccountNumber = A.AccountNumber where cardNumber = @cardNumber and pin =@pin";

                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@cardnumber",cardNumber);
                command.Parameters.AddWithValue("@pin", pin);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                decimal balance = Convert.ToDecimal(command.ExecuteScalar());
                
                return balance; 

            }
            catch(Exception ex) 
            {
                if (connection.State == ConnectionState.Open)
                {

                    connection.Close();
                }

                throw ex;
               return 0;
            }

           
        }

        public void WithdrawAmount(int cardNumber,int pin, decimal balance,int wAmount)
        {
            try
            {

                string sql = "update BankAccInfo set balance=(balance-@AMOUNT) where AccountNumber=100";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@AMOUNT", wAmount);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                int count=command.ExecuteNonQuery();
                if (count > 0)
                {
                    string sql2 = "insert into ALLTRANSACTION(ttype,tamount,tAccountNumber) values(@deposite,@amount,(select AccountNumber from ATMAUTHDATA where CardNumber =@cardNumber and pin = @pin))";
                    command = new SqlCommand(sql2, connection);
                    command.Parameters.AddWithValue("@deposite", "withdraw");
                    command.Parameters.AddWithValue("@amount", wAmount);
                    command.Parameters.AddWithValue("@cardNumber",cardNumber);
                    command.Parameters.AddWithValue("@pin", pin);

                    int c = command.ExecuteNonQuery();
                }


            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                {

                    connection.Close();
                }

                throw ex;
             
            }


        }

        public int DepositeAmount( int cardNumber,int pin,decimal balance,int depositeAmount) 
        {

           try
            {

                string sql = "update BankAccInfo set balance=(@balance+@deposite) where AccountNumber=(select AccountNumber from ATMAUTHDATA where cardNumber =@cardnumber and pin = @pin)";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@cardnumber", cardNumber);
                command.Parameters.AddWithValue("@pin", pin);
                command.Parameters.AddWithValue("@deposite", depositeAmount);
                command.Parameters.AddWithValue("@balance", balance);
                

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                int rows=command.ExecuteNonQuery();
                if (rows > 0)
                {
                    string sql2 = "insert into ALLTRANSACTION(ttype,tamount,tAccountNumber) values(@deposite,@amount,(select AccountNumber from ATMAUTHDATA where CardNumber = 12345678 and pin = 1313))";
                    command = new SqlCommand(sql2, connection);
                    command.Parameters.AddWithValue("@deposite", "deposite");
                    command.Parameters.AddWithValue("@amount", depositeAmount);
                  
                    int count = command.ExecuteNonQuery();
                }

                return rows;

            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                {

                    connection.Close();
                }

                throw ex;
                return 0;
            }
        }

        public DataTable LastThreeTransactionMetho(int cardNumber,int pin) 
        {
                string sql = "select top 3 transactionid,ttype,tamount,ttime from ALLTRANSACTION where tAccountNumber=(select AccountNumber from ATMAUTHDATA where cardNumber =@cardNumber and pin = @pin) order by ttime desc";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@cardNumber", cardNumber);
                command.Parameters.AddWithValue("@pin", pin);

               SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
               DataTable dataTable = new DataTable();
                  sqlDataAdapter.Fill(dataTable);


                return dataTable;
            


        }



    }
}
