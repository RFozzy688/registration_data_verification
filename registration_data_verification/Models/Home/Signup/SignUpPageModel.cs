namespace registration_data_verification.Models.Home.Signup
{
    public class SignUpPageModel
    {
        public String Message { get; set; } = null!;
        public bool? IsSuccess { get; set; }
        public SignUpFormModel? SignUpFormModel { get; set; }
        public Dictionary<String, String> ValidationErrors { get; set; } = null!;
        public int? Count { get; set; }
    }
}
