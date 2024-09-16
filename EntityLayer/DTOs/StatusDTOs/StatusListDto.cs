using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.StatusDTOs
{
    public class StatusListDto
    {
        public int StatusID { get; set; }
        public bool Active { get; set; }
        public string Information { get; set; }
    }
}
