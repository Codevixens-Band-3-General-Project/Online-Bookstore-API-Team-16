using System.ComponentModel.DataAnnotations;

namespace BookstoreAPI.Models.DTO
{
    public class UserRegisterationDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
