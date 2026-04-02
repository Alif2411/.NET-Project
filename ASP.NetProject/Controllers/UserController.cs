using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserServices userServices;
        JobServices jobServices;
        ProposalServices proposalServices;
        ContractServices contractServices;
        PaymentServices paymentServices;
        public UserController(UserServices userServices, JobServices jobServices, ProposalServices proposalServices, ContractServices contractServices, PaymentServices paymentServices)
        {
            this.userServices = userServices;
            this.jobServices = jobServices;
            this.proposalServices = proposalServices;
            this.contractServices = contractServices;
            this.paymentServices = paymentServices;
        }

        [HttpPost("post-job")]
        public IActionResult PostJob (JobDTO job)
        {
            job.Status = JobStatus.Open;
            var res = jobServices.Add(job);
            return Ok(res);
        }

        [HttpPost("submit-proposal")]
        public IActionResult SubmitProposal(ProposalDTO proposal) {
            var jobFound = jobServices.Get(proposal.JobId);
            if (jobFound != null) {
                proposal.Status = ProposalStatus.Applied;
                var res = proposalServices.Add(proposal);
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpPost("create-contract")]
        public IActionResult CreateContract(ContractDTO contract) {
            var proposalFound = proposalServices.Get(contract.ProposalId);
            if (proposalFound != null)
            {
                contract.Status = ContractStatus.NotStarted;
                contract.Amount = proposalFound.BidAmount;
                var res = contractServices.Add(contract);
                var job = jobServices.Get(contract.JobID);
                job.Status = JobStatus.Contracted;
                jobServices.Update(job);
                proposalFound.Status = ProposalStatus.Accepted;
                proposalServices.Update(proposalFound);

                return Ok(res);
            }
            return BadRequest();
        }

        [HttpPost("pay")]
        public IActionResult Pay(PaymentDTO payment)
        {
            try
            {
                var contractFound = contractServices.Get(payment.ContractId);
                if (contractFound != null)
                {
                    if (contractFound.Amount == payment.Amount)
                    {
                        var res = paymentServices.Add(payment);
                        if (res)
                        {
                            // Update Contract to Complete
                            contractFound.Status = ContractStatus.Complete;
                            contractServices.Update(contractFound);

                            // Update Job to Complete
                            var job = jobServices.Get(contractFound.JobID);
                            if (job != null)
                            {
                                job.Status = JobStatus.Complete;
                                jobServices.Update(job);
                            }
                        }
                        return Ok(res);
                    }
                }
                return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, ex.Message);

            }
        }

        [HttpGet("find-jobs")]
        public IActionResult FindJobs()
        {
            return Ok(jobServices.Get());
        }

        [HttpGet("recommendations")]
        public IActionResult GetRecommendations()
        {
            try
            {
                // Simple Logic: Recommendations = Last 5 Jobs that are OPEN
                var allJobs = jobServices.Get();
                var recommendedJobs = (from j in allJobs
                                       where j.Status == JobStatus.Open
                                       orderby j.ID descending
                                       select j).Take(5).ToList();

                return Ok(recommendedJobs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("find-a-job/{id}")]
        public IActionResult FindAJob(int id)
        {
            return Ok(jobServices.Get(id));
        }

        [HttpGet("job-proposals/{id}")]
        public IActionResult GetProposals(int id) {
            var jobFound = jobServices.Get(id);
            if (jobFound != null)
            {
                var res = proposalServices.GetProposals(jobFound);
                return Ok(res);

            }
            return BadRequest();
        }

        [HttpGet("proposal-in-budget/{id}")]
        public IActionResult GetProposalsInRange(int id, [FromQuery] int? min, [FromQuery] int? max) {
            var jobFound = jobServices.Get(id);
            if(jobFound != null)
            {
                var res = proposalServices.GetProposalsInRange(jobFound, min, max);
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpPut("update-profile")]
        public IActionResult UpdateProfile(UserDTO user)
        {
            try
            {
                var userFound = userServices.Get((int)user.ID);
                if (userFound != null)
                {
                    userFound.Email = user.Email;
                    userFound.Password = user.Password;
                    var res = userServices.Update(userFound);
                    if (res) return Ok(res); 
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update-job")]
        public IActionResult UpdateJob(JobDTO job)
        {
            try
            {
                var jobFound = jobServices.Get(job.ID);
                if (jobFound != null)
                {
                    if (jobFound.Status == JobStatus.Open)
                    {
                        jobFound.Title = job.Title;
                        jobFound.Budget = job.Budget;
                        var res = jobServices.Update(jobFound);
                        if(res) return Ok(res);
                    }
                }
                return BadRequest("Job can only be updated when it is Open.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update-proposal")]
        public IActionResult UpdateProposal(ProposalDTO proposal)
        {
            try
            {
                var jobFound = jobServices.Get(proposal.JobId);
                if (jobFound != null && jobFound.Status == JobStatus.Open)
                {
                     var res = proposalServices.Update(proposal);
                     if(res) return Ok(res);
                }
                return BadRequest("Proposal can only be changed when the Job is Open.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete-user/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var res = userServices.Delete(id);
                if (res) return Ok(res);
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete-job/{id}")]
        public IActionResult DeleteJob(int id)
        {
            try
            {
                var res = jobServices.Delete(id);
                if(res) return Ok(res);
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete-proposal/{id}")]
        public IActionResult DeleteProposal(int id)
        {
            try
            {
                var res = proposalServices.Delete(id);
                if(res) return Ok(res);
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        


    }
}
