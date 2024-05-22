using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI1.Application;
using WebAPI1.Domain.Models;

namespace WebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        /// <summary>
        /// API to pull emails
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<EmailDto>>> GetAll(string mailBoxEmail, int top)
        {
            try
            {
                var emails = await _emailService.GetEmails(mailBoxEmail, top, "");
                return Ok(emails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error while retrieving Emails!", ErrorDetails = ex.Message });
            }
        }

        /// <summary>
        /// API to send Email
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Send")]
        public async Task<ActionResult<bool>> Send([FromBody] SendEmailDto payload)
        {
            try
            {
                var isSent = await _emailService.SendEmailAsync(payload);
                return Ok(isSent);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = $"Error while Sending Email!", ErrorDetails = e.Message });
            }
        }
    }
}
