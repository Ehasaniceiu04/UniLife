using System.Threading.Tasks;

namespace UniLife.Shared
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }
}