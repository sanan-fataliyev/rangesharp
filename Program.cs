using System;

namespace rangesharp
{
    class Program
    {
        static void Main(string[] args)
        {

            var range = new Range(20,200,11);

            System.Console.WriteLine(range.Sum);
        }
    }
}
