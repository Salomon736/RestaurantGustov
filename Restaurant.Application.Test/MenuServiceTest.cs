// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Restaurant.Application.Services.MenuCharge;
// using Restaurant.Application.Test.Common;
// using Restaurant.Application.Test.Builders;
// using Restaurant.Application.Test.Mock;
// using System.Net;
// using Restaurant.Domain.Models.MenuCharge;
//
// namespace Restaurant.Application.Test
// {
//     [TestClass]
//     public class MenuServiceTest : TestBase
//     {
//         [TestInitialize]
//         public void Initialize()
//         {
//             MenuRepositoryMock.ClearData();
//         }
//
//         [TestMethod]
//         public async Task Insert_ValidMenu_ReturnsSuccess()
//         {
//             var menuService = Resolve<MenuService>();
//             var validMenu = MenuBuilder.GetValidMenu();
//
//             // Act
//             var result = await menuService.Insert(validMenu);
//
//             // Assert
//             Assert.IsTrue(result.IsSuccess);
//             Assert.IsTrue(result.Data);
//             Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
//         }
//
//         [TestMethod]
//         public async Task Insert_InvalidDish_ReturnsFailure()
//         {
//             // Arrange
//             var menuService = Resolve<MenuService>();
//             var invalidMenu = MenuBuilder.GetMenuWithInvalidDish();
//
//             // Act
//             var result = await menuService.Insert(invalidMenu);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains("no existe")));
//         }
//
//         [TestMethod]
//         public async Task Insert_InvalidMealPeriod_ReturnsFailure()
//         {
//             // Arrange
//             var menuService = Resolve<MenuService>();
//             var invalidMenu = MenuBuilder.GetMenuWithInvalidMealPeriod();
//
//             // Act
//             var result = await menuService.Insert(invalidMenu);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains("no existe")));
//         }
//
//         [TestMethod]
//         public async Task Insert_InvalidLounge_ReturnsFailure()
//         {
//             // Arrange
//             var menuService = Resolve<MenuService>();
//             var invalidMenu = MenuBuilder.GetMenuWithInvalidLounge();
//
//             // Act
//             var result = await menuService.Insert(invalidMenu);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains("no existe")));
//         }
//
//         [TestMethod]
//         public async Task Insert_PastDate_ReturnsFailure()
//         {
//             // Arrange
//             var menuService = Resolve<MenuService>();
//             var invalidMenu = MenuBuilder.GetMenuWithPastDate();
//
//             // Act
//             var result = await menuService.Insert(invalidMenu);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains("pasado")));
//         }
//
//         [TestMethod]
//         public async Task Insert_DuplicateMenu_ReturnsFailure()
//         {
//             // Arrange
//             var menuService = Resolve<MenuService>();
//             var menu1 = MenuBuilder.GetValidMenu();
//             var menu2 = MenuBuilder.GetValidMenu(); // Mismo menú
//
//             // Act
//             var result1 = await menuService.Insert(menu1);
//             var result2 = await menuService.Insert(menu2);
//
//             // Assert
//             Assert.IsTrue(result1.IsSuccess);
//             Assert.IsFalse(result2.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.BadRequest, result2.StatusCode);
//             Assert.IsTrue(result2.Errors.Any(e => e.Contains("Ya existe un menú")));
//         }
//
//         [TestMethod]
//         public async Task InsertMenuMatrix_ValidMatrix_ReturnsSuccess()
//         {
//             // Arrange
//             var menuService = Resolve<MenuService>();
//             var validMatrix = MenuBuilder.GetValidMenuMatrix();
//
//             // Act
//             var result = await menuService.InsertMenuMatrix(validMatrix);
//
//             // Assert
//             Assert.IsTrue(result.IsSuccess);
//             Assert.IsTrue(result.Data);
//             Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
//         }
//
//         [TestMethod]
//         public async Task GetIdMenu_ExistingId_ReturnsMenu()
//         {
//             // Arrange
//             var menuService = Resolve<MenuService>();
//             var validMenu = MenuBuilder.GetValidMenu();
//             await menuService.Insert(validMenu);
//
//             // Act
//             var result = await menuService.GetIdMenu(1);
//
//             // Assert
//             Assert.IsTrue(result.IsSuccess);
//             Assert.IsNotNull(result.Data);
//             Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
//         }
//
//         [TestMethod]
//         public async Task GetIdMenu_NonExistingId_ReturnsNotFound()
//         {
//             // Arrange
//             var menuService = Resolve<MenuService>();
//
//             // Act
//             var result = await menuService.GetIdMenu(999);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains("No se encontró")));
//         }
//
//         [TestMethod]
//         public async Task DeleteMenu_ExistingId_ReturnsSuccess()
//         {
//             // Arrange
//             var menuService = Resolve<MenuService>();
//             var validMenu = MenuBuilder.GetValidMenu();
//             await menuService.Insert(validMenu);
//
//             // Act
//             var result = await menuService.DeleteMenu(1);
//
//             // Assert
//             Assert.IsTrue(result.IsSuccess);
//             Assert.IsTrue(result.Data);
//             Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
//         }
//
//         [TestMethod]
//         public async Task DeleteMenu_NonExistingId_ReturnsNotFound()
//         {
//             // Arrange
//             var menuService = Resolve<MenuService>();
//
//             // Act
//             var result = await menuService.DeleteMenu(999);
//
//             // Assert
//             Assert.IsFalse(result.IsSuccess);
//             Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
//             Assert.IsTrue(result.Errors.Any(e => e.Contains("No se encontró")));
//         }
//
//         [TestMethod]
//         public async Task GetAllMenu_ReturnsAllMenus()
//         {
//             // Arrange
//             var menuService = Resolve<MenuService>();
//             var menu1 = MenuBuilder.GetValidMenu();
//             // Crear segundo menú con diferente período para evitar duplicado
//             var menu2 = new MenuModel(
//                 id: 0,
//                 menuDate: DateTime.Today.AddDays(1),
//                 idDish: 1,
//                 idMealPeriod: 2, // Diferente período
//                 idLounge: 1,
//                 createdAt: DateTime.UtcNow
//             );
//
//             await menuService.Insert(menu1);
//             await menuService.Insert(menu2);
//
//             // Act
//             var result = await menuService.GetAllMenu();
//
//             // Assert
//             Assert.IsTrue(result.IsSuccess);
//             Assert.AreEqual(2, result.Data.Count);
//             Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
//         }
//     }
// }
