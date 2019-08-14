using System.ComponentModel.DataAnnotations;

namespace ModelsLogic
{
    public class DermatologyService
    {
        [Key]
        public int DemServiceId { get; set; }

        [Required]
        public string ServiceName { get; set; }
    }
}
