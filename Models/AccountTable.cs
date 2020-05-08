using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public class AccountTable
    {

        [Key]
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Mobile Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public string Mobile { get; set; }
        [Required]
    
        [EmailAddress]
       
        public string Email { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        [DataType(DataType.Currency)]
        public float? Amount { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


    }
}
