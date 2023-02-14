
using Employee_Payroll_Model;

namespace Employee_Payroll_Manager.Interface
{
    public interface IUserManager
    {
        public UserModel UserRegistration(UserModel userModel);
        public string Login(string EmailID, string Password);
        public UserTicket CreateTicketForPassword(string emailID, string token);
        public string ForgotPassword(string emailID);
        public bool ResetPassword(string Password, int UserID);
        public bool DeleteEmployee(int UserID);

    }
}
