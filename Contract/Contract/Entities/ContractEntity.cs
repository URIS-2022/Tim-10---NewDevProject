using Contract.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contract.Entities
{
    public class ContractEntity
    {
        /// <summary>
        /// ID of the contract
        /// </summary>
        [Key]
        public Guid contractId { get; set; }
        /// <summary>
        /// ID of the guarantee type
        /// </summary>
        [Required]
        [ForeignKey("TypeOfGuaranteeEntity")]
        public Guid typeId { get; set; }
        /// <summary>
        /// ID of the document
        /// </summary>
        public Guid documentId { get; set; }
        /// <summary>
        /// Reference number
        /// </summary>
        public string referenceNumber { get; set; }
        /// <summary>
        /// ID of the public bidding
        /// </summary>
        public Guid publicBiddingId { get; set; }
        /// <summary>
        /// Date of the contract referencing
        /// </summary>
        public DateTime dateOfContract { get; set; }
        /// <summary>
        /// ID of the buyer
        /// </summary>
        public Guid buyerId { get; set; }
        /// <summary>
        /// Deadline of the contract
        /// </summary>
        public DateTime deadline { get; set; }
        /// <summary>
        /// Location of the contract signing
        /// </summary>
        public string? place { get; set; }
        /// <summary>
        /// Date of the contract signing
        /// </summary>
        public DateTime dateOfSigning { get; set; }
        [NotMapped]
        public DocumentDto? documentDto { get; set; }
        [NotMapped]
        public BuyerDto? buyerDto { get; set; }
        [NotMapped]
        public PublicBiddingDto? publicBiddingDto { get; set; }

        override
        public string ToString()
        {
            return "Contract: { ID: " + this.contractId + ", Guarantee ID: "
                + this.typeId + ", Document ID: " + this.documentId + ", Reference number: "
                + this.referenceNumber + ", Bidding ID: " + this.publicBiddingId
                + ", Date of referencing: " + dateOfContract + ", Buyer ID: "
                + this.buyerId + ", Deadline: " + this.deadline + ", Place: "
                + this.place + " }";
        }
    }
}
