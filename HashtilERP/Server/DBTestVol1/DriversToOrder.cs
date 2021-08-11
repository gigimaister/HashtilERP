using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HashtilERP.DBTestVol1
{
    public class DriversToOrder
    {
        public int DriversToOrderId { get; set; }
        public int? JobId { get; set; }
        public string DriverId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string ScreenName { get; set; }
    }
}
