namespace GeometryCurrencyApp.CircleApiFeature.Models;

public class ApiSettingsModel
{
    /// <summary>
    /// SOAP service URL, e.g. http://www.cbr.ru/DailyInfoWebServ/DailyInfo.asmx
    /// </summary>
    public string CurrencyApiUrl { get; init; }

    /// <summary>
    /// Cuurency code ISO 4217 
    /// <see cref="https://en.wikipedia.org/wiki/ISO_4217"/>
    /// </summary>
    public string CurrencyCode { get; init; }

    /// <summary>
    /// Positive value of circle defining currency date 
    /// </summary>
    public double CircleRadius { get; init; }
}

