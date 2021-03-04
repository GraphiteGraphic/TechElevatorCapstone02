﻿using System;
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

        public Dictionary<int, Transfer> GetTransfers(int user_id)
        {
            Dictionary<int, Transfer> list = new Dictionary<int, Transfer> { };

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * from transfers WHERE account_from = @user_id or account_to = @user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.HasRows && reader.Read())
                    {
                        Transfer transfer = new Transfer();
                        transfer.TransferID = Convert.ToInt32(reader["transfer_id"]);
                        transfer.TransferTypeID = Convert.ToInt32(reader["transfer_type_id"]);
                        transfer.TransferStatusID = Convert.ToInt32(reader["transfer_status_id"]);
                        transfer.AccountFrom = Convert.ToInt32(reader["account_from"]);
                        transfer.AccountTo = Convert.ToInt32(reader["account_to"]);
                        transfer.Amount = Convert.ToDecimal(reader["amount"]);

                        list.Add(transfer.TransferID, transfer);
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
                        VALUES (2, 2, @account_from, @account_to, @amount)
                        UPDATE accounts SET balance = balance + @amount WHERE account_id = @account_to
                        UPDATE accounts SET balance = balance - @amount WHERE account_id = @account_from
                        COMMIT TRANSACTION
                        SELECT balance FROM accounts WHERE account_id = @account_from", conn);

                    cmd.Parameters.AddWithValue("@account_to", transfer.AccountTo);
                    cmd.Parameters.AddWithValue("@account_from", transfer.AccountFrom);
                    cmd.Parameters.AddWithValue("@amount", transfer.Amount);

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
