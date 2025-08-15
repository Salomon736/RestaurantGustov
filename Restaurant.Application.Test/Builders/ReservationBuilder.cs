using Restaurant.Domain.Dtos.MenuCharge;
using Restaurant.Domain.Dtos.Reservation;

namespace Restaurant.Application.Test.Builders
{
    public static class ReservationBuilder
    {
        public static CreateReservationDto GetValidCreateReservationDto()
        {
            return new CreateReservationDto
            {
                idPerson = 1,
                idLounge = 1,
                paymentMethod = "Pago por Planilla",
                totalAmount = 25.50,
                status = false,
                reservationCode = "RES-001-2024",
                voucher = null,
                items = new List<ReservationItemDto>
                {
                    new ReservationItemDto
                    {
                        mealTypeGroup = "Almuerzo",
                        subtotal = 25.50,
                        menuInfo = new MenuDatePeriodDto
                        {
                            Date = DateTime.Today.AddDays(1),
                            MealPeriodId = 2
                        },
                        personTypeInfo = new List<PersonTypeInfoDto>
                        {
                            new PersonTypeInfoDto
                            {
                                personTypeId = 1,
                                quantity = 1,
                                price = 25.50,
                                totalPrice = 25.50
                            }
                        }
                    }
                }
            };
        }

        public static CreateReservationDto GetReservationWithInvalidPerson()
        {
            var reservation = GetValidCreateReservationDto();
            reservation.idPerson = 999; // Persona inexistente
            return reservation;
        }

        public static CreateReservationDto GetReservationWithInvalidLounge()
        {
            var reservation = GetValidCreateReservationDto();
            reservation.idLounge = 999; // Salón inexistente
            return reservation;
        }

        public static CreateReservationDto GetReservationWithPastDate()
        {
            var reservation = GetValidCreateReservationDto();
            reservation.items[0].menuInfo.Date = DateTime.Today.AddDays(-1); // Fecha pasada
            return reservation;
        }

        public static CreateReservationDto GetReservationWithInvalidMealPeriod()
        {
            var reservation = GetValidCreateReservationDto();
            reservation.items[0].menuInfo.MealPeriodId = 999; // Período inexistente
            return reservation;
        }

        public static CreateReservationDto GetReservationWithNoMenuAvailable()
        {
            var reservation = GetValidCreateReservationDto();
            reservation.items[0].menuInfo.Date = DateTime.Today.AddDays(30); // Fecha sin menús
            reservation.items[0].menuInfo.MealPeriodId = 3; // Período sin menús
            return reservation;
        }

        public static CreateReservationDto GetReservationWithInvalidPersonType()
        {
            var reservation = GetValidCreateReservationDto();
            reservation.items[0].personTypeInfo[0].personTypeId = 999; // Tipo de persona inexistente
            return reservation;
        }

        public static CreateReservationDto GetReservationWithZeroQuantity()
        {
            var reservation = GetValidCreateReservationDto();
            reservation.items[0].personTypeInfo[0].quantity = 0; // Cantidad cero
            return reservation;
        }

        public static CreateReservationDto GetReservationWithZeroPrice()
        {
            var reservation = GetValidCreateReservationDto();
            reservation.items[0].personTypeInfo[0].price = 0; // Precio cero
            return reservation;
        }

        public static CreateReservationDto GetReservationWithIncorrectTotalPrice()
        {
            var reservation = GetValidCreateReservationDto();
            reservation.items[0].personTypeInfo[0].totalPrice = 100; // Total incorrecto
            return reservation;
        }

        public static CreateReservationDto GetReservationWithIncorrectSubtotal()
        {
            var reservation = GetValidCreateReservationDto();
            reservation.items[0].subtotal = 100; // Subtotal incorrecto
            return reservation;
        }

        public static CreateReservationDto GetReservationWithIncorrectTotalAmount()
        {
            var reservation = GetValidCreateReservationDto();
            reservation.totalAmount = 100; // Total general incorrecto
            return reservation;
        }

