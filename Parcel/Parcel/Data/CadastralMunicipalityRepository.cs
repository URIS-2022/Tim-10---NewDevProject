using AutoMapper;
using Parcel.Entities;


namespace Parcel.Data
{
    public class CadastralMunicipalityRepository : ICadastralMunicipalityRepository
    {
        private readonly ParcelContext ?context;
        private readonly IMapper mapper;
    
        public CadastralMunicipalityRepository(ParcelContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public CadastralMunicipality CreateCadastralMunicipality(CadastralMunicipality cadastralMunicipality)
        {
            var createdEntity = context.Add(cadastralMunicipality);
            context.SaveChanges();
            return mapper.Map<CadastralMunicipality>(createdEntity.Entity);
        }

        public void DeleteCadastralMunicipality(Guid cadastralMunicipalityId)
        {
            var cadastralMunicipality = GetCadastralMunicipalityById(cadastralMunicipalityId);
            context.Remove(cadastralMunicipality);
            context.SaveChanges();
        }
        public CadastralMunicipality GetCadastralMunicipalityById(Guid cadastralMunicipalityId)
        {
            return context.CadastralMunicipality.FirstOrDefault(c => c.cadastralMunicipalityId == cadastralMunicipalityId);
        }

        public List<CadastralMunicipality> GetCadastralMunicipalityList()
        {
           return context.CadastralMunicipality.ToList();
        }
        public void UpdateCadastralMunicipality(CadastralMunicipality cadastralMunicipality)
        {
            //nije potrebno posebno implementirati update
        }
    }
}
