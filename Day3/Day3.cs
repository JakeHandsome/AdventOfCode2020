using System;
using System.IO;
using System.Linq;

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
      private readonly string[] slope;
      private readonly int maxWidth;
      public TreeCounter(string input)
      {
         slope = File.ReadAllLines(input);
         maxWidth = slope.First().Length;
      }

      public int CountTrees(int xIncrement, int yIncrement)
      {
         int numTrees = 0;
         for (int X = 0, Y = 0; Y < slope.Count(); Y += yIncrement, X += xIncrement)
         {
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
