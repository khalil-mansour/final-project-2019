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
        private readonly IFinancialCapacityFindUseCase _financialCapacityFindUseCase;


        public FinancialCapacityController(
            IFinancialCapacityRegisterUseCase financialCapacityRegisterUseCase,
            IFinancialCapacityFindUseCase financialCapacityFindUseCase)
        {
            _financialCapacityRegisterUseCase = financialCapacityRegisterUseCase;
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

            var presenter = new FinancialCapacityRegisterPresenter();
            await _financialCapacityRegisterUseCase.HandleAsync(
                new FinancialCapacityRegisterRequest(request.User_Id, 
                    request.Annual_Income, 
                    request.Down_Payment, 
                    request.Mensual_Debt, 
                    request.Interest_Rate, 
                    request.Municipal_Taxes, 
                    request.Heating_Cost, 
                    request.Condo_Fee),
                presenter);
            return presenter.ContentResult;
        }

        [HttpPost("api/financial/find")]
        public async Task<ActionResult> Find([FromBody] Models.Request.FinancialCapacityFindRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var presenter = new FinancialCapacityFindPresenter();
            await _financialCapacityFindUseCase.HandleAsync(new FinancialCapacityFindRequest(request.Id), presenter);
            return presenter.ContentResult;
        }

    }
}
