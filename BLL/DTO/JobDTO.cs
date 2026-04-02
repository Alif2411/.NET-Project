using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLL.DTO
{
    public class JobDTO
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        public string Budget { get; set; }
        public JobStatus? Status { get; set; }
        [Required]
        public int UserId { get; set; }
    }

    public enum JobStatus
    {
        Open,
        Contracted,
        Complete

        
    }
}
