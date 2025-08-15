using Restaurant.Domain.Models.MenuCharge;
using Restaurant.Domain.Repositories.MenuCharge;
using Restaurant.Domain.Dtos.MenuCharge;

namespace Restaurant.Application.Test.Mock
{
    internal class MenuRepositoryMock : IMenuRepository
    {
        private static List<MenuModel> _menus = new List<MenuModel>();
        private static int _nextId = 1;

        public Task<MenuModel> InsertAsync(MenuModel model)
        {
            var newMenu = new MenuModel(
                id: _nextId++,
                menuDate: model.menuDate,
                idDish: model.idDish,
                idMealPeriod: model.idMealPeriod,
                idLounge: model.idLounge,
                createdAt: DateTime.UtcNow
            );
            _menus.Add(newMenu);
            return Task.FromResult(newMenu);
        }

        public Task<MenuModel> UpdateAsync(MenuModel model)
        {
            var existingMenu = _menus.FirstOrDefault(m => m.id == model.id);
            if (existingMenu == null)
            {
                throw new KeyNotFoundException("Plato de menu no encontrado");
            }
            
            // Crear nuevo objeto con los datos actualizados
            var updatedMenu = new MenuModel(
                id: model.id,
                menuDate: model.menuDate,
                idDish: model.idDish,
                idMealPeriod: model.idMealPeriod,
                idLounge: model.idLounge,
                createdAt: existingMenu.createdAt
            );
            
            // Reemplazar en la lista
            var index = _menus.IndexOf(existingMenu);
            _menus[index] = updatedMenu;
            
            return Task.FromResult(updatedMenu);
        }

        public Task<MenuModel?> GetByIdAsync(int id)
        {
            var menu = _menus.FirstOrDefault(m => m.id == id);
            return Task.FromResult(menu);
        }

        public Task<List<MenuModel>> GetAllAsync()
        {
            return Task.FromResult(_menus.ToList());
        }

        public Task<bool> IsExistId(int id)
        {
            var exists = _menus.Any(m => m.id == id);
            return Task.FromResult(exists);
        }

        public Task<int> InsertMultipleMenuItems(int dishId, List<MenuDatePeriodDto> selections, int loungeId)
        {
            foreach (var selection in selections)
            {
                var menu = new MenuModel(
                    id: _nextId++,
                    menuDate: selection.Date,
                    idDish: dishId,
                    idMealPeriod: selection.MealPeriodId,
                    idLounge: loungeId,
                    createdAt: DateTime.UtcNow
                );
                _menus.Add(menu);
            }
            return Task.FromResult(selections.Count);
        }

        // Simular datos de entidades relacionadas
        public Task<DishModel> GetDishByIdAsync(int dishId)
        {
            // Simular que los platos con ID 1-10 existen
            if (dishId >= 1 && dishId <= 10)
            {
                var dish = new DishModel(
                    id: dishId,
                    name: $"Plato {dishId}",
                    description: $"Descripción del plato {dishId}",
                    image: $"imagen_plato_{dishId}.jpg",
                    idDishType: 1,
                    idMealPeriod: 1,
                    createdAt: DateTime.UtcNow
                );
                return Task.FromResult(dish);
            }
            return Task.FromResult<DishModel>(null);
        }

        public Task<MealPeriodsModel> GetMealPeriodByIdAsync(int mealPeriodId)
        {
            // Simular que los periodos con ID 1-3 existen
            var periods = new Dictionary<int, (string type, string start, string end, string color)>
            {
                { 1, ("Desayuno", "06:00", "09:00", "#FFE4B5") },
                { 2, ("Almuerzo", "12:00", "15:00", "#98FB98") },
                { 3, ("Cena", "18:00", "21:00", "#DDA0DD") }
            };

            if (periods.ContainsKey(mealPeriodId))
            {
                var period = periods[mealPeriodId];
                var mealPeriod = new MealPeriodsModel(
                    id: mealPeriodId,
                    mealType: period.type,
                    startTime: period.start,
                    endTime: period.end,
                    color: period.color,
                    cancellationTimeLimit: 30
                );
                return Task.FromResult(mealPeriod);
            }
            return Task.FromResult<MealPeriodsModel>(null);
        }

        public Task<LoungeModel> GetLoungeByIdAsync(int loungeId)
        {
            // Simular que los salones con ID 1-5 existen
            if (loungeId >= 1 && loungeId <= 5)
            {
                var lounge = new LoungeModel(
                    id: loungeId,
                    nameLounge: $"Salón {loungeId}",
                    capacity: 50 + (loungeId * 10),
                    description: $"Descripción del salón {loungeId}",
                    status: LoungeStatus.Disponible,
                    image: $"salon_{loungeId}.jpg"
                );
                return Task.FromResult(lounge);
            }
            return Task.FromResult<LoungeModel>(null);
        }

        public Task<List<MenuModel>> GetExistingMenuItems(int dishId, List<MenuDatePeriodDto> selections)
        {
            var existingItems = new List<MenuModel>();
            
            foreach (var selection in selections)
            {
                var existing = _menus.Where(m => 
                    m.idDish == dishId && 
                    m.menuDate.Date == selection.Date.Date && 
                    m.idMealPeriod == selection.MealPeriodId).ToList();
                
                existingItems.AddRange(existing);
            }
            
            return Task.FromResult(existingItems);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var menu = _menus.FirstOrDefault(m => m.id == id);
            if (menu != null)
            {
                _menus.Remove(menu);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        // Método para limpiar datos entre pruebas
        public static void ClearData()
        {
            _menus.Clear();
            _nextId = 1;
        }
    }
}
