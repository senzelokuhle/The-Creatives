using System;
using System.ComponentModel.DataAnnotations;

namespace ModelsLogic
{
    public class PatientRecord
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Start Date")]
        public string Date { get; set; }

        [Required]
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        [Required]
        [Display(Name = "DOB")]
        [DataType(DataType.Date)]
        public string DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Physical Address")]
        public string Address { get; set; }

        [Display(Name = "Home Tel")]
        [MaxLength(10, ErrorMessage = "Please enter 10 digits")]
        [MinLength(10, ErrorMessage = "Please enter 10 digits")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Enter valid telephone number format")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide a telephone number ")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter a valid telephone number")]
        public string HomeTelephone { get; set; }


        [MaxLength(10, ErrorMessage = "Please enter 10 digits")]
        [MinLength(10, ErrorMessage = "Please enter 10 digits")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Enter valid cellphone number format")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide a cellphone number ")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter a valid cellphone number")]
        [Display(Name = "Cellphone")]
        public string CellPhone { get; set; }

        [Required]
        [Display(Name = "Identity No.")]
        public string IdentityNo { get; set; }

        [Display(Name = "Employer Name")]
        public string Employer { get; set; }

        [Display(Name = "Work Tel.")]
        [MaxLength(10, ErrorMessage = "Please enter 10 digits")]
        [MinLength(10, ErrorMessage = "Please enter 10 digits")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Enter valid telephone number format")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide a workplace number ")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter a valid workplace number")]
        public string WorkNumber { get; set; }


        [Display(Name = "Emergency Contact")]
        [MaxLength(10, ErrorMessage = "Please enter 10 digits")]
        [MinLength(10, ErrorMessage = "Please enter 10 digits")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Enter valid telephone number format")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide a emergency contact number ")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter a valid emergency contact number")]
        public string EmergencyContact { get; set; }


        [Display(Name = "Next of Kin")]
        public string NextOfKin { get; set; }


        [Display(Name = "Next of Kin Tel.")]
        [MaxLength(10, ErrorMessage = "Please enter 10 digits")]
        [MinLength(10, ErrorMessage = "Please enter 10 digits")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Enter valid telephone number format")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide a home telephone number ")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter a valid home telephone number")]
        public string HomePhone { get; set; }


        [Display(Name = "Medical Aid")]
        public string InsuranceName { get; set; }

        [Display(Name = "Policy No.")]
        public string PolicyNumber { get; set; }

        [Display(Name = "Medical Aid Tel.")]
        [MaxLength(10, ErrorMessage = "Please enter 10 digits")]
        [MinLength(10, ErrorMessage = "Please enter 10 digits")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Enter valid telephone number format")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide a medical aid telephone number ")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter a valid medical aid telephone number")]
        public string InsuranceNumber { get; set; }

        public int GenderId { get; set; }
        public virtual Gender gender { get; set; }


        //Methods
        //Get start date
        public string StartDate()
        {
            return System.DateTime.Now.ToShortDateString();
        }
    }
}
