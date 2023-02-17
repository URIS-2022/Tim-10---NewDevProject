namespace payment.Models
{
    public class PublicBiddingDto
    {

        public DateTime date { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public int averagePricePerHectare { get; set; }
        public bool excepted { get; set; }
        public Guid publicBiddingTypeId { get; set; }

        public int auctionedPrice { get; set; }
        public int leasePeriod { get; set; }
        public int participantsNumber { get; set; }
        public int depositReplenishmentAmount { get; set; }
        public int round { get; set; }
        public Guid biddingStatusId { get; set; }








    }
}
