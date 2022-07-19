using Microsoft.Extensions.Options;
using System.Globalization;
using GeometryCurrencyApp.CircleApiFeature.Models;
using GeometryCurrencyApp.CircleApiFeature.Services;

namespace GeometryCurrencyApp.CircleApiFeature.Repository;

public class CurrencyRepository : ICurrencyRepository
{
    protected readonly ApiSettingsModel _config;
    protected readonly ICurrencyContext _currencyContext;
    public CurrencyRepository(IOptions<ApiSettingsModel> options, ICurrencyContext currencyContext)
    {
        _config = options.Value;
        _currencyContext = currencyContext;
    }

    public async Task<GeometryCurrencyResponseModel> GetCurrencyOnDate(double xCoord, double yCoord, CancellationToken cancellationToken)
    {
        var circleQuarter = GeometryService.GetCircleQuarter(xCoord, yCoord, _config.CircleRadius);

        if (circleQuarter == GeometryService.CircleQuarters.OutOfCircle)
        {
            return new GeometryCurrencyResponseModel() 
            { 
                Message = "Coordinates are not in circle" 
            };
        }
        
        DateTime currencyDate;
        currencyDate = circleQuarter switch
        {
            GeometryService.CircleQuarters.TopRight => DateTime.Now,
            GeometryService.CircleQuarters.TopLeft => DateTime.Now.AddDays(-1),
            GeometryService.CircleQuarters.BottomLeft => DateTime.Now.AddDays(-2),
            GeometryService.CircleQuarters.BottomRight => DateTime.Now.AddDays(1),
        };

        var currencies = await _currencyContext.GetCurrenciesOnDate(currencyDate);

        if (!currencies.Any())
        {
            return new GeometryCurrencyResponseModel()
            {
                Message = $"Currencies are not available for {currencyDate.Date.ToString("dd.MM.yyyy")}"
            };
        }        

        var currency = currencies.Where(x => x.CodeVch.Equals(_config.CurrencyCode, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

        if (currency != null && double.TryParse(currency.Currency,
                            NumberStyles.Currency,
                            CultureInfo.InvariantCulture,
                            out double curs))
        {
            return new GeometryCurrencyResponseModel() 
            { 
                Currency = curs 
            };
        }
        else
        {
            return new GeometryCurrencyResponseModel()
            {
                Message = $"{_config.CurrencyCode} currency are not available for {currencyDate.Date.ToString("dd.MM.yyyy")}"
            };
        }
    }
}
