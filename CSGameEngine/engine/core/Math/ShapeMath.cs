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

    public static Vector2 GetPositionAroundCircle(float radius, float x, float y, float percentage)
    {
        double posX = x + radius * Math.Cos(2 * Math.PI * percentage);
        double posY = y + radius * Math.Sin(2 * Math.PI * percentage);

        return new Vector2((float)posX, (float)posY);
    }

    public static float GetRotation(double percentage, double circleX, double circleY, double radius, double targetX, double targetY)
    {
        // Calculate the position of the rectangle on the circle
        var x = circleX + radius * Math.Cos(2 * Math.PI * percentage);
        var y = circleY + radius * Math.Sin(2 * Math.PI * percentage);
        var rectanglePosition = new Vector2((float)x, (float)y);

        // Calculate the direction vector from the rectangle's position to the target location
        var direction = new Vector2((float)targetX, (float)targetY) - rectanglePosition;

        // Find the angle of the direction vector
        var angle = Math.Atan2(direction.Y, direction.X);

        // Convert the angle to degrees
        var angleInDegrees = angle * 180 / Math.PI;

        // Normalize the angle to a value between 0 and 360 degrees
        var normalizedAngle = angleInDegrees % 360;
        if (normalizedAngle < 0)
        {
            normalizedAngle += 360;
        }

        // Return the normalized angle as the rotation of the rectangle's top right corner
        return (float)normalizedAngle;
    }
}