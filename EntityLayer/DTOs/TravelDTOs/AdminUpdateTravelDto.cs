using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.TravelDTOs
{
	public class AdminUpdateTravelDto
	{
        public int TravelID { get; set; }
        public int StaffID { get; set; }

		//2eylül burası kapatıldı: onaylandı reddedildi butonları eklenecek
		//public int StatusID { get; set; }
	}
}