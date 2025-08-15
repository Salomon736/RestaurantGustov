// using Microsoft.Extensions.DependencyInjection;
// using FluentValidation;
// using Restaurant.Application.Services.Discount;
// using Restaurant.Application.Services.MenuCharge;
// using Restaurant.Application.Services.Reservation;
// using Restaurant.Application.Validators.MenuCharge;
// using Restaurant.Application.Test.Mock;
// using Restaurant.Application.Validators.Discount;
// using Restaurant.Application.Validators.Reservation;
// using Restaurant.Domain.Dtos.Discount;
// using Restaurant.Domain.Repositories.MenuCharge;
// using Restaurant.Domain.Models.MenuCharge;
// using Restaurant.Domain.Dtos.MenuCharge;
// using Restaurant.Domain.Dtos.Reservation;
// using Restaurant.Domain.Repositories.Discount;
// using Restaurant.Domain.Repositories.Reservation;
// using Restaurant.Application.Providers;
//
// namespace Restaurant.Application.Test.Common
// {
//     public class TestBase
//     {
//         private ServiceProvider _container { get; set; }
//
//         public TestBase()
//         {
//             var registor = new ServiceCollection();
//             
//             // Repositorios mock
//             registor.AddTransient<IMenuRepository, MenuRepositoryMock>();
//             //registor.AddTransient<IOfferRepository, OfferRepositoryMock>();
//             registor.AddTransient<IReservationRepository, ReservationRepositoryMock>();
//             registor.AddTransient<ILoungeRepository, LoungeRepositoryMock>();
//             registor.AddTransient<IPersonTypeRepository, PersonTypeRepositoryMock>();
//             registor.AddTransient<IMealPeriodsRepository, MealPeriodsRepositoryMock>();
//             
//             // Providers mock
//             registor.AddTransient<IPersonProvider, PersonProviderMock>();
//             
//             // Validadores reales
//             registor.AddTransient<IValidator<MenuModel>, MenuModelValidator>();
//             registor.AddTransient<IValidator<MenuMatrixDto>, MenuMatrixDtoValidator>();
//            // registor.AddTransient<IValidator<OfferCreateDto>, OfferValidator>();
//             registor.AddTransient<IValidator<CreateReservationDto>, CreateReservationDtoValidator>();
//             
//             // Servicios
//             registor.AddTransient<MenuService>();
//             registor.AddTransient<OfferService>();
//             registor.AddTransient<ReservationService>();
//             
//             _container = registor.BuildServiceProvider();
//         }
//
//         protected TDependency Resolve<TDependency>() where TDependency : notnull
//         {
//             return _container.GetRequiredService<TDependency>();
//         }
//     }
// }
