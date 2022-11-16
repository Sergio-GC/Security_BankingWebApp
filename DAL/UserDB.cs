using DTO;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace DAL
{
    public class UserDB : IUserDB
    {
        private IConfiguration Configuration { get; }

        public UserDB(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public User getUserByAccountAndPassword(string accountNb, string password)
        {
            User result = null;
            string hashedPassword = null;

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            string salt = getSaltByAccountNb(accountNb);

            if (salt != null)
            {
                hashedPassword = new Rfc2898DeriveBytes(Encoding.ASCII.GetBytes(password), Encoding.ASCII.GetBytes(salt), 100000).ToString();
            }
            else
            {
                throw new Exception("We were unable to find any account with that number :(");
            }

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Users WHERE accountNb = @accountNB AND password = @password";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@accountNB", accountNb);
                    cmd.Parameters.AddWithValue("password", hashedPassword);

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            result = new User();

                            if (dr["firstName"] != DBNull.Value)
                                result.firstName = (string)dr["firstName"];

                            if (dr["lastName"] != DBNull.Value)
                                result.lastName = (string)dr["lastName"];
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

        public void createUser(User user)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Users(accountNb, firstName, lastName, salt, password) VALUES(" +
                        "@account, @fName, @lName, @salt, @password)";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@account", user.accountNb);
                    cmd.Parameters.AddWithValue("@fName", user.firstName);
                    cmd.Parameters.AddWithValue("@lName", user.lastName);
                    cmd.Parameters.AddWithValue("@salt", user.salt);
                    cmd.Parameters.AddWithValue("@password", user.password);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string getSaltByAccountNb(string accountNb)
        {
            string salt = null;

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT salt FROM Users where accountNb = @nb";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@nb", accountNb);

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (dr["salt"] != DBNull.Value)
                                salt = (string)dr["salt"];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return salt;
        }
    }
}