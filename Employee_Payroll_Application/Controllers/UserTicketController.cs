using Employee_Payroll_Manager.Interface;
using Employee_Payroll_Model;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Employee_Payroll_Application.Controllers
{
    [Route("Employee_Payroll/[controller]")]
    [ApiController]
    public class UserTicketController : ControllerBase
    {
        private readonly IBus bus;
        private readonly IUserManager userManager;

        public UserTicketController(IBus bus, IUserManager userManager)
        {
            this.bus = bus;
            this.userManager = userManager;
        }
        [HttpGet("ForgotPassword")]
        public async Task<IActionResult> CreateTicketForPassword(string EmailID)
        {
            try
            {
                if (EmailID != null)
                {
                    var token = this.userManager.ForgotPassword(EmailID);
                    if (!string.IsNullOrEmpty(token))
                    {
                        UserTicket userTicket = this.userManager.CreateTicketForPassword(EmailID, token);
                        Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
                        var endPoint = await this.bus.GetSendEndpoint(uri);
                        await endPoint.Send(userTicket);
                        return Ok(new { sucess = true, message = "Email Sent Successfully" });
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "EmailID not Registered" });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, message = "Something went Wrong" });
                }
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
