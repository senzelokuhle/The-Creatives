using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLogic
{
    [Table("SiteUser")]
    public class SiteUser
    {
        [Key]
        public int SiteUserId { get; set; }
        public string Username { get; set; }

        [DataType(DataType.EmailAddress),Required]
        public string Email { get; set; }

        
        [MinLength(6,ErrorMessage ="Password must be six characters long")]
        public string Password { get; set; }
        public Nullable<bool> IsValid { get; set; }

        //Method to generate default password
        public string setPassword()
        {
            Random rand = new Random();
            int random_value = rand.Next(1, 1000);

            string shortName = Username.Substring(0, 3);

            return shortName + random_value.ToString();
        }
    }
}
