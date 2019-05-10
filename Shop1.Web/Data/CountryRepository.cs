
namespace Shop1.Web.Data
{
    using Entities;

    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(DataContext context) : base(context)
        {
        }
    }    
}
