using Restaurant.Application.Providers;
using Restaurant.Domain.Dtos.Reservation;

namespace Restaurant.Application.Test.Mock
{
    internal class PersonProviderMock : IPersonProvider
    {
        public Task<List<PersonDto>> GetAllPersonsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PersonDto> GetPersonByIdAsync(int personId)
        {
            // Simular que las personas con ID 1-10 existen
            if (personId >= 1 && personId <= 10)
            {
                return Task.FromResult(new PersonDto
                {
                    id = personId,
                    name = $"Persona {personId}",
                    lastNameFather = $"Apellido {personId}",
                    whatsApp = $"123456{personId:D3}",
                    
                });
            }
            return Task.FromResult<PersonDto>(null);
        }
    }
}