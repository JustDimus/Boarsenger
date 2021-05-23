using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Boarsenger.API.MVCInterface.ViewModels
{
    public class RegisterViewModel : LoginViewModel
    {
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Nick Name")]
        public string NickName { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Second Name")]
        public string SecondName { get; set; }
        [Display(Name = "Day of Birth")]
        public DateTime DayOfBirth { get; set; }
    }
}
