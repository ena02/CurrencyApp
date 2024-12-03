using HtmlAgilityPack;
using System.Globalization;

namespace CurrencyApp.Services
{
    public class CurrencyRateService
    {
        private readonly HttpClient _httpClient;

        public CurrencyRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> GetUsdToKztRateAsync(string url, string xPath)
        {
            // Загружаем HTML-страницу
            var html = await _httpClient.GetStringAsync(url);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Используем полный XPath для извлечения курса
            var rateNode = doc.DocumentNode.SelectSingleNode(xPath);

            if (rateNode != null)
            {
                var rateText = rateNode.InnerText.Trim();

                // Преобразуем текст в decimal
                rateText = rateText.Replace(",", ".");
                if (decimal.TryParse(rateText, NumberStyles.Any, CultureInfo.InvariantCulture, out var rate))
                {
                    return rate;
                }

                throw new Exception($"Невозможно преобразовать '{rateText}' в число.");
            }

            throw new Exception("Не найден элемент по указанному XPath.");
        }
    }
}
