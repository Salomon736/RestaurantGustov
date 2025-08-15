using Restaurant.Domain.Dtos.MenuCharge;
using Restaurant.Domain.Models.MenuCharge;
using Restaurant.Domain.Repositories.MenuCharge;

namespace Restaurant.Application.Test.Mock
{
    internal class MealPeriodsRepositoryMock : IMealPeriodsRepository
    {
        public Task<List<MealPeriodsModel>> GetAllAsync()
        {
            var mealPeriods = new List<MealPeriodsModel>
            {
                new MealPeriodsModel(1, "Desayuno", "06:00", "09:00", "#FFE4B5", 30),
                new MealPeriodsModel(2, "Almuerzo", "12:00", "15:00", "#98FB98", 60),
                new MealPeriodsModel(3, "Cena", "18:00", "21:00", "#DDA0DD", 45)
            };
            return Task.FromResult(mealPeriods);
        }

        public Task<MealPeriodsModel> GetByIdAsync(int id)
        {
            var mealPeriods = new Dictionary<int, (string type, string start, string end, string color, int limit)>
            {
                { 1, ("Desayuno", "06:00", "09:00", "#FFE4B5", 30) },
                { 2, ("Almuerzo", "12:00", "15:00", "#98FB98", 60) },
                { 3, ("Cena", "18:00", "21:00", "#DDA0DD", 45) }
            };

            if (mealPeriods.ContainsKey(id))
            {
                var period = mealPeriods[id];
                return Task.FromResult(new MealPeriodsModel(
                    id: id,
                    mealType: period.type,
                    startTime: period.start,
                    endTime: period.end,
                    color: period.color,
                    cancellationTimeLimit: period.limit
                ));
            }
            return Task.FromResult<MealPeriodsModel>(null);
        }

        // Métodos no implementados para las pruebas
        public Task<MealPeriodsModel> InsertAsync(MealPeriodsModel model) => throw new NotImplementedException();
        public Task<MealPeriodsModel> UpdateAsync(MealPeriodsModel model) => throw new NotImplementedException();
        public Task<bool> DeleteAsync(int id) => throw new NotImplementedException();
        public Task<bool> IsExistId(int id) => throw new NotImplementedException();
        public Task<bool> IsmealTypeDuplicate(string mealType, int id)
        {
            throw new NotImplementedException();
        }

        public Task<MealTypeDto> GetByIdMealTypeAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
