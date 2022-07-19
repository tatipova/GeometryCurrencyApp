using MediatR;
using GeometryCurrencyApp.CircleApiFeature.Models;

namespace GeometryCurrencyApp.CircleApiFeature.Query;

public class GetCurrencyInCircleQuery : IRequest<GeometryCurrencyResponseModel>
{
    public double XCoord { get; set; }
    public double YCoord { get; set; }
}
