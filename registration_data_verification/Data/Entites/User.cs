namespace registration_data_verification.Data.Entites
{
    public class User
    {
        public Guid Id { get; set; }
        public String Name { get; set; } = null!;
        public String Email { get; set; } = null!;
        public String? AvatarUrl { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime Registered { get; set; }
        public String Salt { get; set; } = null!;
        public String DerivedKey { get; set; } = null!;
    }
}
