namespace User1.Entities
{
    public class User
    {
        public Guid userId { get; set; }
        public Guid userTypeId { get; set; }    
        public string? name { get; set; }
        public string? surname { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? salt { get; set; } = string.Empty;

    }
}
