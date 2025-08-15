using Restaurant.Domain.Dtos.MenuCharge;
using Restaurant.Domain.Models.Discount;
using Restaurant.Domain.Models.MenuCharge;
using Restaurant.Domain.Repositories.Discount;
using Restaurant.Domain.Repositories.MenuCharge;

namespace Restaurant.Application.Test.Mock
{
    internal class PersonTypeRepositoryMock : IPersonTypeRepository
    {
        public Task<PersonTypeModel> GetByIdAsync(int id)
        {
            // Simular que los tipos de persona con ID 1-3 existen
            var personTypes = new Dictionary<int, (string name, string description)>
            {
                { 1, ("Estudiante", "Estudiante universitario") },
                { 2, ("Docente", "Personal docente") },
                { 3, ("Administrativo", "Personal administrativo") }
            };

            if (personTypes.ContainsKey(id))
            {
                var personType = personTypes[id];
                return Task.FromResult(new PersonTypeModel(
                    id: id,
                    name: personType.name
                ));
            }
            return Task.FromResult<PersonTypeModel>(null);
        }

        public Task<List<PersonTypeModel>> GetAllAsync()
        {
            var personTypes = new List<PersonTypeModel>
            {
                new PersonTypeModel(1, "Estudiante"),
                new PersonTypeModel(2, "Docente"),
                new PersonTypeModel(3, "Administrativo")
            };
            return Task.FromResult(personTypes);
        }

        // Métodos no implementados para las pruebas
        public Task<PersonTypeModel> InsertAsync(PersonTypeModel model) => throw new NotImplementedException();
        public Task<PersonTypeModel> UpdateAsync(PersonTypeModel model) => throw new NotImplementedException();
        public Task<bool> DeleteAsync(int id) => throw new NotImplementedException();
        public Task<bool> IsExistId(int id) => throw new NotImplementedException();
        public Task<bool> IspersonTypeDuplicate(string name, int id)
        {
            throw new NotImplementedException();
        }

        public Task<PersonNameDto> GetByIdPersonNameAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}