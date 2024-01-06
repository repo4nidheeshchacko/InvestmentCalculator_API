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
        public static List<Domain.Models.investmentOptionsModel> investmentOptions = new List<Domain.Models.investmentOptionsModel>()
        {
           new Domain.Models.investmentOptionsModel{  id =1 ,name= "Cash Investments"},
           new Domain.Models.investmentOptionsModel{  id =2 ,name= "Fixed Interest"},
           new Domain.Models.investmentOptionsModel{  id =3 ,name= "Shares"},
           new Domain.Models.investmentOptionsModel{  id =4 ,name= "Managed Funds"},
           new Domain.Models.investmentOptionsModel{  id =5 ,name= "Exchanged Traded Funds"},
           new Domain.Models.investmentOptionsModel{  id =6 ,name= "Investment Bonds"},
           new Domain.Models.investmentOptionsModel{  id =7 ,name= "Annuities"},
           new Domain.Models.investmentOptionsModel{  id =8 ,name= "Listed Investment Companies"},
           new Domain.Models.investmentOptionsModel{  id =9 ,name= "Real Estate Investment Trusts"}
        };
        public List<Domain.Models.investmentOptionsModel> getAllInvestmentOpions()
        {
            return investmentOptions;
        }
        public Domain.Models.investmentViewModel calculateInvestmentShares(investmentViewModel investmentData)
        {
            investmentData.availableAmount = Convert.ToDecimal(investmentData.investmentAmount);
            foreach (investmentShareViewModel row in investmentData.investmentShares)
            {
                decimal investmentShareAmount = investmentData.availableAmount * (row.investmentPercentage / 100);
                row.investedAmount = Convert.ToDecimal(investmentShareAmount.ToString("F2"));
                investmentData.availableAmount = Convert.ToDecimal(investmentData.availableAmount - row.investedAmount);
            }
            //Formating to decimal places
            investmentData.investmentAmount = Convert.ToDecimal(investmentData.investmentAmount.ToString("F2"));
            investmentData.availableAmount = Convert.ToDecimal(investmentData.availableAmount.ToString("F2"));
            return investmentData;
        }
        public Domain.Models.roiModel calculateROI(investmentViewModel investmentData)
        {
            roiModel roiModel = new roiModel();
            try
            {
                decimal investmentAmount = investmentData.investmentAmount;
                decimal totalInvestedAmount = 0;
                decimal totalCalculatedROI = 0;
                decimal totalAssociatedFees = 0;
                foreach (investmentShareModel row in investmentData.investmentShares)
                {
                    decimal investmentSharePercentage = row.investmentPercentage;
                    decimal investmentShareAmount = investmentAmount * (investmentSharePercentage / 100);
                    totalInvestedAmount = totalInvestedAmount + investmentShareAmount;
                    roiCalculateInputModel roiCalculateInput = new roiCalculateInputModel{ investmentAmount = investmentData.investmentAmount, investmentPercentage = investmentSharePercentage, investmentOptionId= row.investmentOptionId };
                    roiCalculateModel roiCalculated = roiCalculator.getRoiCalculator(roiCalculateInput).calculate(roiCalculateInput);
                    Thread.Sleep(200);
                    totalCalculatedROI = totalCalculatedROI + roiCalculated.calculatedROI;
                    totalAssociatedFees = totalAssociatedFees + roiCalculated.associatedFees;
                }
                roiModel.projectedReturn = Convert.ToDecimal((totalInvestedAmount + totalCalculatedROI).ToString("F2"));
                roiModel.totalFees = Convert.ToDecimal(totalAssociatedFees.ToString("F2"));
                //Calling API to convert currency
                AppConfiguration config = new AppConfiguration();
                roiModel.convertedCurrencyCode = config.getToCurrencyCode;
                if (roiModel.projectedReturn > 0)
                {
                    var url = config.getApiUrlString+"?to=" + roiModel.convertedCurrencyCode + "&from="+ config.getFromCurrencyCode + "&amount=" + roiModel.projectedReturn;
                    var request = WebRequest.Create(url);
                    request.Method = "GET";
                    request.Headers.Add("apikey", config.getApiKey);
                    var webResponse = request.GetResponse();
                    var webStream = webResponse.GetResponseStream();

                    var reader = new StreamReader(webStream);
                    var data = reader.ReadToEnd();
                    dynamic json = JsonConvert.DeserializeObject(data);
                    roiModel.convertedROI = Convert.ToDecimal(json.result.ToString("F2"));
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
