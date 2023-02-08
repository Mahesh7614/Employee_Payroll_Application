
using Employee_Payroll_Manager.Interface;
using Employee_Payroll_Model;
using Employee_Payroll_Repository.Interface;
using System;
using System.Collections.Generic;

namespace Employee_Payroll_Manager.Manager
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public EmployeeModel AddEmployee(EmployeeModel employeeModel)
        {
            try
            {
                return this.employeeRepository.AddEmployee(employeeModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public EmployeeModel UpdateEmployee(int EmployeeID, EmployeeModel employeeModel)
        {
            try
            {
                return this.employeeRepository.UpdateEmployee(EmployeeID, employeeModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteEmployee(int EmployeeID)
        {
            try
            {
                return this.employeeRepository.DeleteEmployee(EmployeeID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<GetEmployeeModel> GetAllEmployee()
        {
            try
            {
                return this.employeeRepository.GetAllEmployee();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public GetEmployeeModel GetEmployeeByID(int EmployeeID)
        {
            try
            {
                return this.employeeRepository.GetEmployeeByID(EmployeeID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
