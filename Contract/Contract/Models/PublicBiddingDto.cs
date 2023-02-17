namespace Contract.Models
{
    public class PublicBiddingDto
    {
        public Guid publicBiddingId { get; set; }
        public DateTime date { get; set; }
        public int numberOfBidders { get; set; }
    }
}
