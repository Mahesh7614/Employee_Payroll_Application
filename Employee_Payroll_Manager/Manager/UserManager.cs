
using Employee_Payroll_Manager.Interface;
using Employee_Payroll_Model;
using Employee_Payroll_Repository.Interface;
using System;

namespace Employee_Payroll_Manager.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public UserModel UserRegistration(UserModel userModel)
        {
            try
            {
                return this.userRepository.UserRegistration(userModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string Login(string EmailID, string Password)
        {
            try
            {
                return this.userRepository.Login(EmailID, Password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public UserTicket CreateTicketForPassword(string emailID, string token)
        {
            try
            {
                return this.userRepository.CreateTicketForPassword(emailID,token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string ForgotPassword(string emailID)
        {
            try
            {
                return this.userRepository.ForgotPassword(emailID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ResetPassword(string Password, int UserID)
        {
            try
            {
                return this.userRepository.ResetPassword(Password, UserID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteEmployee(int UserID)
        {
            try
            {
                return this.userRepository.DeleteEmployee(UserID);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
