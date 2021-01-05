using System.ComponentModel.DataAnnotations;


namespace HashtilERP.Shared.Models
{
    public class KobiTestTable
    {
        public string Id { get; set; }

        [Required]
        public string PassportNum { get; set; }
    }
}
