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
        public readonly string _toCurrencyCode = string.Empty;
        public readonly string _fromCurrencyCode = string.Empty;
        public readonly string _apiUrlString = string.Empty;
        public readonly string _apiKey = string.Empty;
        public AppConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            _toCurrencyCode = root.GetSection("CurrencyConversionSettings").GetSection("ConvertToCurrencyCode").Value;
            _fromCurrencyCode = root.GetSection("CurrencyConversionSettings").GetSection("ConvertFromCurrencyCode").Value;
            _apiUrlString = root.GetSection("CurrencyConversionSettings").GetSection("APIUrlString").Value;
            _apiKey = root.GetSection("CurrencyConversionSettings").GetSection("APIKey").Value;
        }
        public string getToCurrencyCode
        {
            get => _toCurrencyCode;
        }
        public string getFromCurrencyCode
        {
            get => _fromCurrencyCode;
        }
        public string getApiUrlString
        {
            get => _apiUrlString;
        }
        public string getApiKey
        {
            get => _apiKey;
        }

    }
}
