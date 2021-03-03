using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountSqlDAO : IAccountDAO
    {
        private readonly string connectionString;

        public AccountSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Account> GetAccounts(int user_id)
        {
            List<Account> list = new List<Account> { };

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * from accounts WHERE user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.HasRows && reader.Read())
                    {
                        Account account = new Account();
                        account.Balance = Convert.ToDecimal(reader["balance"]);
                        account.AccountId = Convert.ToInt32(reader["account_id"]);
                        list.Add(account);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return list;

        }

        public decimal TransferMoney(int account_to, int account_from, decimal amount)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand
                        (@"BEGIN TRANSACTION
                        INSERT into transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount)
                        VALUES (2, 2, @account_from, @account_to, @amount)
                        UPDATE accounts SET balance = balance + @amount WHERE account_id = @account_to
                        UPDATE accounts SET balance = balance - @amount WHERE account_id = @account_from
                        COMMIT TRANSACTION
                        SELECT balance FROM accounts WHERE account_id = @account_from", conn);

                    cmd.Parameters.AddWithValue("@account_to", account_to);
                    cmd.Parameters.AddWithValue("@account_from", account_from);
                    cmd.Parameters.AddWithValue("@amount", amount);

                    decimal newBal = (decimal)cmd.ExecuteScalar();
                    return newBal;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }
    }
}
