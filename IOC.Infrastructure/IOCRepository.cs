using IOC.Application;
using IOC.Domain.Enums;
using IOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
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
        public Domain.Models.InvestmentMainModel CalculateInvestmentShares(InvestmentMainModel investmentData)
        {
            decimal InvestmentAmount = investmentData.InvestmentAmount;
            decimal TotalInvestedAmount = 0;
            foreach (InvestmentShareModel row in investmentData.InvestmentShares)
            {
                decimal investmentSharePercentage = row.InvestmentPercentage;
                decimal investmentShareAmount = InvestmentAmount * (investmentSharePercentage / 100);
                row.InvestedAmount = Convert.ToDecimal(investmentShareAmount);
                TotalInvestedAmount = TotalInvestedAmount + investmentShareAmount;
            }
            investmentData.AvailableAmount = Convert.ToDecimal(InvestmentAmount - TotalInvestedAmount);
            return investmentData;
        }
        public Domain.Models.ROIModel CalculateROI(InvestmentMainModel investmentData)
        {
            ROIModel roiModel = new ROIModel();
            decimal InvestmentAmount = investmentData.InvestmentAmount;
            decimal TotalInvestedAmount = 0;
            decimal TotalCalculatedROI = 0;
            decimal TotalAssociatedFees = 0;
            foreach (InvestmentShareModel row in investmentData.InvestmentShares)
            {
                decimal investmentSharePercentage = row.InvestmentPercentage;
                decimal investmentShareAmount = InvestmentAmount * (investmentSharePercentage / 100);
                TotalInvestedAmount = TotalInvestedAmount + investmentShareAmount;
                decimal CalculatedROI = 0;
                decimal AssociatedFees = 0;
                if (row.InvestmentOptionId == Convert.ToInt32(InvestmentOptionsEnum.CashInvestments)) //Cash Investment
                {
                    if (investmentSharePercentage <= 50)
                    {
                        CalculatedROI = InvestmentAmount * Convert.ToDecimal((8.5 / 100));
                        AssociatedFees = CalculatedROI * Convert.ToDecimal((0.5 / 100));
                    }
                    else if (investmentSharePercentage > 50) //Considering only > condition as = condition is already done in the above loop
                    {
                        CalculatedROI = InvestmentAmount * Convert.ToDecimal((10 / 100));
                        AssociatedFees = 0;
                    }
                }
                else if (row.InvestmentOptionId == Convert.ToInt32(InvestmentOptionsEnum.FixedInterest)) //Fixed Interest
                {
                    CalculatedROI = InvestmentAmount * Convert.ToDecimal((10 / 100));
                    AssociatedFees = CalculatedROI * Convert.ToDecimal((1 / 100));
                }
                else if (row.InvestmentOptionId == Convert.ToInt32(InvestmentOptionsEnum.Shares)) //Shares
                {
                    if (investmentSharePercentage <= 70)
                    {
                        CalculatedROI = InvestmentAmount * Convert.ToDecimal((4.3 / 100));
                    }
                    else if (investmentSharePercentage > 70)
                    {
                        CalculatedROI = InvestmentAmount * Convert.ToDecimal((6 / 100));
                    }
                    AssociatedFees = CalculatedROI * Convert.ToDecimal((2.5 / 100));
                }
                else if (row.InvestmentOptionId == Convert.ToInt32(InvestmentOptionsEnum.ManagedFunds)) //Managed Funds
                {
                    CalculatedROI = InvestmentAmount * Convert.ToDecimal((12 / 100));
                    AssociatedFees = CalculatedROI * Convert.ToDecimal((0.3 / 100));
                }
                else if (row.InvestmentOptionId == Convert.ToInt32(InvestmentOptionsEnum.ExchangeTradedFunds)) //Exchange Traded Funds
                {
                    if (investmentSharePercentage <= 40)
                    {
                        CalculatedROI = InvestmentAmount * Convert.ToDecimal((12.8 / 100));
                    }
                    else if (investmentSharePercentage > 40)
                    {
                        CalculatedROI = InvestmentAmount * Convert.ToDecimal((25 / 100));
                    }
                    AssociatedFees = CalculatedROI * Convert.ToDecimal((2 / 100));
                }
                else if (row.InvestmentOptionId == Convert.ToInt32(InvestmentOptionsEnum.InvestementBonds)) //Investement Bonds
                {
                    CalculatedROI = InvestmentAmount * Convert.ToDecimal((8 / 100));
                    AssociatedFees = CalculatedROI * Convert.ToDecimal((0.9 / 100));
                }
                else if (row.InvestmentOptionId == Convert.ToInt32(InvestmentOptionsEnum.Annuities)) //Annuities
                {
                    CalculatedROI = InvestmentAmount * Convert.ToDecimal((4 / 100));
                    AssociatedFees = CalculatedROI * Convert.ToDecimal((1.4 / 100));
                }
                else if (row.InvestmentOptionId == Convert.ToInt32(InvestmentOptionsEnum.ListedInvestmentCompanies)) //Listed Investment Companies
                {
                    CalculatedROI = InvestmentAmount * Convert.ToDecimal((6 / 100));
                    AssociatedFees = CalculatedROI * Convert.ToDecimal((1.3 / 100));
                }
                else if (row.InvestmentOptionId == Convert.ToInt32(InvestmentOptionsEnum.RealEstateInvestmentTrusts)) //Real Estate Investment Trusts
                {
                    CalculatedROI = InvestmentAmount * Convert.ToDecimal((4 / 100));
                    AssociatedFees = CalculatedROI * Convert.ToDecimal((2 / 100));
                }
                TotalCalculatedROI = TotalCalculatedROI + CalculatedROI;
                TotalAssociatedFees = TotalAssociatedFees + AssociatedFees;
            }
            roiModel.ProjectedReturn = Convert.ToDecimal(TotalInvestedAmount + TotalCalculatedROI);
            roiModel.TotalFees = Convert.ToDecimal(TotalAssociatedFees);
            return roiModel;
        }
    }
}
