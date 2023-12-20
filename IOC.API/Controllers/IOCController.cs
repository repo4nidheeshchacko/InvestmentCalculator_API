using IOC.Application;
using IOC.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace IOC.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IOCController : ControllerBase
    {
        private readonly IIOCServices iocService;

        public IOCController(IIOCServices iocService)
        {
            this.iocService = iocService;
        }
        // GET: api/<MembersController>
        [HttpGet]
        public ActionResult<IList<Domain.Models.InvestmentOptionsModel>> GetAllInvestmentOpions()
        {
            return Ok(this.iocService.GetAllInvestmentOpions());
        }
        [HttpPost]
        public ActionResult<InvestmentMainModel> CalculateInvestmentShares(InvestmentMainModel investmentData)
        {
            try
            {
                return Ok(this.iocService.CalculateInvestmentShares(investmentData));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult<ROIModel> CalculateROI(InvestmentMainModel investmentData)
        {
            try
            {
                return Ok(this.iocService.CalculateROI(investmentData));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<ActionResult<ROIModel>> ConvertROIAmountsAsync(ROIModel roiData)
        {
            try
            {
                var validateROIDData = this.iocService.ValidateROIAmounts(roiData);
                var url = "https://api.apilayer.com/exchangerates_data/convert?to=USD&from=AUD&amount=" + roiData.ProjectedReturn;
                if (roiData.ProjectedReturn > 0)
                {
                    url = "https://api.apilayer.com/exchangerates_data/convert?to=" + roiData.CurrencyCode + "&from=AUD&amount=" + roiData.ProjectedReturn;
                    var client = new RestClient(url);
                    var request = new RestRequest(url, Method.Get);
                    request.AddHeader("apikey", "UWviu9kSHoZrmmAS6C1bj9Zm5zl4qlG2");
                    RestResponse response = await client.ExecuteAsync(request);
                    var Output = response.Content;
                    dynamic json = JsonConvert.DeserializeObject(Output);
                    roiData.ProjectedReturn = Convert.ToDecimal(json.result);
                }
                if (roiData.TotalFees > 0)
                {
                    url = "https://api.apilayer.com/exchangerates_data/convert?to=" + roiData.CurrencyCode + "&from=AUD&amount=" + roiData.TotalFees;
                    var client = new RestClient(url);
                    var request = new RestRequest(url, Method.Get);
                    request.AddHeader("apikey", "UWviu9kSHoZrmmAS6C1bj9Zm5zl4qlG2");
                    RestResponse response = await client.ExecuteAsync(request);
                    var Output = response.Content;
                    dynamic json = JsonConvert.DeserializeObject(Output);
                    roiData.TotalFees = Convert.ToDecimal(json.result);
                }
                return Ok(roiData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
