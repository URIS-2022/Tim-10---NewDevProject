using Buyer.Entities;
using System.Data;

namespace Buyer.Data
{
    public class PriorityMockRepository : IPriorityRepository
    {
        public static List<PriorityModel> priorities { get; set; } = new List<PriorityModel>();

        public PriorityMockRepository() {
            FillData();
        }
        public void FillData()
        {
            priorities.AddRange(new List<PriorityModel>
            {
                new PriorityModel
                {
                priorityId = Guid.Parse("1BB9CB0A-A2AD-4FF3-BBAA-BA312E968A9B"),
                priorityType = "Test priority number 1"
                },
                new PriorityModel
                {
                priorityId = Guid.Parse("12C7B642-416E-4358-90CA-9DDB67336F63"),
                priorityType = "Test priority type number 2"
                }

            });
        }

        public PriorityModel CreatePriority(PriorityModel priority)
        {
            priority.priorityId = Guid.NewGuid();
            priorities.Add(priority);
            PriorityModel p = GetPriorityById(priority.priorityId);

            return new PriorityModel
            {
                priorityId = p.priorityId,
            };
        }

        public void DeletePriority(Guid pid)
        {
            priorities.Remove(priorities.FirstOrDefault(p => p.priorityId == pid));
        }

        public List<PriorityModel> GetPriority()
        {
            return (from p in priorities select p).ToList();
        }

        public PriorityModel GetPriorityById(Guid pid)
        {
            return priorities.FirstOrDefault(p => p.priorityId == pid);
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public PriorityModel UpdatePriority(PriorityModel priority)
        {
            PriorityModel p = GetPriorityById(priority.priorityId);

            p.priorityId = priority.priorityId;
            p.priorityType = priority.priorityType;

            return new PriorityModel
            {
                priorityId = p.priorityId
            };
        }
    }
}
