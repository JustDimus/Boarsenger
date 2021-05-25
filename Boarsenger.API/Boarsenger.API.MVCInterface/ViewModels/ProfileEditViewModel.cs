﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boarsenger.API.MVCInterface.ViewModels
{
    public class ProfileEditViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int Age { get; set; }
    }
}
