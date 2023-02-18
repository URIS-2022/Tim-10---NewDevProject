using AutoMapper;
using Commission.Entities;
using Commission.Models;

namespace Commission.Data
{
    public class MemberRepository : IMemberRepository
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public MemberRepository(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public MemberDto CreateMember(MemberEntity member)
        {
          
            var createdEntity = context.Add(member);
            return mapper.Map<MemberDto>(createdEntity.Entity);
        }

        public void DeleteMember(Guid memberId)
        {
            var member = GetMemberById(memberId);
            context.Remove(member);
        }

        public List<MemberEntity> GetAllMembers(Guid? commissionId = null)
        {
            return context.Member
                 .Where(r => (commissionId == null || r.commissionId == commissionId))
                 .ToList();
        }

        public MemberEntity GetMemberById(Guid memberId) => context.Member.FirstOrDefault(r => r.memberId == memberId);

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateMember(MemberEntity member)
        {
            //does not need to be implemented
        }
    }
}
