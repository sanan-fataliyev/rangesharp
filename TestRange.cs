using System;

class Program
{

    public static void Main()
    {

        var range = new Range(10);


        foreach(int el in range)
            Console.Write(el + " "); // 0 1 2 3 4 5 6 7 8 9

        Console.WriteLine(range.Sum); // 45

        var negRange = new Range(5, -20, -3);

        Console.WriteLine(negRange.Count); // 9

        Console.WriteLine( negRange.Contains(-4) ); // True

        Console.WriteLine( negRange[5] ); // -10

        var bigRange = new Range(-567656, 234543234, 27);

        Console.WriteLine(bigRange.IndexOf(397378)); // 35742

    }

}