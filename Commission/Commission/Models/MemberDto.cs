namespace Commission.Models
{
    public class MemberDto
    {
        public Guid memberId { get; set; }
        public Guid commissionId { get; set; }

        public PersonalityDto? personality { get; set; }
    }
}
