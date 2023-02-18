using Microsoft.AspNetCore.Builder;

namespace Contract.Models
{
    public class ContractDto
    {
        public Guid contractId { get; set; }
        public Guid typeId { get; set; }
        public Guid documentId { get; set; }
        public string? referenceNumber { get; set; }
        public Guid publicBiddingId { get; set; }
        public DateTime dateOfContract { get; set; }
        public Guid BuyerId { get; set; }
        public DateTime deadline { get; set; }
        public string? place { get; set; }
        public DocumentDto? documentDto { get; set; }
        public BuyerDto? buyerDto { get; set; }
        public PublicBiddingDto? publicBiddingDto { get; set; }
        public DateTime dateOfSigning { get; set; }
    }
}
