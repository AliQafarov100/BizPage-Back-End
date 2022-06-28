using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BizPage_Back_End.ViewModels
{
    public class EditVM
    {
        public string FirstName { get; set; }
        [StringLength(maximumLength: 20)]
        public string LastName { get; set; }
        [StringLength(maximumLength: 20)]
        public string Username { get; set; }
        [DataType(DataType.EmailAddress), StringLength(maximumLength: 25)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
