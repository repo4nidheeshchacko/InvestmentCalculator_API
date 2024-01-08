using IOC.Application;
using IOC.Domain.Enums;
using IOC.Domain.Models;
using IOC.Infrastructure.AppConfig;
using IOC.Infrastructure.ROICalculations;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace IOC.Infrastructure
{
    public class IOCRepository : IIOCRepository
    {
        public static List<Domain.Models.InvestmentOptionsModel> investmentOptions = new List<Domain.Models.InvestmentOptionsModel>()
        {
           new Domain.Models.InvestmentOptionsModel{  Id =1 ,Name= "Cash Investments"},
           new Domain.Models.InvestmentOptionsModel{  Id =2 ,Name= "Fixed Interest"},
           new Domain.Models.InvestmentOptionsModel{  Id =3 ,Name= "Shares"},
           new Domain.Models.InvestmentOptionsModel{  Id =4 ,Name= "Managed Funds"},
           new Domain.Models.InvestmentOptionsModel{  Id =5 ,Name= "Exchanged Traded Funds"},
           new Domain.Models.InvestmentOptionsModel{  Id =6 ,Name= "Investment Bonds"},
           new Domain.Models.InvestmentOptionsModel{  Id =7 ,Name= "Annuities"},
           new Domain.Models.InvestmentOptionsModel{  Id =8 ,Name= "Listed Investment Companies"},
           new Domain.Models.InvestmentOptionsModel{  Id =9 ,Name= "Real Estate Investment Trusts"}
        };
        public List<Domain.Models.InvestmentOptionsModel> GetAllInvestmentOpions()
        {
            return investmentOptions;
        }
        public Domain.Models.InvestmentViewModel CalculateInvestmentShares(InvestmentViewModel investmentData)
        {
            investmentData.AvailableAmount = Convert.ToDecimal(investmentData.InvestmentAmount);
            foreach (InvestmentShareViewModel row in investmentData.InvestmentShares)
            {
                decimal investmentShareAmount = investmentData.AvailableAmount * (row.InvestmentPercentage / 100);
                row.InvestedAmount = Convert.ToDecimal(investmentShareAmount.ToString("F2"));
                investmentData.AvailableAmount = Convert.ToDecimal(investmentData.AvailableAmount - row.InvestedAmount);
            }
            //Formating to decimal places
            investmentData.InvestmentAmount = Convert.ToDecimal(investmentData.InvestmentAmount.ToString("F2"));
            investmentData.AvailableAmount = Convert.ToDecimal(investmentData.AvailableAmount.ToString("F2"));
            return investmentData;
        }
        public Domain.Models.RoiModel CalculateROI(InvestmentViewModel investmentData)
        {
            RoiModel roiModel = new RoiModel();
            try
            {
                decimal investmentAmount = investmentData.InvestmentAmount;
                decimal totalInvestedAmount = 0;
                decimal totalCalculatedROI = 0;
                decimal totalAssociatedFees = 0;
                foreach (InvestmentShareModel row in investmentData.InvestmentShares)
                {
                    decimal investmentSharePercentage = row.InvestmentPercentage;
                    decimal investmentShareAmount = investmentAmount * (investmentSharePercentage / 100);
                    totalInvestedAmount = totalInvestedAmount + investmentShareAmount;
                    RoiCalculateInputModel roiCalculateInput = new RoiCalculateInputModel{ InvestmentAmount = investmentData.InvestmentAmount, InvestmentPercentage = investmentSharePercentage, InvestmentOptionId= row.InvestmentOptionId };
                    RoiCalculateModel roiCalculated = RoiCalculator.GetRoiCalculator(roiCalculateInput).Calculate(roiCalculateInput);
                    Thread.Sleep(200);
                    totalCalculatedROI = totalCalculatedROI + roiCalculated.CalculatedROI;
                    totalAssociatedFees = totalAssociatedFees + roiCalculated.AssociatedFees;
                }
                roiModel.ProjectedReturn = Convert.ToDecimal((totalInvestedAmount + totalCalculatedROI).ToString("F2"));
                roiModel.TotalFees = Convert.ToDecimal(totalAssociatedFees.ToString("F2"));
                //Calling API to convert currency
                AppConfiguration config = new AppConfiguration();
                roiModel.ConvertedCurrencyCode = config.GetToCurrencyCode;
                if (roiModel.ProjectedReturn > 0)
                {
                    var url = config.GetApiUrlString;
                    url = url.Replace("{{from}}", config.GetFromCurrencyCode); //replace {{from}} in the api url
                    url = url.Replace("{{to}}", roiModel.ConvertedCurrencyCode); //replace {{to}} in the api url
                    url = url.Replace("{{amount}}", roiModel.ProjectedReturn.ToString("F2")); //replace {{amount}} in the api url
                    var request = WebRequest.Create(url);
                    request.Method = "GET";
                    request.Headers.Add("apikey", config.GetApiKey);
                    var webResponse = request.GetResponse();
                    var webStream = webResponse.GetResponseStream();

                    var reader = new StreamReader(webStream);
                    var data = reader.ReadToEnd();
                    dynamic json = JsonConvert.DeserializeObject(data);
                    roiModel.ConvertedROI = Convert.ToDecimal(json.result.ToString("F2"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return roiModel;
        }
    }
}
