using System.ComponentModel.DataAnnotations;

namespace Buyer.Entities
{
    public class PriorityModel
    {
        [Key]
        public Guid priorityId { get; set; }
        public string priorityType { get; set; }


        override
            public string ToString()
        {
            return "Priority: {Priority ID: " + this.priorityId + ", Priority type: " + this.priorityType + " }";
        }
    }
}
