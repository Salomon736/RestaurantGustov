// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Restaurant.Application.Services.Discount;
// using Restaurant.Application.Test.Common;
// using Restaurant.Application.Test.Builders;
// using Restaurant.Application.Test.Mock;
// using System.Net;
//
// namespace Restaurant.Application.Test
// {
//     [TestClass]
//     public class OfferServiceTest : TestBase
//     {
//         [TestInitialize]
//         public void Initialize()
//         {
//             OfferRepositoryMock.ClearData();
//         }
//
//         [TestMethod]
//         public async Task Insert_ValidOffer_ReturnsSuccess()
//         {
//             // Arrange
//             var offerService = Resolve<OfferService>();
//             var validOffer = OfferBuilder.GetValidOfferCreateDto();
//
//             // Act
//             var result = await offerService.Insert(validOffer);
//
//             // Assert
//             Assert.IsTrue(result.IsSuccess);
//             Assert.IsTrue(result.Data);
//             Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
//         }
//
//         [TestMethod]
//         [DataRow(-1, "fecha de inicio")]
//         [DataRow(-5, "fecha de inicio")]
//         [DataRow(-10, "fecha de inicio")]
//         public async Task Insert_InvalidStartDate_ReturnsFailure(int daysOffset, string expectedError)
//         {
//             // Arrange
//             var offerService = Resolve<OfferService>();
//             var invalidOffer = OfferBuilder.GetValidOfferCreateDto();
//             invalidOffer.startDate = DateTime.Today.AddDays(daysOffset);
//
//             // Act
//             var result = await offerService.Insert(invalidOffer);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains(expectedError)));
//         }
//
//         [TestMethod]
//         [DataRow(-1, "fecha de finalización")]
//         [DataRow(-7, "fecha de finalización")]
//         [DataRow(-30, "fecha de finalización")]
//         public async Task Insert_InvalidEndDate_ReturnsFailure(int daysOffset, string expectedError)
//         {
//             // Arrange
//             var offerService = Resolve<OfferService>();
//             var invalidOffer = OfferBuilder.GetValidOfferCreateDto();
//             invalidOffer.endDate = DateTime.Today.AddDays(daysOffset);
//
//             // Act
//             var result = await offerService.Insert(invalidOffer);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains(expectedError)));
//         }
//
//         [TestMethod]
//         [DataRow(0, "mayor a cero")]
//         [DataRow(-5.50, "mayor a cero")]
//         [DataRow(-100, "mayor a cero")]
//         public async Task Insert_InvalidAmount_ReturnsFailure(double amount, string expectedError)
//         {
//             // Arrange
//             var offerService = Resolve<OfferService>();
//             var invalidOffer = OfferBuilder.GetValidOfferCreateDto();
//             invalidOffer.amount = (decimal)amount;
//
//             // Act
//             var result = await offerService.Insert(invalidOffer);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains(expectedError)));
//         }
//
//         [TestMethod]
//         [DataRow("", "titulo")]
//         [DataRow(null, "titulo")]
//         [DataRow("   ", "titulo")]
//         public async Task Insert_InvalidTitle_ReturnsFailure(string title, string expectedError)
//         {
//             // Arrange
//             var offerService = Resolve<OfferService>();
//             var invalidOffer = OfferBuilder.GetValidOfferCreateDto();
//             invalidOffer.title = title;
//
//             // Act
//             var result = await offerService.Insert(invalidOffer);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains(expectedError)));
//         }
//
//         [TestMethod]
//         [DataRow(0, "días de la semana")]
//         public async Task Insert_InvalidWeekDays_ReturnsFailure(int weekDays, string expectedError)
//         {
//             // Arrange
//             var offerService = Resolve<OfferService>();
//             var invalidOffer = OfferBuilder.GetValidOfferCreateDto();
//             invalidOffer.weekDays = weekDays;
//
//             // Act
//             var result = await offerService.Insert(invalidOffer);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains(expectedError)));
//         }
//
//         [TestMethod]
//         [DataRow(251, "250 caracteres")]
//         [DataRow(300, "250 caracteres")]
//         [DataRow(500, "250 caracteres")]
//         public async Task Insert_LongDescription_ReturnsFailure(int descriptionLength, string expectedError)
//         {
//             // Arrange
//             var offerService = Resolve<OfferService>();
//             var invalidOffer = OfferBuilder.GetValidOfferCreateDto();
//             invalidOffer.description = new string('A', descriptionLength);
//
//             // Act
//             var result = await offerService.Insert(invalidOffer);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains(expectedError)));
//         }
//
//         [TestMethod]
//         public async Task Insert_EmptyRoles_ReturnsFailure()
//         {
//             // Arrange
//             var offerService = Resolve<OfferService>();
//             var invalidOffer = OfferBuilder.GetOfferWithEmptyRoles();
//
//             // Act
//             var result = await offerService.Insert(invalidOffer);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains("Rol")));
//         }
//
//         [TestMethod]
//         [DataRow(10, 5, "posterior o igual")]
//         [DataRow(15, 10, "posterior o igual")]
//         [DataRow(30, 1, "posterior o igual")]
//         public async Task Insert_EndDateBeforeStartDate_ReturnsFailure(int startDaysOffset, int endDaysOffset, string expectedError)
//         {
//             // Arrange
//             var offerService = Resolve<OfferService>();
//             var invalidOffer = OfferBuilder.GetValidOfferCreateDto();
//             invalidOffer.startDate = DateTime.Today.AddDays(startDaysOffset);
//             invalidOffer.endDate = DateTime.Today.AddDays(endDaysOffset);
//
//             // Act
//             var result = await offerService.Insert(invalidOffer);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains(expectedError)));
//         }
//
//         [TestMethod]
//         [DataRow("Oferta Actualizada")]
//         [DataRow("Nueva Oferta de Descuento")]
//         [DataRow("Promoción Especial")]
//         public async Task Update_ExistingOffer_ReturnsSuccess(string newTitle)
//         {
//             // Arrange
//             var offerService = Resolve<OfferService>();
//             var validOffer = OfferBuilder.GetValidOfferCreateDto();
//             await offerService.Insert(validOffer);
//
//             var updateOffer = OfferBuilder.GetValidOfferCreateDto();
//             updateOffer.title = newTitle;
//
//             // Act
//             var result = await offerService.Update(1, updateOffer);
//
//             // Assert
//             Assert.IsTrue(result.IsSuccess);
//             Assert.IsTrue(result.Data);
//             Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
//         }
//
//         [TestMethod]
//         [DataRow(999)]
//         [DataRow(1000)]
//         [DataRow(-1)]
//         public async Task Update_NonExistingOffer_ReturnsNotFound(int nonExistingId)
//         {
//             // Arrange
//             var offerService = Resolve<OfferService>();
//             var updateOffer = OfferBuilder.GetValidOfferCreateDto();
//
//             // Act
//             var result = await offerService.Update(nonExistingId, updateOffer);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
//         }
//
//         [TestMethod]
//         public async Task GetId_ExistingOffer_ReturnsOffer()
//         {
//             // Arrange
//             var offerService = Resolve<OfferService>();
//             var validOffer = OfferBuilder.GetValidOfferCreateDto();
//             await offerService.Insert(validOffer);
//
//             // Act
//             var result = await offerService.GetId(1);
//
//             // Assert
//             Assert.IsTrue(result.IsSuccess);
//             Assert.IsNotNull(result.Data);
//             Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
//         }
//
//         [TestMethod]
//         [DataRow(999)]
//         [DataRow(1000)]
//         [DataRow(-1)]
//         public async Task GetId_NonExistingOffer_ReturnsNotFound(int nonExistingId)
//         {
//             // Arrange
//             var offerService = Resolve<OfferService>();
//
//             // Act
//             var result = await offerService.GetId(nonExistingId);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
//         }
//
//         [TestMethod]
//         public async Task Delete_ExistingOffer_ReturnsSuccess()
//         {
//             // Arrange
//             var offerService = Resolve<OfferService>();
//             var validOffer = OfferBuilder.GetValidOfferCreateDto();
//             await offerService.Insert(validOffer);
//
//             // Act
//             var result = await offerService.Delete(1);
//
//             // Assert
//             Assert.IsTrue(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
//         }
//
//         [TestMethod]
//         [DataRow(999)]
//         [DataRow(1000)]
//         [DataRow(-1)]
//         public async Task Delete_NonExistingOffer_ReturnsNotFound(int nonExistingId)
//         {
//             // Arrange
//             var offerService = Resolve<OfferService>();
//
//             // Act
//             var result = await offerService.Delete(nonExistingId);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
//         }
//
//         [TestMethod]
//         [DataRow(2, "Primera Oferta", "Segunda Oferta")]
//         [DataRow(3, "Oferta A", "Oferta B", "Oferta C")]
//         public async Task GetAll_MultipleOffers_ReturnsAllOffers(int expectedCount, params string[] offerTitles)
//         {
//             // Arrange
//             var offerService = Resolve<OfferService>();
//             
//             foreach (var title in offerTitles)
//             {
//                 var offer = OfferBuilder.GetValidOfferCreateDto();
//                 offer.title = title;
//                 await offerService.Insert(offer);
//             }
//
//             // Act
//             var result = await offerService.GetAll();
//
//             // Assert
//             Assert.IsTrue(result.IsSuccess);
//             Assert.AreEqual(expectedCount, result.Data.Count);
//             Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
//         }
//     }
// }
