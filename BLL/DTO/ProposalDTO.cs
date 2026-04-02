using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLL.DTO
{
    public class ProposalDTO
    {
        public int ID { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Bid Amount must be greater than 0")]
        public int BidAmount { get; set; }
        public ProposalStatus? Status { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int JobId { get; set; }
    }

    public enum ProposalStatus
    {
        Applied,
        Accepted
    }
}
