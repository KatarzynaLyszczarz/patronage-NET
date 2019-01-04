using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FizzBuzz
{
    public interface IMockyService
    {
        Task<string> GetContent();
    }
}
