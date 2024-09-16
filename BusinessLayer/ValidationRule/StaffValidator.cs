using EntityLayer.DTOs.StaffDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRule
{
    public class StaffValidator : AbstractValidator<CreateStaffDto>
    {
        public StaffValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Lütfen isminizi giriniz");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Lütfen soyisminizi giriniz");
            //RuleFor(x => x.AdminID).NotEmpty().WithMessage("Lütfen bir yönetici seçiniz");
			RuleFor(x => x.Mail).NotEmpty().WithMessage("Mail alanı boş geçilemez");
			RuleFor(x => x.Mail).EmailAddress().WithMessage("Lütfen doğru bir eposta giriniz.");
			RuleFor(x => x.Username).NotEmpty().WithMessage("Kullanıcı adı alanı boş geçilemez");
			RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş geçilemez");
			RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifre tekrar alanı boş geçilemez");
			RuleFor(x => x.Username).MinimumLength(5).WithMessage("Lütfen en az 5 karakterlik bir veri girişi yapınız");
			RuleFor(x => x.Username).MaximumLength(20).WithMessage("Lütfen en fazla 20 karakterlik bir veri girişi yapınız");

			RuleFor(x => x.Password).Equal(y => y.ConfirmPassword).WithMessage("Şifreler birbiriyle uyuşmuyor");
		}
    }
}