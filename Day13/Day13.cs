using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

/// <summary>
/// Part 1 was easy
/// Part 2 was not. needed to look shit up to find the CRT
/// </summary>
namespace Day13
{
   class Program
   {
      static void Main(string[] args)
      {
         var input = File.ReadAllLines("Input.txt");
         int time = Convert.ToInt32(input[0]);
         var busses = new List<int>();
         foreach (var bus in input[1].Split(','))
         {
            if (Int32.TryParse(bus, out int busnum))
            {
               busses.Add(busnum);
            }
         }
         var nextBus = (from busNumber in busses
                        select (Number: busNumber, Time: GetNextBusTime(time, busNumber)))
                        .OrderBy(x => x.Time).First();
         var part1Answer = (nextBus.Time - time) * nextBus.Number;
         Console.WriteLine($"Part1: {part1Answer}");

         // Part2: Notes
         // Start at first bus arrival from index 1. This is `t`
         // Take the next bus, look up the index. Figure out if t + index is divisible by busNumber
         //    If true, keep going untill all numbers are exhausted
         //          Recursion? -- No.
         //    If false increment t by first bus number and try again
         // Forget all above, use chinese remained theroem copypasta
         var valueOffset = input[1].Split(',')
            .Select((value, offset) => (value, offset))
            .Where(x => x.value != "x")
            .Select(x => (Value: Convert.ToInt64(x.value), Offset: x.offset));
         var n = valueOffset.Select(x => x.Value).ToArray();
         var a = valueOffset.Select(x => x.Value - x.Offset).ToArray();
         a[0] = 0;
         var part2Answer = ChineseRemainderTheorem.Solve(n, a);
         Console.WriteLine($"Part2: {part2Answer}");
      }

      static long GetNextBusTime(long currentTime, int busNumber)
      {
         long divsor = currentTime / busNumber;
         // Get the next number divisible by busNumber
         return (divsor + 1) * busNumber;
      }
   }

   /// <summary>
   /// This code was taken from the internet and adjusted from ints to longs
   /// </summary>
   public static class ChineseRemainderTheorem
   {
      public static long Solve(long[] n, long[] a)
      {
         long prod = n.Aggregate(1L, (i, j) => i * j);
         long p;
         long sm = 0;
         for (int i = 0; i < n.Length; i++)
         {
            p = prod / n[i];
            sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
         }
         return sm % prod;
      }

      private static long ModularMultiplicativeInverse(long a, long mod)
      {
         long b = a % mod;
         for (int x = 1; x < mod; x++)
         {
            if ((b * x) % mod == 1)
            {
               return x;
            }
         }
         return 1;
      }
   }
}
