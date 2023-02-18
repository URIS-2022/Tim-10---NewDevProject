using System.ComponentModel.DataAnnotations;
namespace complaint.Entities
{
    public class Action
    {
        [Key]
        public Guid actionId { get; set; } = Guid.NewGuid();

        [Required]
        public string? actionName { get; set; }

        public override string ToString()
        {
            return "Action: { ActionId: " + this.actionId + ", ActionName: " + this.actionName + " }";
        }
    }
}
