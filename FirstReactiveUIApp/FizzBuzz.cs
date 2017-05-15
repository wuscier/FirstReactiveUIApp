using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstReactiveUIApp
{
    public class FizzBuzz
    {
        Action<long> fizzBuzz = x =>
        {
            Console.WriteLine($"{x} - ");

            if (x % 3 == 0)
            {
                Console.WriteLine("fizee ");

            }

            if (x % 5 == 0)
            {
                Console.WriteLine("buzz");
            }

            Console.WriteLine();
        };

        
    }
}
