
using Employee_Payroll_Model;

namespace Employee_Payroll_Repository.Interface
{
    public interface IUserRepository
    {
        public UserModel UserRegistration(UserModel userModel);
        public string Login(string EmailID, string Password);
        public string ForgotPassword(string emailID);
        public bool ResetPassword(string Password, int UserID);

    }
}
