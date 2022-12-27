using System;
using System.Numerics;

class ShapeMath
{

    public static (Vector2, Vector2) CalculateTrianglePoints(double centerX, double centerY, double radius, double baseLength, Vector2 targetPoint)
    {
        double height = Math.Sqrt(radius * radius - baseLength * baseLength / 4);

        // Calculate the angle between the base of the triangle and the x-axis
        double angle = Math.Atan2(targetPoint.Y - centerY, targetPoint.X - centerX) - (Math.PI / 180) * 90;

        // Calculate the coordinates of the two points
        double x1 = centerX - baseLength / 2 * Math.Cos(angle) - height * Math.Sin(angle);
        double y1 = centerY - baseLength / 2 * Math.Sin(angle) + height * Math.Cos(angle);
        double x2 = centerX + baseLength / 2 * Math.Cos(angle) - height * Math.Sin(angle);
        double y2 = centerY + baseLength / 2 * Math.Sin(angle) + height * Math.Cos(angle);

        return (new Vector2((float)x1, (float)y1), new Vector2((float)x2, (float)y2));
    }
}