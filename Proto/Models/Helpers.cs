using System;

namespace Models
{
    public static class Helpers
    {
        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            double number = random.NextDouble() * (maximum - minimum) + minimum;
            return Math.Round(number, 3);
        }

        public static int GetRandomInteger(int minimum, int maximum)
        {
            Random random = new Random();
            int integer = random.Next(minimum, maximum);
            return integer;
        }
    }
}
