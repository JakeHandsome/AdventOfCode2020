using System;
using System.IO;
using System.Linq;

namespace Day1
{
   class Program
   {
      static void Main(string[] args)
      {
         var inputs = Array.ConvertAll(File.ReadAllLines("Input.txt"), x => int.Parse(x));

         var part1 = (from a in inputs
                      from b in inputs
                      where a + b == 2020
                      select (a * b)).First();
         Console.WriteLine($"Part1: {part1}");

         var part2 = (from a in inputs
                      from b in inputs
                      from c in inputs
                      where a + b + c == 2020
                      select (a * b * c)).First();
         Console.WriteLine($"Part2: {part2}");
      }
   }
}
