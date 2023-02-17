using AuthorizedPerson.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AuthorizedPerson.Data
{
    public class AuthorizedPersonRepository : IAuthorizedPersonRepository
    {
        private readonly AuthorizedPersonContext context;
        private readonly IMapper mapper;

        public AuthorizedPersonRepository(AuthorizedPersonContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public AuthorizedPersonModel CreateAuthorizedPerson(AuthorizedPersonModel authorizedPerson)
        {
            authorizedPerson.authorizedPersonId = Guid.NewGuid();
            var NewEntity = context.authorizedPeople.Add(authorizedPerson);
            return mapper.Map<AuthorizedPersonModel>(NewEntity.Entity);
        }

        public void DeleteAuthorizedPerson(Guid APID)
        {
            AuthorizedPersonModel authorizedPerson = GetAuthorizedPersonById(APID);
            context.authorizedPeople.Remove(authorizedPerson);
        }

        public List<AuthorizedPersonModel> GetAuthorizedPeople()
        {
            return context.authorizedPeople.ToList();
        }

        public AuthorizedPersonModel GetAuthorizedPersonById(Guid APID)
        {
            return context.authorizedPeople.FirstOrDefault(a => a.authorizedPersonId == APID);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public AuthorizedPersonModel UpdateAuthorizedPerson(AuthorizedPersonModel authorizedPerson)
        {
            throw new NotImplementedException();
        }
    }
}
