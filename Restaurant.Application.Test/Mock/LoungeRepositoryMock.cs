using Restaurant.Domain.Models.MenuCharge;
using Restaurant.Domain.Repositories.MenuCharge;

namespace Restaurant.Application.Test.Mock
{
    internal class LoungeRepositoryMock : ILoungeRepository
    {
        public Task<LoungeModel> GetByIdAsync(int id)
        {
            // Simular que los salones con ID 1-5 existen
            if (id >= 1 && id <= 5)
            {
                return Task.FromResult(new LoungeModel(
                    id: id,
                    nameLounge: $"Salón {id}",
                    capacity: 50 + (id * 10),
                    description: $"Descripción del salón {id}",
                    status: LoungeStatus.Disponible,
                    image: $"salon_{id}.jpg"
                ));
            }
            return Task.FromResult<LoungeModel>(null);
        }

        public Task<List<LoungeModel>> GetAllAsync()
        {
            var lounges = new List<LoungeModel>();
            for (int i = 1; i <= 5; i++)
            {
                lounges.Add(new LoungeModel(
                    id: i,
                    nameLounge: $"Salón {i}",
                    capacity: 50 + (i * 10),
                    description: $"Descripción del salón {i}",
                    status: LoungeStatus.Disponible,
                    image: $"salon_{i}.jpg"
                ));
            }
            return Task.FromResult(lounges);
        }

        // Métodos no implementados para las pruebas
        public Task<LoungeModel> InsertAsync(LoungeModel model) => throw new NotImplementedException();
        public Task<LoungeModel> UpdateAsync(LoungeModel model) => throw new NotImplementedException();
        public Task<bool> DeleteAsync(int id) => throw new NotImplementedException();
        public Task<bool> IsExistId(int id) => throw new NotImplementedException();
        public Task<bool> IsNameLoungeDuplicate(string nameLounge, int id)
        {
            throw new NotImplementedException();
        }
    }
}