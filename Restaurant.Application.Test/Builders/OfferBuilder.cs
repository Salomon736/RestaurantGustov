// using Restaurant.Domain.Dtos.Discount;
// using Restaurant.Domain.Models.Discount;
//
// namespace Restaurant.Application.Test.Builders
// {
//     public static class OfferBuilder
//     {
//         public static OfferCreateDto GetValidOfferCreateDto()
//         {
//             return new OfferCreateDto
//             {
//                 title = "Oferta de Prueba",
//                 startDate = DateTime.Today.AddDays(1),
//                 endDate = DateTime.Today.AddDays(30),
//                 amount = 15.50m,
//                 description = "Descripción de oferta de prueba",
//                 increase = false,
//                 weekDays = 127, // Todos los días (1111111 en binario)
//                 idPersonType = 1,
//                 idMealPeriod = 1,
//                 idDiscountType = 1,
//                 RoleOffer = new List<RoleOfferDto>
//                 {
//                     new RoleOfferDto { idRole = 1 }
//                 }
//             };
//         }
//
//         public static OfferCreateDto GetOfferWithInvalidStartDate()
//         {
//             var offer = GetValidOfferCreateDto();
//             offer.startDate = DateTime.Today.AddDays(-1); // Fecha pasada
//             return offer;
//         }
//
//         public static OfferCreateDto GetOfferWithInvalidEndDate()
//         {
//             var offer = GetValidOfferCreateDto();
//             offer.endDate = DateTime.Today.AddDays(-1); // Fecha pasada
//             return offer;
//         }
//
//         public static OfferCreateDto GetOfferWithEndDateBeforeStartDate()
//         {
//             var offer = GetValidOfferCreateDto();
//             offer.startDate = DateTime.Today.AddDays(10);
//             offer.endDate = DateTime.Today.AddDays(5); // Fin antes que inicio
//             return offer;
//         }
//
//         public static OfferCreateDto GetOfferWithInvalidAmount()
//         {
//             var offer = GetValidOfferCreateDto();
//             offer.amount = 0; // Monto inválido
//             return offer;
//         }
//
//         public static OfferCreateDto GetOfferWithEmptyTitle()
//         {
//             var offer = GetValidOfferCreateDto();
//             offer.title = ""; // Título vacío
//             return offer;
//         }
//
//         public static OfferCreateDto GetOfferWithNoWeekDays()
//         {
//             var offer = GetValidOfferCreateDto();
//             offer.weekDays = 0; // Sin días seleccionados
//             return offer;
//         }
//
//         public static OfferCreateDto GetOfferWithEmptyRoles()
//         {
//             var offer = GetValidOfferCreateDto();
//             offer.RoleOffer = new List<RoleOfferDto>(); // Sin roles
//             return offer;
//         }
//
//         public static OfferCreateDto GetOfferWithLongDescription()
//         {
//             var offer = GetValidOfferCreateDto();
//             offer.description = new string('A', 251); // Descripción muy larga
//             return offer;
//         }
//
//         public static OfferModel GetValidOfferModel()
//         {
//             // Usar el constructor que acepta OfferCreateDto
//             var dto = GetValidOfferCreateDto();
//             var model = new OfferModel(dto);
//             
//             // Asignar ID manualmente ya que el constructor DTO usa 0
//             var modelWithId = new OfferModel(
//                 id: 1,
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
//             modelWithId.RoleOffer = new List<RoleOfferModel>
//             {
//                 new RoleOfferModel(1, 1, 1)
//             };
//             
//             return modelWithId;
//         }
//     }
// }
