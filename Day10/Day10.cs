using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Part 1 easy
/// Part 2 was not
/// </summary>
namespace Day10
{
   class Day10
   {
      static void Main(string[] args)
      {
         var threes = 0;
         var ones = 0;
         var inputs = Array.ConvertAll(File.ReadAllLines("Input.txt"), x => int.Parse(x)).ToList();
         inputs.AddRange(new[] { 0, inputs.Max() + 3 });
         inputs.Sort();

         for (int i = 0; i < inputs.Count() - 1; i++)
         {
            var diff = inputs[i + 1] - inputs[i];
            if (diff == 3)
            {
               threes++;
            }
            else if (diff == 1)
            {
               ones++;
            }
         }
         Console.WriteLine($"Part1: {threes * ones}");

         var counter = new Dictionary<long, long>() { [0] = 1 };
         foreach (var x in inputs)
         {
            // Go through all inputs sorted,
            // Start with a count of 1,
            // If x+1 exists, x's count to x+1
            // If x+1 does not exist initialize it to current counter.
            // Repeate for x+2 and x+3
            // Print the count for the max number
            counter[x + 1] = counter.GetValueOrDefault(x + 1) + counter[x];
            counter[x + 2] = counter.GetValueOrDefault(x + 2) + counter[x];
            counter[x + 3] = counter.GetValueOrDefault(x + 3) + counter[x];

            // How this works, for each number we add the current number of possible paths 
            // If that number is in inputs, it keeps adding to the count
            // If the number is not in inputs it keeps counting

            //This was hard to figure out

         }
         Console.WriteLine($"Part2: {counter[inputs.Max()]}");

      }
   }
}
