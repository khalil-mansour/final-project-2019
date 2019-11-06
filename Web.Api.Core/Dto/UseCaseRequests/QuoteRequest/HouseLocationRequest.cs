using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Dto.UseCaseRequests.QuoteRequest
{
   public class HouseLocationRequest
    {
        public string PostalCode { get; }

        public int CityId { get; }

        public int ProvinceId { get; }

        public string Street { get; }

        public int AppartementUnits { get; }

        public HouseLocationRequest(string postalCode, int cityId, int provinceId, string street, int appartementUnits)
        {
            PostalCode = postalCode;
            CityId = cityId;
            ProvinceId = provinceId;
            Street = street;
            AppartementUnits = appartementUnits;
        }
    }
}
