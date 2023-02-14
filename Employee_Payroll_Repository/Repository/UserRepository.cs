
using System.Data;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Data.SqlClient;
using Employee_Payroll_Repository.Interface;
using Employee_Payroll_Model;

namespace Employee_Payroll_Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration config;
        private string connectionString;
        public UserRepository(IConfiguration configuration, IConfiguration config)
        {
            connectionString = configuration.GetConnectionString("UserDBConnection");
            this.config = config;
        }
        public static string EncryptPassword(string password)
        {
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(encode);
        }
        public string GenerateJWTToken(string emailID, int UserID)
        {
            try
            {
                var loginSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.config[("Jwt:key")]));
                var loginTokenDescripter = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, emailID),
                        new Claim("UserID",UserID.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(5),
                    SigningCredentials = new SigningCredentials(loginSecurityKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = new JwtSecurityTokenHandler().CreateToken(loginTokenDescripter);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public UserModel UserRegistration(UserModel userModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPUserRegistrations", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FullName", userModel.Fullname);
                    command.Parameters.AddWithValue("@EmailID", userModel.EmailID);
                    command.Parameters.AddWithValue("@MobileNumber", userModel.MobileNumber);
                    command.Parameters.AddWithValue("@Password", EncryptPassword(userModel.Password));

                    connection.Open();
                    int registerOrNot = command.ExecuteNonQuery();

                    if (registerOrNot >= 1)
                    {
                        return userModel;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public string Login(string EmailID, string Password)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                int UserID = 0;
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPLoginUser", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmailID", EmailID);
                    command.Parameters.AddWithValue("@Password", EncryptPassword(Password));

                    connection.Open();
                    SqlDataReader Reader = command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            UserID = Reader.IsDBNull("UserID") ? 0 : Reader.GetInt32("UserID");
                        }
                        string token = GenerateJWTToken(EmailID, UserID);
                        return token;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public UserTicket CreateTicketForPassword(string emailID, string token)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                UserTicket ticket = new UserTicket();
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPForgotPassword", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmailID", emailID);

                    connection.Open();
                    SqlDataReader Reader = command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            ticket.FullName = Reader.IsDBNull("FullName") ? string.Empty : Reader.GetString("FullName");
                            ticket.EmailId = Reader.IsDBNull("EmailID") ? string.Empty : Reader.GetString("EmailID");
                            ticket.Token = token;
                            ticket.IssueAt = DateTime.Now;

                        }
                        return ticket;
                    }
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string ForgotPassword(string emailID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                UserModel userModel = new UserModel();

                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPForgotPassword", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmailID", emailID);

                    connection.Open();
                    SqlDataReader Reader = command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            userModel.UserID = Reader.IsDBNull("UserID") ? 0 : Reader.GetInt32("UserID");
                            //userModel.Fullname = Reader.IsDBNull("FullName") ? string.Empty : Reader.GetString("FullName");
                        }
                        string token = GenerateJWTToken(emailID, userModel.UserID);
                        return token;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public bool ResetPassword(string Password, int UserID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPResetPassword", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@Password", EncryptPassword(Password));
                    connection.Open();
                    int resetOrNot = command.ExecuteNonQuery();

                    if (resetOrNot >= 1)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        } 
        public bool DeleteEmployee(int UserID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPDeleteUser", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);
                    connection.Open();

                    int deleteOrNot = command.ExecuteNonQuery();
                    if(deleteOrNot >= 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally 
            { 
                connection.Close(); 
            }
        }
    }
}
