using System;
using System.ComponentModel.DataAnnotations;
using ModelsLogic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLogic
{
    [Table("Appointment")]
    public class Appointment
    {
        [Key]
        public int EventID { get; set; }

        [Display(Name = "Fullname")]
        [Required]
        public string Subject { get; set; }

        //[Required]
        public string Description { get; set; }
        [Required]
        public System.DateTime Start { get; set; }
        public Nullable<System.DateTime> End { get; set; }

        [Display(Name = "Urgency")]
        public string ThemeColor { get; set; }

        public bool IsFullDay { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Cellphone { get; set; }



        public string EMailBody { get; set; }
        [Display(Name = "Subject")]
        public string EmailSubject { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "CC")]
        public string EmailCC { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "BCC")]
        public string EmailBCC { get; set; }



    }
}
