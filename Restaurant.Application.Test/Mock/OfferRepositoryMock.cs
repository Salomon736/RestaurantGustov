// using Restaurant.Domain.Dtos.Discount;
// using Restaurant.Domain.Models.Discount;
// using Restaurant.Domain.Repositories.Discount;
//
// namespace Restaurant.Application.Test.Mock
// {
//     internal class OfferRepositoryMock : IOfferRepository
//     {
//         private static List<OfferModel> _offers = new List<OfferModel>();
//         private static int _nextId = 1;
//
//         public Task<List<OfferModel>> InsertAsync(OfferCreateDto dto)
//         {
//             var offers = new List<OfferModel>();
//             
//             foreach (var roleOffer in dto.RoleOffer)
//             {
//                 var offer = new OfferModel(
//                     id: _nextId++,
//                     title: dto.title,
//                     startDate: dto.startDate,
//                     endDate: dto.endDate,
//                     amount: dto.amount,
//                     description: dto.description,
//                     increase: dto.increase,
//                     weekDays: dto.weekDays,
//                     idPersonType: dto.idPersonType,
//                     idMealPeriod: dto.idMealPeriod,
//                     idDiscountType: dto.idDiscountType
//                 );
//                 
//                 // Asignar RoleOffer después de la construcción
//                 offer.RoleOffer = new List<RoleOfferModel>
//                 {
//                     new RoleOfferModel(_nextId, offer.id, roleOffer.idRole)
//                 };
//                 
//                 _offers.Add(offer);
//                 offers.Add(offer);
//             }
//             
//             return Task.FromResult(offers);
//         }
//
//         public Task<OfferModel> InsertAsync(OfferModel model)
//         {
//             var newOffer = new OfferModel(
//                 id: _nextId++,
//                 title: model.title,
//                 startDate: model.startDate,
//                 endDate: model.endDate,
//                 amount: model.amount,
//                 description: model.description,
//                 increase: model.increase,
//                 weekDays: model.weekDays,
//                 idPersonType: model.idPersonType,
//                 idMealPeriod: model.idMealPeriod,
//                 idDiscountType: model.idDiscountType
//             );
//             
//             // Asignar RoleOffer después de la construcción
//             newOffer.RoleOffer = model.RoleOffer ?? new List<RoleOfferModel>();
//             
//             _offers.Add(newOffer);
//             return Task.FromResult(newOffer);
//         }
//
//         public Task<OfferModel> UpdateAsync(int id, OfferCreateDto dto)
//         {
//             var existingOffer = _offers.FirstOrDefault(o => o.id == id);
//             if (existingOffer == null)
//             {
//                 throw new KeyNotFoundException("La oferta no fue encontrada.");
//             }
//
//             var updatedOffer = new OfferModel(
//                 id: id,
//                 title: dto.title,
//                 startDate: dto.startDate,
//                 endDate: dto.endDate,
//                 amount: dto.amount,
//                 description: dto.description,
//                 increase: dto.increase,
//                 weekDays: dto.weekDays,
//                 idPersonType: dto.idPersonType,
//                 idMealPeriod: dto.idMealPeriod,
//                 idDiscountType: dto.idDiscountType
//             );
//
//             // Asignar RoleOffer después de la construcción
//             updatedOffer.RoleOffer = dto.RoleOffer.Select(ro => new RoleOfferModel(1, id, ro.idRole)).ToList();
//
//             var index = _offers.IndexOf(existingOffer);
//             _offers[index] = updatedOffer;
//             
//             return Task.FromResult(updatedOffer);
//         }
//
//         public Task<bool> IsTitleDuplicate(string title, int id)
//         {
//             throw new NotImplementedException();
//         }
//
//         public Task<List<OfferResponseDto>> GetAllDtoAsync()
//         {
//             throw new NotImplementedException();
//         }
//
//         public Task<OfferModel> UpdateAsync(OfferModel model)
//         {
//             var existingOffer = _offers.FirstOrDefault(o => o.id == model.id);
//             if (existingOffer == null)
//             {
//                 throw new KeyNotFoundException("La oferta no fue encontrada.");
//             }
//
//             var updatedOffer = new OfferModel(
//                 id: model.id,
//                 title: model.title,
//                 startDate: model.startDate,
//                 endDate: model.endDate,
//                 amount: model.amount,
//                 description: model.description,
//                 increase: model.increase,
//                 weekDays: model.weekDays,
//                 idPersonType: model.idPersonType,
//                 idMealPeriod: model.idMealPeriod,
//                 idDiscountType: model.idDiscountType
//             );
//             
//             // Asignar RoleOffer después de la construcción
//             updatedOffer.RoleOffer = model.RoleOffer ?? new List<RoleOfferModel>();
//
//             var index = _offers.IndexOf(existingOffer);
//             _offers[index] = updatedOffer;
//             
//             return Task.FromResult(updatedOffer);
//         }
//
//         public Task<OfferModel?> GetByIdAsync(int id)
//         {
//             var offer = _offers.FirstOrDefault(o => o.id == id);
//             return Task.FromResult(offer);
//         }
//
//         public Task<List<OfferModel>> GetAllAsync()
//         {
//             return Task.FromResult(_offers.ToList());
//         }
//
//         public Task<bool> IsExistId(int id)
//         {
//             throw new NotImplementedException();
//         }
//
//         public Task<bool> DeleteAsync(int id)
//         {
//             var offer = _offers.FirstOrDefault(o => o.id == id);
//             if (offer != null)
//             {
//                 _offers.Remove(offer);
//                 return Task.FromResult(true);
//             }
//             return Task.FromResult(false);
//         }
//
//         // Método para limpiar datos entre pruebas
//         public static void ClearData()
//         {
//             _offers.Clear();
//             _nextId = 1;
//         }
//     }
// }
