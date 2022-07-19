using Microsoft.Extensions.Options;
using System.ServiceModel;
using System.Xml.Linq;
using GeometryCurrencyApp.CircleApiFeature.Models;
using CbrDailyInfo;


namespace GeometryCurrencyApp.CircleApiFeature.Services;

public class CBRCurrencyContext : ICurrencyContext
{
    public readonly EndpointAddress endpointAddress;
    public readonly BasicHttpBinding basicHttpBinding;
    public CBRCurrencyContext(IOptions<ApiSettingsModel> options)
    {
        endpointAddress = new EndpointAddress(options.Value.CurrencyApiUrl);

        basicHttpBinding =
            new BasicHttpBinding(endpointAddress.Uri.Scheme.ToLower() == "http" ?
                        BasicHttpSecurityMode.None : BasicHttpSecurityMode.Transport);

        basicHttpBinding.OpenTimeout = TimeSpan.MaxValue;
        basicHttpBinding.CloseTimeout = TimeSpan.MaxValue;
        basicHttpBinding.ReceiveTimeout = TimeSpan.MaxValue;
        basicHttpBinding.SendTimeout = TimeSpan.MaxValue;
    }

    private async Task<DailyInfoSoapClient> GetInstanceAsync()
    {
        return await Task.Run(() => new DailyInfoSoapClient(basicHttpBinding, endpointAddress));
    }

    public async Task<List<CurrencyModel>> GetCurrenciesOnDate(DateTime onDate)
    {
        List<CurrencyModel> valuteCurses = new List<CurrencyModel>();
        try
        {
            var client = await GetInstanceAsync();
            var result = await client.GetCursOnDateAsync(onDate);

            foreach (XElement node in result.Nodes)
            {
                var currencies = node.Descendants(nameof(ValuteCursOnDate));
                if (currencies == null)
                    continue;

                foreach (XElement curr in currencies)
                {
                    valuteCurses.Add(new CurrencyModel()
                    {
                        Name = curr.Element(nameof(ValuteCursOnDate.Vname))?.Value,
                        Currency = curr.Element(nameof(ValuteCursOnDate.Vcurs))?.Value,
                        CodeId = curr.Element(nameof(ValuteCursOnDate.Vcode))?.Value,
                        CodeVch = curr.Element(nameof(ValuteCursOnDate.VchCode))?.Value,
                    });
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }

        return valuteCurses;
    }
}
