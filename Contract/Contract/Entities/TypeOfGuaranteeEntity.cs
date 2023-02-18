using System.ComponentModel.DataAnnotations;

namespace Contract.Entities
{
    public class TypeOfGuaranteeEntity
    {
        [Key]
        /// <summary>
        /// ID of the guarantee type
        /// </summary>
        public Guid typeId { get; set; }
        /// <summary>
        /// Name of the guarantee type
        /// </summary>
        public string? type { get; set; }
    }
}
