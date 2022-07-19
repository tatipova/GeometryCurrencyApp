using GeometryCurrencyApp.CircleApiFeature.Models;

namespace GeometryCurrencyApp.CircleApiFeature.Repository;

public interface ICurrencyRepository
{
    Task<GeometryCurrencyResponseModel> GetCurrencyOnDate(double xCoord, double yCoord, CancellationToken cancellationToken);
}
