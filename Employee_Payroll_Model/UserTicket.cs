
using System;

namespace Employee_Payroll_Model
{
    public class UserTicket
    {
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string Token { get; set; }
        public DateTime IssueAt { get; set; }
    }
}
