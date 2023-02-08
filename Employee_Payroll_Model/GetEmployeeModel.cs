
using System;

namespace Employee_Payroll_Model
{
    public class GetEmployeeModel
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime Start_Date { get; set; }
        public string Gender { get; set; }
        public string Departments { get; set; }
        public long MobileNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public decimal Basic_Salary { get; set; }
        public decimal Other_Allowances { get; set; }
        public decimal OtherIncome { get; set; }
        public decimal Professional_Tax { get; set; }
        public decimal HRA { get; set; }
        public decimal PF { get; set;}
        public decimal Gross_Salary { get; set; }
        public decimal CTC { get; set; }
        public decimal Deductions { get; set; }
        public decimal Taxable_Income { get; set; }
        public decimal Income_Tax { get; set; }
        public decimal Home_Take_Salary { get; set; }
    }
}
