using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeNumbersApp.DAL.Interfaces.Repositories;
using PrimeNumbersApp.DAL.Models;

namespace PrimeNumbersApp.Controllers
{
    [Route("api/prime/")]
    [ApiController]
    public class PrimeNumbersController : ControllerBase
    {
        private readonly IPrimeNumberRepository _primeNumberRepository;
        public PrimeNumbersController(IPrimeNumberRepository primeNumberRepository) 
        { 
            _primeNumberRepository = primeNumberRepository;
        }


        [HttpGet]
        public async Task<IActionResult> IsThisAPrimeNumber(int number)
        {
            if (number == 0)
            {
                return BadRequest($"Number {number} is not prime");
            }

            if (number == 1)
            {
                return Ok($"Number {number} is neither prime nor composite");
            }

            if (await _primeNumberRepository.GetByNumber(number) is not null)
            {
                return Ok($"Number {number} is prime");
            }

            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    return BadRequest($"Number {number} is not prime");
                }
            }

            await _primeNumberRepository.Create(new PrimeNumber
            {
                Number = number,
            });

            return Ok($"Number {number} is prime");
        }
    }
}
