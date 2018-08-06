using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Models.AccountViewModels
{
    public class LoginWith2faViewModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "{0} должен быть длинной от {2} до {1} символов(а).", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Код аутонтификации")]
        public string TwoFactorCode { get; set; }

        [Display(Name = "Запомнить это устройство?")]
        public bool RememberMachine { get; set; }

        public bool RememberMe { get; set; }
    }
}
