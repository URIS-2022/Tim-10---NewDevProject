using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DocumentAPI.Entities
{
	public class TypeOfDocumentEntity
	{
		[Key]
		public Guid typeOfDocumentId { get; set; }
		public string typeOfDocumentName { get; set; }

	}
}
