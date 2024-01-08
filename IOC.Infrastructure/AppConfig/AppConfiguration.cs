using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Infrastructure.AppConfig
{
    public class AppConfiguration
    {
        public readonly string toCurrencyCode = string.Empty;
        public readonly string fromCurrencyCode = string.Empty;
        public readonly string apiUrlString = string.Empty;
        public readonly string apiKey = string.Empty;
        public AppConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            toCurrencyCode = root.GetSection("CurrencyConversionSettings").GetSection("ConvertToCurrencyCode").Value;
            fromCurrencyCode = root.GetSection("CurrencyConversionSettings").GetSection("ConvertFromCurrencyCode").Value;
            apiUrlString = root.GetSection("CurrencyConversionSettings").GetSection("APIUrlString").Value;
            apiKey = root.GetSection("CurrencyConversionSettings").GetSection("APIKey").Value;
        }
        public string GetToCurrencyCode
        {
            get => toCurrencyCode;
        }
        public string GetFromCurrencyCode
        {
            get => fromCurrencyCode;
        }
        public string GetApiUrlString
        {
            get => apiUrlString;
        }
        public string GetApiKey
        {
            get => apiKey;
        }

    }
}
