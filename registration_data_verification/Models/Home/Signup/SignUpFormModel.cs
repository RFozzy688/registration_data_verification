using Microsoft.AspNetCore.Mvc;

namespace registration_data_verification.Models.Home.Signup
{
    public class SignUpFormModel
    {
        [FromForm(Name = "signup-username")]
        public String UserName { get; set; }

        [FromForm(Name = "signup-email")]
        public String UserEmail { get; set; }

        [FromForm(Name = "signup-password")]
        public String UserPassword { get; set; }

        [FromForm(Name = "signup-avatar")]
        public IFormFile AvatarFile { get; set; } = null!;

        [FromForm(Name = "signup-birthdate")]
        public DateTime? Birthdate { get; set; } = null!;

        [FromForm(Name = "signup-confirm")]
        public bool Confirm { get; set; }
    }
}
