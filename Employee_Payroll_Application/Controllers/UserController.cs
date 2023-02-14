using Employee_Payroll_Manager.Interface;
using Employee_Payroll_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace Employee_Payroll_Application.Controllers
{
    [Route("EmployeePayroll/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;

        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }
        [HttpPost]
        [Route("EmployeePayroll/Registration")]
        public IActionResult UserRegistration(UserModel userModel)
        {
            try
            {
                UserModel registrationData = this.userManager.UserRegistration(userModel);
                if (registrationData != null)
                {
                    return this.Ok(new { success = true, message = "Registration Successfull", result = registrationData });
                }
                return this.Ok(new { success = true, message = "User Already Exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        [Route("EmployeePayroll/Login")]
        public IActionResult Login(string EmailID, string Password)
        {
            try
            {
                string userToken = this.userManager.Login(EmailID, Password);
                if (userToken != null)
                {
                    return this.Ok(new { success = true, message = "Login Successfull", result = userToken });
                }
                return this.Ok(new { success = true, message = "Enter Valid EmailID or Password" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        [Route("EmployeePayroll/ForgotPassword")]
        public IActionResult ForgotPassword(string EmailID)
        {
            try
            {
                string emailToken = this.userManager.ForgotPassword(EmailID);
                if (emailToken != null)
                {
                    return this.Ok(new { success = true, message = "Password Forgot Sucessfully", result = emailToken });
                }
                return this.Ok(new { success = true, message = "Enter Valid EmailID" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("EmployeePayroll/ResetPassword")]
        public IActionResult ResetPassword(string password, string confirmPassword)
        {
            try
            {
                int UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                if (password == confirmPassword)
                {
                    bool userPassword = this.userManager.ResetPassword(password, UserID);
                    if (userPassword)
                    {
                        return this.Ok(new { success = true, message = "Password Reset Successfully", result = userPassword });
                    }
                }
                return this.Ok(new { success = true, message = "Enter Password same as above" });

            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("Employee_Payroll/DeleteUser")]
        public IActionResult DeletePassword(int UserID)
        {
            try
            {
                bool delete = this.userManager.DeleteEmployee(UserID);
                if(delete)
                {
                    return this.Ok(new {sucess = true, Message = "User Deleted Successfully" ,result =delete});
                }
                return this.Ok(new { sucess = true, Message = "User Not Deleted ", result = delete });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { sucess = true, message = ex.Message });
            }
        }
    }
}
