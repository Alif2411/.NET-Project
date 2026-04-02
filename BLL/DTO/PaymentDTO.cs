using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLL.DTO
{
    public class PaymentDTO
    {
        public int ID { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Amount { get; set; }
        [Required]
        public DateTime PaidDate { get; set; }
        [Required]
        public int ContractId { get; set; }

    }
}
