using complaint.Entities;
namespace complaint.Data
{
    public interface IActionRepository
    {
        List<Entities.Action> GetActionList();

        Entities.Action GetActionById(Guid actionId);

        Entities.Action CreateAction(Entities.Action action);

        void UpdateAction(Entities.Action action);

        void DeleteAction(Guid actionId);
        bool SaveChanges();



    }
}
