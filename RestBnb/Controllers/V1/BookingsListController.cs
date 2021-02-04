using Microsoft.AspNetCore.Mvc;
using RestBnb.API.Services;
using RestBnb.API.Services.Interfaces;
using RestBnb.Core.Constants;
using RestBnb.Core.Entities;
using RestBnb.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestBnb.API.Controllers.V1
{
    public class BookingsListController : BaseController
    {
        private readonly IBookingsService _bookingsService;
        private readonly IPropertiesService _propertiesService;
        private readonly IPropertyImagesService _propertyImagesService;
        private readonly ICitiesService _citiesService;
        private readonly UserResolverService _userResolverService;

        public BookingsListController(IBookingsService bookingsService,
            IPropertyImagesService propertyImagesService,
            ICitiesService citiesService,
            UserResolverService userResolverService, 
            IPropertiesService propertiesService)
        {
            _bookingsService = bookingsService;
            _propertyImagesService = propertyImagesService;
            _citiesService = citiesService;
            _userResolverService = userResolverService;
            _propertiesService = propertiesService;
        }


        [HttpGet(ApiRoutes.BookingsList.GetMyBookings)]
        public async Task<IActionResult> GetMyBookings()
        {
            var bookings = await _bookingsService.GetAllBookingsAsync(new GetAllBookingsFilter { UserId = _userResolverService.GetUserId() });

            var list = new List<BookingsListElement>();

            foreach (var booking in bookings)
            {
                var property = await _propertiesService.GetPropertyByIdAsync(booking.PropertyId);
                var city = await _citiesService.GetCityByIdAsync(property.CityId);
                var propertyImage = await _propertyImagesService.GetAllAsync(property.Id);

                var element = new BookingsListElement
                {
                    AccommodatesNumber = property.AccommodatesNumber,
                    Id = booking.Id,
                    Address = property.Address,
                    BathroomNumber = property.BathroomNumber,
                    BedroomNumber = property.BedroomNumber,
                    BookingState = booking.BookingState,
                    CancellationDate = booking.CancellationDate,
                    CheckInDate = booking.CheckInDate,
                    CheckOutDate = booking.CheckOutDate,
                    City = city.Name,
                    Description = property.Description,
                    ImageUrl = propertyImage.FirstOrDefault().Image,
                    PricePerNight = booking.PricePerNight,
                    PropertyId = property.Id,
                    PropertyName = property.Name,
                    TotalPrice = booking.TotalPrice
                };
                list.Add(element);
            }
            return Ok(list);
        }

        [HttpGet(ApiRoutes.BookingsList.GetMyPropertiesBookings)]
        public async Task<IActionResult> GetMyPropertiesBookings()
        {
            var properties = await _propertiesService.GetAllPropertiesAsync(new GetAllPropertiesFilter { UserId = _userResolverService.GetUserId() });

            var list = new List<BookingsListElement>();

            foreach (var property in properties)
            {
                var bookings = await _bookingsService.GetAllBookingsAsync(new GetAllBookingsFilter { PropertyId = property.Id });
                var city = await _citiesService.GetCityByIdAsync(property.CityId);


                foreach (var booking in bookings)
                {
                    var element = new BookingsListElement
                    {
                        Id = booking.Id,
                        BookingState = booking.BookingState,
                        CancellationDate = booking.CancellationDate,
                        CheckInDate = booking.CheckInDate,
                        CheckOutDate = booking.CheckOutDate,
                        PricePerNight = booking.PricePerNight,
                        PropertyId = property.Id,
                        PropertyName = property.Name,
                        TotalPrice = booking.TotalPrice,
                        City = city.Name,
                        Address = property.Address
                    };
                    list.Add(element);
                }
            }

            return Ok(list);
        }


        public class BookingsListElement
        {
            public int Id { get; set; }
            public string PropertyName { get; set; }
            public string Description { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public decimal PricePerNight { get; set; }
            public decimal TotalPrice { get; set; }
            public DateTime CheckInDate { get; set; }
            public DateTime CheckOutDate { get; set; }
            public BookingState BookingState { get; set; }
            public int AccommodatesNumber { get; set; }
            public int BathroomNumber { get; set; }
            public int BedroomNumber { get; set; }
            public int PropertyId { get; set; }
            public DateTime? CancellationDate { get; set; }
            public byte[] ImageUrl { get; set; }
        }
    }
}
