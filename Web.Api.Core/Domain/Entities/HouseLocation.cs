using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Domain.Entities
{
    public class HouseLocation
    {


        public int Id { get; }

        public string PostalCode { get; }

        public string City { get; }

        public int ProvinceId { get; }

        public string Address { get; }

        public string ApartmentUnit { get; }

        public HouseLocation(int id, string postalCode, string city, int provinceId, string address, string apartmentUnit)
        {
            Id = id;
            PostalCode = postalCode;
            City = city;
            ProvinceId = provinceId;
            Address = address;
            ApartmentUnit = apartmentUnit;
        }

        public HouseLocation(string postalCode, string city, int provinceId, string address, string apartmentUnit)
        {
            PostalCode = postalCode;
            City = city;
            ProvinceId = provinceId;
            Address = address;
            ApartmentUnit = apartmentUnit;
        }
    }
}
