namespace complaint.Models
{
    public class ComplaintDto
    {
        public Guid complaintId { get; set; }
        public Guid complaintTypeId { get; set; }
        public DateTime complaintDate { get; set; }
        public Guid complaintSubmitter { get; set; }
        public string cause { get; set; }
        public string reason { get; set; }
        public DateTime rescriptDate { get; set; }
        public string rescriptNumber { get; set; }
        public Guid complaintStatusId { get; set; }
        public string decisionNumber { get; set; }
        public string complaintNumber { get; set; }
        public Guid actionId { get; set; }
      


    }
}
