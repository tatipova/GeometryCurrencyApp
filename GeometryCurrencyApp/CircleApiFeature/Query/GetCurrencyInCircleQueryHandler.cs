using MediatR;
using GeometryCurrencyApp.CircleApiFeature.Models;
using GeometryCurrencyApp.CircleApiFeature.Repository;

namespace GeometryCurrencyApp.CircleApiFeature.Query;

public class GetCurrencyInCircleQueryHandler : IRequestHandler<GetCurrencyInCircleQuery, GeometryCurrencyResponseModel>
{
    private readonly ICurrencyRepository _currencyRepository;

    public GetCurrencyInCircleQueryHandler(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task<GeometryCurrencyResponseModel> Handle(GetCurrencyInCircleQuery request, CancellationToken cancellationToken)
    {
        return await _currencyRepository.GetCurrencyOnDate(request.XCoord, request.YCoord, cancellationToken);
    }
}
