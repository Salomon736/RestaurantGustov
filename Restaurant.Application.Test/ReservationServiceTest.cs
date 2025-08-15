// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Restaurant.Application.Services.Reservation;
// using Restaurant.Application.Test.Common;
// using Restaurant.Application.Test.Builders;
// using Restaurant.Application.Test.Mock;
// using System.Net;
//
// namespace Restaurant.Application.Test
// {
//     [TestClass]
//     public class ReservationServiceTest : TestBase
//     {
//         [TestInitialize]
//         public void Initialize()
//         {
//             ReservationRepositoryMock.ClearData();
//         }
//
//         [TestMethod]
//         [DataRow(999, "persona")]
//         [DataRow(1000, "persona")]
//         [DataRow(-1, "persona")]
//         public async Task CreateReservation_InvalidPerson_ReturnsFailure(int personId, string expectedError)
//         {
//             // Arrange
//             var reservationService = Resolve<ReservationService>();
//             var invalidReservation = ReservationBuilder.GetValidCreateReservationDto();
//             invalidReservation.idPerson = personId;
//
//             // Act
//             var result = await reservationService.CreateReservation(invalidReservation);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains(expectedError)));
//         }
//
//         [TestMethod]
//         [DataRow(999, "salón")]
//         [DataRow(1000, "salón")]
//         [DataRow(-1, "salón")]
//         public async Task CreateReservation_InvalidLounge_ReturnsFailure(int loungeId, string expectedError)
//         {
//             // Arrange
//             var reservationService = Resolve<ReservationService>();
//             var invalidReservation = ReservationBuilder.GetValidCreateReservationDto();
//             invalidReservation.idLounge = loungeId;
//
//             // Act
//             var result = await reservationService.CreateReservation(invalidReservation);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains(expectedError)));
//         }
//
//         [TestMethod]
//         [DataRow(-1, "futuro")]
//         [DataRow(-5, "futuro")]
//         [DataRow(-10, "futuro")]
//         public async Task CreateReservation_PastDate_ReturnsFailure(int daysOffset, string expectedError)
//         {
//             // Arrange
//             var reservationService = Resolve<ReservationService>();
//             var invalidReservation = ReservationBuilder.GetValidCreateReservationDto();
//             invalidReservation.items[0].menuInfo.Date = DateTime.Today.AddDays(daysOffset);
//
//             // Act
//             var result = await reservationService.CreateReservation(invalidReservation);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains(expectedError)));
//         }
//
//         [TestMethod]
//         [DataRow(999, "período")]
//         [DataRow(1000, "período")]
//         [DataRow(-1, "período")]
//         public async Task CreateReservation_InvalidMealPeriod_ReturnsFailure(int mealPeriodId, string expectedError)
//         {
//             // Arrange
//             var reservationService = Resolve<ReservationService>();
//             var invalidReservation = ReservationBuilder.GetValidCreateReservationDto();
//             invalidReservation.items[0].menuInfo.MealPeriodId = mealPeriodId;
//
//             // Act
//             var result = await reservationService.CreateReservation(invalidReservation);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains(expectedError)));
//         }
//
//         [TestMethod]
//         [DataRow(999, "tipo de persona")]
//         [DataRow(1000, "tipo de persona")]
//         [DataRow(-1, "tipo de persona")]
//         public async Task CreateReservation_InvalidPersonType_ReturnsFailure(int personTypeId, string expectedError)
//         {
//             // Arrange
//             var reservationService = Resolve<ReservationService>();
//             var invalidReservation = ReservationBuilder.GetValidCreateReservationDto();
//             invalidReservation.items[0].personTypeInfo[0].personTypeId = personTypeId;
//
//             // Act
//             var result = await reservationService.CreateReservation(invalidReservation);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains(expectedError)));
//         }
//
//         [TestMethod]
//         [DataRow(0, "mayor que cero")]
//         [DataRow(-1, "mayor que cero")]
//         [DataRow(-5, "mayor que cero")]
//         public async Task CreateReservation_InvalidQuantity_ReturnsFailure(int quantity, string expectedError)
//         {
//             // Arrange
//             var reservationService = Resolve<ReservationService>();
//             var invalidReservation = ReservationBuilder.GetValidCreateReservationDto();
//             invalidReservation.items[0].personTypeInfo[0].quantity = quantity;
//
//             // Act
//             var result = await reservationService.CreateReservation(invalidReservation);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains(expectedError)));
//         }
//
//         [TestMethod]
//         [DataRow(0, "mayor que cero")]
//         [DataRow(-1, "mayor que cero")]
//         [DataRow(-25.50, "mayor que cero")]
//         public async Task CreateReservation_InvalidPrice_ReturnsFailure(double price, string expectedError)
//         {
//             // Arrange
//             var reservationService = Resolve<ReservationService>();
//             var invalidReservation = ReservationBuilder.GetValidCreateReservationDto();
//             invalidReservation.items[0].personTypeInfo[0].price = price;
//
//             // Act
//             var result = await reservationService.CreateReservation(invalidReservation);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains(expectedError)));
//         }
//
//         [TestMethod]
//         public async Task CreateReservation_IncorrectTotalPrice_ReturnsFailure()
//         {
//             // Arrange
//             var reservationService = Resolve<ReservationService>();
//             var invalidReservation = ReservationBuilder.GetReservationWithIncorrectTotalPrice();
//
//             // Act
//             var result = await reservationService.CreateReservation(invalidReservation);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains("precio total")));
//         }
//
//         [TestMethod]
//         public async Task CreateReservation_IncorrectSubtotal_ReturnsFailure()
//         {
//             // Arrange
//             var reservationService = Resolve<ReservationService>();
//             var invalidReservation = ReservationBuilder.GetReservationWithIncorrectSubtotal();
//
//             // Act
//             var result = await reservationService.CreateReservation(invalidReservation);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains("subtotal")));
//         }
//
//         [TestMethod]
//         public async Task CreateReservation_IncorrectTotalAmount_ReturnsFailure()
//         {
//             // Arrange
//             var reservationService = Resolve<ReservationService>();
//             var invalidReservation = ReservationBuilder.GetReservationWithIncorrectTotalAmount();
//
//             // Act
//             var result = await reservationService.CreateReservation(invalidReservation);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains("monto total")));
//         }
//
//         [TestMethod]
//         public async Task CreateReservation_EmptyItems_ReturnsFailure()
//         {
//             // Arrange
//             var reservationService = Resolve<ReservationService>();
//             var invalidReservation = ReservationBuilder.GetReservationWithEmptyItems();
//
//             // Act
//             var result = await reservationService.CreateReservation(invalidReservation);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains("ítem")));
//         }
//
//         [TestMethod]
//         public async Task CreateReservation_EmptyPersonTypes_ReturnsFailure()
//         {
//             // Arrange
//             var reservationService = Resolve<ReservationService>();
//             var invalidReservation = ReservationBuilder.GetReservationWithEmptyPersonTypes();
//
//             // Act
//             var result = await reservationService.CreateReservation(invalidReservation);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains("tipo de persona")));
//         }
//
//         [TestMethod]
//         public async Task CreateReservation_EmptyCode_ReturnsFailure()
//         {
//             // Arrange
//             var reservationService = Resolve<ReservationService>();
//             var invalidReservation = ReservationBuilder.GetReservationWithEmptyCode();
//
//             // Act
//             var result = await reservationService.CreateReservation(invalidReservation);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains("código")));
//         }
//
//         [TestMethod]
//         [DataRow(999)]
//         [DataRow(1000)]
//         [DataRow(-1)]
//         public async Task GetReservationById_NonExistingId_ReturnsNotFound(int nonExistingId)
//         {
//             // Arrange
//             var reservationService = Resolve<ReservationService>();
//
//             // Act
//             var result = await reservationService.GetReservationById(nonExistingId);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains("no encontrada")));
//         }
//
//         // [TestMethod]
//         // public async Task GetReservationsByPersonId_ExistingPerson_ReturnsReservations()
//         // {
//         //     // Arrange
//         //     var reservationService = Resolve<ReservationService>();
//         //     var reservation1 = ReservationBuilder.GetValidCreateReservationDto();
//         //     var reservation2 = ReservationBuilder.GetMultipleItemsReservation();
//         //
//         //     await reservationService.CreateReservation(reservation1);
//         //     await reservationService.CreateReservation(reservation2);
//         //
//         //     // Act
//         //     var result = await reservationService.GetReservationsByPersonId(1);
//         //
//         //     // Assert
//         //     Assert.IsTrue(result.IsSuccess);
//         //     Assert.AreEqual(2, result.Data.Count);
//         //     Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
//         // }
//
//         [TestMethod]
//         [DataRow(999)]
//         [DataRow(1000)]
//         [DataRow(-1)]
//         public async Task GetReservationsByPersonId_NonExistingPerson_ReturnsEmptyList(int nonExistingPersonId)
//         {
//             // Arrange
//             var reservationService = Resolve<ReservationService>();
//
//             // Act
//             var result = await reservationService.GetReservationsByPersonId(nonExistingPersonId);
//
//             // Assert
//             Assert.IsTrue(result.IsSuccess);
//             Assert.AreEqual(0, result.Data.Count);
//             Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
//         }
//
//         [TestMethod]
//         [DataRow(999)]
//         [DataRow(1000)]
//         [DataRow(-1)]
//         public async Task DeleteReservation_NonExistingReservation_ReturnsNotFound(int nonExistingId)
//         {
//             // Arrange
//             var reservationService = Resolve<ReservationService>();
//
//             // Act
//             var result = await reservationService.DeleteReservation(nonExistingId);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//         }
//
//         [TestMethod]
//         [DataRow(999, 999)]
//         [DataRow(1000, 1000)]
//         [DataRow(-1, -1)]
//         public async Task DeleteMealPeriod_NonExistingPeriod_ReturnsNotFound(int reservationId, int detailId)
//         {
//             // Arrange
//             var reservationService = Resolve<ReservationService>();
//
//             // Act
//             var result = await reservationService.DeleteMealPeriod(reservationId, detailId);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//         }
//     }
// }
