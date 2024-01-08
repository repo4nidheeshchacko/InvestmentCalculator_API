using AutoMapper;
using IOC.Application;
using IOC.Domain.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IOC.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IOCController : ControllerBase
    {
        private readonly IIOCServices iocService;

        private readonly IMapper _mapper;

        public IOCController(IIOCServices iocService, IMapper mapper)
        {
            this.iocService = iocService;
            _mapper = mapper;
        }
        [Route("/error-development")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            return Problem(
                detail: exceptionHandlerFeature.Error.StackTrace,
                title: exceptionHandlerFeature.Error.Message);
        }

        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleError() =>Problem();

        // GET: api/<MembersController>
        [HttpGet]
        public ActionResult<IList<Domain.Models.InvestmentOptionsModel>> GetAllInvestmentOpions()
        {
            return Ok(this.iocService.GetAllInvestmentOpions());
        }
        [HttpPost]
        public ActionResult<InvestmentViewModel> CalculateInvestmentShares(InvestmentModel investmentData)
        {
            try
            {
                InvestmentViewModel investmentView = _mapper.Map<InvestmentViewModel>(investmentData);
                return Ok(this.iocService.CalculateInvestmentShares(investmentView));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult<RoiModel> CalculateROI(InvestmentModel investmentData)
        {
            try
            {
                InvestmentViewModel investmentView = _mapper.Map<InvestmentViewModel>(investmentData);
                return Ok(this.iocService.CalculateROI(investmentView));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
