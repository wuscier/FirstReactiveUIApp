using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz
{
    class Program
    {
       static Action<long> fizzBuzz = x => {
            Console.WriteLine($"{x} - ");

            if (x % 3 == 0)
            {
                Console.Write("fizee ");

            }

            if (x % 5 == 0)
            {
                Console.Write("buzz");
            }

            Console.WriteLine();

        };

        static void Main(string[] args)
        {

            var query = from number in Enumerable.Range(1, 5)
                        select number;


            var observableQuery = query.ToObservable();

            observableQuery.Subscribe(Console.WriteLine, () => { Console.WriteLine("I'm Done!!!"); });



            //Observable.Range(1, 100).TakeLast(10).Skip(5)
            //    .Subscribe(x => fizzBuzz(x), ex => { Console.WriteLine(ex.Message); }, () => { Console.WriteLine("Done..............."); });
            //Observable.Range(1, 100)
            //    .Subscribe(x => fizzBuzz(x), ex => { Console.WriteLine(ex.Message); }, () => { Console.WriteLine("Done..............."); });

            //var f = new Subject<int>();

            //f.Subscribe(x => Console.WriteLine($"-- {x} --"));

            //f.OnNext(1000);
            //f.OnNext(2000);
            //f.OnNext(3000);

            Console.ReadLine();
        }
    }
}
