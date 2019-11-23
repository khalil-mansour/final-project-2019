using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Presenters;
using Web.Api.Core.Dto.UseCaseRequests;
using Microsoft.AspNetCore.Authorization;

namespace Web.Api.Controllers
{
    [ApiController]
    public class FinancialCapacityController : ControllerBase
    {
        private readonly IFinancialCapacityRegisterUseCase _financialCapacityRegisterUseCase;
        private readonly FinancialCapacityRegisterPresenter _financialCapacityRegisterPresenter;
        private readonly IFinancialCapacityFindUseCase _financialCapacityFindUseCase;
        private readonly FinancialCapacityFindPresenter _financialCapacityFindPresenter;


        public FinancialCapacityController(
            IFinancialCapacityRegisterUseCase financialCapacityRegisterUseCase,
            FinancialCapacityRegisterPresenter financialCapacityRegisterPresenter,
            IFinancialCapacityFindUseCase financialCapacityFindUseCase, 
            FinancialCapacityFindPresenter financialCapacityFindPresenter)
        {
            _financialCapacityRegisterUseCase = financialCapacityRegisterUseCase;
            _financialCapacityRegisterPresenter = financialCapacityRegisterPresenter;
            _financialCapacityFindPresenter = financialCapacityFindPresenter;
            _financialCapacityFindUseCase = financialCapacityFindUseCase;

        }


        /// <summary>
        /// Creates the financial capacity.
        /// </summary>
        /// <param></param>
        /// <returns>A newly created financial capacity</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>    
        [HttpPost("api/financial")]
        public async Task<ActionResult> Register([FromBody] Models.Request.FinancialCapacityRegisterRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }
            await _financialCapacityRegisterUseCase.Handle(
                new FinancialCapacityRegisterRequest(request.Id, 
                    request.AnnualIncome, 
                    request.DownPayment, 
                    request.MensualDebt, 
                    request.InterestRate, 
                    request.MunicipalTaxes, 
                    request.HeatingCost, 
                    request.CondoFee), 
                _financialCapacityRegisterPresenter);
            return _financialCapacityRegisterPresenter.ContentResult;
        }

        [HttpPost("api/financial/find")]
        public async Task<ActionResult> Find([FromBody] Models.Request.FinancialCapacityFindRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _financialCapacityFindUseCase.Handle(new FinancialCapacityFindRequest(request.Id), _financialCapacityFindPresenter);
            return _financialCapacityFindPresenter.ContentResult;
        }

    }
}
