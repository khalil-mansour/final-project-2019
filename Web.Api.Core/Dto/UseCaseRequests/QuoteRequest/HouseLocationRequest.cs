using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Dto.UseCaseRequests.QuoteRequest
{
   public class HouseLocationRequest
    {
        public string PostalCode { get; }

        public string City { get; }

        public int ProvinceId { get; }

        public string Address { get; }

        public int AppartementUnits { get; }

        public HouseLocationRequest(string postalCode, string city, int provinceId, string address, int appartementUnits)
        {
            PostalCode = postalCode;
            City = city;
            ProvinceId = provinceId;
            Address = address;
            AppartementUnits = appartementUnits;
        }
    }
}
