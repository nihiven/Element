using System;

namespace Element.Classes
{
    public static class Cardinal
    {
        // TODO: Should this be bitwise?
        public const int North = 1;
        public const int Northeast = 11;
        public const int East = 10;
        public const int Southeast = 12;
        public const int South = 2;
        public const int Southwest = 22;
        public const int West = 20;
        public const int Northwest = 21;

        public static string String(int cardinal)
        {
            switch (cardinal)
            {
                case 1:
                    return "North";
                case 11:
                    return "Northeast";
                case 10:
                    return "East";
                case 12:
                    return "Southeast";
                case 2:
                    return "South";
                case 22:
                    return "Southwest";
                case 20:
                    return "West";
                case 21:
                    return "Northwest";
                case 0:
                    return "None";
                default:
                    throw new ArgumentOutOfRangeException("cardinal");
            }
        }
    }

}
