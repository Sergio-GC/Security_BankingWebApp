using System.ComponentModel.DataAnnotations;

namespace BankingWebApp.Models
{
    public class LoginVM
    {
        [Required]
        public string accountNb { get; set; }
        [Required]
        public string password { get; set; }
    }
}
