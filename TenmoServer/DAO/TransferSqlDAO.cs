using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferSqlDAO : ITransferDAO
    {
        private readonly string connectionString;

        public TransferSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Transfer> GetTransfers(int user_id)
        {
            List<Transfer> list = new List<Transfer> { };

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"Select transfer_id, transfer_type_id, transfer_status_id, account_from, account_to, amount, u.username AS from_username, us.username AS to_username from transfers t
	                                                    JOIN accounts a ON a.account_id = t.account_from
	                                                    JOIN accounts ac ON ac.account_id = t.account_to
	                                                    JOIN users	u ON u.user_id = a.user_id
	                                                    JOIN users	us ON us.user_id = ac.user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.HasRows && reader.Read())
                    {
                        Transfer transfer = new Transfer();
                        transfer.TransferID = Convert.ToInt32(reader["transfer_id"]);
                        transfer.TransferTypeID = Convert.ToInt32(reader["transfer_type_id"]);
                        transfer.TransferStatusID = Convert.ToInt32(reader["transfer_status_id"]);
                        transfer.AccountFrom.AccountId = Convert.ToInt32(reader["account_from"]);
                        transfer.AccountTo.AccountId = Convert.ToInt32(reader["account_to"]);
                        transfer.Amount = Convert.ToDecimal(reader["amount"]);
                        transfer.FromUsername = Convert.ToString(reader["from_username"]);
                        transfer.ToUsername = Convert.ToString(reader["to_username"]);

                        list.Add(transfer);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return list;
        }

        public decimal TransferMoney(Transfer transfer)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand
                        (@"BEGIN TRANSACTION
                        INSERT into transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount)
                        VALUES (@transfer_type_id, @transfer_status_id, @account_from, @account_to, @amount)
                        UPDATE accounts SET balance = balance + @amount WHERE account_id = @account_to
                        UPDATE accounts SET balance = balance - @amount WHERE account_id = @account_from
                        COMMIT TRANSACTION
                        SELECT balance FROM accounts WHERE account_id = @account_from", conn);

                    cmd.Parameters.AddWithValue("@account_to", transfer.AccountTo.AccountId);
                    cmd.Parameters.AddWithValue("@account_from", transfer.AccountFrom.AccountId);
                    cmd.Parameters.AddWithValue("@amount", transfer.Amount);
                    cmd.Parameters.AddWithValue("@transfer_type_id", transfer.TransferTypeID);
                    cmd.Parameters.AddWithValue("@transfer_status_id", transfer.TransferStatusID);
                    
                    decimal newBal = (decimal)cmd.ExecuteScalar();
                    return newBal;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public int RequestMoney(Transfer transfer)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand
                        (@"BEGIN TRANSACTION
                        INSERT into transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount)
                        VALUES (@transfer_type_id, @transfer_status_id, @account_from, @account_to, @amount)
                           SELECT @@IDENTITY
                        COMMIT TRANSACTION", conn);

                    cmd.Parameters.AddWithValue("@account_to", transfer.AccountTo);
                    cmd.Parameters.AddWithValue("@account_from", transfer.AccountFrom);
                    cmd.Parameters.AddWithValue("@amount", transfer.Amount);
                    cmd.Parameters.AddWithValue("@transfer_type_id", transfer.TransferTypeID);
                    cmd.Parameters.AddWithValue("@transfer_status_id", transfer.TransferStatusID);

                    int newId = (int)cmd.ExecuteScalar();
                    return newId;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }
    }
}
