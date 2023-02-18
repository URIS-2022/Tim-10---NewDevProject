using Microsoft.EntityFrameworkCore;

namespace Parcel.Entities
{
    public class ParcelContext : DbContext
    {
        public readonly IConfiguration configuration;
        public ParcelContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }
        public DbSet<Parcel> Parcel { get; set; }
        public DbSet<CadastralMunicipality> CadastralMunicipality { get; set; }
        public DbSet<Culture> Culture { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<Workability> Workability { get; set; }
        public DbSet<ProtectedZone> ProtectedZone { get; set; }
        public DbSet<FormOfProperty> FormOfProperty { get; set; }
        public DbSet<Drainage> Drainage { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ParcelDB;Integrated Security=True;Connect Timeout=30");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CadastralMunicipality>()
                .HasData(
                    new CadastralMunicipality
                    {
                        cadastralMunicipalityId = Guid.Parse("22D1F47A-75B6-4F78-ADD1-ADBB38E009EF"),
                        cadastralMunicipalityName = "Čantavir"
                    },
                    new CadastralMunicipality
                    {
                        cadastralMunicipalityId = Guid.Parse("6E079249-1322-4EBE-B336-D1D80F801FBC"),
                        cadastralMunicipalityName = "Bački Vinogradi"
                    },
                    new CadastralMunicipality
                    {
                        cadastralMunicipalityId = Guid.Parse("93921298-4AA8-4E64-AD44-D8DFF1ED0D67"),
                        cadastralMunicipalityName = "Bikovo"
                    },
                    new CadastralMunicipality
                    {
                        cadastralMunicipalityId = Guid.Parse("7957DB6A-CE96-4364-BCE3-D712FAC17292"),
                        cadastralMunicipalityName = "Đuđin"
                    },
                    new CadastralMunicipality
                    {
                        cadastralMunicipalityId = Guid.Parse("60E4D41F-C6CE-48E5-918D-A834EA17EFEE"),
                        cadastralMunicipalityName = "Žednik"
                    },
                    new CadastralMunicipality
                    {
                        cadastralMunicipalityId = Guid.Parse("8421A65C-1929-4E1B-B005-C19CE6B8084E"),
                        cadastralMunicipalityName = "Tavankut"
                    },
                    new CadastralMunicipality
                    {
                        cadastralMunicipalityId = Guid.Parse("56845D4B-4F9D-40CF-A58C-338949E3719F"),
                        cadastralMunicipalityName = "Bajmok"
                    },
                    new CadastralMunicipality
                    {
                        cadastralMunicipalityId = Guid.Parse("2DA53652-4366-45E7-9F4E-75B1768456F9"),
                        cadastralMunicipalityName = "Donji Grad"
                    },
                    new CadastralMunicipality
                    {
                        cadastralMunicipalityId = Guid.Parse("04B12CB0-5D05-4432-87DA-81CA464011A2"),
                        cadastralMunicipalityName = "Stari Grad"
                    },
                    new CadastralMunicipality
                    {
                        cadastralMunicipalityId = Guid.Parse("44607CB5-C1CA-4CEC-AB46-F0EF67F8656B"),
                        cadastralMunicipalityName = "Novi Grad"
                    },
                    new CadastralMunicipality
                    {
                        cadastralMunicipalityId = Guid.Parse("F427BDF0-4F91-4411-9E9F-085FC44BBDF7"),
                        cadastralMunicipalityName = "Palić"
                    }
            );

            modelBuilder.Entity<Culture>()
                .HasData(
                    new Culture
                    {
                        cultureId = Guid.Parse("4960AD3A-CD69-494E-864D-D4E410CE6094"),
                        cultureName = "Njive",
                        cultureDescription = "1"
                    },
                    new Culture
                    {
                        cultureId = Guid.Parse("581B4400-BCDE-4FA5-AC85-72A6077AF503"),
                        cultureName = "Vrtovi",
                        cultureDescription = "2"
                    },
                    new Culture
                    {
                        cultureId = Guid.Parse("5475E615-4137-4CBC-8DAF-4538EAE0F37E"),
                        cultureName = "Voćnjaci",
                        cultureDescription = "3"
                    },
                    new Culture
                    {
                        cultureId = Guid.Parse("2D9DCD0E-1705-4ACA-B847-724A2C4C4877"),
                        cultureName = "Vinogradi",
                        cultureDescription = "4"
                    },
                    new Culture
                    {
                        cultureId = Guid.Parse("FFCECFAA-B835-41C2-A568-57A564E7A03B"),
                        cultureName = "Livade",
                        cultureDescription = "5"
                    },
                    new Culture
                    {
                        cultureId = Guid.Parse("F0D417FC-8E78-4D0F-A018-D6DF8C270737"),
                        cultureName = "Pašnjaci",
                        cultureDescription = "6"
                    },
                    new Culture
                    {
                        cultureId = Guid.Parse("29B64040-AD2A-4859-A540-1D59A475652C"),
                        cultureName = "Šume",
                        cultureDescription ="7"
                    },
                    new Culture
                    {
                        cultureId = Guid.Parse("5FBA88B4-C642-4E9E-A5C1-074F91F417DF"),
                        cultureName = "Trstici-močvare",
                        cultureDescription = "8"
                    }
            );

            modelBuilder.Entity<Class>()
                .HasData(
                    new Class
                    {
                        classId = Guid.Parse("9AF0AB6F-C437-4959-9EA3-0C721214147B"),
                        className = "I"
                    },
                    new Class
                    {
                        classId = Guid.Parse("7AF0E459-501B-47CD-9E4E-D329407B1C5B"),
                        className = "II"
                    },
                    new Class
                    {
                        classId = Guid.Parse("A2D03F10-884E-49C8-A30E-E606A22DC2BA"),
                        className = "III"
                    },
                    new Class
                    {
                        classId = Guid.Parse("FE3E5D4B-18E5-41D7-B35C-D00EC8171F5F"),
                        className = "IV"
                    },
                    new Class
                    {
                        classId = Guid.Parse("C8D44720-A933-4A4E-9E73-ACC047535A5E"),
                        className = "V"
                    },
                    new Class
                    {
                        classId = Guid.Parse("ED5E7420-3728-4829-8AC0-67C7CCDB506C"),
                        className = "VI"
                    },
                    new Class
                    {
                        classId = Guid.Parse("C5B969B4-26CA-4A34-AC80-B26C2A2A5F17"),
                        className = "VII"
                    },
                    new Class
                    {
                        classId = Guid.Parse("B472B7C5-2648-40CA-A15F-37BE449BC80A"),
                        className = "VIII"
                    }
            );

            modelBuilder.Entity<Workability>()
                .HasData(
                    new Workability
                    {
                        workabilityId = Guid.Parse("69679E47-4D0B-4277-96F5-C1583A97ABE8"),
                        workabilityName = "Obradivo"
                    },
                    new Workability
                    {
                        workabilityId = Guid.Parse("004B067A-BCBB-47D2-8869-550AEB147138"),
                        workabilityName = "Ostalo"
                    }
            );

            modelBuilder.Entity<ProtectedZone>()
                .HasData(
                    new ProtectedZone
                    {
                        protectedZoneId = Guid.Parse("5B1F0CBE-A20C-4747-80DB-0B13AF254388"),
                        protectedZoneName = "1"
                    },
                    new ProtectedZone
                    {
                        protectedZoneId = Guid.Parse("12D2A98D-7890-44FA-BC30-EE3D77461A7F"),
                        protectedZoneName = "2"
                    },
                    new ProtectedZone
                    {
                        protectedZoneId = Guid.Parse("A029CB37-3CB3-4B24-9DB2-EF427401F1A5"),
                        protectedZoneName = "3"
                    },
                    new ProtectedZone
                    {
                        protectedZoneId = Guid.Parse("3EE483C7-B878-4A0E-A742-8C87A287E5CD"),
                        protectedZoneName = "4"
                    }
            );

            modelBuilder.Entity<FormOfProperty>()
                .HasData(
                    new FormOfProperty
                    {
                        formOfPropertyId = Guid.Parse("D54F5CAA-A148-46AB-A6DA-F83C9440CBAB"),
                        formOfPropertyName = "Privatna svojina",
                        formOfPropertyDescription = "1"
                    },
                    new FormOfProperty
                    {
                        formOfPropertyId = Guid.Parse("109932FB-A5B9-4FAB-8BB2-4D644DF61245"),
                        formOfPropertyName = "Državna svojina RS",
                        formOfPropertyDescription = "2"
                    },
                    new FormOfProperty
                    {
                        formOfPropertyId = Guid.Parse("92351865-973D-4EC2-BE9C-CDD81B849E99"),
                        formOfPropertyName = "Državna svojina",
                        formOfPropertyDescription ="3"
                    },
                    new FormOfProperty
                    {
                        formOfPropertyId = Guid.Parse("2FA236F3-60D1-408D-AC04-BEDBD4919BBB"),
                        formOfPropertyName = "Društvena svojina",
                        formOfPropertyDescription = "4"
                    },
                    new FormOfProperty
                    {
                        formOfPropertyId = Guid.Parse("9590EDB7-593F-42AE-82B8-CF29298FE2D4"),
                        formOfPropertyName = "Zadružna svojina",
                        formOfPropertyDescription = "5"
                    },
                    new FormOfProperty
                    {
                        formOfPropertyId = Guid.Parse("35636422-B81C-43C6-9A86-9486CF138C30"),
                        formOfPropertyName = "Mešovita svojina",
                        formOfPropertyDescription = "6"
                    },
                    new FormOfProperty
                    {
                        formOfPropertyId = Guid.Parse("9B7B6C58-ACE6-4FE8-9176-46323C00D005"),
                        formOfPropertyName = "Drugi oblici",
                        formOfPropertyDescription ="7"
                    }
            );

            modelBuilder.Entity<Drainage>()
                .HasData(
                    new Drainage
                    {
                        drainageId = Guid.Parse("D98745A1-7C94-417C-B31A-6EFC83ACAEFC"),
                        drainageName = "Površinsko odvodnjavanje"
                    },
                    new Drainage
                    {
                        drainageId = Guid.Parse("E59F5CCA-F6AB-44F4-9C04-698D0B310BC5"),
                        drainageName = "Podzemno odvodnjavanje"
                    }
            );

            modelBuilder.Entity<Parcel>()
                .HasData(
                    new Parcel
                    {
                        parcelId = Guid.Parse("CF69A921-7A2F-4B8C-BE71-92EE821B19EE"),
                        userOfParcelId = Guid.Parse("9F122326-746A-426A-84D1-09501AE77664"),
                        surface = 100,
                        parcelNumber = "PC-2601",
                        cadastralMunicipalityId = Guid.Parse("F427BDF0-4F91-4411-9E9F-085FC44BBDF7"),
                        immovablePropertyListNumber = "LN101",
                        cultureId = Guid.Parse("FFCECFAA-B835-41C2-A568-57A564E7A03B"),
                        classId = Guid.Parse("7AF0E459-501B-47CD-9E4E-D329407B1C5B"),
                        workabilityId = Guid.Parse("69679E47-4D0B-4277-96F5-C1583A97ABE8"),
                        protectedZoneId = Guid.Parse("5B1F0CBE-A20C-4747-80DB-0B13AF254388"),
                        formOfPropertyId = Guid.Parse("92351865-973D-4EC2-BE9C-CDD81B849E99"),
                        drainageId = Guid.Parse("D98745A1-7C94-417C-B31A-6EFC83ACAEFC"),
                        cultureRealCondition = "3",
                        classRealCondition = "2",
                        workabilityRealCondition = "5",
                        protectedZoneRealCondition = "4",
                        drainageRealCondition = "1"
                    }
            );

            
        }

    }
}
