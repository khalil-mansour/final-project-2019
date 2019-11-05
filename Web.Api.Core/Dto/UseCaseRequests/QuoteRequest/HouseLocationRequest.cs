using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Dto.UseCaseRequests.QuoteRequest
{
   public class HouseLocationRequest
    {
        public string PostalCode { get; }

        public string City { get; }

        public string Province { get; }

        public string Address { get; }

        public int AppartementUnits { get; }

        public HouseLocationRequest(string postalCode, string city, string province, string address, int appartementUnits)
        {
            PostalCode = postalCode;
            City = city;
            Province = province;
            Address = address;
            AppartementUnits = appartementUnits;
        }
    }
}
