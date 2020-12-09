using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// LINQ everything
/// 
/// Part 1 was like day1 but with a queue
/// Part 2 was a one linq function
/// </summary>
namespace Day9
{
   class Day9
   {
      static readonly List<long> Total = new List<long>();
      static readonly List<long> Preamble = new List<long>();
      static readonly int PREAMBLE_SIZE = 25;
      static void Main(string[] args)
      {
         var input = Array.ConvertAll(File.ReadAllLines("Input.txt"), x => long.Parse(x));

         foreach (var number in input)
         {
            if (Preamble.Count < PREAMBLE_SIZE)
            {
               Preamble.Add(number);
            }
            else
            {
               var match = from a in Preamble
                           from b in Preamble
                           where a != b && a + b == number
                           select a + b;
               if (!match.Any())
               {
                  Console.WriteLine($"Part1: {number}");
                  Console.WriteLine($"Part2: {Part2(number)}");
                  break;
               }
               Preamble.RemoveAt(0);
               Preamble.Add(number);
            }
            Total.Add(number);
         }
      }

      /// <summary>
      /// I like how simple this function became
      /// 
      /// Looks at the range of numbers and finds a continous subset that sums to the number
      /// Returns subset.Min() + subset.Max()
      /// </summary>
      /// <param name="number">Answer to part 1</param>
      /// <returns>subset.Min() + subset.Max()</returns>
      private static long Part2(long number)
      {

         var part2 = from start in Enumerable.Range(0, Total.Count)
                     from length in Enumerable.Range(0, Total.Count - start)
                     let subSet = Total.GetRange(start, length)
                     where subSet.Sum() == number
                     select subSet.Max() + subSet.Min();

         return part2.First();
      }
   }
}
