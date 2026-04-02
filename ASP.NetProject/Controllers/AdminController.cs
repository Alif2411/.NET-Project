using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ASP.NetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserServices userServices;
        private readonly JobServices jobServices;
        private readonly ProposalServices proposalServices;
        private readonly ContractServices contractServices;
        private readonly PaymentServices paymentServices;

        public AdminController(UserServices userServices, JobServices jobServices, ProposalServices proposalServices, ContractServices contractServices, PaymentServices paymentServices)
        {
            this.userServices = userServices;
            this.jobServices = jobServices;
            this.proposalServices = proposalServices;
            this.contractServices = contractServices;
            this.paymentServices = paymentServices;
        }

        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = userServices.Get();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("jobs")]
        public IActionResult GetAllJobs()
        {
            try
            {
                var jobs = jobServices.Get();
                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("contracts")]
        public IActionResult GetAllContracts()
        {
            try
            {
                var contracts = contractServices.Get();
                return Ok(contracts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("summary")]
        public IActionResult GetSystemSummary()
        {
            try
            {
                // Fetch all data
                var users = userServices.Get();
                var jobs = jobServices.Get();
                var payments = paymentServices.Get();
                var contracts = contractServices.Get();

                // Calculate Analytics
                var totalUsers = users.Count;
                
                var totalJobs = jobs.Count;
                var openJobs = jobs.Count(j => j.Status == JobStatus.Open);
                var contractedJobs = jobs.Count(j => j.Status == JobStatus.Contracted);
                var completedJobs = jobs.Count(j => j.Status == JobStatus.Complete);

                var totalRevenue = payments.Sum(p => p.Amount);
                var totalContracts = contracts.Count;

                var summary = new
                {
                    AppStats = new
                    {
                        TotalUsers = totalUsers,
                        TotalJobs = totalJobs,
                        TotalContracts = totalContracts,
                        TotalRevenue = totalRevenue
                    },
                    JobStatusBreakdown = new
                    {
                        Open = openJobs,
                        Contracted = contractedJobs,
                        Completed = completedJobs
                    }
                };

                return Ok(summary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
