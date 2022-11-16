using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AccountDB
    {
        private IConfiguration Configuration { get; }

        public AccountDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<Account> getAccountsByOwner(string owner)
        {
            List<Account> result = null;
            string connectionString = Configuration.GetConnectionString("DefaultConection");

            try
            {
                using(SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Accounts WHERE accountNbOwner = @nb";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@nb", owner);
                    cn.Open();

                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (result == null)
                                result = new List<Account>();

                            Account account = new Account();

                            if (dr["accountNumber"] != null)
                                account.accountNumber = (int)dr["accountNumber"];

                            if (dr["accountNbOwner"] != DBNull.Value)
                                account.accountNbOwner = (string)dr["accountNbOwner"];

                            if (dr["type"] != DBNull.Value)
                                account.type = (string)dr["type"];

                            if (dr["amount"] != null)
                                account.amount = (float)dr["amount"];

                            result.Add(account);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }
    }
}
