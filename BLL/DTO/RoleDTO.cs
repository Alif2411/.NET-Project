using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTO
{
    public class RoleDTO
    {
        public int ID { get; set; }

        [Required]
        public string RoleName { get; set; }

    }
}
