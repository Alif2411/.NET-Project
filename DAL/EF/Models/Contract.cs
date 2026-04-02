using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.EF.Models
{
    public class Contract
    {
        [Key]
        public int ID { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; }

        public int JobID { get; set; }

        [ForeignKey("Proposal")]
        public int ProposalId { get; set; }
        public Proposal Proposal { get; set; }

    }
}
