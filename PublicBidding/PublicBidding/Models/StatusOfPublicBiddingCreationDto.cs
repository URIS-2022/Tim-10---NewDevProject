namespace PublicBidding.Models
{
	/// <summary>
	/// Model for creation public bidding status
	/// </summary>
	public class StatusOfPublicBiddingCreationDto
	{
		//izbacili smo id jer nam to ne treba prilikom kreiranja jer ce to baza napraviti sama

		/// <summary>
		/// Public bidding status name
		/// </summary>
		/// [Required(ErrorMessage = "Obavezno je uneti naziv statusa javnog nadmetanja.")]
		public string statusOfPublicBiddingName { get; set; }

	}
}
