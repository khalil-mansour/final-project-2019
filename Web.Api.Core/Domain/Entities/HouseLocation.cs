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

        public string ProvinceId { get; }

        public string Address { get; }

        public int AppartementUnits { get; }

        public HouseLocation(int id, string postalCode, string city, string provinceId, string address, int appartementUnits)
        {
            Id = id;
            PostalCode = postalCode;
            City = city;
            ProvinceId = provinceId;
            Address = address;
            AppartementUnits = appartementUnits;
        }

        public HouseLocation(string postalCode, string city, string provinceId, string address, int appartementUnits)
        {
            PostalCode = postalCode;
            City = city;
            ProvinceId = provinceId;
            Address = address;
            AppartementUnits = appartementUnits;
        }
    }
}
