using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

/// <summary>
/// So basically we have a 10 bit number, first 7 bits are front/back last 3 bits are left/right
/// </summary>
namespace Day5
{
   class Day5
   {
      static void Main(string[] args)
      {
         List<int> decodedPasses = new List<int>();
         var input = File.ReadAllLines("Input.txt");
         foreach (var entry in input)
         {
            decodedPasses.Add(GetSeatID(entry));
         }
         Console.WriteLine($"Part1: {decodedPasses.Max()}");
         decodedPasses.Sort();

         // Make a new enumerable that starts at the minimum and goes until the size of array
         var myTicket = Enumerable.Range(decodedPasses.Min(), decodedPasses.Count)
            // Find the first missing number
            .Except(decodedPasses).First();

         Console.WriteLine($"Part2: {myTicket}");
      }

      /// <summary>
      /// Convert the FBLR string into binary and use base 2 conversion to get our number
      /// </summary>
      /// <param name="entry">FBLR string</param>
      /// <returns>The seatID for the ticket</returns>
      static int GetSeatID(string entry)
      {
         entry = entry.Replace('F', '0')
                      .Replace('B', '1')
                      .Replace('L', '0')
                      .Replace('R', '1');

         var rowstr = entry.Substring(0, 7);
         var colstr = entry.Substring(7, 3);

         var row = Convert.ToInt32(rowstr, 2);
         var col = Convert.ToInt32(colstr, 2);

         return row * 8 + col;
      }
   }
}
