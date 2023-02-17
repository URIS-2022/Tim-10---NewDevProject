namespace PublicBidding.Models
{
	/// <summary>
	/// Model for creation public bidding type
	/// </summary>

	public class TypeOfPublicBiddingCreationDto
	{
		//izbacili smo id jer nam to ne treba prilikom kreiranja jer ce to baza napraviti sama

		/// <summary>
		///Public bidding type name
		/// </summary>
		/// [Required(ErrorMessage = "You must enter the name of the public bidding type.")]
		public string typePublicBiddingName { get; set; }
	}
}
