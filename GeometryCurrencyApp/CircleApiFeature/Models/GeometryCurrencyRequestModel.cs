using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace GeometryCurrencyApp.CircleApiFeature.Models;


public class GeometryCurrencyRequestModel
{
    [BindRequired]
    public double XCoord { get; set; }
    [BindRequired]
    public double YCoord { get; set; }
}
