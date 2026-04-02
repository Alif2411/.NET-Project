using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.EF.Models
{
    public class Proposal
    {
        [Key]
        public int ID { get; set; }
        public int BidAmount { get; set; }
        public string Status { get; set; }
        
        public int UserId { get; set; }

        [ForeignKey("Job")]
        public int JobId { get; set; }
        public Job Job { get; set; }
    }
}
