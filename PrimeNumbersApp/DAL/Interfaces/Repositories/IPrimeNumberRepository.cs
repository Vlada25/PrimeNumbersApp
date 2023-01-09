using PrimeNumbersApp.DAL.Models;

namespace PrimeNumbersApp.DAL.Interfaces.Repositories
{
    public interface IPrimeNumberRepository
    {
        Task<IEnumerable<PrimeNumber>> GetAll();
        Task<PrimeNumber> GetById(Guid id);
        Task<PrimeNumber> GetByNumber(int number);
        Task Create(PrimeNumber primeNumber);
        Task Delete(PrimeNumber primeNumber);
        Task Update(PrimeNumber primeNumber);
    }
}
