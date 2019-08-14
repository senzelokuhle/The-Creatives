using System.ComponentModel.DataAnnotations;

namespace ModelsLogic
{
    public class DoctorProfesion
    {
        [Key]
        public int ProfId { get; set; }

        [Display(Name = "Professionality")]
        [Required]
        public string ProfName { get; set; }
    }
}
