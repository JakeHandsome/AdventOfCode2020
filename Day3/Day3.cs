using System;
using System.IO;
using System.Linq;

/// <summary>
/// This problem was to just plot a line and check if each data point landed on a '#'
/// The only tricky part was doing the auto-wrapping but easy enough with modulus
/// </summary>
namespace Day3
{
   class Day3
   {
      static void Main(string[] args)
      {
         var counter = new TreeCounter("Input.txt");
         int part1 = counter.CountTrees(3, 1);
         Console.WriteLine($"Part 1: {part1}");

         int part2 = counter.CountTrees(1, 1) *
                     part1 *
                     counter.CountTrees(5, 1) *
                     counter.CountTrees(7, 1) *
                     counter.CountTrees(1, 2);

         Console.WriteLine($"Part 2: {part2}");

      }
   }
   class TreeCounter
   {
      /// <summary>
      /// This is the raw input of the ski slope
      /// </summary>
      private readonly string[] slope;
      /// <summary>
      /// This is the maxWidth of the slope, when to wrap around
      /// </summary>
      private readonly int maxWidth;
      public TreeCounter(string input)
      {
         slope = File.ReadAllLines(input);
         maxWidth = slope.First().Length;
      }

      /// <summary>
      /// Counts number of trees following a specific path
      /// </summary>
      /// <param name="xIncrement">How far to move X each iteration</param>
      /// <param name="yIncrement">How far to move Y each iteration</param>
      /// <returns></returns>
      public int CountTrees(int xIncrement, int yIncrement)
      {
         int numTrees = 0;
         // Loop through the whole Y axis of the chart, starting at 0,0
         for (int X = 0, Y = 0; Y < slope.Count(); Y += yIncrement, X += xIncrement)
         {
            // Get current position by wrapping around the max width
            var position = X % maxWidth;
            if (slope[Y].ElementAt(position) == '#')
            {
               numTrees++;
            }
         }
         return numTrees;
      }

   }
}
