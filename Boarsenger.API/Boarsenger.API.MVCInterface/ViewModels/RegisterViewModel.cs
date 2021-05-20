using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boarsenger.API.MVCInterface.ViewModels
{
    public class RegisterViewModel : LoginViewModel
    {
        public string ConfirmPassword { get; set; }
    }
}
