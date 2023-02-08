
using Employee_Payroll_Repository.Interface;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using Employee_Payroll_Model;

namespace Employee_Payroll_Repository.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private string connectionString;
        public EmployeeRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("UserDBConnection");
        }
        public EmployeeModel AddEmployee(EmployeeModel employeeModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPAddEmployee", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeName", employeeModel.EmployeeName);
                    command.Parameters.AddWithValue("@Start_Date", employeeModel.Start_Date);
                    command.Parameters.AddWithValue("@Gender", employeeModel.Gender);
                    command.Parameters.AddWithValue("@Departments", employeeModel.Departments);
                    command.Parameters.AddWithValue("@MobileNumber", employeeModel.MobileNumber);
                    command.Parameters.AddWithValue("@Address", employeeModel.Address);
                    command.Parameters.AddWithValue("@City", employeeModel.City);
                    command.Parameters.AddWithValue("@State", employeeModel.State);
                    command.Parameters.AddWithValue("@Country", employeeModel.Country);
                    command.Parameters.AddWithValue("@Basic_Salary", employeeModel.Basic_Salary);
                    command.Parameters.AddWithValue("@Other_allowances", employeeModel.Other_Allowances);
                    command.Parameters.AddWithValue("@Other_Income", employeeModel.OtherIncome);
                    command.Parameters.AddWithValue("@Professional_Tax", employeeModel.Professional_Tax);

                    connection.Open();
                    int AddOrNot = command.ExecuteNonQuery();

                    if (AddOrNot >= 1)
                    {
                        return employeeModel;
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
        public EmployeeModel UpdateEmployee(int EmployeeID, EmployeeModel employeeModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPUpdateEmployees", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                    command.Parameters.AddWithValue("@EmployeeName", employeeModel.EmployeeName);
                    command.Parameters.AddWithValue("@Start_Date", employeeModel.Start_Date);
                    command.Parameters.AddWithValue("@Gender", employeeModel.Gender);
                    command.Parameters.AddWithValue("@Departments", employeeModel.Departments);
                    command.Parameters.AddWithValue("@MobileNumber", employeeModel.MobileNumber);
                    command.Parameters.AddWithValue("@Address", employeeModel.Address);
                    command.Parameters.AddWithValue("@City", employeeModel.City);
                    command.Parameters.AddWithValue("@State", employeeModel.State);
                    command.Parameters.AddWithValue("@Country", employeeModel.Country);
                    command.Parameters.AddWithValue("@Basic_Salary", employeeModel.Basic_Salary);
                    command.Parameters.AddWithValue("@Other_allowances", employeeModel.Other_Allowances);
                    command.Parameters.AddWithValue("@Other_Income", employeeModel.OtherIncome);
                    command.Parameters.AddWithValue("@Professional_Tax", employeeModel.Professional_Tax);
                    connection.Open();
                    int UpdateOrNot = command.ExecuteNonQuery();

                    if (UpdateOrNot >= 1)
                    {
                        return employeeModel;
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
        public bool DeleteEmployee(int EmployeeID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPDeleteEmployees", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeID);

                    connection.Open();
                    int DeleteOrNot = command.ExecuteNonQuery();

                    if (DeleteOrNot >= 1)
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
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public List<GetEmployeeModel> GetAllEmployee()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                List<GetEmployeeModel> getEmployees = new List<GetEmployeeModel>();
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPGetAllEmployee", connection);

                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader Reader = command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            GetEmployeeModel employee = new GetEmployeeModel()
                            {
                                EmployeeID = Reader.IsDBNull("EmployeeID") ? 0 : Reader.GetInt32("EmployeeID"),
                                EmployeeName = Reader.IsDBNull("EmployeeName") ? string.Empty : Reader.GetString("EmployeeName"),
                                Start_Date = Reader.IsDBNull("Start_Date") ? DateTime.MinValue : Reader.GetDateTime("Start_Date"),
                                Gender = Reader.IsDBNull("Gender") ? string.Empty : Reader.GetString("Gender"),
                                Departments = Reader.IsDBNull("Departments") ? string.Empty : Reader.GetString("Departments"),
                                MobileNumber = Reader.IsDBNull("MobileNumber") ? 0 : Reader.GetInt64("MobileNumber"),
                                Address = Reader.IsDBNull("Address") ? string.Empty : Reader.GetString("Address"),
                                City = Reader.IsDBNull("City") ? string.Empty : Reader.GetString("City"),
                                State = Reader.IsDBNull("State") ? string.Empty : Reader.GetString("State"),
                                Country = Reader.IsDBNull("Country") ? string.Empty : Reader.GetString("Country"),
                                Basic_Salary = Reader.IsDBNull("Basic_Salary") ? 0 : Reader.GetDecimal("Basic_Salary"),
                                Other_Allowances = Reader.IsDBNull("Other_allowances") ? 0 : Reader.GetDecimal("Other_allowances"),
                                OtherIncome = Reader.IsDBNull("Other_Income") ? 0 : Reader.GetDecimal("Other_Income"),
                                Professional_Tax = Reader.IsDBNull("Professional_Tax") ? 0 : Reader.GetDecimal("Professional_Tax"),
                                HRA = Reader.IsDBNull("HRA") ? 0 : Reader.GetDecimal("HRA"),
                                PF = Reader.IsDBNull("PF") ? 0 : Reader.GetDecimal("PF"),
                                Gross_Salary = Reader.IsDBNull("Gross_Salary") ? 0 : Reader.GetDecimal("Gross_Salary"),
                                CTC = Reader.IsDBNull("CTC") ? 0 : Reader.GetDecimal("CTC"),
                                Deductions = Reader.IsDBNull("Deductions") ? 0 : Reader.GetDecimal("Deductions"),
                                Taxable_Income = Reader.IsDBNull("Taxable_Income") ? 0 : Reader.GetDecimal("Taxable_Income"),
                                Income_Tax = Reader.IsDBNull("Income_Tax") ? 0 : Reader.GetDecimal("Income_Tax"),
                                Home_Take_Salary = Reader.IsDBNull("Home_Take_Salary") ? 0 : Reader.GetDecimal("Home_Take_Salary"),
                            };
                            getEmployees.Add(employee);
                        }
                        return getEmployees;
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
        public GetEmployeeModel GetEmployeeByID(int EmployeeID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    GetEmployeeModel employeeModel = new GetEmployeeModel();
                    SqlCommand command = new SqlCommand("SPGetEmployeeByID", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeID);

                    connection.Open();
                    SqlDataReader Reader = command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {

                            employeeModel.EmployeeID = Reader.IsDBNull("EmployeeID") ? 0 : Reader.GetInt32("EmployeeID");
                            employeeModel.EmployeeName = Reader.IsDBNull("EmployeeName") ? string.Empty : Reader.GetString("EmployeeName");
                            employeeModel.Start_Date = Reader.IsDBNull("Start_Date") ? DateTime.MinValue : Reader.GetDateTime("Start_Date");
                            employeeModel.Gender = Reader.IsDBNull("Gender") ? string.Empty : Reader.GetString("Gender");
                            employeeModel.Departments = Reader.IsDBNull("Departments") ? string.Empty : Reader.GetString("Departments");
                            employeeModel.MobileNumber = Reader.IsDBNull("MobileNumber") ? 0 : Reader.GetInt64("MobileNumber");
                            employeeModel.Address = Reader.IsDBNull("Address") ? string.Empty : Reader.GetString("Address");
                            employeeModel.City = Reader.IsDBNull("City") ? string.Empty : Reader.GetString("City");
                            employeeModel.State = Reader.IsDBNull("State") ? string.Empty : Reader.GetString("State");
                            employeeModel.Country = Reader.IsDBNull("Country") ? string.Empty : Reader.GetString("Country");
                            employeeModel.Basic_Salary = Reader.IsDBNull("Basic_Salary") ? 0 : Reader.GetDecimal("Basic_Salary");
                            employeeModel.Other_Allowances = Reader.IsDBNull("Other_allowances") ? 0 : Reader.GetDecimal("Other_allowances");
                            employeeModel.OtherIncome = Reader.IsDBNull("Other_Income") ? 0 : Reader.GetDecimal("Other_Income");
                            employeeModel.Professional_Tax = Reader.IsDBNull("Professional_Tax") ? 0 : Reader.GetDecimal("Professional_Tax");
                            employeeModel.HRA = Reader.IsDBNull("HRA") ? 0 : Reader.GetDecimal("HRA");
                            employeeModel.PF = Reader.IsDBNull("PF") ? 0 : Reader.GetDecimal("PF");
                            employeeModel.Gross_Salary = Reader.IsDBNull("Gross_Salary") ? 0 : Reader.GetDecimal("Gross_Salary");
                            employeeModel.CTC = Reader.IsDBNull("CTC") ? 0 : Reader.GetDecimal("CTC");
                            employeeModel.Deductions = Reader.IsDBNull("Deductions") ? 0 : Reader.GetDecimal("Deductions");
                            employeeModel.Taxable_Income = Reader.IsDBNull("Taxable_Income") ? 0 : Reader.GetDecimal("Taxable_Income");
                            employeeModel.Income_Tax = Reader.IsDBNull("Income_Tax") ? 0 : Reader.GetDecimal("Income_Tax");
                            employeeModel.Home_Take_Salary = Reader.IsDBNull("Home_Take_Salary") ? 0 : Reader.GetDecimal("Home_Take_Salary");
                        }
                        return employeeModel;
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
    }
}
