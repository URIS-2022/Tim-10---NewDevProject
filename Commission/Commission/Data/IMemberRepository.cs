using Commission.Entities;
using Commission.Models;

namespace Commission.Data
{
    public interface IMemberRepository
    {
        List<MemberEntity> GetAllMembers(Guid? commissionId = null);
        MemberEntity GetMemberById(Guid memberId);
        MemberDto CreateMember(MemberEntity member);
        void UpdateMember(MemberEntity member);
        void DeleteMember(Guid memberId);
        bool SaveChanges();
    }
}
