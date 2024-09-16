using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.StaffDTOs
{
    public class AdminListDto
    {
        public int StaffID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Active { get; set; }
        public bool IsAdmin { get; set; }
    }
}
//yerine stafflistdto kullanılıyor