using System;

namespace Element.Classes
{
    public static class Cardinal
    {
        public static int North = 1;
        public static int Northeast = 11;
        public static int East = 10;
        public static int Southeast = 12;
        public static int South = 2;
        public static int Southwest = 22;
        public static int West = 20;
        public static int Northwest = 21;

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
