using GeometryCurrencyApp.CircleApiFeature.Models;

namespace GeometryCurrencyApp.CircleApiFeature.Services;

public interface ICurrencyContext
{
    Task<List<CurrencyModel>> GetCurrenciesOnDate(DateTime onDate);
}
