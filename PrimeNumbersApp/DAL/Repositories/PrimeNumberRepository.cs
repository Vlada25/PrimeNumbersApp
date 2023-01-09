using Microsoft.EntityFrameworkCore;
using PrimeNumbersApp.DAL.Interfaces.Repositories;
using PrimeNumbersApp.DAL.Models;

namespace PrimeNumbersApp.DAL.Repositories
{
    public class PrimeNumberRepository : IPrimeNumberRepository
    {
        private readonly AppDbContext _dbContext;
        public PrimeNumberRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(PrimeNumber primeNumber)
        {
            await _dbContext.AddAsync(primeNumber);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(PrimeNumber primeNumber)
        {
            _dbContext.Remove(primeNumber);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PrimeNumber>> GetAll() =>
            await _dbContext.PrimeNumbers.ToListAsync();

        public async Task<PrimeNumber> GetById(Guid id) =>
            await _dbContext.PrimeNumbers.FirstOrDefaultAsync(x => x.Id.Equals(id));

        public async Task<PrimeNumber> GetByNumber(int number) => 
            await _dbContext.PrimeNumbers.FirstOrDefaultAsync(x => x.Number == number);

        public async Task Update(PrimeNumber primeNumber)
        {
            _dbContext.Update(primeNumber);
            await _dbContext.SaveChangesAsync();
        }
    }
}
