using System.ComponentModel.DataAnnotations;

namespace UniLife.Shared.Dto.Account
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Kullanıcı Adı alanı zorunludur.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
