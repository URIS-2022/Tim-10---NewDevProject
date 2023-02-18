namespace User1.Models
{
    public class UserUpdateDto
    { 
        
        public Guid userId { get; set; }
        public Guid userTypeId { get; set; }
        public string? name { get; set; }
        public string? surname { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
    }
}

