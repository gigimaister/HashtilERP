namespace HashtilERP.Shared.Models.Drivers
{
    public class DriversToOrder
    {
        public int DriversToOrderId { get; set; }
        public int? JobId { get; set; }
        public string DriverId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string ScreenName { get; set; }
        public string Contractor => GetContractor(UserName.Split('@')[1]);
        public string FullName => $"{ScreenName} - {Contractor}";
        //Get Contractor Name
        public string GetContractor(string name)
        {
            string contractor = "";
            switch (name)
            {
                case "hashtil":
                    contractor = "השתיל";
                    break;
                case "nadav":
                    contractor = "נדב";
                    break;
            }

            return contractor;
        }
    }
}
