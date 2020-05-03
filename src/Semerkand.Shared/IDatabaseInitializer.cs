using System.Threading.Tasks;

namespace Semerkand.Shared
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }
}