using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses
{
    public class FinancialCapacityFindResponse : UseCaseResponseMessage
    {
        public FinancialCapacity FinancialCapacity { get; }
        public IEnumerable<Error> Errors { get; }

        public FinancialCapacityFindResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base (success, message)
        {
            Errors = errors;
        }

        public FinancialCapacityFindResponse(FinancialCapacity financialCapacity, bool success = false, string message = null) : base(success, message)
        {
            FinancialCapacity = financialCapacity;
        }
    }
}