using Parcel.Entities;

namespace Parcel.Data
{
    public interface ICadastralMunicipalityRepository
    {
        List<CadastralMunicipality> GetCadastralMunicipalityList();
        CadastralMunicipality GetCadastralMunicipalityById(Guid cadastralMunicipalityId);
        CadastralMunicipality CreateCadastralMunicipality(CadastralMunicipality cadastralMunicipality);

        void UpdateCadastralMunicipality(CadastralMunicipality cadastralMunicipality);

        void DeleteCadastralMunicipality(Guid cadastralMunicipalityId);
        bool SaveChanges();
    }
}
