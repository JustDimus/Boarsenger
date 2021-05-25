using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Boarsenger.API.MVCInterface.ViewModels
{
    public class ServerViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Users { get; set; }

        public int Online { get; set; }
    }
}
