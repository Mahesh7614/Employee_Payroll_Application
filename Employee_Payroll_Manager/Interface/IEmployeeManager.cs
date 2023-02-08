﻿
using Employee_Payroll_Model;
using System.Collections.Generic;

namespace Employee_Payroll_Manager.Interface
{
    public interface IEmployeeManager
    {
        public EmployeeModel AddEmployee(EmployeeModel employeeModel);
        public EmployeeModel UpdateEmployee(int EmployeeID, EmployeeModel employeeModel);
        public bool DeleteEmployee(int EmployeeID);
        public List<GetEmployeeModel> GetAllEmployee();
        public GetEmployeeModel GetEmployeeByID(int EmployeeID);
    }
}
