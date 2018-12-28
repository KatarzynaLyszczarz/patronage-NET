using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FizzBuzz.Methods
{
    public static partial class Methods
    {
        /// <summary>
        /// Print Fizz when value is divisible by 2 and/or Buzz for 3.
        /// </summary>
        /// <param name="value">It must be beetwen 0-1000</param>
        /// <returns>Fizz, Buzz, FizzBuzz or empty.</returns>
        public static string FizzBuzz(int value)
        {
            var result="";
            if (value < 0 || value > 1000)
            {
                throw new ArgumentOutOfRangeException();
            }
            
            if (value % 2 == 0) { result += "Fizz"; }
            if (value % 3 == 0) { result += "Buzz"; }

            return result;
        }
    }
}
