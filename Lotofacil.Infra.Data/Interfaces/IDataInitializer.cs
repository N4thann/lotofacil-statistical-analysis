using Lotofacil.Infra.Data.Context;

namespace Lotofacil.Infra.Data.Interfaces
{
    public interface IDataInitializer
    {
        void Seed(ApplicationDbContext context);
    }
}
