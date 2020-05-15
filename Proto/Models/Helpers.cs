using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public static class Helpers
    {
        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
