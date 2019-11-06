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

        public string ApartmentUnit { get; }

        public HouseLocationRequest(string postalCode, string city, int provinceId, string address, string apartmentUnit)
        {
            PostalCode = postalCode;
            City = city;
            ProvinceId = provinceId;
            Address = address;
            ApartmentUnit = apartmentUnit;
        }
    }
}