        public static CreateReservationDto GetReservationWithEmptyItems()
        {
            var reservation = GetValidCreateReservationDto();
            reservation.items = new List<ReservationItemDto>(); // Sin items
            return reservation;
        }

        public static CreateReservationDto GetReservationWithEmptyPersonTypes()
        {
            var reservation = GetValidCreateReservationDto();
            reservation.items[0].personTypeInfo = new List<PersonTypeInfoDto>(); // Sin tipos de persona
            return reservation;
        }

        public static CreateReservationDto GetReservationWithEmptyCode()
        {
            var reservation = GetValidCreateReservationDto();
            reservation.reservationCode = ""; // Código vacío
            return reservation;
        }

        public static CreateReservationDto GetReservationWithPaidStatus()
        {
            var reservation = GetValidCreateReservationDto();
            reservation.status = true; // Ya pagada
            reservation.paymentMethod = "Tarjeta de Crédito";
            return reservation;
        }

        public static CreateReservationDto GetReservationWithVoucher()
        {
            var reservation = GetValidCreateReservationDto();
            reservation.voucher = "https://example.com/voucher.pdf";
            return reservation;
        }

        public static CreateReservationDto GetMultipleItemsReservation()
        {
            return new CreateReservationDto
            {
                idPerson = 1,
                idLounge = 1,
                paymentMethod = "Pago por Planilla",
                totalAmount = 51.00,
                status = false,
                reservationCode = "RES-002-2024",
                voucher = null,
                items = new List<ReservationItemDto>
                {
                    new ReservationItemDto
                    {
                        mealTypeGroup = "Desayuno",
                        subtotal = 15.50,
                        menuInfo = new MenuDatePeriodDto
                        {
                            Date = DateTime.Today.AddDays(1),
                            MealPeriodId = 1
                        },
                        personTypeInfo = new List<PersonTypeInfoDto>
                        {
                            new PersonTypeInfoDto
                            {
                                personTypeId = 1,
                                quantity = 1,
                                price = 15.50,
                                totalPrice = 15.50
                            }
                        }
                    },
                    new ReservationItemDto
                    {
                        mealTypeGroup = "Almuerzo",
                        subtotal = 35.50,
                        menuInfo = new MenuDatePeriodDto
                        {
                            Date = DateTime.Today.AddDays(1),
                            MealPeriodId = 2
                        },
                        personTypeInfo = new List<PersonTypeInfoDto>
                        {
                            new PersonTypeInfoDto
                            {
                                personTypeId = 1,
                                quantity = 1,
                                price = 35.50,
                                totalPrice = 35.50
                            }
                        }
                    }
                }
            };
        }

        public static CreateReservationDto GetReservationWithMultiplePersonTypes()
        {
            return new CreateReservationDto
            {
                idPerson = 1,
                idLounge = 1,
                paymentMethod = "Pago por Planilla",
                totalAmount = 45.00,
                status = false,
                reservationCode = "RES-003-2024",
                voucher = null,
                items = new List<ReservationItemDto>
                {
                    new ReservationItemDto
                    {
                        mealTypeGroup = "Almuerzo",
                        subtotal = 45.00,
                        menuInfo = new MenuDatePeriodDto
                        {
                            Date = DateTime.Today.AddDays(1),
                            MealPeriodId = 2
                        },
                        personTypeInfo = new List<PersonTypeInfoDto>
                        {
                            new PersonTypeInfoDto
                            {
                                personTypeId = 1, // Estudiante
                                quantity = 2,
                                price = 15.00,
                                totalPrice = 30.00
                            },
                            new PersonTypeInfoDto
                            {
                                personTypeId = 2, // Docente
                                quantity = 1,
                                price = 15.00,
                                totalPrice = 15.00
                            }
                        }
                    }
                }
            };
        }
    }
}
