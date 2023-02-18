namespace Contract.Models
{
    public class DocumentDto
    {
        public Guid documentId { get; set; }
        public string? referenceNumber { get; set; }
        public DateTime date { get; set; }
    }
}
