using System.Linq;
using System.Threading.Tasks;
using CS.Data.Entities;

namespace CS.Services.Interfaces
{
    public interface IRepo
    {
        Task<IQueryable<ActProprietate>> GetActeProprietate();
        Task<Parcela> GetParcelaByIndex(int index, bool includeProprietari=false, bool includeActe=false);
        Task<Proprietar> GetProprietarByIndex(int index, bool includeParcele=false);

    }
}
