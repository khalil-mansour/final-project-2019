using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses
{
    public class FinancialCapacityRegisterRepoResponse : UseCaseResponseMessage
    {
        public FinancialCapacity FinancialCapacity { get; }
        public IEnumerable<Error> Errors { get; }

        public FinancialCapacityRegisterRepoResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public FinancialCapacityRegisterRepoResponse(FinancialCapacity financialCapacity, bool success = false, string message = null) : base(success, message)
        {
            FinancialCapacity = financialCapacity;
        }
    }
}