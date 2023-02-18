namespace Commission.Models
{
    public class CommissionDto
    {
        public Guid commissionId { get; set; }
        public string? nameOfCommission { get; set; }
        public Guid presidentId { get; set; }
    }
}
