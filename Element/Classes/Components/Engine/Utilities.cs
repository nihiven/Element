using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Element.Classes;
using System;
using System.Collections.Generic;

namespace Element
{
    public static class Utilities
    {
        private static Texture2D rect;
        public static Random SeededRand = new Random(Guid.NewGuid().GetHashCode());

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

        public static void DrawRectangle(Rectangle coords, Color color, SpriteBatch spriteBatch)
        {
            if (rect == null)
            {
                rect = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                rect.SetData(new[] { Color.White });
            }

            spriteBatch.Draw(rect, coords, color);
        }

        public static T GetRandomListMember<T>(List<T> list)
        {
            return list[SeededRand.Next(0, list.Count - 1)];
        }

        public static Rectangle ClampRect(Rectangle rect, Rectangle constraint)
        {
            if (!constraint.Contains(rect))
            {
                float x = (rect.X < constraint.X) ? constraint.X : rect.X;
                float y = (rect.Y < constraint.Y) ? constraint.Y : rect.Y;

                x = rect.Right > constraint.Right ? constraint.Right - rect.Width : rect.X;
                y = rect.Bottom > constraint.Bottom ? constraint.Right - rect.Height : rect.Y;

                return new Rectangle((int)x, (int)y, rect.Width, rect.Height);
            }
            else
                return rect;
        }
    }
}

