// using FluentValidation;
// using Restaurant.Domain.Dtos.MenuCharge;
//
// namespace Restaurant.Application.Test.Mock
// {
//     public class MenuMatrixValidator : AbstractValidator<MenuMatrixDto>
//     {
//         public MenuMatrixValidator()
//         {
//             RuleFor(x => x.DishId)
//                 .GreaterThan(0).WithMessage("El ID del plato debe ser mayor a 0");
//
//             RuleFor(x => x.LoungeId)
//                 .GreaterThan(0).WithMessage("El ID del salón debe ser mayor a 0");
//
//             RuleFor(x => x.Selections)
//                 .NotEmpty().WithMessage("Debe seleccionar al menos una fecha y período");
//
//             RuleForEach(x => x.Selections)
//                 .ChildRules(selection =>
//                 {
//                     selection.RuleFor(s => s.MealPeriodId)
//                         .GreaterThan(0).WithMessage("El ID del período de comida debe ser mayor a 0");
//                     
//                     selection.RuleFor(s => s.Date)
//                         .GreaterThanOrEqualTo(DateTime.Today)
//                         .WithMessage("La fecha no puede estar en el pasado");
//                 });
//         }
//     }
// }