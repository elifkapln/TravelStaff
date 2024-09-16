using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EntityLayer.Concrete
{
    public class Staff:IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Active { get; set; }
        public bool IsAdmin { get; set; }


        [ForeignKey("AdminID")]
        public int? AdminID { get; set; }
        public Staff Admin { get; set; }

        public  List<Travel> Travels { get; set; }
        public  List<Travel> TravelAdmins { get; set; }
        public  List<Staff> Staffs { get; set; }
    }
}