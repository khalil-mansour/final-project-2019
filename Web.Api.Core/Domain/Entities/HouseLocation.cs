using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Domain.Entities
{
    public class HouseLocation
    {


        public int Id { get; }

        public string PostalCode { get; }

        public int CityId { get; }

        public int ProvinceId { get; }

        public string Street { get; }

        public int AppartementUnits { get; }

        public HouseLocation(int id, string postalCode, int cityId, int provinceId, string street, int appartementUnits)
        {
            Id = id;
            PostalCode = postalCode;
            CityId = cityId;
            ProvinceId = provinceId;
            Street = street;
            AppartementUnits = appartementUnits;
        }

        public HouseLocation(string postalCode, int cityId, int provinceId, string street, int appartementUnits)
        {
            PostalCode = postalCode;
            CityId = cityId;
            ProvinceId = provinceId;
            Street = street;
            AppartementUnits = appartementUnits;
        }
    }
}
