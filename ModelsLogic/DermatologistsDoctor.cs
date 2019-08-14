using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModelsLogic
{
    public class DermatologistsDoctor
    {
        [Key]
        public int DoctorId { get; set; }
        [DisplayName("Fullame")]
        public string Name { get; set; }
        public int ProfId { get; set; }
        public virtual DoctorProfesion profession { get; set; }
    }
}
