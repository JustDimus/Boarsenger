using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.BLL.Models
{
    public class UpdateServerPublicationStatusDTO
    {
        public ServerOwnerDataDTO ServerOwnerData { get; set; }

        public bool PublicationStatus { get; set; }
    }
}
