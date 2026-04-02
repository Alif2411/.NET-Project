using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLL.DTO
{
    public class ContractDTO
    {
        public int ID { get; set; }
        public int? Amount { get; set; }
        public ContractStatus? Status { get; set; }
        [Required]
        public int JobID { get; set; }
        [Required]
        public int ProposalId { get; set; }

    }

    public enum ContractStatus
    {
        NotStarted,
        Working,
        Complete
    }
}
