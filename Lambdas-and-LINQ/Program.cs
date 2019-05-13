using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lambdas_and_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //Lambda
            //1.An expression which does not return a value, takes no parameters, and evaluates to the string "Hello, World".
            Func<string> helloworld = () => "Hello, World";
            Console.WriteLine(helloworld());

            //2. An expression which returns an integer, takes a single integer parameter, and adds the integer 1 to it.
            Func<int, int> AddOne = x => x + 1;
            Console.WriteLine(AddOne(65535));

            //3. An expression which returns an integer, takes two integer parameters, and raises the second parameter to the power of the first.
            Func<int, int, int> Pow = (x, y) => (int)Math.Pow(y, x);
            Console.WriteLine(Pow(5,3));

            //4. An expression which returns an integer, takes two integer parameters and sums them.
            Func<int, int, int> Sum = (x, y) => x + y;
            Console.WriteLine(Sum(4,9));

            //5. An expression which returns a string, takes two strings, and appends the first to the second.
            Func<string, string, string> Append = (x, y) => y + x;
            Console.WriteLine(Append("first","second"));

            //LINQ
            var numlist = Enumerable.Range(0, 21);
            var wordlist = new[]{"lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                            "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                            "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"};

            Console.WriteLine("Original Num List:");
            numlist.ToList().ForEach(x => Console.Write($"{x} "));
            Console.WriteLine();


            Console.WriteLine("Add One:");
            numlist.Select(AddOne).ToList().ForEach(x => Console.Write($"{x} "));
            Console.WriteLine();

            Console.WriteLine("Raise to Power of 2");
            numlist.Select(x => Pow(2, x)).ToList().ForEach(x => Console.Write($"{x} "));
            Console.WriteLine();

            Console.WriteLine("Concat strings");
            Console.WriteLine(wordlist.Aggregate(Append));
            Console.WriteLine();

            Console.WriteLine("repeated exponentiation, also known as tetration");
            Func<int, int, int> tetration = (x, y) => {
                int res=x;
                for (int i = 0; i < y; i++)
                {
                    res = Pow(x, res);
                }
                return res;
            };

            Func<int, int, int> tetration2 = (x, y) =>
                 Enumerable.Range(0, y-1).Aggregate(seed: x,func: (k, index) => Pow(k, x));

            Func<int, int, int> tetration1 = (x, y) =>
                Enumerable.Repeat(x, y).Aggregate(func: (k, index) => Pow(k, x));

            Console.WriteLine("Printing 2↑↑4");
            Console.WriteLine(tetration2(2,4));
            Console.WriteLine("Printing 3↑↑2");
            Console.WriteLine(tetration2(3,2));


            Func<BigInteger, BigInteger, BigInteger> PowBig = (x, y) => Enumerable.Repeat(y, (int)x).Aggregate((a,b)=>BigInteger.Multiply(a, b));
            Func<BigInteger, int, BigInteger> TetrationBig = (x, y) =>
                Enumerable.Range(0, y - 1).Aggregate(seed:x, func: (k, index) => PowBig(k,x));
            Func<BigInteger, int, BigInteger> TetrationBig2 = (x, y) =>
                Enumerable.Repeat(x, y).Aggregate(func: (k, index) => PowBig(k, x));

            Console.WriteLine("BigInteger version! Printing 3↑↑3");
            Console.WriteLine(TetrationBig(3, 3));
            Console.WriteLine("Printing 2↑↑4");
            Console.WriteLine(TetrationBig(2, 4));
            Console.WriteLine("Printing another 3↑↑3");
            Console.WriteLine(TetrationBig2(3, 3));
            Console.WriteLine("Printing 2↑↑5");
            Console.WriteLine(TetrationBig(2, 5));
            Console.WriteLine("Printing another 2↑↑5");
            Console.WriteLine(TetrationBig2(2, 5));
        }
    }
}
