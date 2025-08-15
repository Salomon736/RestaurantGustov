// using Restaurant.Domain.Dtos.Reservation;
// using Restaurant.Domain.Models.Reservation;
// using Restaurant.Domain.Repositories;
// using Restaurant.Domain.Repositories.Reservation;
//
// namespace Restaurant.Application.Test.Mock
// {
//     internal class ReservationRepositoryMock : IReservationRepository
//     {
//         private static List<ReservationResponseDto> _reservations = new List<ReservationResponseDto>();
//         private static int _nextId = 1;
//
//         public Task<ReservationResponseDto> InsertAsync(CreateReservationDto dto)
//         {
//             var reservation = new ReservationResponseDto
//             {
//                 id = _nextId++,
//                 reservationCode = dto.reservationCode,
//                 voucher = dto.voucher,
//                 totalAmount = dto.totalAmount,
//                 status = dto.status,
//                 createdAt = DateTime.UtcNow,
//                 paymentMethod = dto.paymentMethod,
//                 menuDate = dto.items.First().menuInfo.Date,
//                 person = new PersonDto
//                 {
//                     id = dto.idPerson,
//                     name = $"Persona {dto.idPerson}",
//                     lastNameFather = $"Apellido {dto.idPerson}",
//                     whatsApp = $"123456{dto.idPerson:D3}",
//                   
//                 },
//                 lounge = new LoungeDto
//                 {
//                     id = dto.idLounge,
//                     nameLounge = $"Salón {dto.idLounge}"
//                 },
//                 plan = null,
//                 details = dto.items.Select((item, index) => new ReservationDetailResponseDto
//                 {
//                     id = index + 1,
//                     mealTypeGroup = item.mealTypeGroup,
//                     subtotal = item.subtotal,
//                     menuItems = new List<MenuItemResponseDto>
//                     {
//                         new MenuItemResponseDto
//                         {
//                             id = 1,
//                             name = $"Plato para {item.mealTypeGroup}",
//                             dishType = "Principal",
//                             image = "plato.jpg"
//                         }
//                     },
//                     personTypes = item.personTypeInfo.Select(pt => new PersonTypeResponseDto
//                     {
//                         id = pt.personTypeId,
//                         name = GetPersonTypeName(pt.personTypeId),
//                         quantity = pt.quantity,
//                         price = pt.price,
//                         totalPrice = pt.totalPrice
//                     }).ToList()
//                 }).ToList()
//             };
//
//             _reservations.Add(reservation);
//             return Task.FromResult(reservation);
//         }
//
//         Task<ReservationModel> IReservationRepository.UpdateAsync(ReservationModel model)
//         {
//             throw new NotImplementedException();
//         }
//
//         // Método para insertar directamente sin validaciones (solo para pruebas)
//         public static Task<ReservationResponseDto> InsertDirectly(CreateReservationDto dto)
//         {
//             var reservation = new ReservationResponseDto
//             {
//                 id = _nextId++,
//                 reservationCode = dto.reservationCode,
//                 voucher = dto.voucher,
//                 totalAmount = dto.totalAmount,
//                 status = dto.status,
//                 createdAt = DateTime.UtcNow,
//                 paymentMethod = dto.paymentMethod,
//                 menuDate = dto.items.First().menuInfo.Date,
//                 person = new PersonDto
//                 {
//                     id = dto.idPerson,
//                     name = $"Persona {dto.idPerson}",
//                     lastNameFather = $"Apellido {dto.idPerson}",
//                     whatsApp = $"123456{dto.idPerson:D3}",
//                   
//                 },
//                 lounge = new LoungeDto
//                 {
//                     id = dto.idLounge,
//                     nameLounge = $"Salón {dto.idLounge}"
//                 },
//                 plan = null,
//                 details = dto.items.Select((item, index) => new ReservationDetailResponseDto
//                 {
//                     id = index + 1,
//                     mealTypeGroup = item.mealTypeGroup,
//                     subtotal = item.subtotal,
//                     menuItems = new List<MenuItemResponseDto>
//                     {
//                         new MenuItemResponseDto
//                         {
//                             id = 1,
//                             name = $"Plato para {item.mealTypeGroup}",
//                             dishType = "Principal",
//                             image = "plato.jpg"
//                         }
//                     },
//                     personTypes = item.personTypeInfo.Select(pt => new PersonTypeResponseDto
//                     {
//                         id = pt.personTypeId,
//                         name = GetPersonTypeName(pt.personTypeId),
//                         quantity = pt.quantity,
//                         price = pt.price,
//                         totalPrice = pt.totalPrice
//                     }).ToList()
//                 }).ToList()
//             };
//
//             _reservations.Add(reservation);
//             return Task.FromResult(reservation);
//         }
//
//         public Task<ReservationResponseDto> GetByIdAsync(int id)
//         {
//             var reservation = _reservations.FirstOrDefault(r => r.id == id);
//             return Task.FromResult(reservation);
//         }
//
//         Task<List<ReservationModel>> IGenericRepository<ReservationModel>.GetAllAsync()
//         {
//             throw new NotImplementedException();
//         }
//
//         public Task<bool> IsExistId(int id)
//         {
//             throw new NotImplementedException();
//         }
//
//         Task<ReservationModel?> IGenericRepository<ReservationModel>.GetByIdAsync(int id)
//         {
//             throw new NotImplementedException();
//         }
//
//         public Task<List<ReservationResponseDto>> GetAllAsync()
//         {
//             return Task.FromResult(_reservations.ToList());
//         }
//
//         public Task<List<ReservationResponseDto>> GetByPersonIdAsync(int personId)
//         {
//             var reservations = _reservations.Where(r => r.person.id == personId).ToList();
//             return Task.FromResult(reservations);
//         }
//
//         public Task<bool> UpdateStatusAsync(int id, bool status)
//         {
//             var reservation = _reservations.FirstOrDefault(r => r.id == id);
//             if (reservation != null)
//             {
//                 reservation.status = status;
//                 return Task.FromResult(true);
//             }
//             return Task.FromResult(false);
//         }
//
//         public Task<bool> UpdateVoucherAsync(int id, string voucherUrl)
//         {
//             var reservation = _reservations.FirstOrDefault(r => r.id == id);
//             if (reservation != null)
//             {
//                 reservation.voucher = voucherUrl;
//                 return Task.FromResult(true);
//             }
//             return Task.FromResult(false);
//         }
//
//         public Task<ReservationModel> InsertAsync(ReservationModel model)
//         {
//             throw new NotImplementedException();
//         }
//
//         Task<ReservationModel> IGenericRepository<ReservationModel>.UpdateAsync(ReservationModel model)
//         {
//             throw new NotImplementedException();
//         }
//
//         public Task<bool> DeleteAsync(int id)
//         {
//             var reservation = _reservations.FirstOrDefault(r => r.id == id);
//             if (reservation != null)
//             {
//                 _reservations.Remove(reservation);
//                 return Task.FromResult(true);
//             }
//             return Task.FromResult(false);
//         }
//
//         public Task<bool> CanCancelReservation(int id)
//         {
//             var reservation = _reservations.FirstOrDefault(r => r.id == id);
//             if (reservation == null) return Task.FromResult(false);
//             
//             // Lógica simplificada: solo se puede cancelar si no está pagada y la fecha es futura
//             var canCancel = !reservation.status && 
//                            reservation.menuDate.HasValue && 
//                            reservation.menuDate.Value.Date > DateTime.Today;
//             return Task.FromResult(canCancel);
//         }
//
//         public Task<bool> CanCancelMealPeriod(int reservationId, int detailId)
//         {
//             var reservation = _reservations.FirstOrDefault(r => r.id == reservationId);
//             if (reservation == null) return Task.FromResult(false);
//             
//             var detail = reservation.details.FirstOrDefault(d => d.id == detailId);
//             if (detail == null) return Task.FromResult(false);
//             
//             var canCancel = !reservation.status && 
//                            reservation.menuDate.HasValue && 
//                            reservation.menuDate.Value.Date > DateTime.Today;
//             return Task.FromResult(canCancel);
//         }
//
//         public Task<bool> DeleteMealPeriodAsync(int reservationId, int detailId)
//         {
//             var reservation = _reservations.FirstOrDefault(r => r.id == reservationId);
//             if (reservation == null) return Task.FromResult(false);
//             
//             var detail = reservation.details.FirstOrDefault(d => d.id == detailId);
//             if (detail == null) return Task.FromResult(false);
//             
//             reservation.details.Remove(detail);
//             
//             if (!reservation.details.Any())
//             {
//                 _reservations.Remove(reservation);
//             }
//             else
//             {
//                 reservation.totalAmount = reservation.details.Sum(d => d.subtotal);
//             }
//             
//             return Task.FromResult(true);
//         }
//
//         private static string GetPersonTypeName(int personTypeId)
//         {
//             return personTypeId switch
//             {
//                 1 => "Estudiante",
//                 2 => "Docente",
//                 3 => "Administrativo",
//                 _ => $"Tipo {personTypeId}"
//             };
//         }
//
//         public static void ClearData()
//         {
//             _reservations.Clear();
//             _nextId = 1;
//         }
//     }
// }
