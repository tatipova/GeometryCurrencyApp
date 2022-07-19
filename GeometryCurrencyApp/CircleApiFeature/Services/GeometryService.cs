namespace GeometryCurrencyApp.CircleApiFeature.Services;

public class GeometryService
{
    public enum CircleQuarters
    { 
        OutOfCircle = 0,
        TopRight = 1,
        TopLeft = 2,
        BottomLeft = 3,
        BottomRight = 4,
    }

    public static CircleQuarters GetCircleQuarter(double xCoord, double yCoord, double circleRadius)
    {
        if (Math.Pow(xCoord, 2) + Math.Pow(yCoord, 2) > Math.Pow(circleRadius, 2) || circleRadius <= 0)
        {
            return CircleQuarters.OutOfCircle;
        }

        if (xCoord >= 0 && yCoord >= 0)
        {
            return CircleQuarters.TopRight;
        }
        else if (xCoord < 0 && yCoord >= 0)
        {
            return CircleQuarters.TopLeft;
        }
        else if (xCoord < 0 && yCoord < 0)
        {
            return CircleQuarters.BottomLeft;
        }
        else 
        {
            return CircleQuarters.BottomRight;
        }
    }
}
