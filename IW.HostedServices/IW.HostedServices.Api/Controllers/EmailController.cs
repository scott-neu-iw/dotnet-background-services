using IW.HostedServices.Core.Models;
using IW.HostedServices.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IW.HostedServices.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class EmailController : ControllerBase
    {
        private readonly IEmailRepository _emailRepository;
        public EmailController(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var emailEntry = new EmailEntry { Subject = "Demo Subject", Message = "Demo Message", CreatedDate = DateTime.Now };
            await _emailRepository.Add(emailEntry);
            return Ok(emailEntry);
        }
    }
}
