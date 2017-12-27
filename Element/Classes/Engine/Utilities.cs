using Microsoft.Xna.Framework;
using Element.Classes;
using System;

namespace Element
{
    public static class Utilities
    {
        public static int GetCardinalDirection(Vector2 vector)
        {
            int cardinal = 0;

            if (vector.X < 0)
                cardinal += Cardinal.West;

            if (vector.X > 0)
                cardinal += Cardinal.East;

            if (vector.Y > 0)
                cardinal += Cardinal.North;

            if (vector.Y < 0)
                cardinal += Cardinal.South;

            return cardinal;
        }

        public static double GetAngleFromVectors(Vector2 p1, Vector2 p2)
        {
            float xDiff = p2.X - p1.X;
            float yDiff = p2.Y - p1.Y;
            return Math.Atan2(yDiff, xDiff) * (180 / Math.PI);
        }

        public static Vector2 GetVectorFromAngle(double angle)
        {
            return new Vector2((float)Math.Cos(angle), -(float)Math.Sin(angle));
        }
    }
}

